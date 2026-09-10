CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L3_Y2022]
    AS BIGINT
    START WITH 3593
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la combinación de período T24, nivel contable L3 y el ejercicio fiscal 2022. La secuencia inicia en 3593, lo que sugiere continuidad con registros previos ya generados para ese año.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2022';
GO
