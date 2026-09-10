CREATE SEQUENCE [GeneralLedger].[Seq_JV_T2_L1_Y2021]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos contables (Journal Vouchers) del tipo 2, libro 1, correspondientes al año fiscal 2021, dentro del esquema de contabilidad general. La secuencia inicia en 9, incrementa de uno en uno y no se reinicia ni usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L1_Y2021';
GO
