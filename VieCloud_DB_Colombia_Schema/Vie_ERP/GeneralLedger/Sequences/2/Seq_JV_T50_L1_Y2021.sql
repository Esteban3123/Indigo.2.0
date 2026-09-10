CREATE SEQUENCE [GeneralLedger].[Seq_JV_T50_L1_Y2021]
    AS BIGINT
    START WITH 590
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor general, correspondientes a la tabla T50, libro 1 (L1), del año fiscal 2021. La secuencia inicia en 12, incrementa de uno en uno y no reinicia al alcanzar el máximo, lo que indica registros contables ya existentes previos a su uso actual.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L1_Y2021';
GO
