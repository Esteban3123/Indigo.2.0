CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L1_Y2021]
    AS BIGINT
    START WITH 3055
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable 1 (L1), tipo 14 (T14), correspondientes al ejercicio fiscal 2021, dentro del esquema `GeneralLedger`. La secuencia inicia en 25067, indicando registros previos ya existentes, y no permite reinicio ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2021';
GO
