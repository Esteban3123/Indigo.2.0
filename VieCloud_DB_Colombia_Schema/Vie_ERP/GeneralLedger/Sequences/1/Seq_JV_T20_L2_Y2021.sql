CREATE SEQUENCE [GeneralLedger].[Seq_JV_T20_L2_Y2021]
    AS BIGINT
    START WITH 220
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del tipo de transacción 20, nivel 2, correspondientes al año fiscal 2021, dentro del módulo de Contabilidad General. La secuencia inicia en 30087, indicando registros previos ya generados, y no permite ciclos ni almacenamiento en caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L2_Y2021';
GO
