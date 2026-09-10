CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L2_Y2020]
    AS BIGINT
    START WITH 2302
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para asientos de diario (Journal Voucher) correspondientes al tipo 25, nivel 2 del año 2020 en el esquema de Contabilidad General. La secuencia inicia en 35, indicando que ya existen 34 registros previos generados para esa combinación de tipo y nivel.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2020';
GO
