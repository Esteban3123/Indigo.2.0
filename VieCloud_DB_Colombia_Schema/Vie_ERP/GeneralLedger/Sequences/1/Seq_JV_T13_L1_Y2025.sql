CREATE SEQUENCE [GeneralLedger].[Seq_JV_T13_L1_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable número 1 (L1), período o tipo 13 (T13), correspondientes al año 2025, dentro del esquema `GeneralLedger`. La secuencia inicia en 10615 sin ciclo ni caché, garantizando unicidad en la numeración de cada comprobante contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T13_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T13_L1_Y2025';
GO
