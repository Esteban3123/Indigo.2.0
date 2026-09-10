CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1075_L2_Y2024]
    AS BIGINT
    START WITH 16
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor general, específicamente para el libro de nivel 2 (L2) de la entidad o centro de costo T1075, correspondiente al año fiscal 2024. La secuencia inicia en 16, indicando registros previos ya generados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L2_Y2024';
GO
