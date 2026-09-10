CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L3_Y2021]
    AS BIGINT
    START WITH 8
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo de transacción T51, nivel 3 (L3), correspondientes al año fiscal 2021. La secuencia inicia en 8, lo que indica que ya fueron registrados valores previos antes de su creación formal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2021';
GO
