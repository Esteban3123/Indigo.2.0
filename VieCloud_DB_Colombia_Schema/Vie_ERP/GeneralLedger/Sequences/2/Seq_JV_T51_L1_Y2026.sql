CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor, correspondientes a la transacción tipo 51, libro 1, del ejercicio fiscal 2026. La secuencia inicia en 1, no se reinicia al alcanzar el máximo y no usa caché, garantizando unicidad en el esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L1_Y2026';
GO
