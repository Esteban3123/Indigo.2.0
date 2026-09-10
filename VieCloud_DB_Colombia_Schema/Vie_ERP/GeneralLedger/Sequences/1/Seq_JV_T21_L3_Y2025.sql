CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L3_Y2025]
    AS BIGINT
    START WITH 10
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al tipo 21, nivel 3 del año 2025, dentro del esquema de contabilidad general. La secuencia inicia en 10 e incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período y clasificación específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L3_Y2025';
GO
