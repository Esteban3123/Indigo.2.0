CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L2_Y2023]
    AS BIGINT
    START WITH 14
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para registros de asientos contables (Journal Vouchers) correspondientes al Tipo 4, Libro 2, del año fiscal 2023, dentro del esquema de contabilidad general. La secuencia inicia en 359538, no cicla y no usa caché, garantizando unicidad continua.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L2_Y2023';
GO
