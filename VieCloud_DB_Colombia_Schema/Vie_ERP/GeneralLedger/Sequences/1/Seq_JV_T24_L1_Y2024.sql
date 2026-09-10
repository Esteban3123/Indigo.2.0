CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L1_Y2024]
    AS BIGINT
    START WITH 1153
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos de diario (Journal Vouchers) del libro contable T24, nivel L1, correspondientes al año fiscal 2024. La secuencia inicia en 2499, incrementa de uno en uno y no rota al alcanzar el máximo, garantizando unicidad irrestricta en el esquema `GeneralLedger`.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2024';
GO
