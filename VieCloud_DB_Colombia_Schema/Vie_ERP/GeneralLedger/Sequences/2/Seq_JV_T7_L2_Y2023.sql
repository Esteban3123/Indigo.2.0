CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L2_Y2023]
    AS BIGINT
    START WITH 11678
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo bigint que genera identificadores únicos y consecutivos para asientos de diario (Journal Vouchers) correspondientes al tipo 7, libro 2 del año 2023, dentro del esquema de contabilidad general. Inicia en 2630 e incrementa de uno en uno sin ciclo ni caché, garantizando unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L2_Y2023';
GO
