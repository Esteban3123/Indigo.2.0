CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1083_L3_Y2024]
    AS BIGINT
    START WITH 10
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores correlativos para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla o lote identificado con código T1083, nivel 3 (L3), correspondiente al ejercicio fiscal 2024. Inicia en 10 e incrementa de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L3_Y2024';
GO
