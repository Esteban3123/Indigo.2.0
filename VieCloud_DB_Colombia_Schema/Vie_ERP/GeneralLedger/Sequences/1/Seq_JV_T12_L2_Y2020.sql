CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L2_Y2020]
    AS BIGINT
    START WITH 3309
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo 12, nivel 2 del año 2020, en el esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 449, lo que indica registros previos ya existentes para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2020';
GO
