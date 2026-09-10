# Índice — Autorizaciones Intrahospitalarias (ERP-84)

Punto de entrada a toda la documentación y scripts del pipeline de Autorizaciones Intrahospitalarias en el proyecto de base de datos. Las rutas son **relativas a la raíz del repo** `VieCloud_DB_Colombia_Schema`.

## Documentación

| Documento | Ruta | Para qué / quién |
|---|---|---|
| **Guía de Onboarding** (empezar por acá) | `docs/GUIA_ONBOARDING_AUTORIZACIONES.md` | Cómo dejar operativa una BD nueva/restaurada. Incluye cómo obtener los usuarios (MI) de las Function Apps y el handoff con DevOps. **DBA + DevOps** |
| **Manual técnico** (PDF) | `docs/Manual_Tecnico_Autorizaciones.pdf` | Manual completo con paso a paso, grants, troubleshooting. **DBA / DevOps / Dev** |
| **Manual técnico** (fuente Markdown) | `docs/MANUAL_TECNICO_AUTORIZACIONES.md` | Fuente del PDF (para editar/versionar). |
| **Arquitectura** | `docs/ARQUITECTURA_AUTORIZACIONES.md` | Vista estructural: componentes, eventos, seguridad, recursos a provisionar. **Arquitectura / Dev / DevOps** |

## Scripts SQL

| Script | Ruta | Para qué |
|---|---|---|
| **Onboarding completo** (INDIGO636 / plantilla) | `Vie_Erp/Authorization/Scripts/Onboarding_INDIGO636_ERP84.sql` | Runbook de punta a punta: crea tablas (outbox + control) → Change Tracking → usuarios + grants (PARTE 1, BD tenant) → registra tenant + cursores + grants Platform (PARTE 2, Control Plane). Sirve de plantilla para otras bases: cambiar el nombre de la BD y el `<ServerName>`. |
| **Solo creación de tablas** | `Vie_Erp/Authorization/Scripts/Recrear_Tablas_Authorization_ERP84.sql` | Solo los `CREATE` de las tablas del modelo de control (`AuthorizationControl`, `Trace`, `Alert`, `ProcessedInbox`, `SubjectType` + seed, y las del Agente). Útil si solo faltan las tablas. |

## Scripts relacionados ya existentes en el repo

| Script / doc | Ruta |
|---|---|
| Runbook de onboarding original | `Vie_Erp/Authorization/Scripts/ERP84_OnboardingRunbook.sql` |
| Usuarios/grants Control Plane | `Vie_Erp/Authorization/Scripts/OnboardErp84ControlPlaneUsers.sql` |
| Usuarios/grants BD tenant | `Vie_Erp/Authorization/Scripts/OnboardErp84TenantDatabaseUsers.sql` |
| Registrar tenant en el catálogo | `Vie_Erp/Authorization/Scripts/OnboardErp84_02_RegisterTenantInCatalog.sql` |
| Habilitación de Change Tracking (por esquema) | `Vie_Erp/<Esquema>/ChangeTracking-enablement.sql` |
| DDL de las tablas outbox | `Vie_Erp/{Clinical,Hospitalization,Billing,Admissions}/Tables/OutboxEvent.sql` |
| DDL del modelo de control | `Vie_Erp/Authorization/Tables/AuthorizationControl*.sql`, `ProcessedInbox.sql`, `AdmissionAuthorizerAssignmentLog.sql`, etc. |

## Orden recomendado para el DBA

1. Leer **`docs/GUIA_ONBOARDING_AUTORIZACIONES.md`** (explica actores, orden y cómo pedir a DevOps los nombres de las Managed Identity).
2. Pedir a DevOps: nombres de las 3 Function Apps (relay/normalizer/authz), sus `principalId`, y el FQDN del servidor SQL del tenant.
3. Ejecutar **`Onboarding_INDIGO636_ERP84.sql`** — PARTE 1 conectado a la BD del tenant, PARTE 2 conectado al Control Plane (completar el `<ServerName>`).
4. DevOps: roles de **Cosmos** y **Service Bus** + App Settings (`Relay__AllowedEventTypes`, conexiones) — §8-9 de la guía.
5. Validar (§10 de la guía) con una **orden/HC nueva** (Change Tracking no rellena hacia atrás).

> Nota: los scripts de `Vie_Erp/Authorization/Scripts/` son **operativos** (los ejecuta el DBA en despliegue), no objetos declarativos del DACPAC — por eso no se agregan como `<Build Include>` al `.sqlproj`.
