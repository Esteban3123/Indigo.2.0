CREATE SEQUENCE [GeneralLedger].[Seq_JV_T54_L2_Y2021]
    AS BIGINT
    START WITH 12
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores únicos para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla T54, nivel 2 (L2), correspondiente al año fiscal 2021. Inicia en el valor 12, incrementando de uno en uno, lo que indica que al momento de su creación ya existían registros previos para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T54_L2_Y2021';
GO
