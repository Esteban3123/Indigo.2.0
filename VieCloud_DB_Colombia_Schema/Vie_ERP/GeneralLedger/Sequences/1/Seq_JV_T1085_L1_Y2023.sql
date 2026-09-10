CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1085_L1_Y2023]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al libro 1 (L1) del tipo de transacción T1085 del año 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 4, incrementa de uno en uno y no utiliza caché, garantizando valores consecutivos sin saltos en el registro contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1085_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1085_L1_Y2023';
GO
