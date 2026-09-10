CREATE SEQUENCE [GeneralLedger].[Seq_JV_T53_L1_Y2020]
    AS BIGINT
    START WITH 10
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo `bigint` para los asientos del diario contable (Journal Voucher) correspondientes al tipo de transacción T53, libro contable L1, del ejercicio fiscal 2020, dentro del esquema `GeneralLedger`. La secuencia inicia en 2, no se reinicia y no usa caché, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L1_Y2020';
GO
