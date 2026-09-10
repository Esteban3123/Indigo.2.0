CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L2_Y2022]
    AS BIGINT
    START WITH 3219
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) correspondientes al libro 2 (L2) del tipo 14 (T14) del ejercicio fiscal 2022, en el esquema de contabilidad general. Inicia desde el valor 33658, indicando registros previos ya existentes, y no se reinicia ni usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2022';
GO
