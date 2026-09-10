CREATE SEQUENCE [GeneralLedger].[Seq_JV_T2_L3_Y2021]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del tipo 2, nivel 3, correspondientes al año fiscal 2021, dentro del módulo de contabilidad general (GeneralLedger). La secuencia inicia en 4 e incrementa de uno en uno sin caché, garantizando correlatividad estricta en los registros contables de ese periodo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T2_L3_Y2021';
GO
