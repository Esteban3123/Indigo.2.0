CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L1_Y2020]
    AS BIGINT
    START WITH 2302
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos del diario contable (Journal Voucher) correspondientes al tipo de transacción 25, libro 1 (L1) del año 2020, dentro del esquema de Libro Mayor General. La secuencia inicia en 34, lo que indica que ya fueron registradas entradas previas en ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L1_Y2020';
GO
