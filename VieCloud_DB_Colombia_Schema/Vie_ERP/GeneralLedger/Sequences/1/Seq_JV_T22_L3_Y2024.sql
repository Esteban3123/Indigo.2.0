CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L3_Y2024]
    AS BIGINT
    START WITH 19
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 22, nivel 3 del año 2024, almacenados en el esquema GeneralLedger. La secuencia inicia en 19, lo que indica que ya existen 18 registros previos creados antes de su definición formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L3_Y2024';
GO
