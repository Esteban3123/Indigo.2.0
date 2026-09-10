CREATE SEQUENCE [GeneralLedger].[Seq_JV_T43_L1_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor, específicamente para el tipo 43, libro 1, correspondientes al año 2025. La secuencia inicia en 181389, sugiriendo registros previos migrados o acumulados hasta ese punto.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T43_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T43_L1_Y2025';
GO
