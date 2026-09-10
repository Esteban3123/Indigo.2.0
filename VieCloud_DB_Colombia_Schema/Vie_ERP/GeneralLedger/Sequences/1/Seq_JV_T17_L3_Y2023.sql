CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L3_Y2023]
    AS BIGINT
    START WITH 9401
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor general, específicamente para el tipo de transacción 17, nivel 3, correspondiente al ejercicio fiscal 2023. La secuencia inicia en 9401 e incrementa de uno en uno sin caché, garantizando unicidad en la numeración de comprobantes contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2023';
GO
