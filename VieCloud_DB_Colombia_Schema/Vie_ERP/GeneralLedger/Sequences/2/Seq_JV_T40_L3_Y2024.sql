CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L3_Y2024]
    AS BIGINT
    START WITH 203
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 40, nivel 3, del ejercicio fiscal 2024, dentro del esquema de Contabilidad General. La secuencia inicia en 203, lo que indica que ya existen registros previos creados antes de su definición formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L3_Y2024';
GO
