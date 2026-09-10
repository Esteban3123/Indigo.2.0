CREATE SEQUENCE [GeneralLedger].[Seq_JV_T49_L1_Y2020]
    AS BIGINT
    START WITH 121
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos del diario contable (Journal Voucher) correspondientes al tipo de transacción 49, libro 1 (L1) del año fiscal 2020, iniciando desde el valor 121 con incremento de 1 sin caché, en el esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T49_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T49_L1_Y2020';
GO
