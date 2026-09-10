CREATE SEQUENCE [GeneralLedger].[Seq_JV_T54_L1_Y2020]
    AS BIGINT
    START WITH 6
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) del libro 1 (L1) correspondientes al período fiscal 2020, asociados al tipo de transacción o tabla 54 (T54) dentro del esquema de contabilidad general (GeneralLedger). La secuencia inicia en 6, incrementa de uno en uno y no utiliza caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L1_Y2020';
GO
