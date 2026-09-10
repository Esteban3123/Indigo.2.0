CREATE SEQUENCE [GeneralLedger].[Seq_JV_T58_L2_Y2024]
    AS BIGINT
    START WITH 95
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla de transacciones tipo 58, libro 2, correspondiente al año fiscal 2024. La secuencia inicia en 95, lo que indica que ya existían 94 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2024';
GO
