CREATE SEQUENCE [GeneralLedger].[Seq_JV_T2_L3_Y2020]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos contables (Journal Vouchers) correspondientes al tipo 2, libro 3 del año 2020, dentro del esquema de Contabilidad General. La secuencia inicia en 4, indicando que ya existen registros previos creados antes de su uso formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2020';
GO
