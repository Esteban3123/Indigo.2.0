CREATE SEQUENCE [GeneralLedger].[Seq_JV_T33_L1_Y2021]
    AS BIGINT
    START WITH 1687
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la empresa o entidad T33, libro L1, correspondientes al año fiscal 2021. La secuencia inicia en 1823, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2021';
GO
