CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L1_Y2023]
    AS BIGINT
    START WITH 18308
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable tipo 6, libro mayor 1, correspondientes al año fiscal 2023, iniciando desde el valor 3472 sin reinicio de ciclo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2023';
GO
