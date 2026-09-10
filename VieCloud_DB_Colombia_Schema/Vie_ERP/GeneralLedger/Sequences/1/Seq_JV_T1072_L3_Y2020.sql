CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1072_L3_Y2020]
    AS BIGINT
    START WITH 22
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) asociados al libro mayor, correspondientes a la tabla con código T1072, nivel 3 (L3), del ejercicio fiscal 2020. La secuencia inicia en 22, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1072_L3_Y2020';
GO
