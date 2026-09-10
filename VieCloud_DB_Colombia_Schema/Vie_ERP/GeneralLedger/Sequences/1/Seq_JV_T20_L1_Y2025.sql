CREATE SEQUENCE [GeneralLedger].[Seq_JV_T20_L1_Y2025]
    AS BIGINT
    START WITH 24525
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 20, libro 1 del año 2025, dentro del esquema de contabilidad general. La secuencia inicia en 77769 sin ciclo ni caché, garantizando unicidad en la numeración correlativa de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2025';
GO
