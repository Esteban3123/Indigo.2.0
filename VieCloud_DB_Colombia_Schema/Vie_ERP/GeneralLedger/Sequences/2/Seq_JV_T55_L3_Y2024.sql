CREATE SEQUENCE [GeneralLedger].[Seq_JV_T55_L3_Y2024]
    AS BIGINT
    START WITH 45
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo de transacción T55, nivel contable L3, correspondientes al ejercicio fiscal 2024. La secuencia inicia en 45, indicando que ya fueron registrados asientos previos antes de su creación o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T55_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T55_L3_Y2024';
GO
