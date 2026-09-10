CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L3_Y2021]
    AS BIGINT
    START WITH 25
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos contables (Journal Vouchers) correspondientes al Tipo 4, Libro 3 del año fiscal 2021, en el esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 25, lo que indica que ya existían 24 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L3_Y2021';
GO
