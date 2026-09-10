CREATE SEQUENCE [GeneralLedger].[Seq_JV_T53_L2_Y2020]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor, correspondientes al tipo de transacción T53, nivel L2, del ejercicio fiscal 2020. La secuencia inicia en 1, no se reinicia al alcanzar el máximo y no utiliza caché, garantizando integridad en la numeración de registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L2_Y2020';
GO
