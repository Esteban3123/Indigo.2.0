CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L3_Y2022]
    AS BIGINT
    START WITH 931
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos de diario (Journal Vouchers) del tipo 34, nivel 3, correspondientes al ejercicio fiscal 2022, dentro del esquema de Contabilidad General. La secuencia inicia en 931, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L3_Y2022';
GO
