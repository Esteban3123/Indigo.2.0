CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L3_Y2021]
    AS BIGINT
    START WITH 4550
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos contables (Journal Vouchers) del libro mayor general, específicamente para la transacción T24, nivel L3, correspondiente al año 2021. La secuencia inicia en 4550, lo que indica que ya existían registros previos al crearla.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L3_Y2021';
GO
