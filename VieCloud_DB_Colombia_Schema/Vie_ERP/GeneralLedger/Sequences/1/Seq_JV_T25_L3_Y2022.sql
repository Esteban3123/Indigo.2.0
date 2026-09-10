CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L3_Y2022]
    AS BIGINT
    START WITH 5501
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al tipo 25, nivel 3 y ejercicio fiscal 2022, en el módulo de Contabilidad General. La secuencia inicia en 5501 e incrementa de uno en uno, sin caché, garantizando unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L3_Y2022';
GO
