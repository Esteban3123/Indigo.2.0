CREATE SEQUENCE [GeneralLedger].[Seq_JV_T14_L1_Y2025]
    AS BIGINT
    START WITH 10
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del libro 1 (L1), tipo 14 (T14), correspondientes al ejercicio fiscal 2025, dentro del esquema de Contabilidad General. La secuencia inicia en 65339, incrementa de uno en uno y no permite reinicio cíclico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T14_L1_Y2025';
GO
