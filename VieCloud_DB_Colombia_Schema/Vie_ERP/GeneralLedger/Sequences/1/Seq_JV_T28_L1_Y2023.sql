CREATE SEQUENCE [GeneralLedger].[Seq_JV_T28_L1_Y2023]
    AS BIGINT
    START WITH 13
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para registros del libro diario (Journal Voucher) correspondientes al tipo 28, libro 1 (L1) del año fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 5, no se reinicia al alcanzar el máximo y no usa caché, garantizando continuidad en la numeración de asientos contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2023';
GO
