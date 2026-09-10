CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L1_Y2022]
    AS BIGINT
    START WITH 14
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos o líneas de diario contable (Journal Voucher) correspondientes al Tipo 4, Libro 1 del año 2022, dentro del esquema de Mayor General (`GeneralLedger`). La secuencia inicia en 516728, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L1_Y2022';
GO
