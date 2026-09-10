CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L3_Y2022]
    AS BIGINT
    START WITH 2137
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla de nivel 3 (L3), tipo 40 (T40), correspondiente al año fiscal 2022. La secuencia inicia en 2137, lo que indica registros previos ya existentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L3_Y2022';
GO
