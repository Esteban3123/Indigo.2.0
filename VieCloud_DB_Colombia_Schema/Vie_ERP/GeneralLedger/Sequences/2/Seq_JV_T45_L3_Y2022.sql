CREATE SEQUENCE [GeneralLedger].[Seq_JV_T45_L3_Y2022]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos del diario contable (Journal Vouchers) correspondientes al tipo de transacción 45, nivel 3, del ejercicio fiscal 2022, dentro del esquema de contabilidad general.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T45_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T45_L3_Y2022';
GO
