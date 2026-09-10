CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L3_Y2021]
    AS BIGINT
    START WITH 1105
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para asientos de diario (Journal Vouchers) del tipo 8, libro 3, correspondientes al ejercicio fiscal 2021, dentro del esquema de contabilidad general. La secuencia inicia en 1105, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L3_Y2021';
GO
