CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1078_L3_Y2024]
    AS BIGINT
    START WITH 9971
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro mayor, específicamente para la tabla o lote identificado con el código T1078, nivel 3 (L3), correspondiente al ejercicio fiscal 2024. La secuencia inicia en 9971, lo que indica que ya existen registros previos generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L3_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L3_Y2024';
GO
