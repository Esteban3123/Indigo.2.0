CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1075_L3_Y2024]
    AS BIGINT
    START WITH 16
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor, específicamente para la entidad T1075, nivel contable L3, correspondientes al año fiscal 2024. La secuencia inicia en 16, lo que indica que ya se registraron valores previos antes de su definición actual.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L3_Y2024';
GO
