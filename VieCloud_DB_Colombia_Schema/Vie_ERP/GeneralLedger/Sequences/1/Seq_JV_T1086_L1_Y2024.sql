CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1086_L1_Y2024]
    AS BIGINT
    START WITH 15
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro 1 (L1) del período fiscal 2024, correspondientes a la transacción o tipo de comprobante T1086 dentro del esquema de contabilidad general. La secuencia inicia en 15, lo que indica que ya fueron registradas entradas previas antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1086_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1086_L1_Y2024';
GO
