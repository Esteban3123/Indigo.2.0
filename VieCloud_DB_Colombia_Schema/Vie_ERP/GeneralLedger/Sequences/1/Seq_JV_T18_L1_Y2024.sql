CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L1_Y2024]
    AS BIGINT
    START WITH 10569
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) correspondientes al tipo 18, libro 1 del ejercicio fiscal 2024, dentro del esquema de Contabilidad General. La secuencia inicia en 304572, sin ciclo ni caché, garantizando unicidad en la numeración correlativa de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2024';
GO
