CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L2_Y2022]
    AS BIGINT
    START WITH 4635
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 19, libro 2 del año 2022, en el esquema de contabilidad general. La secuencia inicia en 536, indicando registros previos ya existentes, y no permite ciclos ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L2_Y2022';
GO
