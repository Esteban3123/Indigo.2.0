CREATE SEQUENCE [GeneralLedger].[Seq_JV_T11_L2_Y2021]
    AS BIGINT
    START WITH 126084
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos contables (Journal Vouchers) del tipo 11, libro 2 (Level 2), correspondientes al año fiscal 2021, dentro del esquema de Libro Mayor General (GeneralLedger). La secuencia inicia en 12764, no es cíclica y no usa caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T11_L2_Y2021';
GO
