CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos contables (Journal Vouchers) del tipo 19, libro 2 (L2), correspondientes al ejercicio fiscal 2025, dentro del esquema de contabilidad general. La secuencia inicia en 894, sin ciclo ni caché, garantizando unicidad en la numeración de dichos comprobantes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L2_Y2025';
GO
