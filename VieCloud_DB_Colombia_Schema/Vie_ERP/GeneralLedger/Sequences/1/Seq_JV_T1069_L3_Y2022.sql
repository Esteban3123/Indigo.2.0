CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1069_L3_Y2022]
    AS BIGINT
    START WITH 6
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo de transacción T1069, nivel 3 (L3), del ejercicio fiscal 2022. La secuencia inicia en 6 e incrementa de uno en uno, sin caché, garantizando orden y unicidad en los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1069_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1069_L3_Y2022';
GO
