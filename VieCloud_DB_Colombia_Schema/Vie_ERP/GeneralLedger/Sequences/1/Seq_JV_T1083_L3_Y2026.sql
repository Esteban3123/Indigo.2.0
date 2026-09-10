CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1083_L3_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica que genera identificadores únicos correlativos para asientos de diario (Journal Vouchers) asociados al libro mayor (GeneralLedger), correspondientes al tipo de transacción T1083, nivel 3 (L3), para el ejercicio fiscal 2026.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L3_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L3_Y2026';
GO
