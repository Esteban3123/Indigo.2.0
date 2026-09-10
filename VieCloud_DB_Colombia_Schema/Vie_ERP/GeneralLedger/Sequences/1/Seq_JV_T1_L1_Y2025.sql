CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1_L1_Y2025]
    AS BIGINT
    START WITH 2
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos contables (Journal Vouchers) del libro mayor general, correspondientes al tipo 1, libro 1 del año 2025. La secuencia inicia en 1097, incrementa de uno en uno y no se reinicia al alcanzar el máximo, garantizando unicidad en el esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1_L1_Y2025';
GO
