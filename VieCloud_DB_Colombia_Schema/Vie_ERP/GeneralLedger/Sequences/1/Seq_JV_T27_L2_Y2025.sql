CREATE SEQUENCE [GeneralLedger].[Seq_JV_T27_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo `bigint` para asientos de diario (Journal Vouchers) del libro mayor contable, específicamente para el tipo de transacción 27, libro 2 (L2), correspondiente al ejercicio fiscal 2025. La secuencia inicia en 431, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2025';
GO
