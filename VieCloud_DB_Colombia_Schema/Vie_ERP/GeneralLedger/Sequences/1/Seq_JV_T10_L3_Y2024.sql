CREATE SEQUENCE [GeneralLedger].[Seq_JV_T10_L3_Y2024]
    AS BIGINT
    START WITH 4141
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 10, nivel 3, del ejercicio fiscal 2024, dentro del módulo de contabilidad general. La secuencia inicia en 4141 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L3_Y2024';
GO
