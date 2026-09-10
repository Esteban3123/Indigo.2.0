CREATE SEQUENCE [GeneralLedger].[Seq_JV_T48_L1_Y2023]
    AS BIGINT
    START WITH 6
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del tipo 48, libro 1 (L1), correspondientes al ejercicio fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 85342, sin ciclo ni caché, garantizando unicidad irrepetible en la numeración de esos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L1_Y2023';
GO
