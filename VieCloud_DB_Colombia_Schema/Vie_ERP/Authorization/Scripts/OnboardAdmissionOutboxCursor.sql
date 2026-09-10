/*
================================================================================
 OnboardAdmissionOutboxCursor.sql
================================================================================
 Script operativo (NO DACPAC) para registrar el cursor de Admissions.OutboxEvent
 en la tabla Platform.TenantOutboxCursor del Control Plane, habilitando al relay
 (AzClinicalOutboxRelay) para drenar la outbox de ingresos hospitalarios que
 alimenta la feature Distribucion de Usuarios de Autorizaciones.

 CUANDO EJECUTAR
 ---------------
 Una vez por tenant, DESPUES de:
   1. Haber ejecutado Admissions/ChangeTracking-enablement.sql en la base tenant.
   2. Haber desplegado Admissions.OutboxEvent en la base tenant (DACPAC v{version}).
   3. Haber desplegado el trigger TR_ADINGRESO_AdmissionStatusChanged (dbo/Tables/Tables1/ADINGRESO.sql)
      y el cambio correspondiente en EHR_ServicesCore (IND_ADM_GrabaIngresosTransaccion).
   4. Haber desplegado OutboxTableCatalog.cs con AdmissionOutbox en el relay (agregar
      'Admissions.OutboxEvent' a Indigo.AzClinicalOutboxRelay.Logic.OutboxTableCatalog
      o a la configuracion Relay:OutboxTables).
   5. Haber agregado clinical.admission-registered.v1 y clinical.admission-status-changed.v1
      a Relay:AllowedEventTypes.

 DONDE EJECUTAR
 --------------
 Ejecutar en la base de datos del CONTROL PLANE (donde viven Platform.TenantCatalog
 y Platform.TenantOutboxCursor). NO ejecutar en la base tenant.

 PARAMETROS
 ----------
 Ajustar @TenantId antes de ejecutar. El tenant DEV-COLOMBIA se incluye como
 ejemplo/referencia. Para produccion: consultar Platform.TenantCatalog para
 obtener el TenantId correcto de cada tenant.

 IDEMPOTENCIA
 ------------
 Usa INSERT IF NOT EXISTS (NOT EXISTS + SELECT). Seguro re-ejecutar: si la fila
 ya existe no falla ni duplica.
================================================================================
*/

-- *** AJUSTAR POR TENANT ***
DECLARE @TenantId   UNIQUEIDENTIFIER = '3E9BF075-2FC3-4151-A747-B4B7D848D24A'; -- DEV-COLOMBIA
DECLARE @OutboxName NVARCHAR(200)    = 'Admissions.OutboxEvent';

-- Verificar que el tenant existe y esta ACTIVE antes de insertar el cursor.
IF NOT EXISTS (
    SELECT 1
    FROM   [Platform].[TenantCatalog]
    WHERE  [TenantId] = @TenantId
      AND  [Status]   = 'ACTIVE'
)
BEGIN
    RAISERROR(
        'El tenant %s no existe o no tiene Status=ACTIVE en Platform.TenantCatalog. Verifica el TenantId y el estado antes de continuar.',
        16, 1, @TenantId
    );
    RETURN;
END

-- Insertar el cursor con LastSyncVersion = -1 (NeedsInitialization).
-- El relay detectara -1 en el primer ciclo y lo inicializara a la version actual
-- de Change Tracking de la base tenant sin procesar historial previo. Esto es lo
-- correcto para un onboarding nuevo: no queremos reintentar altas/cambios de
-- estado de ingresos anteriores al alta del cursor.
IF NOT EXISTS (
    SELECT 1
    FROM   [Platform].[TenantOutboxCursor]
    WHERE  [TenantId]   = @TenantId
      AND  [OutboxName] = @OutboxName
)
BEGIN
    INSERT INTO [Platform].[TenantOutboxCursor]
        ([TenantId], [OutboxName], [LastSyncVersion], [ErrorCount], [UpdatedAtUtc])
    VALUES
        (@TenantId, @OutboxName, -1, 0, SYSUTCDATETIME());

    PRINT CONCAT('Cursor creado para tenant ', CAST(@TenantId AS NVARCHAR(36)), ' / outbox ', @OutboxName, ' con LastSyncVersion = -1 (pendiente de inicializacion por el relay).');
END
ELSE
BEGIN
    PRINT CONCAT('Cursor ya existe para tenant ', CAST(@TenantId AS NVARCHAR(36)), ' / outbox ', @OutboxName, '. No se realizo ninguna modificacion.');
END
GO
