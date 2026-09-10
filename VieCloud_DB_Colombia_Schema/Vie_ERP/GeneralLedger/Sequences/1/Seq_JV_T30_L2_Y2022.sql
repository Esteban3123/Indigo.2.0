CREATE SEQUENCE [GeneralLedger].[Seq_JV_T30_L2_Y2022]
    AS BIGINT
    START WITH 18
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del tipo de transacción 30, nivel 2, correspondientes al ejercicio fiscal 2022, dentro del esquema de Contabilidad General. Inicia en 9815 e incrementa de uno en uno sin ciclo ni caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T30_L2_Y2022';
GO
