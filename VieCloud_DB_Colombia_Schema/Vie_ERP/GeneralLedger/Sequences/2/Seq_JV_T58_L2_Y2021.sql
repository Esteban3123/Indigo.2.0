CREATE SEQUENCE [GeneralLedger].[Seq_JV_T58_L2_Y2021]
    AS BIGINT
    START WITH 239
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al libro mayor (GeneralLedger), específicamente para la tabla T58, nivel L2, del ejercicio fiscal 2021. La secuencia inicia en 239, lo que indica que ya existen registros previos para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2021';
GO
