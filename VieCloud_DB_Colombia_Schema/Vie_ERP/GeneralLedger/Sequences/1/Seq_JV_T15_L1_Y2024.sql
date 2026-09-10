CREATE SEQUENCE [GeneralLedger].[Seq_JV_T15_L1_Y2024]
    AS BIGINT
    START WITH 109
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 15 (T15), libro contable 1 (L1) del ejercicio fiscal 2024, dentro del módulo de contabilidad general (GeneralLedger). La secuencia inicia en 97028, no es cíclica y no usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T15_L1_Y2024';
GO
