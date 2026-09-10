CREATE SEQUENCE [GeneralLedger].[Seq_JV_T29_L1_Y2024]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los registros de asientos contables (Journal Vouchers) correspondientes al tipo 29, libro 1 (L1), del año fiscal 2024, dentro del esquema de contabilidad general. La secuencia inicia en 499, sin ciclo ni caché, lo que garantiza valores irrepetibles y ordenados cronológicamente para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T29_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T29_L1_Y2024';
GO
