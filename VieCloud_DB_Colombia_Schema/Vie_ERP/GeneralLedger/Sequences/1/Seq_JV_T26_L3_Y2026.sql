CREATE SEQUENCE [GeneralLedger].[Seq_JV_T26_L3_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor, correspondientes al período fiscal 2026, tipo de transacción 26 y nivel contable 3. Inicia en 1 con incremento unitario sin caché, garantizando unicidad en los registros de la tabla de asientos del esquema GeneralLedger para ese ejercicio y clasificación específicos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T26_L3_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T26_L3_Y2026';
GO
