CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1078_L1_Y2024]
    AS BIGINT
    START WITH 63789
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro mayor general, correspondientes a la tabla o lote T1078, libro 1 (L1), del ejercicio fiscal 2024. Inició en 63789 e incrementa de uno en uno sin caché, garantizando continuidad en la numeración de registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L1_Y2024';
GO
