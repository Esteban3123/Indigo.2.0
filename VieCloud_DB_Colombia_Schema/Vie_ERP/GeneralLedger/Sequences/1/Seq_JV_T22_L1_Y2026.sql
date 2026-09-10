CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia que genera identificadores únicos correlativos de tipo bigint para asientos contables (Journal Vouchers) correspondientes al tipo de transacción 22, libro 1 (L1), del ejercicio fiscal 2026, dentro del esquema de Libro Mayor General (GeneralLedger). Inicia en 16353 sin ciclo ni caché, garantizando unicidad en la numeración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2026';
GO
