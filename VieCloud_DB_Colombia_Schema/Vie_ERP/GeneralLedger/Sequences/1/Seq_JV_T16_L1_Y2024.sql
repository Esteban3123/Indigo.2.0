CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L1_Y2024]
    AS BIGINT
    START WITH 417
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro 1 (L1), tipo 16 (T16), correspondientes al año fiscal 2024, dentro del esquema de Contabilidad General. La secuencia inicia en 6364, no se reinicia al alcanzar el máximo y no utiliza caché, garantizando continuidad en la numeración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2024';
GO
