CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L3_Y2023]
    AS BIGINT
    START WITH 14
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al período fiscal 2023, específicamente para el cuarto tipo de transacción (T4) y tercer nivel contable (L3) del esquema GeneralLedger. La secuencia inicia en 14, lo que indica que ya fueron registradas entradas previas antes de su creación o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L3_Y2023';
GO
