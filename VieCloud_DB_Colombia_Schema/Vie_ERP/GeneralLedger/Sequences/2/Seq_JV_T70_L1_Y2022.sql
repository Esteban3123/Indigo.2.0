CREATE SEQUENCE [GeneralLedger].[Seq_JV_T70_L1_Y2022]
    AS BIGINT
    START WITH 3
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos contables (Journal Voucher) del libro mayor general, correspondientes al tipo de transacción T70, nivel 1 (L1), del año fiscal 2022. Inicia en 3 e incrementa de uno en uno sin caché, garantizando unicidad en los registros del esquema GeneralLedger para ese período y clasificación contable específica.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T70_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T70_L1_Y2022';
GO
