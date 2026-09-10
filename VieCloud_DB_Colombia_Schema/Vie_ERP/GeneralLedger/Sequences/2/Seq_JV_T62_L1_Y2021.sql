CREATE SEQUENCE [GeneralLedger].[Seq_JV_T62_L1_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla T62, libro 1 (L1) del año fiscal 2021. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período anual.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T62_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T62_L1_Y2021';
GO
