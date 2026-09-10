CREATE SEQUENCE [GeneralLedger].[Seq_JV_T21_L2_Y2024]
    AS BIGINT
    START WITH 325
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del tipo de transacción 21, nivel 2, correspondientes al ejercicio fiscal 2024, dentro del esquema de Libro Mayor General (`GeneralLedger`). La secuencia inicia en 354214, lo que sugiere registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T21_L2_Y2024';
GO
