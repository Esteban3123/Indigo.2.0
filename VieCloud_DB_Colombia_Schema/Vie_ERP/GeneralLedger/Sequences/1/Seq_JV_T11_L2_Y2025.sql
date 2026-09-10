CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para los asientos de diario (Journal Voucher) correspondientes al tipo 11, libro/nivel 2, del ejercicio fiscal 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 38393, no se reinicia cíclicamente y no utiliza caché, garantizando unicidad estricta en la numeración de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2025';
GO
