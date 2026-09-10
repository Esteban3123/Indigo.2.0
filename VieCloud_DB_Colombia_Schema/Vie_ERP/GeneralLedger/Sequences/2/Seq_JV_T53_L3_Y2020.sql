CREATE SEQUENCE [GeneralLedger].[Seq_JV_T53_L3_Y2020]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores únicos e incrementales para asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo 53, nivel 3, del año fiscal 2020. Inicia en el valor 9, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T53_L3_Y2020';
GO
