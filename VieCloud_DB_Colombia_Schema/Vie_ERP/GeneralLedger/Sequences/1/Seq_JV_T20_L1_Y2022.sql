CREATE SEQUENCE [GeneralLedger].[Seq_JV_T20_L1_Y2022]
    AS BIGINT
    START WITH 259
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para registros del libro de comprobantes del diario (Journal Vouchers) correspondientes al tipo 20, nivel 1 del año 2022, en el esquema de contabilidad general (GeneralLedger). La secuencia inicia en 41077, lo que indica que ya existían ese número de registros previos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2022';
GO
