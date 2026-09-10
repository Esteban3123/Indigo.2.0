CREATE SEQUENCE [GeneralLedger].[Seq_JV_T50_L2_Y2021]
    AS BIGINT
    START WITH 590
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor, específicamente para el tipo de transacción T50, libro L2, correspondientes al año 2021. La secuencia inicia en 12, sin ciclo ni caché, lo que sugiere continuidad desde registros preexistentes.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L2_Y2021';
GO
