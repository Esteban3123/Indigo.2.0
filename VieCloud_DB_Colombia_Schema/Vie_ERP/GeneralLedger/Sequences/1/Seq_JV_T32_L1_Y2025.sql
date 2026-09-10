CREATE SEQUENCE [GeneralLedger].[Seq_JV_T32_L1_Y2025]
    AS BIGINT
    START WITH 10002
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para los asientos o líneas de diario (Journal Voucher) del libro mayor general, específicamente para el tipo de transacción 32, libro 1 (L1), correspondiente al ejercicio fiscal 2025. Inicia en 1358, lo que indica registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2025';
GO
