CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L3_Y2024]
    AS BIGINT
    START WITH 481
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos o líneas de diario contable (Journal Voucher) correspondientes al libro o nivel 3 del tipo 14, ejercicio fiscal 2024, dentro del esquema de Libro Mayor (GeneralLedger). La secuencia inicia en 481, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2024';
GO
