CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1087_L1_Y2024]
    AS BIGINT
    START WITH 109
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos del libro de diario (Journal Voucher) correspondientes al tipo de transacción T1087, libro L1, del año fiscal 2024. La secuencia inicia en 109, indicando que ya existen registros previos para ese periodo contable en el módulo de Contabilidad General.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1087_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1087_L1_Y2024';
GO
