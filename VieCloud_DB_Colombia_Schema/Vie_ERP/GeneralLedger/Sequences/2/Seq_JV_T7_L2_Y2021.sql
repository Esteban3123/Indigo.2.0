CREATE SEQUENCE [GeneralLedger].[Seq_JV_T7_L2_Y2021]
    AS BIGINT
    START WITH 10881
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del tipo 7, libro 2, correspondientes al año fiscal 2021, dentro del esquema de Contabilidad General. Inicia en 1674, lo que sugiere registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T7_L2_Y2021';
GO
