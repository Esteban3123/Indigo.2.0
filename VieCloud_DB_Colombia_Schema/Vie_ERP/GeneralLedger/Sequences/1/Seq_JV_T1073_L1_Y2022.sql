CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1073_L1_Y2022]
    AS BIGINT
    START WITH 1399
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro contable 1 (L1) correspondientes al período fiscal 2022, asociados al libro mayor de la entidad o centro de costo T1073, iniciando desde el valor 1399.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L1_Y2022';
GO
