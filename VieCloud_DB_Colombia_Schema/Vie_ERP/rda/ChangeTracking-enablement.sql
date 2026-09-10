/*
================================================================================
 ChangeTracking-enablement.sql  (rda / Interoperabilidad)
================================================================================
 Habilitacion de SQL Change Tracking para la outbox CANONICA del bounded context
 de RDA/Interoperabilidad ([rda].[OutboxEvent]). Sigue el modelo de ADR-003
 seccion 3 y el mecanismo del relay de ADR-011, aplicado a la tabla propia del
 bounded context (ADR-012: una outbox de bounded context que se conserve DEBE
 ser append-only + Change Tracking, nunca UPDLOCK/status mutable — el patron
 legacy [rda].[outbox_messages] con Status/RetryCount/UPDLOCK fue justamente lo
 que se retiro en el cutover 2026-06-16, PBI #37513).

 ALCANCE / NATURALEZA DE ESTE SCRIPT
 -----------------------------------
 Este NO es un objeto SSDT declarativo de tabla. Contiene DDL a nivel de
 instruccion (ALTER DATABASE / ALTER TABLE ... ENABLE CHANGE TRACKING) que el
 compilador de SSDT no modela como un objeto del esquema. Por eso:

   - En VieCloudColombia.sqlproj se referencia como <None Include="...">, NO
     como <Build Include="...">. Incluirlo como Build romperia la compilacion
     del DACPAC (no es una definicion declarativa de objeto). Mismo criterio que
     Clinical/ChangeTracking-enablement.sql, Hospitalization/ChangeTracking-enablement.sql
     y Billing/ChangeTracking-enablement.sql.
   - El ALTER DATABASE (SET CHANGE_TRACKING = ON, SET ALLOW_SNAPSHOT_ISOLATION
     ON) se gestiona por CONFIGURACION DEL PROYECTO / POST-DEPLOY, no como
     objeto de tabla SSDT. NOTA: si la base ya habilito Change Tracking para
     cualquiera de las otras outbox (Clinical/Hospitalization/Billing), los dos
     ALTER DATABASE de abajo ya estan aplicados y son idempotentes a nivel de
     base; el paso imprescindible y especifico de este bounded context es el
     ALTER TABLE sobre rda.OutboxEvent.
   - El ALTER TABLE rda.OutboxEvent ENABLE CHANGE TRACKING tampoco es
     declarativo en este proyecto; SSDT (SQL Azure V12) no expone el tracking
     por tabla en el modelo de objetos, por lo que se aplica aqui como paso de
     onboarding operativo por base tenant.

 OBLIGATORIO POR BASE TENANT (ADR-003 #6 / ADR-011): este DDL debe ejecutarse
 UNA VEZ en cada base tenant ANTES de activar el tenant en Platform.TenantCatalog
 (o antes de agregar la fila de cursor en Platform.TenantOutboxCursor para
 rda.OutboxEvent — ver OnboardRdaOutboxCursor.sql). Sin el, el relay no puede
 leer la outbox de RDA.

 TODO(DBA / onboarding tenant): ejecutar este script como parte del
 aprovisionamiento de cada base tenant, idealmente automatizado en el pipeline
 de release o en el runbook de onboarding.
 TODO(verificar): en Azure SQL Database ALLOW_SNAPSHOT_ISOLATION suele estar
 ON por defecto; confirmarlo antes de produccion (ADR-011, Consecuencias).
================================================================================
*/

-- Habilitar Change Tracking con retencion de 7 dias.
-- Por que 7 dias: si el relay cae mas tiempo que la retencion, LastSyncVersion
-- puede quedar por debajo de CHANGE_TRACKING_MIN_VALID_VERSION y el tenant entra
-- en REQUIRES_RESYNC (ver ADR-011, recovery). Idempotente a nivel de base: si
-- alguna otra outbox ya lo habilito, este SET no cambia nada.
ALTER DATABASE CURRENT
SET CHANGE_TRACKING = ON
(
    CHANGE_RETENTION = 7 DAYS,
    AUTO_CLEANUP = ON
);
GO

-- Snapshot isolation para lecturas consistentes de Change Tracking por el relay.
ALTER DATABASE CURRENT
SET ALLOW_SNAPSHOT_ISOLATION ON;
GO

-- Habilitar Change Tracking sobre la outbox canonica de RDA.
-- TRACK_COLUMNS_UPDATED = OFF: la outbox es append-only; solo interesa que la
-- fila se inserto (operacion 'I'), no que columnas cambiaron (ver ADR-003 seccion 3).
ALTER TABLE [rda].[OutboxEvent]
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF);
GO
