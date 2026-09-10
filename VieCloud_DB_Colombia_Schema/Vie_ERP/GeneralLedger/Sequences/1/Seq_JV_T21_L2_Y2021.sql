CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L2_Y2021]
    AS BIGINT
    START WITH 6658
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para transacciones del tipo 21, libro 2, correspondientes al año fiscal 2021. Inicia desde el valor 146726, lo que indica registros contables preexistentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2021';
GO
