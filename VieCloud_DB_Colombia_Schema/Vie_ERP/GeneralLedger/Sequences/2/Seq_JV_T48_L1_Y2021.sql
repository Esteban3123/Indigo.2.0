CREATE SEQUENCE [GeneralLedger].[Seq_JV_T48_L1_Y2021]
    AS BIGINT
    START WITH 2
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor (GeneralLedger), correspondientes al tipo 48, libro 1 (L1) del año 2021. La secuencia inicia en 55377, incrementa de uno en uno y no se reinicia al alcanzar su máximo valor.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L1_Y2021';
GO
