-- =============================================================================
-- POC: Prueba del flujo Billing.OutboxEvent → AzClinicalOutboxRelay → Service Bus
-- =============================================================================
-- Qué hace este script:
--   1. Muestra el estado actual del cursor del relay para Billing.OutboxEvent
--   2. Inserta 5 filas de prueba en Billing.OutboxEvent (tomadas de AccountControlStays)
--   3. Muestra qué filas nuevas vería el relay con Change Tracking
--   4. Muestra cómo quedaría el cursor actualizado (solo lectura; el relay lo actualiza solo)
--
-- PRECONDICIÓN: Change Tracking habilitado en la base y en Billing.OutboxEvent
--   (ver Billing/ChangeTracking-enablement.sql). Si no está habilitado, el paso 3
--   lanzará error — ejecutar habilitación primero.
--
-- IDEMPOTENTE: las inserciones usan el índice único BusinessEventHash.
--   Insertar dos veces el mismo row no duplica: la segunda vez falla silencioso (TRY/CATCH).
--
-- Para simular un "botón de actualizar" basta con ejecutar el PASO 2 de nuevo con
--   un OccurredAtUtc distinto (1 segundo más tarde) → genera un BusinessEventHash
--   diferente → el relay lo vería como evento nuevo.
-- =============================================================================

SET NOCOUNT ON;

-- ─────────────────────────────────────────────────────────────────────────────
-- PASO 1: Estado actual del cursor
-- ─────────────────────────────────────────────────────────────────────────────
PRINT '== PASO 1: Estado del cursor del relay para Billing.OutboxEvent ==';

SELECT
    TenantId,
    OutboxName,
    LastSyncVersion,
    ErrorCount
FROM Platform.TenantOutboxCursor
WHERE OutboxName = 'Billing.OutboxEvent'
ORDER BY TenantId;

PRINT '(Si no hay filas → el cursor no está inicializado. Ejecutar Authorization/Scripts/OnboardBillingOutboxCursor.sql primero)';
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- PASO 2: Insertar filas de prueba en Billing.OutboxEvent
--   Datos: Billing.AccountControlStays (5 rows del ingreso 821870, CAMA518)
--   AdmissionCode : 821870
--   StayIds       : 9243, 9244, 9245, 9246, 9247
--   Paciente      : 110111120 (doc: 1079183992)
--   Entidad EPS   : 001
--   CUPS Id       : 9759
--   Días estancia : 999
-- ─────────────────────────────────────────────────────────────────────────────
PRINT '';
PRINT '== PASO 2: Insertar 5 eventos de prueba en Billing.OutboxEvent ==';

DECLARE @OccurredAt DATETIME2(7) = SYSUTCDATETIME();
DECLARE @TenantId   NVARCHAR(36) = '3E9BF075-2FC3-4151-A747-B4B7D848D24A';  -- DEV-COLOMBIA

-- Tabla temporal con los datos de AccountControlStays a procesar
DECLARE @Stays TABLE (
    StayId        INT,
    AdmissionCode NVARCHAR(20),
    Bed           NVARCHAR(20),
    BedId         INT,
    PatientCode   NVARCHAR(20),
    CupsId        INT,
    EntityCode    NVARCHAR(10),
    StartDate     DATETIME,
    EndDate       DATETIME,
    StayDays      INT
);

INSERT INTO @Stays VALUES
    (9247, '821870', 'CAMA518', 101684, '110111120', 9759, '001', '2025-11-23 00:00:00', '2025-11-23 23:59:59', 999),
    (9246, '821870', 'CAMA518', 101684, '110111120', 9759, '001', '2025-11-22 00:00:00', '2025-11-22 23:59:59', 999),
    (9245, '821870', 'CAMA518', 101684, '110111120', 9759, '001', '2025-11-21 00:00:00', '2025-11-21 23:59:59', 999),
    (9244, '821870', 'CAMA518', 101684, '110111120', 9759, '001', '2025-11-20 00:00:00', '2025-11-20 23:59:59', 999),
    (9243, '821870', 'CAMA518', 101684, '110111120', 9759, '001', '2025-11-19 00:00:00', '2025-11-19 23:59:59', 999);

-- Insertar en la outbox.  BusinessEventHash se calcula automáticamente (columna PERSISTED).
-- Si ya existe el mismo hash (reintento del mismo evento), la fila se omite con TRY/CATCH.
DECLARE @Inserted INT = 0;
DECLARE @Skipped  INT = 0;

DECLARE
    @StayId        INT,
    @AdmissionCode NVARCHAR(20),
    @Bed           NVARCHAR(20),
    @BedId         INT,
    @PatientCode   NVARCHAR(20),
    @CupsId        INT,
    @EntityCode    NVARCHAR(10),
    @StartDate     DATETIME,
    @EndDate       DATETIME,
    @StayDays      INT;

DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
    SELECT StayId, AdmissionCode, Bed, BedId, PatientCode, CupsId, EntityCode, StartDate, EndDate, StayDays
    FROM @Stays
    ORDER BY StayId;

OPEN cur;
FETCH NEXT FROM cur INTO @StayId, @AdmissionCode, @Bed, @BedId, @PatientCode, @CupsId, @EntityCode, @StartDate, @EndDate, @StayDays;

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @AggregateId  NVARCHAR(255) = CAST(@AdmissionCode AS NVARCHAR) + ':' + CAST(@StayId AS NVARCHAR);
    DECLARE @OccurredAtS  NVARCHAR(30)  = CONVERT(NVARCHAR(30), @OccurredAt, 126);

    -- Construir el PayloadJson como StayControlFact (camelCase, igual que el contrato)
    DECLARE @PayloadJson NVARCHAR(MAX) = (
        SELECT
            NEWID()                         AS eventId,
            'TENANT:' + @TenantId
                + ':OUTBOX:billing.stay-committed.v1:'
                + @AggregateId              AS messageId,
            '1'                             AS schemaVersion,
            @TenantId                       AS tenantId,
            @StayId                         AS stayRecordId,
            @AdmissionCode                  AS admissionNumber,
            @BedId                          AS bedId,
            @Bed                            AS bed,
            @CupsId                         AS cupsId,
            @EntityCode                     AS entityCode,
            @PatientCode                    AS patientCode,
            CONVERT(NVARCHAR(30), @StartDate, 126) AS startDate,
            CONVERT(NVARCHAR(30), @EndDate,   126) AS endDate,
            @StayDays                       AS stayDays,
            @OccurredAtS                    AS occurredAtUtc
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    );

    BEGIN TRY
        INSERT INTO Billing.OutboxEvent (EventType, AggregateType, AggregateId, PayloadJson, OccurredAtUtc)
        VALUES (
            'billing.stay-committed.v1',
            'AccountControlStay',
            @AggregateId,
            @PayloadJson,
            @OccurredAt
        );
        SET @Inserted += 1;
        PRINT '  ✓ Insertado  StayId=' + CAST(@StayId AS NVARCHAR) + '  AggregateId=' + @AggregateId;
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() IN (2601, 2627) -- violación de UQ_BillingOutboxEvent_BusinessEventHash
        BEGIN
            SET @Skipped += 1;
            PRINT '  ≡ Duplicado  StayId=' + CAST(@StayId AS NVARCHAR) + ' (mismo BusinessEventHash — ya existe en outbox)';
        END
        ELSE
            THROW; -- otro error: lo propaga
    END CATCH;

    FETCH NEXT FROM cur INTO @StayId, @AdmissionCode, @Bed, @BedId, @PatientCode, @CupsId, @EntityCode, @StartDate, @EndDate, @StayDays;
END;

CLOSE cur; DEALLOCATE cur;

PRINT '';
PRINT 'Resumen PASO 2 → Insertados: ' + CAST(@Inserted AS NVARCHAR) + '  |  Omitidos (dup): ' + CAST(@Skipped AS NVARCHAR);
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- PASO 3: Qué vería el relay con Change Tracking
--   El relay hace: CHANGETABLE(CHANGES Billing.OutboxEvent, @lastVersion)
--   Aquí simulamos eso con la versión mínima (0) para ver TODOS los cambios
--   rastreados, no solo los nuevos desde el último cursor.
-- ─────────────────────────────────────────────────────────────────────────────
PRINT '';
PRINT '== PASO 3: Cambios que vería el relay (Change Tracking) ==';

DECLARE @CursorVersion BIGINT;

-- Tomar la última versión del cursor (o 0 si aún no se inicializó)
SELECT @CursorVersion = ISNULL(MAX(LastSyncVersion), 0)
FROM Platform.TenantOutboxCursor
WHERE TableName = 'Billing.OutboxEvent';

PRINT 'Consultando cambios desde SYS_CHANGE_VERSION > ' + CAST(@CursorVersion AS NVARCHAR);

SELECT
    ct.SYS_CHANGE_VERSION   AS ChangeVersion,
    ct.SYS_CHANGE_OPERATION AS Operation,     -- 'I' = insert (append-only → siempre I)
    e.OutboxId,
    e.EventType,
    e.AggregateType,
    e.AggregateId,
    e.OccurredAtUtc,
    e.CreatedAtUtc,
    LEFT(e.PayloadJson, 200) AS PayloadPreview
FROM CHANGETABLE(CHANGES Billing.OutboxEvent, @CursorVersion) AS ct
JOIN Billing.OutboxEvent e ON e.OutboxId = ct.OutboxId
ORDER BY ct.SYS_CHANGE_VERSION ASC;

PRINT '(Si no hay filas → Change Tracking no está habilitado aún, o el cursor ya procesó estas filas)';
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- PASO 4: Verificación completa de lo insertado en la outbox
-- ─────────────────────────────────────────────────────────────────────────────
PRINT '';
PRINT '== PASO 4: Contenido actual de Billing.OutboxEvent ==';

SELECT
    OutboxId,
    EventType,
    AggregateId,
    OccurredAtUtc,
    CreatedAtUtc,
    LEFT(PayloadJson, 300) AS PayloadPreview
FROM Billing.OutboxEvent
ORDER BY OutboxId DESC;
GO

-- ─────────────────────────────────────────────────────────────────────────────
-- PASO 5 (OPCIONAL): Simular "botón Actualizar" → reinsertar con nuevo timestamp
--   Descomentar para simular un segundo evento sobre la misma estancia
--   (OccurredAt distinto → BusinessEventHash distinto → el relay lo ve como nuevo)
-- ─────────────────────────────────────────────────────────────────────────────
/*
PRINT '';
PRINT '== PASO 5: Simular re-inserción (botón Actualizar) para StayId=9247 ==';

DECLARE @NewOccurred DATETIME2(7) = DATEADD(SECOND, 1, SYSUTCDATETIME());

INSERT INTO Billing.OutboxEvent (EventType, AggregateType, AggregateId, PayloadJson, OccurredAtUtc)
VALUES (
    'billing.stay-committed.v1',
    'AccountControlStay',
    '821870:9247',
    (SELECT
        NEWID()                                  AS eventId,
        '1'                                      AS schemaVersion,
        '3E9BF075-2FC3-4151-A747-B4B7D848D24A'  AS tenantId,
        9247                                     AS stayRecordId,
        '821870'                                 AS admissionNumber,
        101684                                   AS bedId,
        'CAMA518'                                AS bed,
        9759                                     AS cupsId,
        '001'                                    AS entityCode,
        '110111120'                              AS patientCode,
        '2025-11-23T00:00:00'                    AS startDate,
        '2025-11-23T23:59:59'                    AS endDate,
        999                                      AS stayDays,
        CONVERT(NVARCHAR(30), @NewOccurred, 126) AS occurredAtUtc
     FOR JSON PATH, WITHOUT_ARRAY_WRAPPER),
    @NewOccurred
);
PRINT '  ✓ Re-insertado con OccurredAtUtc=' + CONVERT(NVARCHAR(30), @NewOccurred, 126);
*/
GO
