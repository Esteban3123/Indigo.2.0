CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L1_Y2025]
    AS BIGINT
    START WITH 8
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo de transacción 40, libro 1 (L1), del ejercicio fiscal 2025. La secuencia inicia en 128, lo que indica que ya existen registros previos generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L1_Y2025';
GO
