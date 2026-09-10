CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L2_Y2020]
    AS BIGINT
    START WITH 996
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 14, nivel 2, del ejercicio fiscal 2020, dentro del esquema de contabilidad general. La secuencia inicia en 16211, indicando registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L2_Y2020';
GO
