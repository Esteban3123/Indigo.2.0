CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L1_Y2024]
    AS BIGINT
    START WITH 258
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos del libro mayor (Journal Voucher), específicamente para la tabla o partición correspondiente al tipo T1071, libro L1 del año 2024. La secuencia inicia en 258, lo que indica registros previos ya existentes para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2024';
GO
