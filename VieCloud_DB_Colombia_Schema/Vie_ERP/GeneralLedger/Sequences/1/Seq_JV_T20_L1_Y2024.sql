CREATE SEQUENCE [GeneralLedger].[Seq_JV_T20_L1_Y2024]
    AS BIGINT
    START WITH 303
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos de diario (Journal Vouchers) del libro contable tipo 20, libro 1, correspondientes al ejercicio fiscal 2024, dentro del esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 62703, sugiriendo registros previos ya creados para ese periodo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2024';
GO
