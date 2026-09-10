CREATE SEQUENCE [GeneralLedger].[Seq_JV_T36_L3_Y2022]
    AS BIGINT
    START WITH 20889
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del tipo 36, nivel 3, correspondientes al año 2022 en el módulo de Contabilidad General. La secuencia inicia en 20889 e incrementa de uno en uno, sin caché, garantizando unicidad en la numeración de esos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L3_Y2022';
GO
