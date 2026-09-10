CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L1_Y2022]
    AS BIGINT
    START WITH 10
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor (GeneralLedger), específicamente para la transacción tipo 31, libro 1 (L1) del año 2022. Inicia en 4, no se reinicia ni usa caché, garantizando unicidad en el registro contable de ese período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L1_Y2022';
GO
