CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L1_Y2021]
    AS BIGINT
    START WITH 70
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para asientos o líneas de diario contable (journal vouchers) correspondientes al tipo 22, libro 1 (L1), ejercicio fiscal 2021, dentro del esquema de Libro Mayor (GeneralLedger). La secuencia inicia en 9923, sugiriendo registros previos ya creados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2021';
GO
