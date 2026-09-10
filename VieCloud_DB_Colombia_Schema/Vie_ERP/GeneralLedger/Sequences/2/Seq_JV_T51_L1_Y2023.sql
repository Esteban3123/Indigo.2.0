CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L1_Y2023]
    AS BIGINT
    START WITH 20
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo T51, libro L1 del año 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 20 e incrementa de uno en uno, sin caché, garantizando valores consecutivos sin saltos para registros contables de ese período fiscal específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L1_Y2023';
GO
