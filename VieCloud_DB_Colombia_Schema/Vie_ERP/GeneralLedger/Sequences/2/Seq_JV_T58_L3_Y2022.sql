CREATE SEQUENCE [GeneralLedger].[Seq_JV_T58_L3_Y2022]
    AS BIGINT
    START WITH 215
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos de diario (Journal Voucher) del libro mayor general, específicamente para la tabla T58, nivel 3 (L3), correspondiente al ejercicio fiscal 2022. La secuencia parte desde el valor 215, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L3_Y2022';
GO
