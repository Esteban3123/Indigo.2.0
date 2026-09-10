CREATE SEQUENCE [GeneralLedger].[Seq_JV_T32_L1_Y2024]
    AS BIGINT
    START WITH 116
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la combinación de tipo de transacción 32, libro 1 (L1) del ejercicio fiscal 2024. La secuencia inicia en 1091, indicando que ya existen registros previos para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2024';
GO
