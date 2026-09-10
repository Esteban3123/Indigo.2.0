CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L2_Y2024]
    AS BIGINT
    START WITH 137
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para asientos de diario contable (Journal Vouchers) correspondientes al tipo 8, libro 2 del año 2024, dentro del esquema de libro mayor general (GeneralLedger). La secuencia inicia en 2272827 sin ciclo ni caché, garantizando valores irrepetibles para ese período y clasificación contable específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L2_Y2024';
GO
