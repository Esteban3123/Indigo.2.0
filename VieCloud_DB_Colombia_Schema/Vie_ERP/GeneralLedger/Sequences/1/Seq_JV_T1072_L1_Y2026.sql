CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1072_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para las entradas del libro mayor (GeneralLedger), específicamente para el diario de comprobantes (JV) asociado a la tabla T1072, nivel L1, correspondientes al año 2026. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en cada valor asignado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L1_Y2026';
GO
