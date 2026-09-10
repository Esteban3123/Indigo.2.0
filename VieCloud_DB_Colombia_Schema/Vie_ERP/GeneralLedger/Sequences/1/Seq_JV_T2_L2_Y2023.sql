CREATE SEQUENCE [GeneralLedger].[Seq_JV_T2_L2_Y2023]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro contable tipo 2, nivel 2, correspondientes al año fiscal 2023, dentro del esquema GeneralLedger. La secuencia inicia en 16, incrementa de uno en uno y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L2_Y2023';
GO
