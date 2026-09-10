CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L3_Y2023]
    AS BIGINT
    START WITH 22472
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al tipo 18, nivel 3 del año fiscal 2023, dentro del esquema de contabilidad general (GeneralLedger). El valor inicial indica que ya se habían registrado previamente 22 471 entradas antes de su creación o reinicio.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L3_Y2023';
GO
