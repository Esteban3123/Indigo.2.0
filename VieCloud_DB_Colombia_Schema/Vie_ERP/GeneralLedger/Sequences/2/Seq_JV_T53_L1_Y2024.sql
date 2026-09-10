CREATE SEQUENCE [GeneralLedger].[Seq_JV_T53_L1_Y2024]
    AS BIGINT
    START WITH 7
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos o líneas de diario (Journal Voucher) correspondientes al libro 1 (L1) del tipo de transacción 53 (T53) durante el año fiscal 2024, dentro del esquema de libro mayor general (GeneralLedger).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L1_Y2024';
GO
