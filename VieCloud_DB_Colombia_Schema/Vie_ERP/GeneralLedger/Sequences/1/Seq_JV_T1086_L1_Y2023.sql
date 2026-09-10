CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1086_L1_Y2023]
    AS BIGINT
    START WITH 29
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Voucher) del libro mayor general, específicamente para la tabla o lote identificado como T1086, libro 1 (L1), correspondiente al año fiscal 2023. La secuencia inicia en 29, lo que indica que ya se habían registrado entradas previas al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1086_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1086_L1_Y2023';
GO
