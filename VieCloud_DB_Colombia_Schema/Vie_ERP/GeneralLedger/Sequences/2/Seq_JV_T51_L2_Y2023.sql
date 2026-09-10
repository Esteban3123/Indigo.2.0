CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L2_Y2023]
    AS BIGINT
    START WITH 19
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores únicos e incrementales para asientos de diario (Journal Vouchers) correspondientes al libro mayor (GeneralLedger), específicamente para la transacción tipo 51, libro 2 (L2) del año fiscal 2023. Inicia en el valor 19, lo que indica que ya existían 18 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L2_Y2023';
GO
