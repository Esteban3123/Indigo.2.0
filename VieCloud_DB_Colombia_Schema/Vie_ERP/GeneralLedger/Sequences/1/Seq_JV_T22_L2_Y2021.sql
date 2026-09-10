CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L2_Y2021]
    AS BIGINT
    START WITH 69
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para asientos contables (Journal Vouchers) del tipo 22, libro 2 (L2), correspondientes al ejercicio fiscal 2021, dentro del esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 9710, incrementa de uno en uno y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2021';
GO
