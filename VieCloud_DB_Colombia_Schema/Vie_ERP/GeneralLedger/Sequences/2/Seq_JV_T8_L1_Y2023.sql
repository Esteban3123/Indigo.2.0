CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L1_Y2023]
    AS BIGINT
    START WITH 1412
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para asientos de diario (Journal Vouchers) del libro contable tipo 8, libro 1, correspondientes al ejercicio fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 1 898 744, sugiriendo registros previos migrados o acumulados hasta ese punto.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2023';
GO
