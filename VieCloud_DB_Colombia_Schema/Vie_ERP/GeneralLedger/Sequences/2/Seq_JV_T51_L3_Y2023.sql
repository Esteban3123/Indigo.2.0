CREATE SEQUENCE [GeneralLedger].[Seq_JV_T51_L3_Y2023]
    AS BIGINT
    START WITH 19
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) correspondientes al tipo de transacción T51, nivel 3 (L3), del ejercicio fiscal 2023, en el esquema de Contabilidad General. La secuencia inicia en 19, lo que indica que ya existen registros previos para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T51_L3_Y2023';
GO
