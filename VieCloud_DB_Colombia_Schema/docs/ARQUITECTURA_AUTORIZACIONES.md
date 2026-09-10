# Arquitectura — Pipeline de Autorizaciones Intrahospitalarias (ERP-84)

> Documento de **arquitectura** (estructura y componentes). Para el paso a paso de configuración y los scripts, ver `MANUAL_TECNICO_AUTORIZACIONES.md`.

## 1. Visión general

El pipeline es **event-driven, multi-tenant y desacoplado**. Convierte cambios clínicos del ERP (historia clínica, urgencias, estancias, ingresos) en **controles de autorización** que alimentan el dashboard de autorizaciones intrahospitalarias, sin acoplar el ERP a la lógica de autorizaciones.

```
   BASE DEL TENANT (ERP)                CONTROL PLANE          MENSAJERÍA / CÓMPUTO                 DESTINO
 ┌───────────────────────┐          ┌────────────────┐    ┌────────────────────────┐   ┌───────────────────────┐
 │  *.OutboxEvent         │  cambios │ Platform.*     │    │  Service Bus           │   │ Authorization.*       │
 │  (Clinical, Hospital., │◄────────►│ TenantCatalog  │    │  Topics + Subs         │   │ AuthorizationControl  │
 │   Billing, Admissions) │  (cursor)│ OutboxCursor   │    │                        │   │ (worklist dashboard)  │
 └──────────┬────────────┘          │ OutboxLease    │    │                        │   └──────────▲────────────┘
            │ Change Tracking       └───────┬────────┘    │                        │              │
            ▼                               │             │                        │              │
     ┌─────────────┐   RelayDispatch  ┌─────▼──────┐  ehr-source-events  ┌─────────▼────────┐     │
     │   RELAY     │─────(cola)──────►│  RELAY     │────────────────────►│   NORMALIZER     │     │
     │ (dispatcher)│                  │ (worker)   │                     │                  │     │
     └─────────────┘                  └────────────┘                     └───┬──────────────┘     │
                                                                             │ fact topics        │
                                                    ┌────────────────────────┼────────────────────┤
                                                    ▼                        ▼                     │
                                         clinical-order-facts     clinical-stay-facts             │
                                         clinical-emergency-facts clinical-admission-facts        │
                                                    │                        │                     │
                                                    ▼                        ▼                     │
                                              ┌──────────────── AUTHORIZATIONCONTROL ──────────────┘
                                              │  (consumers: Order / Emergency / Stay / AdmissionDistribution)
                                              └──► Cosmos (claim-check de órdenes)
```

Principio rector: el ERP solo **produce eventos** en tablas outbox de su propia base. Todo lo demás (relay, normalización, autorización) vive en Azure Functions y consume esos eventos.

## 2. Componentes

### 2.1 Azure Functions (.NET 8 isolated, Linux)

Tres aplicaciones independientes, cada una con **System-Assigned Managed Identity** propia:

| App | Rol | Lee | Escribe / Publica |
|---|---|---|---|
| **Relay** (`Indigo.AzClinicalOutboxRelay`) | Drena las tablas outbox de cada tenant por **Change Tracking** y publica a Service Bus. Dispatcher + worker (cola interna `relay-dispatch`). | `*.OutboxEvent` (Change Tracking); Control Plane (cursores/leases) | Cola `relay-dispatch`; topic `ehr-source-events`; avanza cursores |
| **Normalizer** (`Indigo.AzClinicalOrderNormalizer`) | Enruta por `eventType`, lee la fuente clínica **una sola vez** (ADR-004), desnormaliza y publica *facts* por dominio. | `ehr-source-events`; base del tenant (HC, estancias, ingresos); Control Plane (resuelve tenant) | Cosmos (upsert claim-check de órdenes); topics de facts |
| **AuthorizationControl** (`Indigo.AzClinicalAuthorizationControl`) | Consume los facts y **materializa el control** de autorización + trazas + inbox de idempotencia. | topics de facts; Cosmos (claim-check); base del tenant (contexto/maestros) | `Authorization.AuthorizationControl` + `*Trace` + `ProcessedInbox` (+ agente, + alertas) |

Todas son .NET 8 isolated, con `ServerGarbageCollection` activo, deploy en contenedores Linux, CI/CD por Azure Pipelines.

### 2.2 Almacenes de datos

- **Bases SQL de tenant (Azure SQL):** una por cliente. Contienen el ERP (esquemas `dbo`, `Clinical`, `Hospitalization`, `Billing`, `Contract`, `Inventory`, …), las **tablas outbox** por dominio y el esquema **`Authorization`** (destino). Registradas en el Control Plane.
- **Base del Control Plane (Azure SQL, esquema `Platform`):** catálogo de tenants y estado de sincronización del relay. Puede ser una base dedicada.
- **Cosmos DB:** *claim-check* de las órdenes normalizadas (container `NormalizedMedicalOrders`). El bus lleva solo un identificador; el documento clínico desnormalizado (sin PII sensible en el bus) vive en Cosmos. Particionado por tenant/documento.
- **Service Bus (namespace compartido multi-entorno):** topics de eventos fuente y de facts, con suscripciones filtradas.

## 3. Modelo de eventos

### 3.1 Outbox por dominio (append-only, en la base del tenant)

Contrato idéntico en las cuatro tablas: `OutboxId` (BIGINT IDENTITY), `EventId` (uniqueidentifier), `EventType`, `AggregateType`, `AggregateId`, `PayloadJson`, `BusinessEventHash` (SHA2_256 de `EventType|AggregateId|OccurredAtUtc`, UNIQUE → idempotencia del productor), `PayloadHash`, `OccurredAtUtc`, `CreatedAtUtc`. Change Tracking habilitado por tabla.

| Outbox | Productor | eventType canónico |
|---|---|---|
| `Clinical.OutboxEvent` | Grabado de HC / órdenes (EHR_ServicesCore) | `clinical.record-committed.v1` |
| `Hospitalization.OutboxEvent` | Cama / egreso | `hospitalization.stay-changed.v1`, `clinical.encounter-discharged.v1` |
| `Billing.OutboxEvent` | Stella (liquidación de estancias) | `billing.stay-committed.v1` |
| `Admissions.OutboxEvent` | Trigger `TR_ADINGRESO_AdmissionStatusChanged` / emit C# | `clinical.admission-registered.v1`, `clinical.admission-status-changed.v1` |

### 3.2 Topics de facts (salida del Normalizer)

`clinical-order-facts`, `clinical-emergency-facts`, `clinical-stay-facts`, `clinical-admission-facts`. Cada uno con la suscripción `authorization` (y variantes `-localdev` para aislar máquinas de desarrollo). Deduplicación por `MessageId` determinístico (ADR-010).

### 3.3 Los flujos (ramificación por `eventType`)

1. **Órdenes** (`SubjectType` 1/2/3): HC → upsert Cosmos → `clinical-order-facts` → control por orden.
2. **Urgencias / AIU** (`SubjectType` 4): `ADATEINIU` → `clinical-emergency-facts` → control con `RequestedQuantity=1`.
3. **Estancias / Stella** (`SubjectType` 5): `billing.stay-committed.v1` + `hospitalization.stay-changed.v1` → `clinical-stay-facts`. Stella acumula la cantidad de días.
4. **Agente Autorizador**: `ADINGRESO` → `clinical-admission-facts` → `AdmissionAuthorizerAssignmentLog` → (trigger backfill) → `AuthorizationControl.AssignedAuthorizer`. Asignación por menor carga.
5. **Estancia desde HC (orden de traslado)**: `HCHISPACA.INDICAPAC` ∈ {3,4,5,6,9,21} → el mismo `clinical.record-committed.v1` publica un fact "solo-tipificación" a `clinical-stay-facts` → crea/actualiza la estancia (SubjectType 5). Una estancia abierta por ingreso; tipo distinto reemplaza. Sin objetos de BD nuevos.

## 4. Multi-tenancy y Control Plane

El **`Platform.TenantCatalog`** es la fuente de verdad: `TenantId` (GUID), `TenantCode`, `DatabaseName`, `ServerName`, `ElasticPoolName`, `Status`. `TenantResolver` (compartido por las 3 apps) sustituye `{ServerName}`/`{DatabaseName}` en `Tenant:ConnectionStringTemplate` y decide si el tenant se procesa (`Status = ACTIVE`).

- **`Platform.TenantOutboxCursor`**: por (tenant, outbox), la última versión de Change Tracking sincronizada. El relay avanza este cursor.
- **`Platform.TenantOutboxLease`**: lease para que una sola instancia del relay procese un tenant a la vez.
- **`Platform.PoolThrottlingConfig`**: límite de tenants del mismo Elastic Pool procesados en paralelo.

El **`tenantId` viaja en cada mensaje** (`ApplicationProperties["tenantId"]`, formato "D" minúsculas), estampado SIEMPRE desde el Control Plane, nunca desde el payload. Un evento cuyo tenant no esté ACTIVE en el catálogo se **ignora y completa** (`TenantNotActiveException`) — el namespace es compartido y llegan copias de otros entornos.

## 5. Seguridad

- **Identidad:** cada Function App usa su **Managed Identity**. No hay secretos de conexión en el código; las cadenas usan `Authentication=Active Directory Managed Identity`.
- **Service Bus (RBAC data-plane, scoped por entidad):** la MI del Relay/Normalizer necesita **Azure Service Bus Data Sender** sobre los topics a los que publica; la del consumidor, **Data Receiver** sobre las suscripciones. Scoped al topic/cola, no al namespace.
- **Cosmos (RBAC data-plane):** Normalizer = **Data Contributor**; Authz = **Data Reader**, scoped al container.
- **SQL (grants por MI en la base del tenant):** `GRANT SELECT` NO cubre `EXECUTE` de funciones ni `VIEW CHANGE TRACKING`. El Relay necesita `SELECT` + `VIEW CHANGE TRACKING` sobre cada outbox; el Normalizer `SELECT` sobre los esquemas que lee + `EXECUTE` de funciones escalares; el Authz `SELECT` sobre los esquemas de contexto + `INSERT/UPDATE` sobre las tablas de `Authorization` que escribe. Detalle exacto en el manual (§4.3).

## 6. Patrones arquitectónicos

- **Outbox + Change Tracking:** la escritura de negocio y el evento son atómicos en la base del tenant; el relay drena por Change Tracking (no polling de tablas de negocio).
- **Claim-check (Cosmos):** el bus transporta identificadores; el detalle clínico (potencial PII) va a Cosmos, reduciendo el tamaño del mensaje y la exposición.
- **Idempotencia en capas (ADR-010):** `BusinessEventHash` (productor) + `MessageId` determinístico (relay/normalizer) + `ProcessedInbox` con índice único `(ConsumerName, MessageId)` (consumidor, transaccional con la escritura del control).
- **Fan-out vs competing consumers:** cada suscripción recibe su copia (fan-out entre suscripciones); dentro de una suscripción, un mensaje lo procesa un solo consumidor. Regla operativa: solo la Function App desplegada del ambiente consume cada suscripción productiva (evita "a veces llega, a veces no").
- **Lectura única (ADR-004):** el Normalizer lee la fuente clínica una sola vez y desnormaliza; los consumidores no vuelven a leer la HC.

## 7. Recursos a provisionar en producción (DevOps / DBA)

**Azure**
- 3 Function Apps (Relay, Normalizer, AuthorizationControl) con System-Assigned Managed Identity.
- Service Bus: cola `relay-dispatch`; topics `ehr-source-events`, `clinical-order-facts`, `clinical-emergency-facts`, `clinical-stay-facts`, `clinical-admission-facts`, con sus suscripciones y `SqlFilter`; deduplicación habilitada.
- Roles RBAC: Data Sender/Receiver (Service Bus) y Data Contributor/Reader (Cosmos) por MI, scoped a cada entidad.
- Cosmos DB: cuenta + base + container `NormalizedMedicalOrders`.
- App Settings + Key Vault: `ControlPlane:ConnectionString`, `Tenant:ConnectionStringTemplate`, endpoints de Service Bus/Cosmos, `Relay:OutboxTables` (incluir `Admissions.OutboxEvent` para el agente), `Relay:AllowedEventTypes`, `Authorization:SystemUser`, y los nombres de suscripción (`%setting%`) por ambiente.

**Base de datos (por tenant productivo — lo ejecuta el DBA)**
- Esquema `Authorization` y tablas del DDL (control, trazas, inbox, **tablas del agente**: `AdmissionAuthorizerAssignmentLog`, `UsersAssignment`, `UserNovelties`, `AuthorizationManagementParameters`, y `AuthorizationControlAlert`) + triggers del agente.
- `Change Tracking` habilitado en las 4 tablas outbox.
- Usuarios `FROM EXTERNAL PROVIDER` (una por MI) y sus **grants** (§4.3 del manual): incluye el bloque del agente y el grant `INSERT, UPDATE ON Authorization.AuthorizationControlAlert`.
- Datos semilla del agente: `AuthorizationManagementParameters.AutomaticAssignment = 1` y el pool `UsersAssignment` (activos, hospitalarios).

**Control Plane (una vez por tenant)**
- Registrar el tenant en `Platform.TenantCatalog` (`Status = ACTIVE`).
- Sembrar `Platform.TenantOutboxCursor` por cada outbox (incluida `Admissions.OutboxEvent`).
- Grants de la MI del Relay sobre `Platform.*`; grant `SELECT` de Normalizer/Authz sobre `Platform.TenantCatalog`.

## 8. Referencias

- Manual técnico (paso a paso + scripts): `MANUAL_TECNICO_AUTORIZACIONES.md`.
- ADRs: `docs/adr/ADR-001..012` (topología, multi-tenant, lectura única, Service Bus, idempotencia, outbox canónico).
- Runbook de onboarding SQL: `VieCloud_DB_Colombia_Schema/Vie_Erp/Authorization/Scripts/ERP84_OnboardingRunbook.sql`.
