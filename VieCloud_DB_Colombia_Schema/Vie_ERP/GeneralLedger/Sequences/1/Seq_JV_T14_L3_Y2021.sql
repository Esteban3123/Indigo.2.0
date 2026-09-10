CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L3_Y2021]
    AS BIGINT
    START WITH 3055
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos contables (Journal Vouchers) del tipo 14, nivel 3, correspondientes al ejercicio fiscal 2021, dentro del esquema de libro mayor general (GeneralLedger). El valor inicial 3055 indica que ya se habían registrado previamente más de tres mil entradas antes de su uso.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L3_Y2021';
GO
