CREATE SEQUENCE [GeneralLedger].[Seq_JV_T13_L3_Y2024]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) correspondientes al tipo 13, libro 3 del año 2024, dentro del esquema de Contabilidad General. Inicia en 1 con incremento de 1 sin caché, garantizando unicidad en cada registro generado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T13_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T13_L3_Y2024';
GO
