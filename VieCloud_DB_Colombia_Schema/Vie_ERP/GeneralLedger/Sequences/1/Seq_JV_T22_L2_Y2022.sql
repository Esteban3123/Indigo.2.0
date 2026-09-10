CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L2_Y2022]
    AS BIGINT
    START WITH 147
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos de diario (Journal Vouchers) del tipo 22, libro 2 (L2), correspondientes al ejercicio fiscal 2022, dentro del esquema de Libro Mayor (GeneralLedger). La secuencia inicia en 12231 y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2022';
GO
