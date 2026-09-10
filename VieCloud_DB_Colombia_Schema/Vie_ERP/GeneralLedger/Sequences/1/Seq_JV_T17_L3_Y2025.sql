CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L3_Y2025]
    AS BIGINT
    START WITH 395
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 17 (T17), libro o nivel 3 (L3) del año fiscal 2025, en el módulo de Contabilidad General. Inicia desde el valor 395, incrementando de uno en uno sin caché, lo que garantiza continuidad en la numeración de comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2025';
GO
