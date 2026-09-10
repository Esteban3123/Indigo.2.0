CREATE SEQUENCE [GeneralLedger].[Seq_JV_T10_L3_Y2021]
    AS BIGINT
    START WITH 51841
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo BIGINT para registros de asientos contables (Journal Vouchers) correspondientes al tipo 10, nivel 3 del año 2021, en el esquema de Libro Mayor General. El valor inicial 51841 sugiere continuidad con registros preexistentes de ese período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L3_Y2021';
GO
