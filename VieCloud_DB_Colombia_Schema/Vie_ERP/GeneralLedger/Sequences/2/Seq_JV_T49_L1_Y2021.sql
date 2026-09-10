CREATE SEQUENCE [GeneralLedger].[Seq_JV_T49_L1_Y2021]
    AS BIGINT
    START WITH 312
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro 1 (L1), tipo 49 (T49), correspondientes al año fiscal 2021, dentro del esquema de Contabilidad General. Inicia en el valor 312, incrementando de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T49_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T49_L1_Y2021';
GO
