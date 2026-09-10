CREATE SEQUENCE [GeneralLedger].[Seq_JV_T9_L2_Y2020]
    AS BIGINT
    START WITH 587
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al tipo 9, libro 2 del año 2020, dentro del esquema de Contabilidad General. La secuencia inicia en 20699 y no se reinicia ni usa caché, garantizando unicidad continua en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L2_Y2020';
GO
