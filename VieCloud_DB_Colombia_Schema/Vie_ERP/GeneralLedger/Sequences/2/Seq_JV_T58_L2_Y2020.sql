CREATE SEQUENCE [GeneralLedger].[Seq_JV_T58_L2_Y2020]
    AS BIGINT
    START WITH 126
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor (GeneralLedger), específicamente para la tabla o partición correspondiente al tipo 58, nivel 2 (L2), del ejercicio fiscal 2020. La secuencia inicia en 126, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L2_Y2020';
GO
