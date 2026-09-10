CREATE SEQUENCE [GeneralLedger].[Seq_JV_T22_L2_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia que genera identificadores únicos correlativos de tipo bigint para asientos de diario (Journal Vouchers) del tipo 22, nivel 2, correspondientes al ejercicio fiscal 2026, dentro del esquema de Contabilidad General. Inicia en 15698 e incrementa de uno en uno sin reinicio ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T22_L2_Y2026';
GO
