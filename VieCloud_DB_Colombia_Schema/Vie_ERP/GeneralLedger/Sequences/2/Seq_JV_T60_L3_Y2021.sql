CREATE SEQUENCE [GeneralLedger].[Seq_JV_T60_L3_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo BIGINT para los asientos contables (Journal Vouchers) del libro mayor general, específicamente para la combinación de tipo de transacción T60, nivel contable L3 y el ejercicio fiscal 2021. Se utiliza para asignar números de secuencia correlativos a registros del esquema GeneralLedger correspondientes a ese período y clasificación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T60_L3_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T60_L3_Y2021';
GO
