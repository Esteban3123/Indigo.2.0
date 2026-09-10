/*
================================================================================
 OnboardBillingOutboxCursor.sql
================================================================================
 Script operativo (NO DACPAC) para registrar el cursor de Billing.OutboxEvent
 en la tabla Platform.TenantOutboxCursor del Control Plane, habilitando al relay
 (AzClinicalOutboxRelay) para drenar la nueva outbox de facturacion de estancias.

 CUANDO EJECUTAR
 ---------------
 Una vez por tenant, DESPUES de:
   1. Haber ejecutado Billing/ChangeTracking-enablement.sql en la base tenant.
   2. Haber desplegado Billing.OutboxEvent en la base tenant (DACPAC v{version}).
   3. Haber desplegado OutboxTableCatalog.cs con BillingOutbox en el relay.

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
DECLARE @OutboxName NVARCHAR(200)    = 'Billing.OutboxEvent';

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
-- de Change Tracking de la base tenant sin procesar historial previo (ADR-002
-- §Inicializacion del cursor). Esto es lo correcto para un onboarding nuevo:
-- no queremos reintentar eventos historicos anteriores al alta del cursor.
IF NOT EXISTS (
    SELECT 1
    FROM   [Platform].[TenantOutboxCursor]
    WHERE  [TenantId]   = @TenantId
      AND  [OutboxName] = @OutboxName
)
BEGIN
    INSERT INTO [Platform].[TenantOutboxCursor]
        ([TenantId], [OutboxName], [LastSyncVersion], [ErrorCount], [CreatedAtUtc], [UpdatedAtUtc])
    VALUES
        (@TenantId, @OutboxName, -1, 0, SYSUTCDATETIME(), SYSUTCDATETIME());

    PRINT CONCAT('Cursor creado para tenant ', CAST(@TenantId AS NVARCHAR(36)), ' / outbox ', @OutboxName, ' con LastSyncVersion = -1 (pendiente de inicializacion por el relay).');
END
ELSE
BEGIN
    PRINT CONCAT('Cursor ya existe para tenant ', CAST(@TenantId AS NVARCHAR(36)), ' / outbox ', @OutboxName, '. No se realizo ninguna modificacion.');
END
GO
