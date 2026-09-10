CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L3_Y2023]
    AS BIGINT
    START WITH 1221
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la transacción T24, libro 3 (L3), correspondiente al año fiscal 2023. La secuencia inicia en 1221, lo que sugiere continuidad desde registros previos del mismo período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2023';
GO
