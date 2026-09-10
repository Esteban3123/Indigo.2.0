CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1074_L2_Y2023]
    AS BIGINT
    START WITH 50
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al tipo de transacción T1074, nivel 2 (L2), correspondientes al año fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 50 e incrementa de uno en uno, lo que sugiere que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2023';
GO
