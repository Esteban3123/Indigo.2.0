CREATE SEQUENCE [GeneralLedger].[Seq_JV_T27_L2_Y2024]
    AS BIGINT
    START WITH 5
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos contables (Journal Vouchers) del tipo 27, libro 2 (L2), correspondientes al ejercicio fiscal 2024, dentro del esquema de contabilidad general. La secuencia inicia en 306, sugiriendo registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2024';
GO
