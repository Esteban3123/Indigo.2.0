CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1076_L2_Y2020]
    AS BIGINT
    START WITH 60
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores correlativos para asientos de diario (Journal Vouchers) asociados al libro mayor, correspondientes a la tabla T1076, nivel 2 (L2), del ejercicio fiscal 2020. Inicia en 60 e incrementa de uno en uno, sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L2_Y2020';
GO
