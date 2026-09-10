CREATE SEQUENCE [GeneralLedger].[Seq_JV_T50_L3_Y2021]
    AS BIGINT
    START WITH 590
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para asientos contables (Journal Vouchers) del libro mayor general, específicamente para la tabla de nivel 3 (L3) del tipo 50 (T50) correspondiente al ejercicio fiscal 2021, iniciando desde el valor 590.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L3_Y2021';
GO
