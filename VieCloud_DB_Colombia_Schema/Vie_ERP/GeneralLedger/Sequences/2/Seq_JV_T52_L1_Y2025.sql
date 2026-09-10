CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L1_Y2025]
    AS BIGINT
    START WITH 16
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos o líneas de diario (Journal Voucher) correspondientes al tipo 52, libro 1 (L1) del año fiscal 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 16, indicando que ya existen registros previos creados antes de su definición formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2025';
GO
