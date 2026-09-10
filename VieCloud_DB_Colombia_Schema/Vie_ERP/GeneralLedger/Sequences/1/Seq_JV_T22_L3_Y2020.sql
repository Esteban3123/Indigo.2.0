CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L3_Y2020]
    AS BIGINT
    START WITH 30
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica que genera identificadores únicos de tipo BIGINT para asientos contables (Journal Vouchers) correspondientes al tipo 22, nivel 3, del ejercicio fiscal 2020, dentro del esquema de libro mayor general (GeneralLedger). Inicia en 30 e incrementa de uno en uno sin caché, garantizando valores consecutivos sin saltos en la numeración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L3_Y2020';
GO
