CREATE SEQUENCE [GeneralLedger].[Seq_JV_T41_L1_Y2024]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos o líneas de diario (Journal Voucher) del libro 1 (L1), tipo 41 (T41), correspondientes al año fiscal 2024, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 30617, no es cíclica y no usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T41_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T41_L1_Y2024';
GO
