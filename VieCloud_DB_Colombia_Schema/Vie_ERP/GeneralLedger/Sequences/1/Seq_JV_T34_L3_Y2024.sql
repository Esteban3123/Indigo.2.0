CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L3_Y2024]
    AS BIGINT
    START WITH 97
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos o vouchers del libro mayor general, específicamente asociados al tipo de transacción 34, nivel 3, del año fiscal 2024. La secuencia inicia en 97, lo que indica que ya existían 96 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2024';
GO
