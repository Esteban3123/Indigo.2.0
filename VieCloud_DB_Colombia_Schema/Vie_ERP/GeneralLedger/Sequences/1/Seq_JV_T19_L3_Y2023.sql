CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L3_Y2023]
    AS BIGINT
    START WITH 10488
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 19, nivel 3 del año fiscal 2023, en el esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 10488 e incrementa de uno en uno, sin caché, garantizando unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L3_Y2023';
GO
