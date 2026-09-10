CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1077_L3_Y2021]
    AS BIGINT
    START WITH 473476
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores únicos y consecutivos para asientos de diario (Journal Vouchers) asociados al tipo de transacción T1077, nivel 3 (L3), correspondientes al año fiscal 2021, dentro del esquema de Libro Mayor General (GeneralLedger). Inicia en 473476 e incrementa de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1077_L3_Y2021';
GO
