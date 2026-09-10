CREATE SEQUENCE [GeneralLedger].[Seq_JV_T30_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del tipo 30, libro 1, correspondientes al año fiscal 2026, dentro del esquema de contabilidad general (`GeneralLedger`). Inicia desde el valor 17154, indicando registros previos ya existentes, y no permite reinicio de ciclo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2026';
GO
