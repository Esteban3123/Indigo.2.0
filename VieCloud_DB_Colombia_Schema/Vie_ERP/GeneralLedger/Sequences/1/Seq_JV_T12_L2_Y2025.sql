CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L2_Y2025]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales para asientos de diario (Journal Vouchers) del tipo 12, libro 2, correspondientes al año fiscal 2025, dentro del módulo de Contabilidad General. La secuencia inicia en 1193, garantizando continuidad con registros previos sin repetición de valores.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2025';
GO
