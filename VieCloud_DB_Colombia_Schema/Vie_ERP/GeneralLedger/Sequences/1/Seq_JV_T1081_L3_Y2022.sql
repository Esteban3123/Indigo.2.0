CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1081_L3_Y2022]
    AS BIGINT
    START WITH 43
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos contables (Journal Vouchers) asociados al libro mayor (GeneralLedger), específicamente para la tabla o proceso del tipo T1081, nivel 3 (L3), correspondiente al año fiscal 2022. La secuencia inicia en 43, lo que indica que ya existían 42 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1081_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1081_L3_Y2022';
GO
