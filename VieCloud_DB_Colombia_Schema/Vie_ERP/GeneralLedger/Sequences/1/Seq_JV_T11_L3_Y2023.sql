CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L3_Y2023]
    AS BIGINT
    START WITH 126565
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos o líneas de diario (Journal Vouchers) correspondientes al tipo 11, nivel 3 del año 2023 en el módulo de Contabilidad General. El valor inicial indica que ya existen registros previos creados para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2023';
GO
