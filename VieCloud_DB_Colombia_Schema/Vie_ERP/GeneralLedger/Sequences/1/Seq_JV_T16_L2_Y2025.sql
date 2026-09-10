CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Vouchers) correspondientes al tipo 16, nivel 2, del año fiscal 2025, dentro del esquema de contabilidad general. La secuencia inicia en 7180 e incrementa de uno en uno sin ciclo ni caché, garantizando unicidad en los registros contables generados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L2_Y2025';
GO
