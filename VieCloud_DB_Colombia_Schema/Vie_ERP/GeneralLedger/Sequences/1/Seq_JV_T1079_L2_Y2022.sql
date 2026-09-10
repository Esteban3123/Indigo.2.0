CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1079_L2_Y2022]
    AS BIGINT
    START WITH 370
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al tipo de transacción T1079, nivel 2 (L2), correspondientes al ejercicio fiscal 2022. La secuencia inicia en 370, lo que indica registros previos ya generados para ese período contable dentro del esquema de Libro Mayor General.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1079_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1079_L2_Y2022';
GO
