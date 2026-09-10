CREATE SEQUENCE [GeneralLedger].[Seq_JV_T46_L3_Y2023]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos contables (Journal Vouchers) correspondientes al tipo 46, nivel 3 del año 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 1 y se incrementa de uno en uno sin caché, garantizando orden y unicidad en los registros del libro mayor para ese período y clasificación específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T46_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T46_L3_Y2023';
GO
