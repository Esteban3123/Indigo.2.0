CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los registros del libro contable 1 (L1) correspondientes al asiento de diario (Journal Voucher) de la tabla T1071 del año 2026, dentro del esquema de Libro Mayor General (GeneralLedger). Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de asientos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2026';
GO
