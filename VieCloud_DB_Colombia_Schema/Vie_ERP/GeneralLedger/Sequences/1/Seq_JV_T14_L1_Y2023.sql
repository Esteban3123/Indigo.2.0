CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L1_Y2023]
    AS BIGINT
    START WITH 3979
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos contables (Journal Vouchers) correspondientes al libro contable 1 (L1) del tipo de transacción 14 (T14) del ejercicio fiscal 2023, dentro del esquema de Contabilidad General.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2023';
GO
