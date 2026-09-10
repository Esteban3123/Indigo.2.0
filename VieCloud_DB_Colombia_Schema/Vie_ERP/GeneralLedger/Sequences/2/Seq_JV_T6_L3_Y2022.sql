CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L3_Y2022]
    AS BIGINT
    START WITH 34145
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo BIGINT para los asientos del diario contable (Journal Vouchers) correspondientes al tipo 6, nivel 3 del año 2022, en el módulo de Contabilidad General. La secuencia inició en 34145, lo que indica registros previos ya existentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L3_Y2022';
GO
