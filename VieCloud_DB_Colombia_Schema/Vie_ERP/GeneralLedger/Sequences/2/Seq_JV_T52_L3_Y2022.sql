CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L3_Y2022]
    AS BIGINT
    START WITH 539
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos o líneas de diario (Journal Voucher) correspondientes al tipo 52, nivel 3 del año 2022 en el módulo de Contabilidad General, iniciando desde el valor 539 e incrementando de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2022';
GO
