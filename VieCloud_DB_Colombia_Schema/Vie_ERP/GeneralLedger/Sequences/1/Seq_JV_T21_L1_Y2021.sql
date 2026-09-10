CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L1_Y2021]
    AS BIGINT
    START WITH 6658
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del tipo 21, libro 1, correspondientes al año 2021, dentro del módulo de contabilidad general. Inicia en 151321, incrementa de uno en uno sin ciclo ni caché, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L1_Y2021';
GO
