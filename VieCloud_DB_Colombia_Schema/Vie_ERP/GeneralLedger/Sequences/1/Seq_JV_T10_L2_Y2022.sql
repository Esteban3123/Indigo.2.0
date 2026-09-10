CREATE SEQUENCE [GeneralLedger].[Seq_JV_T10_L2_Y2022]
    AS BIGINT
    START WITH 54563
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 10, nivel 2, del año fiscal 2022, dentro del módulo de contabilidad general. La secuencia inicia en 5 885 474 sin ciclo ni caché, garantizando valores irrepetibles para registros contables de ese período específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L2_Y2022';
GO
