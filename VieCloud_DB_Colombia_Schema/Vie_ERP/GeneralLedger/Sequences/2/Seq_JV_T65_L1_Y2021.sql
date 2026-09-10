CREATE SEQUENCE [GeneralLedger].[Seq_JV_T65_L1_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo BIGINT para registros de asientos de diario (Journal Voucher) del libro mayor general, específicamente para la tabla T65, libro 1 (L1), correspondiente al ejercicio fiscal 2021. Forma parte del esquema GeneralLedger y su numeración inicia en 1 con incrementos de una unidad.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T65_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T65_L1_Y2021';
GO
