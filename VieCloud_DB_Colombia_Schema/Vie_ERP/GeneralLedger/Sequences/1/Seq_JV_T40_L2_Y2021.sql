CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L2_Y2021]
    AS BIGINT
    START WITH 2442
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos del diario contable (Journal Voucher) del tipo de transacción T40, libro o nivel L2, correspondientes al ejercicio fiscal 2021. La secuencia inicia en 46, lo que indica que ya se registraron asientos previos antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2021';
GO
