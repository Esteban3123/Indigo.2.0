CREATE SEQUENCE [GeneralLedger].[Seq_JV_T67_L1_Y2020]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores únicos e incrementales para asientos de diario (Journal Vouchers) correspondientes al tipo/transacción 67, libro 1 (L1) del año fiscal 2020, dentro del esquema de Contabilidad General (GeneralLedger).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T67_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T67_L1_Y2020';
GO
