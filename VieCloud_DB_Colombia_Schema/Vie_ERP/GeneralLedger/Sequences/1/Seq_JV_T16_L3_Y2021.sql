CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L3_Y2021]
    AS BIGINT
    START WITH 509
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica de tipo BIGINT que genera identificadores consecutivos para asientos de diario (Journal Vouchers) correspondientes al tipo 16, nivel 3 del año 2021, en el esquema de Libro Mayor General. Inicia en 509, lo que indica registros previos ya existentes para ese período y clasificación contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L3_Y2021';
GO
