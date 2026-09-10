CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos contables (Journal Vouchers) correspondientes al **Tipo 4, Libro 2, año 2025** en el módulo de contabilidad general (`GeneralLedger`). Inicia desde 522158, sin ciclo ni caché, garantizando unicidad continua para los registros de dicho período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L2_Y2025';
GO
