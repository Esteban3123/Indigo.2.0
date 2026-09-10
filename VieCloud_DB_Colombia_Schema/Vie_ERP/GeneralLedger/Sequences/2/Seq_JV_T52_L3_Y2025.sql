CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L3_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 52, libro/nivel 3 (L3), del año fiscal 2025, dentro del esquema de Contabilidad General (GeneralLedger). La secuencia inicia en 1, incrementa de uno en uno y no utiliza caché, garantizando consecutivos sin saltos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2025';
GO
