CREATE SEQUENCE [GeneralLedger].[Seq_JV_T36_L3_Y2024]
    AS BIGINT
    START WITH 1141
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la combinación de tipo de transacción 36, nivel 3, correspondiente al año fiscal 2024. La secuencia inicia en 1141, lo que indica registros previos ya generados para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L3_Y2024';
GO
