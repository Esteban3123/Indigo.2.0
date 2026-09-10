CREATE SEQUENCE [GeneralLedger].[Seq_JV_T8_L1_Y2022]
    AS BIGINT
    START WITH 1415
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos de tipo bigint para los asientos contables (Journal Vouchers) del libro contable tipo 8, libro 1, correspondientes al año fiscal 2022, dentro del esquema GeneralLedger. La secuencia inicia en 1,485,885, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T8_L1_Y2022';
GO
