CREATE SEQUENCE [GeneralLedger].[Seq_JV_T15_L1_Y2023]
    AS BIGINT
    START WITH 247
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos o líneas de diario contable (Journal Voucher) correspondientes al tipo 15, libro 1 del año fiscal 2023, dentro del esquema de Contabilidad General. La secuencia parte del valor 76753, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L1_Y2023';
GO
