CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable tipo 8, libro 1, correspondientes al ejercicio fiscal 2026, dentro del esquema de Libro Mayor General (`GeneralLedger`). La secuencia inicia en 2,982,446, no se reinicia cíclicamente y no utiliza caché, garantizando la integridad y continuidad de la numeración de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2026';
GO
