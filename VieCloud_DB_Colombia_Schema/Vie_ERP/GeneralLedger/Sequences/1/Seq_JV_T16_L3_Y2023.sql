CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L3_Y2023]
    AS BIGINT
    START WITH 530
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al tipo 16, nivel 3 del año 2023 en el módulo de contabilidad general. La secuencia inicia en 530, incrementa de uno en uno, sin caché, lo que garantiza valores consecutivos sin saltos para ese período y clasificación contable específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2023';
GO
