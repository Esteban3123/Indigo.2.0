CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L1_Y2021]
    AS BIGINT
    START WITH 7727
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 25, lote 1 del año 2021, dentro del esquema de contabilidad general. La secuencia inicia en 47, incrementa de uno en uno y no permite ciclos, lo que garantiza valores irrepetibles para ese período contable específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L1_Y2021';
GO
