CREATE SEQUENCE [GeneralLedger].[Seq_JV_T57_L3_Y2022]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores correlativos para los asientos o líneas de diario (Journal Voucher) del libro mayor general, específicamente para el tipo de transacción T57, nivel 3 (L3), correspondiente al ejercicio fiscal 2022. Inicia en 1 e incrementa de uno en uno sin caché, garantizando unicidad en la tabla destino del esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T57_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T57_L3_Y2022';
GO
