CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L1_Y2025]
    AS BIGINT
    START WITH 548
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al tipo 17, libro 1 del año 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 2019 y no se reinicia ni usa caché, garantizando unicidad continua en los registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L1_Y2025';
GO
