CREATE SEQUENCE [GeneralLedger].[Seq_JV_T30_L1_Y2023]
    AS BIGINT
    START WITH 16
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos contables (Journal Vouchers) del libro mayor general, específicamente para el tipo 30, libro 1 (L1) del ejercicio fiscal 2023. Inicia en 13444, incrementa de uno en uno sin ciclo ni caché, lo que garantiza unicidad en la tabla de transacciones contables correspondiente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L1_Y2023';
GO
