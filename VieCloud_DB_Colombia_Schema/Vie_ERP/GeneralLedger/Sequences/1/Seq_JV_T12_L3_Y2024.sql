CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L3_Y2024]
    AS BIGINT
    START WITH 1018
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para comprobantes de diario (Journal Vouchers) correspondientes al período contable del año 2024, tabla 12, nivel 3, dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 1018, lo que indica que ya se han registrado entradas previas en ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L3_Y2024';
GO
