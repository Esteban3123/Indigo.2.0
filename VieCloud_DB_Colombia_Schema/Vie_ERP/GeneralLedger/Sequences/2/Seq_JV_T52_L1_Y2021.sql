CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L1_Y2021]
    AS BIGINT
    START WITH 563
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo bigint para asientos de diario (Journal Voucher) del libro mayor general, correspondientes al tipo 52, libro 1 (L1) del año 2021. Inicia en 5 y no se reinicia al alcanzar el máximo, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2021';
GO
