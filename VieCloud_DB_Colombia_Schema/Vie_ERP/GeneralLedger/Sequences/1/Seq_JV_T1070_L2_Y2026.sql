CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1070_L2_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para los asientos de diario (Journal Vouchers) asociados al libro mayor general, específicamente para el nivel 2 (L2) de la tabla T1070 correspondiente al ejercicio fiscal 2026. Incrementa de uno en uno sin caché, garantizando la integridad de la numeración correlativa.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L2_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L2_Y2026';
GO
