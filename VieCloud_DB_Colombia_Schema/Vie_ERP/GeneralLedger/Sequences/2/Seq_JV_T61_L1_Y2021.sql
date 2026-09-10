CREATE SEQUENCE [GeneralLedger].[Seq_JV_T61_L1_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores correlativos para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo de comprobante T61, libro L1, correspondiente al ejercicio fiscal 2021. Inicia en 1 con incrementos de 1 sin caché, garantizando unicidad en la numeración de registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T61_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T61_L1_Y2021';
GO
