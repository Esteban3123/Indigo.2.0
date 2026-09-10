CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L2_Y2024]
    AS BIGINT
    START WITH 45
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor (GeneralLedger), específicamente para la tabla o proceso de nivel 2 (L2) del tipo 1071 correspondiente al ejercicio fiscal 2024. La secuencia inicia en 45, lo que indica que ya se registraron entradas previas antes de su creación o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L2_Y2024';
GO
