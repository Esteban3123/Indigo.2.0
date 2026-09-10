CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L3_Y2023]
    AS BIGINT
    START WITH 1101
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al libro mayor (GeneralLedger), específicamente para la tabla 34, nivel 3 del año fiscal 2023. La secuencia inicia en 1101 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de cada registro contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2023';
GO
