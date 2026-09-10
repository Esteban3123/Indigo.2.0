CREATE SEQUENCE [GeneralLedger].[Seq_JV_T39_L2_Y2024]
    AS BIGINT
    START WITH 1607
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 39, nivel 2, del ejercicio fiscal 2024, dentro del esquema de contabilidad general (`GeneralLedger`). La secuencia inicia en 2605, incrementa de uno en uno y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L2_Y2024';
GO
