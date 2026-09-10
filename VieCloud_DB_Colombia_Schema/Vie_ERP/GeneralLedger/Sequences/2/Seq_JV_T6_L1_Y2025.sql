CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L1_Y2025]
    AS BIGINT
    START WITH 116
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable tipo 6, nivel 1, correspondientes al ejercicio fiscal 2025, dentro del esquema `GeneralLedger`. La secuencia inicia en 5096, incrementa de uno en uno y no se reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L1_Y2025';
GO
