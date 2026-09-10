CREATE SEQUENCE [GeneralLedger].[Seq_JV_T56_L1_Y2024]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción T56, libro contable L1, del ejercicio fiscal 2024, dentro del esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 4, sugiriendo que ya se registraron entradas previas antes de su creación formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T56_L1_Y2024';
GO
