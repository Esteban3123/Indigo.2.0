CREATE SEQUENCE [GeneralLedger].[Seq_JV_T63_L3_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos o líneas contables del libro mayor (GeneralLedger), específicamente para el tipo de transacción 63, nivel 3, correspondientes al año 2026. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de los registros del diario (Journal Voucher).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T63_L3_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T63_L3_Y2026';
GO
