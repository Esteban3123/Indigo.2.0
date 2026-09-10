CREATE SEQUENCE [GeneralLedger].[Seq_JV_T70_L3_Y2020]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor, específicamente asociados al tipo de transacción T70, nivel 3 (L3), correspondientes al año fiscal 2020.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T70_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T70_L3_Y2020';
GO
