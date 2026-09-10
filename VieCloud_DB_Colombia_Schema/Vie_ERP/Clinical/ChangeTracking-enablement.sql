/*
================================================================================
 ChangeTracking-enablement.sql  (Tarea 37515)
================================================================================
 Habilitacion de SQL Change Tracking para la outbox clinica (ADR-003 seccion 3,
 mecanismo del relay en ADR-011).

 ALCANCE / NATURALEZA DE ESTE SCRIPT
 -----------------------------------
 Este NO es un objeto SSDT declarativo de tabla. Contiene DDL a nivel de
 instruccion (ALTER DATABASE / ALTER TABLE ... ENABLE CHANGE TRACKING) que el
 compilador de SSDT no modela como un objeto del esquema. Por eso:

   - En VieCloudColombia.sqlproj se referencia como <None Include="...">, NO
     como <Build Include="...">. Incluirlo como Build romperia la compilacion
     del DACPAC (no es una definicion declarativa de objeto).
   - El ALTER DATABASE (SET CHANGE_TRACKING = ON, SET ALLOW_SNAPSHOT_ISOLATION
     ON) se gestiona por CONFIGURACION DEL PROYECTO / POST-DEPLOY, no como
     objeto de tabla SSDT. En un proyecto SSDT esto corresponde a las
     propiedades de base de datos (Database Settings) o a un script de
     post-deployment ejecutado por el DBA, segun el flujo de despliegue MANUAL
     descrito en el CLAUDE.md del repo.
   - El ALTER TABLE Clinical.OutboxEvent ENABLE CHANGE TRACKING tampoco es
     declarativo en este proyecto; SSDT (SQL Azure V12) no expone el tracking
     por tabla en el modelo de objetos, por lo que se aplica aqui como paso de
     onboarding operativo por base tenant.

 OBLIGATORIO POR BASE TENANT (ADR-003 #6 / ADR-011): este DDL debe ejecutarse
 UNA VEZ en cada base tenant ANTES de activar el tenant en Platform.TenantCatalog.
 Sin el, el relay no puede leer la outbox.

 TODO(DBA / onboarding tenant): ejecutar este script como parte del
 aprovisionamiento de cada base tenant, idealmente automatizado en el pipeline
 de release (azure-pipelines-release.yml) o en el runbook de onboarding.
 TODO(verificar): en Azure SQL Database ALLOW_SNAPSHOT_ISOLATION suele estar
 ON por defecto; confirmarlo antes de produccion (ADR-011, Consecuencias).
================================================================================
*/

-- Habilitar Change Tracking con retencion de 7 dias.
-- Por que 7 dias: si el relay cae mas tiempo que la retencion, LastSyncVersion
-- puede quedar por debajo de CHANGE_TRACKING_MIN_VALID_VERSION y el tenant entra
-- en REQUIRES_RESYNC (ver ADR-011, recovery).
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

-- Habilitar Change Tracking sobre la outbox.
-- TRACK_COLUMNS_UPDATED = OFF: la outbox es append-only; solo interesa que la fila
-- se inserto (operacion 'I'), no que columnas cambiaron (ver ADR-003 seccion 3).
ALTER TABLE [Clinical].[OutboxEvent]
ENABLE CHANGE_TRACKING
WITH (TRACK_COLUMNS_UPDATED = OFF);
GO
