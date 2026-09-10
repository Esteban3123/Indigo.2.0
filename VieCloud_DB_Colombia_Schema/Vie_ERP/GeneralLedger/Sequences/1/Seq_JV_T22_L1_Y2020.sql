CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L1_Y2020]
    AS BIGINT
    START WITH 30
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla tipo 22, libro 1, correspondiente al año fiscal 2020. Inicia en 5372, lo que indica registros contables preexistentes a esa secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L1_Y2020';
GO
