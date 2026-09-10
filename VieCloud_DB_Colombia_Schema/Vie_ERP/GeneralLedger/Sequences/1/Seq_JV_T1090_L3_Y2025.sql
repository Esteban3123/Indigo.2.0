CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1090_L3_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos del diario (Journal Vouchers) asociados a la tabla T1090, nivel 3 (L3), correspondientes al año fiscal 2025, dentro del esquema de Contabilidad General (GeneralLedger). Inicia en 1 con incremento unitario sin caché, garantizando unicidad en cada registro contable generado para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1090_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1090_L3_Y2025';
GO
