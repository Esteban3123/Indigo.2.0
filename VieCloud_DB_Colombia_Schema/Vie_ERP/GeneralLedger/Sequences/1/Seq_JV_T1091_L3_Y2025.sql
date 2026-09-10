CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1091_L3_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor general, correspondientes al nivel 3 (L3) del tipo o tabla 1091 (T1091) para el año fiscal 2025. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en cada registro generado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1091_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1091_L3_Y2025';
GO
