CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L1_Y2021]
    AS BIGINT
    START WITH 4550
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) asociados al tipo de transacción T24, libro L1, correspondientes al año 2021, dentro del esquema de Libro Mayor General. La secuencia inicia en 933, lo que indica registros previos ya creados para ese período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2021';
GO
