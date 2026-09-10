CREATE SEQUENCE [GeneralLedger].[Seq_JV_T33_L1_Y2023]
    AS BIGINT
    START WITH 1617
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del libro contable (`T33`, nivel `L1`) correspondientes al ejercicio fiscal 2023, dentro del esquema `GeneralLedger`. La secuencia inicia en 3248, refleja registros previos ya existentes, y no recicla valores al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2023';
GO
