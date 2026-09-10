CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1072_L2_Y2023]
    AS BIGINT
    START WITH 47
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor, específicamente para la tabla o lote identificado con código T1072, nivel L2, correspondiente al año fiscal 2023. La secuencia inicia en 47, lo que indica que ya se generaron registros previos antes de su definición o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L2_Y2023';
GO
