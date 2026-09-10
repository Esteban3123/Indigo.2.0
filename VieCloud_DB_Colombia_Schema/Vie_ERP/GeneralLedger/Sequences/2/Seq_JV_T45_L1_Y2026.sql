CREATE SEQUENCE [GeneralLedger].[Seq_JV_T45_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla de nivel 1 (L1) del tipo 45 (T45) correspondiente al año fiscal 2026. La secuencia inicia en 1398, lo que indica registros previos ya existentes, e incrementa de uno en uno sin ciclo ni caché, garantizando unicidad irrepetible.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T45_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T45_L1_Y2026';
GO
