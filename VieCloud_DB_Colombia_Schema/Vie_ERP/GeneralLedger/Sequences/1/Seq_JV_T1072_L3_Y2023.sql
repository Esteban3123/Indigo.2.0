CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1072_L3_Y2023]
    AS BIGINT
    START WITH 47
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al libro mayor (GeneralLedger), asociados a la tabla T1072, nivel 3 (L3), del ejercicio fiscal 2023. Inicia en 47 e incrementa de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L3_Y2023';
GO
