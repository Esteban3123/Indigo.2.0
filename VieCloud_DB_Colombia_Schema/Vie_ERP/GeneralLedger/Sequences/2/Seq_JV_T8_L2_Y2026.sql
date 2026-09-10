CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L2_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Voucher) del libro mayor general, específicamente para el tipo 8, libro 2, correspondiente al ejercicio fiscal 2026. La secuencia inicia en 2909724 y no se reinicia al alcanzar su límite.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L2_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L2_Y2026';
GO
