CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1073_L3_Y2022]
    AS BIGINT
    START WITH 1399
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores consecutivos para asientos de diario (Journal Vouchers) asociados al libro mayor (GeneralLedger), específicamente para la tabla T1073, nivel 3 (L3), correspondiente al año fiscal 2022. Inicia en 1399, lo que indica que ya existían registros previos al crearla.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L3_Y2022';
GO
