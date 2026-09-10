CREATE SEQUENCE [GeneralLedger].[Seq_JV_T52_L3_Y2023]
    AS BIGINT
    START WITH 733
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor, correspondientes al tipo de comprobante T52, nivel de libro L3 y el período fiscal del año 2023. La secuencia inicia en 733, lo que indica registros previos ya generados en ese contexto contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T52_L3_Y2023';
GO
