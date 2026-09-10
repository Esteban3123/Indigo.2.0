CREATE SEQUENCE [GeneralLedger].[Seq_JV_T58_L1_Y2022]
    AS BIGINT
    START WITH 215
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) del libro 1 (L1) correspondiente al año 2022, asociados a la tabla 58 (T58) dentro del esquema de Libro Mayor General. Inicia en el valor 215 con incremento de 1, sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T58_L1_Y2022';
GO
