CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L2_Y2024]
    AS BIGINT
    START WITH 42
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 16, libro 2 del año 2024, dentro del esquema de contabilidad general. La secuencia inicia en 6160 y no se reinicia ni usa caché, garantizando unicidad continua.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L2_Y2024';
GO
