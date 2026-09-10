CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L1_Y2023]
    AS BIGINT
    START WITH 126565
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para asientos contables (Journal Vouchers) correspondientes al tipo 11, libro 1 del año 2023, dentro del esquema de contabilidad general. La secuencia inicia en 21742, no se reinicia al alcanzar el máximo y no utiliza caché, garantizando continuidad y unicidad en la numeración de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L1_Y2023';
GO
