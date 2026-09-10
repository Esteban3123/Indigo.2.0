CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1070_L2_Y2024]
    AS BIGINT
    START WITH 13
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos contables (Journal Vouchers) asociados al libro mayor (GeneralLedger), específicamente para la tabla o partición T1070, nivel L2, correspondiente al año fiscal 2024. La secuencia inicia en 13, lo que indica que ya fueron registradas entradas previas antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L2_Y2024';
GO
