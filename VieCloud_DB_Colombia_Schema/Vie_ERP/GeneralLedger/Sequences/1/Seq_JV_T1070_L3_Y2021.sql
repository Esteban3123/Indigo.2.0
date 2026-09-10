CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1070_L3_Y2021]
    AS BIGINT
    START WITH 85
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para registros de asientos contables (Journal Vouchers) asociados al libro mayor, específicamente para la tabla o partición correspondiente al código T1070, nivel 3 (L3) del ejercicio fiscal 2021. La secuencia inicia en 85, indicando que ya existen registros previos generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1070_L3_Y2021';
GO
