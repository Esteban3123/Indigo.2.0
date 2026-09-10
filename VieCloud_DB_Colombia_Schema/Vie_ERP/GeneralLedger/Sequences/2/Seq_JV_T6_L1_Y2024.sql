CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L1_Y2024]
    AS BIGINT
    START WITH 10186
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos contables (Journal Vouchers) del libro contable número 6, nivel 1, correspondientes al año fiscal 2024, dentro del esquema de Libro Mayor General (`GeneralLedger`). La secuencia inicia en 4209, lo que indica registros previos ya creados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2024';
GO
