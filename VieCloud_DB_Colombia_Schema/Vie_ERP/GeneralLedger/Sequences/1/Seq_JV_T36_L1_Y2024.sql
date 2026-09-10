CREATE SEQUENCE [GeneralLedger].[Seq_JV_T36_L1_Y2024]
    AS BIGINT
    START WITH 9444
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 36, libro 1 (L1) del ejercicio fiscal 2024, dentro del esquema de contabilidad general. La secuencia inicia en 52414, no se reinicia cíclicamente y no usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L1_Y2024';
GO
