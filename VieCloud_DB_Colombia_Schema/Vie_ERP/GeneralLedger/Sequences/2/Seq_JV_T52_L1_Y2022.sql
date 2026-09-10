CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L1_Y2022]
    AS BIGINT
    START WITH 539
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro contable T52, libro mayor nivel 1 (L1), correspondientes al ejercicio fiscal 2022, dentro del esquema GeneralLedger. Inició en 539, lo que indica registros previos ya existentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L1_Y2022';
GO
