CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L3_Y2022]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo BIGINT para asientos de diario (Journal Voucher) del libro mayor general, correspondientes al tipo 31, nivel 3 y ejercicio fiscal 2022. La secuencia inicia en 9, indicando que ya existen registros previos creados antes de su formalización.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L3_Y2022';
GO
