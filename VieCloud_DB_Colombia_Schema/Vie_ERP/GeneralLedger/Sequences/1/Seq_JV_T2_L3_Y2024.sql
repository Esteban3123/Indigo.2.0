CREATE SEQUENCE [GeneralLedger].[Seq_JV_T2_L3_Y2024]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 2, libro 3 y ejercicio fiscal 2024, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 1, incrementa de uno en uno sin caché, garantizando unicidad en la numeración de cada comprobante contable del período indicado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2024';
GO
