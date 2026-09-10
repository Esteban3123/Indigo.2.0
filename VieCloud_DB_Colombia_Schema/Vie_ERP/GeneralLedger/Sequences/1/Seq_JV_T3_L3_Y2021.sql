CREATE SEQUENCE [GeneralLedger].[Seq_JV_T3_L3_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) correspondientes al tipo 3, nivel 3 del año 2021, dentro del esquema de contabilidad general (GeneralLedger). Inicia en 1 con incremento de 1, sin caché, garantizando orden y unicidad en los registros de dicho período y clasificación contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T3_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T3_L3_Y2021';
GO
