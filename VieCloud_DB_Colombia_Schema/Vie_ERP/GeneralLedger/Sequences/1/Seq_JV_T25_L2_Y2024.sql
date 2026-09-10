CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L2_Y2024]
    AS BIGINT
    START WITH 324
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos del libro mayor (Journal Vouchers), específicamente para la transacción tipo 25, libro 2 (L2), correspondiente al año fiscal 2024. La secuencia inicia en 88, lo que sugiere que ya se registraron entradas previas antes de su creación o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2024';
GO
