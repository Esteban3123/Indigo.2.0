CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L1_Y2024]
    AS BIGINT
    START WITH 280024
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al tipo de transacción T1077, libro L1, del ejercicio fiscal 2024. La secuencia inicia en 280024 e incrementa de uno en uno, sin caché, garantizando unicidad en los registros del módulo de contabilidad general.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2024';
GO
