CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L1_Y2022]
    AS BIGINT
    START WITH 18384
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos de diario (Journal Vouchers) del tipo de transacción 18, libro 1, correspondientes al ejercicio fiscal 2022 en el esquema de contabilidad general. El valor inicial 202531 sugiere que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2022';
GO
