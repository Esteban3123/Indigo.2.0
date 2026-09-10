CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L2_Y2022]
    AS BIGINT
    START WITH 2137
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la transacción tipo 40, libro 2, correspondiente al año fiscal 2022. La secuencia inicia en 64, lo que indica que ya se registraron asientos previos antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2022';
GO
