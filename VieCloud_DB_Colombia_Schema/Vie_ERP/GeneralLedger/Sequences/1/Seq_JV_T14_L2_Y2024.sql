CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L2_Y2024]
    AS BIGINT
    START WITH 481
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos contables (Journal Vouchers) correspondientes al tipo 14, libro 2 del año 2024, dentro del esquema de libro mayor general (`GeneralLedger`). La secuencia inició en 52953, no es cíclica y no usa caché, garantizando unicidad en la numeración de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2024';
GO
