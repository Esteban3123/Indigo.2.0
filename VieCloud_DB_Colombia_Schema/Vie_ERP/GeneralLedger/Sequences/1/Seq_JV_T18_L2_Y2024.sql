CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L2_Y2024]
    AS BIGINT
    START WITH 1777
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos contables (Journal Vouchers) correspondientes al tipo 18, libro 2 del año 2024, dentro del esquema `GeneralLedger`. La secuencia inicia en 291751, lo que indica registros previos ya existentes para ese período contable, y no permite ciclo ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2024';
GO
