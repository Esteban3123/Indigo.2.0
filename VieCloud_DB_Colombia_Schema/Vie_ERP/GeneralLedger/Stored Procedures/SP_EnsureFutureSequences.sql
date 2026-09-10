CREATE PROCEDURE [GeneralLedger].[SP_EnsureFutureSequences]
    @YearBase INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @sql NVARCHAR(MAX) = N'';

    -- Determina los años a asegurar
    DECLARE @YearNext INT = @YearBase + 1;
    DECLARE @YearAfterNext INT = @YearBase + 2;

    -- Por cada combinación existente de tipo y libro, asegura la secuencia de los años requeridos
    ;WITH ExistingCombinations AS (
        SELECT
            CAST(SUBSTRING(s.name, CHARINDEX('_T', s.name) + 2, CHARINDEX('_L', s.name) - CHARINDEX('_T', s.name) - 2) AS INT) AS JournalVoucherTypeId,
            CAST(SUBSTRING(s.name, CHARINDEX('_L', s.name) + 2, CHARINDEX('_Y', s.name) - CHARINDEX('_L', s.name) - 2) AS INT) AS LegalBookId
        FROM sys.sequences s
        JOIN sys.schemas sc ON sc.schema_id = s.schema_id
        WHERE sc.name = 'GeneralLedger'
        AND s.name LIKE 'Seq_JV_T%_L%_Y%'
        GROUP BY 
            CAST(SUBSTRING(s.name, CHARINDEX('_T', s.name) + 2, CHARINDEX('_L', s.name) - CHARINDEX('_T', s.name) - 2) AS INT),
            CAST(SUBSTRING(s.name, CHARINDEX('_L', s.name) + 2, CHARINDEX('_Y', s.name) - CHARINDEX('_L', s.name) - 2) AS INT)
    )
    SELECT @sql = @sql + '
        -- Asegurar año siguiente
        IF NOT EXISTS (SELECT 1 FROM sys.sequences s JOIN sys.schemas sc ON sc.schema_id = s.schema_id 
                       WHERE sc.name = ''GeneralLedger'' AND s.name = ''Seq_JV_T' + CAST(JournalVoucherTypeId AS VARCHAR) +
                           '_L' + CAST(LegalBookId AS VARCHAR) + '_Y' + CAST(@YearNext AS VARCHAR) + ''')
        BEGIN
            EXEC(''CREATE SEQUENCE GeneralLedger.Seq_JV_T' + CAST(JournalVoucherTypeId AS VARCHAR) + 
            '_L' + CAST(LegalBookId AS VARCHAR) + '_Y' + CAST(@YearNext AS VARCHAR) + ' AS bigint START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE'');
        END

        -- Asegurar año subsiguiente
        IF NOT EXISTS (SELECT 1 FROM sys.sequences s JOIN sys.schemas sc ON sc.schema_id = s.schema_id 
                       WHERE sc.name = ''GeneralLedger'' AND s.name = ''Seq_JV_T' + CAST(JournalVoucherTypeId AS VARCHAR) +
                           '_L' + CAST(LegalBookId AS VARCHAR) + '_Y' + CAST(@YearAfterNext AS VARCHAR) + ''')
        BEGIN
            EXEC(''CREATE SEQUENCE GeneralLedger.Seq_JV_T' + CAST(JournalVoucherTypeId AS VARCHAR) + 
            '_L' + CAST(LegalBookId AS VARCHAR) + '_Y' + CAST(@YearAfterNext AS VARCHAR) + ' AS bigint START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE'');
        END;'
    FROM ExistingCombinations;

    EXEC(@sql);
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de mantenimiento contable que garantiza la existencia de secuencias numéricas para los comprobantes de diario (Journal Vouchers) de los años siguientes al año base indicado. Dado un año de referencia, crea automáticamente las secuencias de numeración para el año siguiente y el subsiguiente, por cada combinación de tipo de comprobante y libro contable legal que ya exista en el esquema GeneralLedger. Su propósito es evitar interrupciones en la numeración correlativa de asientos contables al cruzar cambios de año fiscal, asegurando que el Libro Mayor siempre tenga secuencias disponibles con anticipación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_EnsureFutureSequences';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_EnsureFutureSequences';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Garantiza que existan secuencias de numeración de comprobantes de diario por tipo y libro legal para los dos años siguientes al año base, creándolas dinámicamente si faltan.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una secuencia previa con el patrón Seq_JV_T<tipo>_L<libro>_Y<año> en el esquema GeneralLedger para que se generen nuevas; El usuario ejecutor debe tener permisos de CREATE SEQUENCE en el esquema GeneralLedger; El año base debe ser un entero válido', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa secuencias del esquema GeneralLedger cuyo nombre coincide con el patrón ''Seq_JV_T%_L%_Y%''; Las secuencias creadas son siempre BIGINT, inician en 1, incrementan de 1 en 1, con MINVALUE 0 y NO CACHE; Solo se aseguran secuencias para combinaciones (tipo, libro) que ya existen para algún año previo; no inventa combinaciones nuevas; Garantiza disponibilidad de numeración para los dos años inmediatamente posteriores al año base; Nunca sobrescribe secuencias existentes (uso de IF NOT EXISTS)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de diario (Journal Voucher); Tipo de comprobante; Libro legal (Legal Book); Secuencias contables anuales', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.Seq_JV_T<tipo>_L<libro>_Y<YearBase+1>: Si no existe la secuencia para el año siguiente de una combinación (tipo, libro) ya existente, se ejecuta CREATE SEQUENCE BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE; [INSERT] GeneralLedger.Seq_JV_T<tipo>_L<libro>_Y<YearBase+2>: Si no existe la secuencia para el año subsiguiente de una combinación (tipo, libro) ya existente, se ejecuta CREATE SEQUENCE BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 0 NO CACHE', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe la secuencia GeneralLedger.Seq_JV_T<tipo>_L<libro>_Y<año+1> → Crea dinámicamente esa secuencia BIGINT (START 1, INCREMENT 1, MINVALUE 0, NO CACHE) else No la recrea; si No existe la secuencia GeneralLedger.Seq_JV_T<tipo>_L<libro>_Y<año+2> → Crea dinámicamente esa secuencia BIGINT (START 1, INCREMENT 1, MINVALUE 0, NO CACHE) else No la recrea', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.sequences; sys.schemas', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_EnsureFutureSequences';
-- GO
