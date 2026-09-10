CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L2_Y2021]
    AS BIGINT
    START WITH 10549
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos del diario contable (Journal Voucher) correspondientes al tipo 12, nivel 2 del año fiscal 2021, dentro del esquema GeneralLedger. La secuencia inicia en 630 y no se reinicia ni usa caché, garantizando valores únicos y sin repetición.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2021';
GO
