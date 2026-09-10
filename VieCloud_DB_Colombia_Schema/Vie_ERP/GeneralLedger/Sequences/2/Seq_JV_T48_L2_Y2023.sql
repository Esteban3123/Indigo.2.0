CREATE SEQUENCE [GeneralLedger].[Seq_JV_T48_L2_Y2023]
    AS BIGINT
    START WITH 4
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos contables (Journal Vouchers) del libro mayor, correspondientes al tipo 48, nivel 2, del año fiscal 2023. Inicia en 82202, incrementa de uno en uno y no recicla valores, garantizando unicidad en la tabla de movimientos contables del esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L2_Y2023';
GO
