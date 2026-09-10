CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1072_L1_Y2020]
    AS BIGINT
    START WITH 22
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) asociados al libro 1 (L1) de la transacción o tipo de documento T1072, correspondientes al ejercicio fiscal 2020, dentro del esquema de contabilidad general (GeneralLedger).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L1_Y2020';
GO
