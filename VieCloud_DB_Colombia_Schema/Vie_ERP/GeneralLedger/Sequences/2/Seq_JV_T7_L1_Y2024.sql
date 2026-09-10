CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L1_Y2024]
    AS BIGINT
    START WITH 5569
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo 7, libro 1, correspondiente al ejercicio fiscal 2024. La secuencia inicia en 3097, no es cíclica y no usa caché, garantizando unicidad en la numeración de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L1_Y2024';
GO
