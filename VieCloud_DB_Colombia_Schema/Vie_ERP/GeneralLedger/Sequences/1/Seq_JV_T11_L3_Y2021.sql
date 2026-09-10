CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L3_Y2021]
    AS BIGINT
    START WITH 126084
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para las entradas del libro diario (Journal Voucher) correspondientes al tipo 11, nivel 3 del año 2021, en el esquema de contabilidad general. El valor inicial indica que ya existen más de 126 000 registros previos para esa combinación de tipo y período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L3_Y2021';
GO
