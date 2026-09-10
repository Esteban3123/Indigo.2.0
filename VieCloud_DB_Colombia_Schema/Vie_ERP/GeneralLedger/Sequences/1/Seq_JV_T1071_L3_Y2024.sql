CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L3_Y2024]
    AS BIGINT
    START WITH 45
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor, específicamente para la tabla o lote T1071, nivel 3 (L3), correspondiente al ejercicio fiscal 2024. La secuencia inicia en 45, lo que indica que ya se pre-asignaron o registraron 44 entradas previas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L3_Y2024';
GO
