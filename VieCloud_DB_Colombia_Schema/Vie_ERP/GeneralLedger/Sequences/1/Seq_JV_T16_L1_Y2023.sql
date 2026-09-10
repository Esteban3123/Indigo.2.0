CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L1_Y2023]
    AS BIGINT
    START WITH 530
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para los asientos contables (Journal Vouchers) del libro 1 (L1), tipo 16 (T16), correspondientes al año fiscal 2023, dentro del esquema de contabilidad general. La secuencia inicia en 5524, incrementa de uno en uno y no se reinicia ni usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2023';
GO
