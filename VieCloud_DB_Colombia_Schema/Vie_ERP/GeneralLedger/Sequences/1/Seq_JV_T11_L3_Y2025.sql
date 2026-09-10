CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L3_Y2025]
    AS BIGINT
    START WITH 28
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos o líneas de diario (Journal Voucher) correspondientes al tipo 11, nivel 3 del año 2025, dentro del módulo de contabilidad general. La secuencia inicia en 28, lo que indica que ya existen registros previos para esa combinación de tipo y nivel en el ejercicio fiscal 2025.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2025';
GO
