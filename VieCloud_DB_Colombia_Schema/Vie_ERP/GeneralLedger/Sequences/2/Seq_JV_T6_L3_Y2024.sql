CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L3_Y2024]
    AS BIGINT
    START WITH 1530
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 6, libro 3 del año 2024, dentro del esquema de contabilidad general. La secuencia inicia en 1530, lo que indica registros previos ya existentes para esa combinación de tipo y libro.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L3_Y2024';
GO
