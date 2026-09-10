CREATE SEQUENCE [GeneralLedger].[Seq_JV_T30_L1_Y2024]
    AS BIGINT
    START WITH 7
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para el tipo 30, libro 1, correspondiente al año fiscal 2024. La secuencia inicia en 16851 y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2024';
GO
