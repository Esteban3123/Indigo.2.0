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