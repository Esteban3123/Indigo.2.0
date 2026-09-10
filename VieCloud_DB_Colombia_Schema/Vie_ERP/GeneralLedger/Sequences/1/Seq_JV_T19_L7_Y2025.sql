CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L7_Y2025]
    AS BIGINT
    START WITH 575
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 19, libro contable 7 del año 2025, dentro del esquema GeneralLedger. La secuencia inicia en 575 e incrementa de uno en uno, sugiriendo que ya existen 574 registros previos para esa combinación de período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L7_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L7_Y2025';
GO
