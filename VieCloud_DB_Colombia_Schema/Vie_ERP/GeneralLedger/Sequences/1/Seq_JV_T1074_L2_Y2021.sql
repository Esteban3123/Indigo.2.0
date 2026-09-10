CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1074_L2_Y2021]
    AS BIGINT
    START WITH 42
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo de transacción T1074, nivel 2 (L2), del año fiscal 2021, dentro del módulo de Contabilidad General. La secuencia inició en el valor 42, lo que indica registros previos ya generados antes de su creación o migración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2021';
GO
