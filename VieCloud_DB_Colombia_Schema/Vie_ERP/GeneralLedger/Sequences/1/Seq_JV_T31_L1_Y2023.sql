CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L1_Y2023]
    AS BIGINT
    START WITH 21
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al período fiscal 2023, específicamente para el libro 1 (L1) del tipo de transacción 31 (T31) dentro del esquema de contabilidad general. La secuencia inicia en 5, no se reinicia y no usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L1_Y2023';
GO
