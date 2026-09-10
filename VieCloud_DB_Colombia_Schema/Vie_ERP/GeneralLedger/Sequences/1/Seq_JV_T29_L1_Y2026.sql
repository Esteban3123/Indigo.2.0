CREATE SEQUENCE [GeneralLedger].[Seq_JV_T29_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 29, libro 1 (L1) del año fiscal 2026, dentro del esquema de Libro Mayor General (`GeneralLedger`). La secuencia inicia en 542, incrementa de uno en uno y no se reinicia automáticamente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T29_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T29_L1_Y2026';
GO
