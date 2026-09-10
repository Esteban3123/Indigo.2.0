CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L3_Y2025]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos o partidas del libro mayor (GeneralLedger), correspondientes al tipo de transacción 14, nivel 3 del año 2025. La secuencia inicia en 9 e incrementa de uno en uno, sin caché, garantizando unicidad en los registros del período fiscal indicado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2025';
GO
