/*
================================================================================
 ChangeTracking-enablement.sql  (Admissions / Ingresos hospitalarios)
================================================================================
 Habilitacion de SQL Change Tracking para la outbox CANONICA del bounded context
 de Admisiones ([Admissions].[OutboxEvent]), soporte de la feature Distribucion
 de Usuarios de Autorizaciones (ver docs/PBI_BD_DISTRIBUCION_AUTORIZADORES_INGRESOS.md).
 Mismo modelo que rda/ChangeTracking-enablement.sql, Clinical/ChangeTracking-enablement.sql,
 Hospitalization/ChangeTracking-enablement.sql y Billing/ChangeTracking-enablement.sql
 (Outbox + Relay + Change Tracking, nunca UPDLOCK/status mutable).

 ALCANCE / NATURALEZA DE ESTE SCRIPT
 -----------------------------------
 Este NO es un objeto SSDT declarativo de tabla. Contiene DDL a nivel de
 instruccion (ALTER DATABASE / ALTER TABLE ... ENABLE CHANGE TRACKING) que el
 compilador de SSDT no modela como un objeto del esquema. Por eso:

   - En VieCloudColombia.sqlproj se referencia como <None Include="...">, NO
     como <Build Include="...">. Mismo criterio que las otras outbox del
     proyecto.
   - El ALTER DATABASE (SET CHANGE_TRACKING = ON, SET ALLOW_SNAPSHOT_ISOLATION
     ON) se gestiona por CONFIGURACION DEL PROYECTO / POST-DEPLOY, no como
     objeto de tabla SSDT. NOTA: si la base ya habilito Change Tracking para
     cualquiera de las otras outbox (Clinical/Hospitalization/Billing/rda), los
     dos ALTER DATABASE de abajo ya estan aplicados y son idempotentes a nivel
     de base; el paso imprescindible y especifico de este bounded context es el
     ALTER TABLE sobre Admissions.OutboxEvent.
   - dbo.ADINGRESO YA TIENE Change Tracking habilitado (ver dbo/Tables/Tables1/ADINGRESO.sql)
     — eso es independiente de este script y no requiere cambios; el Normalizer
     (Function 2) lee ADINGRESO directamente (ADR-004), no via outbox.

 OBLIGATORIO POR BASE TENANT: este DDL debe ejecutarse UNA VEZ en cada base
 tenant ANTES de agregar la fila de cursor en Platform.TenantOutboxCursor para
 Admissions.OutboxEvent (ver Authorization/Scripts/OnboardAdmissionOutboxCursor.sql).
 Sin el, el relay no puede leer la outbox de Admisiones.
================================================================================
*/

-- Habilitar Change Tracking con retencion de 7 dias.
-- Por que 7 dias: si el relay cae mas tiempo que la retencion, LastSyncVersion
-- puede quedar por debajo de CHANGE_TRACKING_MIN_VALID_VERSION y el tenant entra
-- en REQUIRES_RESYNC. Idempotente a nivel de base: si alguna otra outbox ya lo
-- habilito, este SET no cambia nada.
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

-- Habilitar Change Tracking sobre la outbox canonica de Admisiones.
-- TRACK_COLUMNS_UPDATED = OFF: la outbox es append-only; solo interesa que la
-- fila se inserto (operacion 'I'), no que columnas cambiaron.
ALTER TABLE [Admissions].[OutboxEvent]
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF);
GO
