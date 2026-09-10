CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L3_Y2025]
    AS BIGINT
    START WITH 68
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 7, lote 3 del año 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 68, lo que indica que ya existen 67 registros previos para esa combinación de tipo y lote.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L3_Y2025';
GO
