CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L1_Y2021]
    AS BIGINT
    START WITH 10881
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para registros de asientos contables (Journal Vouchers) correspondientes al tipo 7, libro 1 del año 2021, dentro del esquema de contabilidad general. La secuencia inicia en 1712, no se reinicia al alcanzar el máximo y no usa caché, garantizando unicidad en la numeración de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2021';
GO
