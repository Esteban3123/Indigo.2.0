CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L2_Y2021]
    AS BIGINT
    START WITH 4550
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo `bigint` para asientos de diario (Journal Vouchers) del libro mayor general, específicamente del tipo T24, nivel 2 (L2), correspondientes al año fiscal 2021. Inicia desde el valor 955, lo que indica que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L2_Y2021';
GO
