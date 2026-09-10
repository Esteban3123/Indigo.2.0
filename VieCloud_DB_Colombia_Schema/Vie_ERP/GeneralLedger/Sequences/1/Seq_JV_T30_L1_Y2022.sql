CREATE SEQUENCE [GeneralLedger].[Seq_JV_T30_L1_Y2022]
    AS BIGINT
    START WITH 11
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro contable tipo 30, nivel 1, correspondientes al año fiscal 2022, dentro del esquema GeneralLedger. La secuencia inicia en 10189 y no se reinicia ni usa caché, garantizando integridad en la numeración de registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2022';
GO
