CREATE SEQUENCE [GeneralLedger].[Seq_JV_T56_L1_Y2023]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para comprobantes de diario (Journal Vouchers) correspondientes al libro 1 (L1) de la tabla o período 56 del año 2023, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 1, incrementa de uno en uno sin caché, garantizando unicidad en los registros contables de ese período específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L1_Y2023';
GO
