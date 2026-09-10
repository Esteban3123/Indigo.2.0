CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L2_Y2023]
    AS BIGINT
    START WITH 4162
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos contables (Journal Vouchers) del tipo 21, libro 2 (L2), correspondientes al año fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 296628, sin ciclo ni caché, garantizando unicidad en la numeración correlativa de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2023';
GO
