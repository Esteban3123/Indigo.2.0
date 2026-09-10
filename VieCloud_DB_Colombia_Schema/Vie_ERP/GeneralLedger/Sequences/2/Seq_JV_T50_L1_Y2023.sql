CREATE SEQUENCE [GeneralLedger].[Seq_JV_T50_L1_Y2023]
    AS BIGINT
    START WITH 658
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla T50, libro 1 (L1), correspondiente al año fiscal 2023. La secuencia inicia en 16, no se reinicia al alcanzar el máximo y no utiliza caché, garantizando integridad en la numeración correlativa de registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T50_L1_Y2023';
GO
