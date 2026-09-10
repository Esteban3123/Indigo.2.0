CREATE SEQUENCE [GeneralLedger].[Seq_JV_T38_L3_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales para los asientos de diario (Journal Vouchers) correspondientes al tipo 38, nivel 3 del año 2026, dentro del módulo de Contabilidad General. Cada nuevo registro en la tabla de asientos asociada obtiene un valor entero incremental comenzando en 1, sin caché para garantizar la continuidad estricta de la numeración.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T38_L3_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T38_L3_Y2026';
GO
