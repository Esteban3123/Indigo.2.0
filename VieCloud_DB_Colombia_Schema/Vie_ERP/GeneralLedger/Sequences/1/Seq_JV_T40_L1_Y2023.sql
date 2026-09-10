CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L1_Y2023]
    AS BIGINT
    START WITH 2207
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos o entradas del libro mayor (General Ledger), específicamente para el tipo de transacción T40, libro L1, correspondientes al año 2023. La secuencia inicia en 81, lo que indica registros previos ya creados, y no permite reinicio ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L1_Y2023';
GO
