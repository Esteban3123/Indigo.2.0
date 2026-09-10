CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1078_L2_Y2023]
    AS BIGINT
    START WITH 126250
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para entradas de diario (Journal Voucher) asociadas al libro mayor general, específicamente para la tabla o lote identificado como T1078, nivel 2 (L2), correspondiente al año fiscal 2023. La secuencia inicia en 126250, lo que indica registros previos ya existentes para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L2_Y2023';
GO
