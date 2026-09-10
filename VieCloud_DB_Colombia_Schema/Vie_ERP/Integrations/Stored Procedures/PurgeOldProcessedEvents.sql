CREATE   PROCEDURE Integrations.PurgeOldProcessedEvents
    @RetentionDays INT = 7
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CutoffDate DATETIME2(3) = DATEADD(DAY, -@RetentionDays, SYSUTCDATETIME());
    DECLARE @DeletedCount INT;
    
    -- Eliminar en batches para evitar bloqueos largos
    WHILE 1 = 1
    BEGIN
        DELETE TOP (5000) FROM Integrations.ProcessedEvents
        WHERE ProcessedAt < @CutoffDate;
        
        SET @DeletedCount = @@ROWCOUNT;
        
        IF @DeletedCount = 0
            BREAK;
            
        -- Pequeña pausa entre batches
        WAITFOR DELAY '00:00:00.100';
    END
END;

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Depura eventos de integración ya procesados que superan un periodo de retención, eliminándolos por lotes para evitar bloqueos prolongados.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El periodo de retención debe ser un valor entero (por defecto 7 días).; Debe existir la tabla de eventos procesados con la marca temporal de procesamiento.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca elimina eventos cuya fecha de procesamiento sea posterior o igual al corte (UTC actual - días de retención).; Las eliminaciones se realizan siempre en lotes acotados de 5000 filas.; Entre lotes siempre se introduce una pausa de 100 ms para reducir contención.; El cálculo del corte se realiza en horario UTC.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Eventos de integración procesados; Retención de datos; Purga por lotes', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Integrations.ProcessedEvents: Cuando ProcessedAt < (UTC actual - RetentionDays), elimina hasta 5000 filas por iteración hasta que no queden registros que cumplan la condición.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DeletedCount = 0 (ningún registro eliminado en el batch) → Termina el bucle de purga (BREAK). else Espera 100 ms (WAITFOR DELAY) y continúa con el siguiente batch.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Integrations.ProcessedEvents', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'PurgeOldProcessedEvents';
-- GO
