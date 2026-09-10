CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1083_L1_Y2024]
    AS BIGINT
    START WITH 48
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al libro 1 (L1) de la tabla o entidad 1083 (T1083) del módulo de Contabilidad General, específicamente para el período fiscal 2024. La secuencia inicia en 48, indicando que ya existen 47 registros previos generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L1_Y2024';
GO
