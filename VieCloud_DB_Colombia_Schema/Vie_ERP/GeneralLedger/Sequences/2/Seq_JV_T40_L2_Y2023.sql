CREATE SEQUENCE [GeneralLedger].[Seq_JV_T40_L2_Y2023]
    AS BIGINT
    START WITH 2207
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al libro contable T40, nivel 2, del ejercicio fiscal 2023, dentro del esquema de Mayor General. La secuencia inicia en 83, lo que indica que ya existen registros previos en dicho período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T40_L2_Y2023';
GO
