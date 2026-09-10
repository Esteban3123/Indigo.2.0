CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L2_Y2024]
    AS BIGINT
    START WITH 67
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos o líneas del libro mayor correspondientes al tipo de comprobante 52, nivel 2, del ejercicio fiscal 2024. Inicia en 67, lo que indica que ya fueron registradas entradas previas en dicho periodo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L2_Y2024';
GO
