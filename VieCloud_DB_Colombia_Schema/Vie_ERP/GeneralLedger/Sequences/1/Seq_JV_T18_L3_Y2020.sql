CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L3_Y2020]
    AS BIGINT
    START WITH 6262
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al libro 3 (L3) del tipo 18 (T18) del ejercicio fiscal 2020, dentro del esquema de Contabilidad General. La secuencia inicia en 6262, lo que indica registros preexistentes al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L3_Y2020';
GO
