CREATE SEQUENCE [GeneralLedger].[Seq_JV_T6_L2_Y2022]
    AS BIGINT
    START WITH 34145
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos contables (Journal Vouchers) del tipo 6, libro 2, correspondientes al ejercicio fiscal 2022, dentro del esquema de contabilidad general. Inicia en 2636, incrementa de uno en uno y no reinicia al alcanzar el máximo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T6_L2_Y2022';
GO
