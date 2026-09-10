CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L3_Y2023]
    AS BIGINT
    START WITH 1412
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del tipo 8, libro 3, correspondientes al ejercicio fiscal 2023, dentro del módulo de Contabilidad General. Inicia en el valor 1412, incrementando de uno en uno sin caché, lo que garantiza la continuidad y trazabilidad de los comprobantes contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L3_Y2023';
GO
