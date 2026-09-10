CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L1_Y2025]
    AS BIGINT
    START WITH 12
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro mayor, específicamente para la transacción tipo 21, libro 1 (L1), correspondiente al ejercicio fiscal 2025. La secuencia inicia en 432920, no se reinicia al alcanzar el máximo y no utiliza caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L1_Y2025';
GO
