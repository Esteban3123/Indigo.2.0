CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L2_Y2022]
    AS BIGINT
    START WITH 117570
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 11, libro 2 del año 2022, en el esquema de Contabilidad General. La secuencia inicia en 16601, no cicla y no usa caché, garantizando continuidad y unicidad en la numeración de dichos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2022';
GO
