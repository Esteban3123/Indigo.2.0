CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L2_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos de diario (Journal Voucher) correspondientes al tipo 52, nivel 2, del año fiscal 2026, dentro del esquema de Contabilidad General (`GeneralLedger`). La secuencia inicia en 1, no se reinicia cíclicamente y no usa caché, garantizando integridad en la numeración consecutiva de los comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L2_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L2_Y2026';
GO
