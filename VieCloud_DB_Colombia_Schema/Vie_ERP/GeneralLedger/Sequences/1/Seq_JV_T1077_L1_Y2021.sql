CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L1_Y2021]
    AS BIGINT
    START WITH 473476
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Voucher) del libro contable 1 (L1) correspondiente al año fiscal 2021, asociados al tipo de transacción T1077 en el esquema de contabilidad general. El valor inicial 473476 sugiere continuidad desde registros previos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2021';
GO
