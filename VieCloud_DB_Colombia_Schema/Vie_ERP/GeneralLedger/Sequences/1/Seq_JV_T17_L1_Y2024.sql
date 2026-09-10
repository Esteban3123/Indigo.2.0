CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L1_Y2024]
    AS BIGINT
    START WITH 5334
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Vouchers) del libro contable T17, libro mayor L1, correspondiente al año fiscal 2024, dentro del esquema de contabilidad general. La secuencia inicia en 1709, lo que sugiere continuidad de registros previos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L1_Y2024';
GO
