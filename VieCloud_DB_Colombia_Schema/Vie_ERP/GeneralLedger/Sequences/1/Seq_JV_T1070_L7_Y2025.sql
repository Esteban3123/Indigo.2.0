CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1070_L7_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al libro contable nivel 7 (L7) de la tabla T1070 del módulo GeneralLedger, específicamente para el ejercicio fiscal 2025. La secuencia inicia en 1 con incrementos de 1 sin caché, garantizando consecutividad en la numeración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L7_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L7_Y2025';
GO
