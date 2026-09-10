CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L1_Y2023]
    AS BIGINT
    START WITH 506306
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos del libro mayor (Journal Vouchers) correspondientes al tipo de transacción T1077, libro 1 (L1), del año fiscal 2023, iniciando desde el valor 506306.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L1_Y2023';
GO
