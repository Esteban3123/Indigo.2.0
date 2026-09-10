CREATE SEQUENCE [GeneralLedger].[Seq_JV_T32_L7_Y2025]
    AS BIGINT
    START WITH 999
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos o comprobantes del libro diario (Journal Vouchers) correspondientes al tipo 32, libro 7 y ejercicio fiscal 2025, dentro del módulo de Contabilidad General. Inicia en 999 e incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L7_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L7_Y2025';
GO
