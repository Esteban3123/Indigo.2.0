CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L1_Y2023]
    AS BIGINT
    START WITH 11678
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo 7, libro 1 y ejercicio fiscal 2023. Inicia en 2741 y no se reinicia al alcanzar el máximo, garantizando unicidad en la tabla de movimientos contables del esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2023';
GO
