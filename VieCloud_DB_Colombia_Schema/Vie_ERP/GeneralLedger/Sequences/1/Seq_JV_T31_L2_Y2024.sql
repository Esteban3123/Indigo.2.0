CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L2_Y2024]
    AS BIGINT
    START WITH 3
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para asientos contables del libro mayor (Journal Vouchers), correspondientes al tipo 31, nivel 2, del ejercicio fiscal 2024. La secuencia inicia en 5, incrementa de uno en uno y no se recicla, garantizando unicidad en los registros del esquema `GeneralLedger`.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2024';
GO
