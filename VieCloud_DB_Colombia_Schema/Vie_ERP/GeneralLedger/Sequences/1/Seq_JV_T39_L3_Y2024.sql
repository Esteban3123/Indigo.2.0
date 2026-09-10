CREATE SEQUENCE [GeneralLedger].[Seq_JV_T39_L3_Y2024]
    AS BIGINT
    START WITH 1607
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales para asientos de diario (Journal Vouchers) correspondientes al tipo 39, nivel 3, del ejercicio fiscal 2024, dentro del módulo de Contabilidad General. La secuencia inicia en 1607, lo que indica registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L3_Y2024';
GO
