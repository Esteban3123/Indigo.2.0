CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1084_L1_Y2024]
    AS BIGINT
    START WITH 55
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor general, específicamente para la transacción T1084, libro L1 del año 2024. La secuencia inicia en 55, lo que indica que ya existían registros previos al crearla.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1084_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1084_L1_Y2024';
GO
