# Guía de Onboarding — Pipeline de Autorizaciones Intrahospitalarias (ERP-84)

> Guía para dejar **operativa una base de datos de tenant** (nueva, migrada o restaurada) en el pipeline de autorizaciones intrahospitalarias. Complementa el manual técnico (`Manual_Tecnico_Autorizaciones.pdf`) y el script ejecutable (`Onboarding_<DB>_ERP84.sql`).

## 1. Cuándo usar esta guía

- Se aprovisiona un **tenant nuevo** (cliente nuevo) en el dashboard de autorizaciones.
- Se **restauró** una base y perdió las tablas/configuración del pipeline (síntoma típico: no fluye ninguna orden/estancia; `CHANGE_TRACKING_MIN_VALID_VERSION` = NULL; errores `Cannot find the object 'AuthorizationControl'`).
- Se levanta un **ambiente nuevo** (QA, producción).

Si el pipeline "dejó de funcionar" en una base que antes andaba, casi siempre es porque **falta uno de estos pasos** (típicamente Change Tracking o los grants tras una restauración).

## 2. Actores y responsabilidades

| Paso | Quién | Dónde |
|---|---|---|
| Crear tablas, Change Tracking, usuarios y grants | **DBA** | Base del **tenant** (Azure SQL) |
| Registrar tenant + cursores + grants Platform | **DBA** | Base del **Control Plane** (Azure SQL) |
| Roles RBAC de Cosmos y Service Bus | **DevOps** | Azure (portal / az CLI) |
| App Settings + Key Vault + despliegue de las Functions | **DevOps** | Azure Functions |

El DBA **no puede completar solo** el onboarding: necesita que DevOps le dé (a) los **nombres de las Managed Identity** de las 3 Function Apps y (b) ejecute la parte de Azure (Cosmos/Service Bus). Ver §4.

## 3. Componentes del pipeline (las 3 Function Apps)

El pipeline lo forman **3 Azure Functions**, cada una con su **Managed Identity (System-Assigned)** propia:

| App (rol) | Qué hace | Necesita en la BD |
|---|---|---|
| **Relay** | Lee las tablas outbox por Change Tracking y publica a Service Bus | usuario + `SELECT` + `VIEW CHANGE TRACKING` sobre las 4 outbox |
| **Normalizer** | Lee la HC/órdenes y publica los *facts* normalizados | usuario + `SELECT` sobre dbo/Contract/Clinical/Billing + `EXECUTE` de funciones |
| **AuthorizationControl** | Materializa el control de autorización | usuario + `SELECT` de contexto + `INSERT/UPDATE` sobre las tablas de `Authorization` |

## 4. Cómo obtener los usuarios (Managed Identity) de las Function Apps  ← clave para el DBA

En SQL, el usuario del pipeline se crea con `CREATE USER [<nombre>] FROM EXTERNAL PROVIDER`, y **`<nombre>` debe ser exactamente el nombre de la Managed Identity en Azure AD**, que para una identidad *System-Assigned* **es el nombre de la Function App**. Por eso el DBA necesita, de parte de DevOps, los nombres exactos de las 3 apps del ambiente.

### 4.1 Patrón de nombres

Las apps siguen el patrón `ehrco-<rol>-<ambiente>-erp84<sufijo>`. Ejemplo (dev):

| Rol | Nombre de la app = nombre de la MI = usuario SQL |
|---|---|
| Relay | `ehrco-relay-dev-erp84d` |
| Normalizer | `ehrco-normalizer-dev-erp84d` |
| AuthorizationControl | `ehrco-authz-dev-erp84d` |

En QA/producción cambia el segmento de ambiente (p. ej. `-qa-`, `-prod-`). **No asumir**: pedir a DevOps los nombres reales del ambiente que se está onboardeando.

### 4.2 Cómo los confirma DevOps (o el DBA con acceso)

Listar las Function Apps del resource group:
```bash
az functionapp list --resource-group <rg-del-ambiente> --query "[].name" -o tsv
```

Confirmar que cada app tiene Managed Identity y obtener su `principalId` (Object ID — se usa para los roles de Cosmos/Service Bus, no para el `CREATE USER`):
```bash
az functionapp identity show --name <app-name> --resource-group <rg> --query "{tipo:type, objectId:principalId}" -o table
```

Puntos a tener claros:
- Para `CREATE USER ... FROM EXTERNAL PROVIDER`, SQL resuelve el principal **por NOMBRE** (el nombre de la app). El `principalId` NO va en el `CREATE USER`.
- El `principalId` (Object ID) **sí** lo necesita DevOps para asignar los roles data-plane de **Cosmos** y **Service Bus**.
- La MI debe estar **habilitada** (`type` debe incluir `SystemAssigned`). Si dice `None`, DevOps la habilita primero.

### 4.3 Qué le pide el DBA a DevOps (checklist de comunicación)

1. Nombres exactos de las 3 Function Apps del ambiente (relay / normalizer / authz).
2. Confirmación de que las 3 tienen System-Assigned Managed Identity habilitada.
3. Los `principalId` (Object ID) de las 3 (para que DevOps asigne los roles de Cosmos/Service Bus — §8).
4. FQDN del servidor SQL del tenant (para registrar el tenant en el Control Plane — §7).

## 5. Orden de ejecución (checklist)

```
[ ] Paso 1 (DBA, BD tenant)      Crear esquemas + tablas outbox + tablas del modelo de control
[ ] Paso 2 (DBA, BD tenant)      Habilitar Change Tracking (base + 4 outbox)
[ ] Paso 3 (DBA, BD tenant)      Crear usuarios MI + grants (relay / normalizer / authz)
[ ] Paso 4 (DBA, Control Plane)  Crear usuarios MI + registrar tenant + cursores + grants Platform
[ ] Paso 5 (DevOps, Azure)       Roles RBAC de Cosmos y Service Bus
[ ] Paso 6 (DevOps, Functions)   App Settings (allow-list, conexiones) + Key Vault
[ ] Paso 7 (DevOps)              Reiniciar/desplegar las Functions
[ ] Paso 8 (todos)               Validación end-to-end (§9) con una orden/HC NUEVA
```

El **orden importa**: los grants (Paso 3) fallan con `Cannot find the object` si las tablas (Paso 1) no existen; y el relay entra en loop silencioso si falta Change Tracking o `VIEW CHANGE TRACKING` (Paso 2/3).

## 6. Paso 1–3 — Base del tenant (DBA)

Ejecutar el script `Onboarding_<DB>_ERP84.sql`, **PARTE 1** (conectado a la base del tenant). Crea, en orden:

- Esquemas `Clinical`, `Hospitalization`, `Billing`, `Admissions`, `Authorization`.
- Tablas outbox: `Clinical.OutboxEvent`, `Hospitalization.OutboxEvent`, `Billing.OutboxEvent`, `Admissions.OutboxEvent`.
- Tablas del modelo de control: `AuthorizationControlSubjectType` (+ seed de tipos 1–5), `AuthorizationControl`, `AuthorizationControlTrace`, `AuthorizationControlAlert`, `ProcessedInbox`, y las del Agente Autorizador (`AuthorizationManagementParameters`, `UsersAssignment`, `UserNovelties`, `AdmissionAuthorizerAssignmentLog`).
- Change Tracking en la base y en las 4 outbox.
- Los 3 usuarios MI (`CREATE USER ... FROM EXTERNAL PROVIDER`) y sus grants.

> **Change Tracking NO rellena hacia atrás.** Solo captura cambios **desde que se habilita**. Las filas que ya estaban en el outbox no van a fluir; se prueba con una orden/HC **nueva**.

> `VIEW CHANGE TRACKING` es un permiso **aparte** de `SELECT`. Sin él, `CHANGE_TRACKING_MIN_VALID_VERSION()` devuelve **NULL en silencio** y el relay entra en loop `REQUIRES_RESYNC` sin lanzar error. Es la causa #1 de "no fluye nada".

## 7. Paso 4 — Base del Control Plane (DBA)

Ejecutar el script `Onboarding_<DB>_ERP84.sql`, **PARTE 2** (conectado a la base del **Control Plane**, donde vive el esquema `Platform`). Hace:

- Crear los 3 usuarios MI en el Control Plane.
- **Registrar el tenant** en `Platform.TenantCatalog` (requiere el **FQDN del servidor SQL** del tenant — se lo pide a DevOps, §4.3).
- Sembrar los cursores en `Platform.TenantOutboxCursor` (una fila por outbox).
- Grants sobre `Platform.*` (relay lee/avanza cursores y leases; normalizer/authz solo `SELECT` de `TenantCatalog`).

> La base del Control Plane es **distinta** de la del tenant. Si el `SELECT ... FROM Platform.TenantCatalog` sale vacío estando en la base del tenant, es porque `Platform` vive en el Control Plane — hay que conectarse allí.

## 8. Paso 5 — Azure RBAC (DevOps)

Estos roles **no son SQL**; los asigna DevOps con los `principalId` (§4.2). Sin ellos, aunque el relay publique, el flujo de **órdenes** falla en el claim-check de Cosmos.

**Cosmos DB (data-plane), scoped al container `NormalizedMedicalOrders`:**
- Normalizer → `Cosmos DB Built-in Data Contributor`
- AuthorizationControl → `Cosmos DB Built-in Data Reader`

**Service Bus (data-plane), scoped por topic/cola:**
- Relay y Normalizer → `Azure Service Bus Data Sender` sobre los topics a los que publican
- Consumidores (authz / normalizer) → `Azure Service Bus Data Receiver` sobre sus suscripciones

## 9. Paso 6 — App Settings (DevOps)

En las Function Apps (vía App Settings + Key Vault, autenticando con Managed Identity):

| Setting | App(s) | Valor |
|---|---|---|
| `ControlPlane__ConnectionString` | relay, normalizer, authz | Key Vault ref al Platform DB (MI) |
| `Tenant__ConnectionStringTemplate` | relay, normalizer, authz | plantilla `{ServerName}`/`{DatabaseName}` (MI) |
| `EhrSourceEventsConnection__fullyQualifiedNamespace` | relay, normalizer | `<ns>.servicebus.windows.net` |
| `ClinicalOrderFactsConnection__fullyQualifiedNamespace` | authz | `<ns>.servicebus.windows.net` |
| `Cosmos__AccountEndpoint` / `Cosmos__DatabaseId` | normalizer, authz | endpoint + base |
| `Relay__OutboxTables` | relay | incluir las 4: Clinical/Hospitalization/Billing/Admissions.OutboxEvent |
| `Relay__AllowedEventTypes` | relay | CSV con TODOS los eventTypes (ver abajo) |
| `Authorization__SystemUser` | authz | usuario de sistema |

`Relay__AllowedEventTypes` debe incluir (si falta uno, el relay lo ignora):
```
clinical.record-committed.v1,clinical.order-placed.v1,clinical.encounter-discharged.v1,clinical.admission-registered.v1,clinical.admission-status-changed.v1,hospitalization.stay-changed.v1,billing.stay-committed.v1,billing.stay-liquidation-failed.v1
```

> En Azure Functions Linux los dos puntos de la config van con **doble guion bajo** (`Relay__AllowedEventTypes`, no `Relay:AllowedEventTypes`).

## 10. Validación end-to-end

1. **Change Tracking (BD tenant):** debe dar **0**, no NULL:
   ```sql
   SELECT CHANGE_TRACKING_MIN_VALID_VERSION(OBJECT_ID('Clinical.OutboxEvent'));
   ```
2. **Cursores (Control Plane):** deben salir las 4 filas del tenant:
   ```sql
   SELECT tc.TenantCode, c.OutboxName, c.LastSyncVersion, c.Status
   FROM [Platform].[TenantCatalog] tc
   LEFT JOIN [Platform].[TenantOutboxCursor] c ON c.TenantId = tc.TenantId
   WHERE LTRIM(RTRIM(tc.DatabaseName)) = '<DB>' ORDER BY c.OutboxName;
   ```
3. **Prueba real:** grabar una **orden/HC nueva** y verificar que cae el control:
   ```sql
   SELECT TOP 20 Id, SubjectType, ServiceType, AdmissionNumber, SourceTable, Status, CreationDate
   FROM [Authorization].[AuthorizationControl] ORDER BY Id DESC;
   ```
4. **Relay avanzando (Control Plane):** `LastSyncVersion` sube y `Status` sin `REQUIRES_RESYNC`.

## 11. Troubleshooting (síntoma → causa → fix)

| Síntoma | Causa | Fix |
|---|---|---|
| `Cannot find the object 'AuthorizationControl'` al dar grants | Se corrieron los grants antes de crear las tablas | Correr PARTE 1 (crear tablas) **antes** de los grants |
| `CHANGE_TRACKING_MIN_VALID_VERSION` = NULL | CT no habilitado en la tabla, o falta `VIEW CHANGE TRACKING` | Habilitar CT + `GRANT ... VIEW CHANGE TRACKING` a la MI del relay |
| Relay en loop `REQUIRES_RESYNC`, no publica | Mismo caso anterior (NULL silencioso) | Igual que arriba |
| `SELECT ... FROM Platform.TenantCatalog` vacío | Se corrió en la BD del tenant, o el tenant no está registrado | Conectarse al Control Plane; si no está la fila, registrarlo (PARTE 2) |
| No caen las **órdenes** (pero sí estancias/urgencias) | Falta rol de **Cosmos Data Reader** del authz (claim-check) | DevOps asigna el rol data-plane de Cosmos |
| `INSERT permission denied on '...'` (Error 229) | Falta grant `INSERT/UPDATE` sobre esa tabla de Authorization | Aplicar los grants del authz (PARTE 1) |
| No cae **nada** tras restaurar la base | Faltan tablas + CT + usuarios (todo el onboarding) | Correr el runbook completo (PARTE 1 + PARTE 2) |
| El eventType nuevo no llega | No está en `Relay__AllowedEventTypes` | Agregarlo al App Setting del relay (§9) |

## 12. Referencias

- Script ejecutable: `Onboarding_<DB>_ERP84.sql` (PARTE 1 tenant, PARTE 2 Control Plane).
- Solo creación de tablas: `Recrear_Tablas_Authorization_ERP84.sql`.
- Manual técnico y arquitectura: `Manual_Tecnico_Autorizaciones.pdf`, `ARQUITECTURA_AUTORIZACIONES.md`.
- Runbook fuente en el repo de BD: `Vie_Erp/Authorization/Scripts/ERP84_OnboardingRunbook.sql`.
