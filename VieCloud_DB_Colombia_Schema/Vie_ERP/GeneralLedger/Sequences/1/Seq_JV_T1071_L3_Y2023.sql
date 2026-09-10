CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L3_Y2023]
    AS BIGINT
    START WITH 403
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla o lote correspondiente al código T1071, nivel 3 (L3), del año fiscal 2023. Inicia en el valor 403, lo que indica que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L3_Y2023';
GO
