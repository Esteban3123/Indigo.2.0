CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L3_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos contables (Journal Vouchers) correspondientes al tipo 16, nivel 3, del año fiscal 2025, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 1 con incremento unitario y sin caché, garantizando unicidad en cada registro generado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2025';
GO
