CREATE SEQUENCE [GeneralLedger].[Seq_JV_T55_L3_Y2020]
    AS BIGINT
    START WITH 118
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo BIGINT para los asientos de diario (Journal Vouchers) asociados al tipo de transacción T55, nivel 3 (L3), correspondientes al ejercicio fiscal 2020, dentro del módulo de Contabilidad General. La secuencia arranca en 118, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T55_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T55_L3_Y2020';
GO
