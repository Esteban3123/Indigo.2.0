CREATE SEQUENCE [GeneralLedger].[Seq_JV_T10_L1_Y2020]
    AS BIGINT
    START WITH 17984
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del libro contable general, específicamente para el tipo de transacción 10, libro 1, correspondiente al año fiscal 2020. La secuencia inicia en 2 915 469, reflejando registros previos ya existentes, y no permite reinicio ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L1_Y2020';
GO
