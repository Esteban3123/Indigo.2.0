CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1075_L3_Y2021]
    AS BIGINT
    START WITH 99
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor general, específicamente para la tabla o proceso del código T1075, nivel 3 (L3), correspondiente al año fiscal 2021. La secuencia inicia en 99 e incrementa de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L3_Y2021';
GO
