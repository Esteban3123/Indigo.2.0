CREATE SEQUENCE [GeneralLedger].[Seq_JV_T56_L2_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al libro mayor (GeneralLedger), específicamente para la tabla T56, libro nivel 2 (L2), del ejercicio fiscal 2026. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L2_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L2_Y2026';
GO
