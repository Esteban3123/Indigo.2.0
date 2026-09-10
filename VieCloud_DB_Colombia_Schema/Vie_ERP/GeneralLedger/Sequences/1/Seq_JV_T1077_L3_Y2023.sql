CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L3_Y2023]
    AS BIGINT
    START WITH 506306
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor, específicamente para la tabla o proceso del período fiscal 2023, nivel 3, correspondiente al código de transacción T1077. La secuencia inicia en 506306, lo que sugiere continuidad respecto a registros previos ya existentes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2023';
GO
