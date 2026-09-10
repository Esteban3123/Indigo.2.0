CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L3_Y2020]
    AS BIGINT
    START WITH 3195
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) correspondientes al tipo 17, nivel 3 del año 2020 en el módulo de contabilidad general. La secuencia inicia en 3195, lo que indica registros previos ya existentes para ese periodo y clasificación contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L3_Y2020';
GO
