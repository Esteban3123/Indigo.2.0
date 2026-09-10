CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1071_L1_Y2021]
    AS BIGINT
    START WITH 372
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para las líneas (L1) del comprobante de diario (JV) correspondiente al tipo de transacción T1071 del ejercicio fiscal 2021, iniciando desde el valor 372. Pertenece al esquema de Libro Mayor (GeneralLedger).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1071_L1_Y2021';
GO
