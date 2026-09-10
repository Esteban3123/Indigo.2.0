CREATE SEQUENCE [GeneralLedger].[Seq_JV_T35_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para los asientos contables (Journal Vouchers) del libro mayor, específicamente para el tipo de transacción 35, nivel 2, correspondiente al año fiscal 2025. La secuencia inicia en 480124 y no se reinicia al alcanzar el máximo, garantizando unicidad permanente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T35_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T35_L2_Y2025';
GO
