CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1074_L2_Y2020]
    AS BIGINT
    START WITH 18
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al tipo de transacción T1074, nivel 2 (L2), correspondientes al año fiscal 2020, dentro del esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 18, lo que indica registros previos ya generados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L2_Y2020';
GO
