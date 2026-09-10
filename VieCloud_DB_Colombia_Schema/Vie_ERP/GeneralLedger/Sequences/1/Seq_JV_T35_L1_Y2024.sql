CREATE SEQUENCE [GeneralLedger].[Seq_JV_T35_L1_Y2024]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo 35, libro 1, correspondiente al año fiscal 2024. Inicia en 2513983, incrementa de uno en uno sin reinicio cíclico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T35_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T35_L1_Y2024';
GO
