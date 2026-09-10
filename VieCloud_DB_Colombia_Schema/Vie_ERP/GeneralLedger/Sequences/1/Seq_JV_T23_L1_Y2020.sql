CREATE SEQUENCE [GeneralLedger].[Seq_JV_T23_L1_Y2020]
    AS BIGINT
    START WITH 2298
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos del libro mayor (Journal Vouchers) correspondientes al tipo de transacción 23, libro 1 (L1), del ejercicio fiscal 2020. La secuencia inicia en 143548, lo que indica registros preexistentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T23_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T23_L1_Y2020';
GO
