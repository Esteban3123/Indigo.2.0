CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L2_Y2024]
    AS BIGINT
    START WITH 1016
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Vouchers) del período contable correspondiente al mes 12, nivel 2, año 2024, dentro del esquema de Libro Mayor General (`GeneralLedger`). La secuencia inicia en 1060, indicando que ya existen registros previos para ese corte contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2024';
GO
