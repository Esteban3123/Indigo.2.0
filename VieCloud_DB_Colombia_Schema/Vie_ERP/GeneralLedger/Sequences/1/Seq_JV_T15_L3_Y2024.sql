CREATE SEQUENCE [GeneralLedger].[Seq_JV_T15_L3_Y2024]
    AS BIGINT
    START WITH 27
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos o entradas del libro mayor general, específicamente para el tipo de transacción 15, nivel 3, correspondientes al año 2024. Inicia en el valor 27, lo que indica que ya existen 26 registros previos generados para esa combinación de tipo, nivel y período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L3_Y2024';
GO
