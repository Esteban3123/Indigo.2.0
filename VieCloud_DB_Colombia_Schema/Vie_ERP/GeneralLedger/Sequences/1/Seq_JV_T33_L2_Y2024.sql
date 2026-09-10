CREATE SEQUENCE [GeneralLedger].[Seq_JV_T33_L2_Y2024]
    AS BIGINT
    START WITH 145
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo de transacción 33, nivel 2, del año fiscal 2024. La secuencia inicia en 3517, incrementa de uno en uno y no permite reinicio cíclico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L2_Y2024';
GO
