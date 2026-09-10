CREATE SEQUENCE [GeneralLedger].[Seq_JV_T9_L3_Y2021]
    AS BIGINT
    START WITH 1485
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 9, libro 3 del año 2021, dentro del módulo de Contabilidad General. La secuencia inicia en 1485, lo que indica registros previos ya existentes para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T9_L3_Y2021';
GO
