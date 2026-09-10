CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L3_Y2022]
    AS BIGINT
    START WITH 10485
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para asientos de diario (Journal Vouchers) del período contable correspondiente al tipo 12, nivel 3 del año 2022 en el módulo de Contabilidad General. La secuencia inicia en 10485 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L3_Y2022';
GO
