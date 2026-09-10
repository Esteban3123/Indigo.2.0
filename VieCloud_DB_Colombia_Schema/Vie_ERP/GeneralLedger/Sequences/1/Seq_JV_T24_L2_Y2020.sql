CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L2_Y2020]
    AS BIGINT
    START WITH 185
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para asientos contables (Journal Vouchers) del libro mayor general, específicamente asociados al tipo T24, nivel L2, correspondientes al año 2020. La secuencia inicia en 641, sin ciclo ni caché, garantizando unicidad irrepetible para los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L2_Y2020';
GO
