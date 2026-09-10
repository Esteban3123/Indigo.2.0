CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L3_Y2024]
    AS BIGINT
    START WITH 971
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 17, nivel 3 del año 2024, dentro del módulo de Contabilidad General. La secuencia inicia en 971 e incrementa de uno en uno, lo que indica que ya existían 970 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2024';
GO
