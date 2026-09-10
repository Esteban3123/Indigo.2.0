CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L1_Y2025]
    AS BIGINT
    START WITH 60
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos de diario (Journal Voucher) del tipo 11, libro 1, correspondientes al ejercicio fiscal 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 38938, no se reinicia y no utiliza caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L1_Y2025';
GO
