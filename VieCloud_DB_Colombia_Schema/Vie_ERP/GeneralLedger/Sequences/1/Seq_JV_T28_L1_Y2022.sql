CREATE SEQUENCE [GeneralLedger].[Seq_JV_T28_L1_Y2022]
    AS BIGINT
    START WITH 13
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos contables (Journal Vouchers) del libro mayor general, específicamente para la tabla tipo 28, libro 1, correspondiente al ejercicio fiscal 2022. La secuencia inicia en 4, no se reinicia al alcanzar el máximo y no utiliza caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T28_L1_Y2022';
GO
