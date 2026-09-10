CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1_L3_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los asientos o líneas de diario (Journal Voucher) correspondientes al tipo 1, nivel 3 del año 2025, dentro del esquema de Contabilidad General. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período y clasificación específica.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1_L3_Y2025';
GO
