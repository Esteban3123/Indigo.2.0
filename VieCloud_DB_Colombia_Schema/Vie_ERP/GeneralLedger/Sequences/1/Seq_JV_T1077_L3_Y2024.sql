CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L3_Y2024]
    AS BIGINT
    START WITH 42679
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al centro de costo o entidad T1077, nivel contable L3, correspondientes al ejercicio fiscal 2024. La secuencia inicia en 42679 e incrementa de uno en uno, sin caché, garantizando orden y unicidad en el módulo de Libro Mayor General.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2024';
GO
