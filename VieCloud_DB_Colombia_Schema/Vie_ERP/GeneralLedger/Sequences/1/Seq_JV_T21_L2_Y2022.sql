CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L2_Y2022]
    AS BIGINT
    START WITH 8347
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del tipo 21, nivel 2, correspondientes al ejercicio fiscal 2022, dentro del esquema de contabilidad general. La secuencia inicia en 192616, lo que sugiere continuidad respecto a registros previos del mismo período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2022';
GO
