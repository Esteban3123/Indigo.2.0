CREATE SEQUENCE [GeneralLedger].[Seq_JV_T54_L1_Y2023]
    AS BIGINT
    START WITH 13
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los registros del libro 1 (L1) del tipo de comprobante 54 (T54) correspondientes al año fiscal 2023, dentro del módulo de contabilidad general (GeneralLedger). La secuencia inicia en 13, lo que indica que ya fueron creados registros previos antes de su definición o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L1_Y2023';
GO
