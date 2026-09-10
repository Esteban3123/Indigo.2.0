CREATE SEQUENCE [GeneralLedger].[Seq_JV_T24_L1_Y2020]
    AS BIGINT
    START WITH 185
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo bigint para asientos contables (Journal Vouchers) del libro contable T24, nivel 1 (L1), correspondientes al año fiscal 2020, dentro del esquema GeneralLedger. La secuencia inicia en 619, lo que indica que ya existían registros previos al crearla.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T24_L1_Y2020';
GO
