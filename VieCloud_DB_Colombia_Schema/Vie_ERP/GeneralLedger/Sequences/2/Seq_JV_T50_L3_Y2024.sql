CREATE SEQUENCE [GeneralLedger].[Seq_JV_T50_L3_Y2024]
    AS BIGINT
    START WITH 53
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al nivel 3 (L3) del tipo/libro 50 (T50) del ejercicio fiscal 2024, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 53, indicando que ya existen 52 registros previos generados para esa combinación de período y clasificación contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L3_Y2024';
GO
