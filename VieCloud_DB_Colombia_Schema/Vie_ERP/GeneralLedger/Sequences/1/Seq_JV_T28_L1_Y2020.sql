CREATE SEQUENCE [GeneralLedger].[Seq_JV_T28_L1_Y2020]
    AS BIGINT
    START WITH 6
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos del libro diario (Journal Voucher) correspondientes al período fiscal 2020, dentro del esquema de Contabilidad General. La secuencia inicia en 2, no se reinicia cíclicamente y está asociada al libro 1 (L1) del tipo de transacción 28 (T28).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2020';
GO
