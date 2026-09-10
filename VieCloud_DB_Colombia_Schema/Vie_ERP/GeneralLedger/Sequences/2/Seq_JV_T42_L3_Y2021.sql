CREATE SEQUENCE [GeneralLedger].[Seq_JV_T42_L3_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los registros del libro mayor (GeneralLedger), específicamente para asientos de diario (JV) correspondientes al tipo/tabla 42 (T42), nivel 3 (L3) del año 2021. La secuencia inicia en 1, incrementa de uno en uno sin caché, garantizando unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T42_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T42_L3_Y2021';
GO
