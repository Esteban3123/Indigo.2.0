CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L3_Y2024]
    AS BIGINT
    START WITH 12
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos o vouchers del libro mayor general, específicamente para el tipo de transacción T51, nivel 3 (L3), correspondientes al ejercicio fiscal 2024. La secuencia inicia en 12, lo que indica que ya fueron registradas entradas previas antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2024';
GO
