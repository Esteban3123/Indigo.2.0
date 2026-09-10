CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L3_Y2022]
    AS BIGINT
    START WITH 11399
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor, específicamente para el tipo 17, nivel 3, correspondientes al año fiscal 2022. El valor inicial de 11 399 sugiere continuidad con registros previos ya existentes en la tabla destino del esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2022';
GO
