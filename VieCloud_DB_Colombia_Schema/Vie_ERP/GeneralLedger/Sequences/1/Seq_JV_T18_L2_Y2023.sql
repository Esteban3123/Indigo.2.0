CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L2_Y2023]
    AS BIGINT
    START WITH 22472
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al tipo 18, nivel 2, del año fiscal 2023, dentro del esquema de contabilidad general. La secuencia inicia en 251898, no recicla valores y no utiliza caché, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2023';
GO
