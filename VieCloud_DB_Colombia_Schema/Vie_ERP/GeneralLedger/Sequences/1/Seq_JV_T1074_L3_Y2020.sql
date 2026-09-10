CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1074_L3_Y2020]
    AS BIGINT
    START WITH 18
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor (GeneralLedger), específicamente para la tabla o partición del tipo de comprobante T1074, nivel 3 (L3), correspondiente al año fiscal 2020. Inicia en 18, incrementando de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1074_L3_Y2020';
GO
