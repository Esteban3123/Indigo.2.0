CREATE SEQUENCE [GeneralLedger].[Seq_JV_T9_L1_Y2023]
    AS BIGINT
    START WITH 1359
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Voucher) del tipo 9, libro 1, correspondientes al año 2023, dentro del esquema de contabilidad general. Inicia en 63657 y no se reinicia al alcanzar el máximo, garantizando unicidad continua en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L1_Y2023';
GO
