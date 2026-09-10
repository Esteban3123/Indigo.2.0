CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L1_Y2020]
    AS BIGINT
    START WITH 759
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro contable 1 (L1), correspondientes al tipo de transacción 19 (T19) del año fiscal 2020, dentro del esquema GeneralLedger. La secuencia inicia en 248, indicando registros previos ya generados, y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L1_Y2020';
GO
