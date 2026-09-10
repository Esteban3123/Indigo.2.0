CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L3_Y2024]
    AS BIGINT
    START WITH 335
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 21, libro/nivel 3 del año fiscal 2024, dentro del módulo de Contabilidad General. La secuencia inicia en 335, lo que indica que ya existen registros previos para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L3_Y2024';
GO
