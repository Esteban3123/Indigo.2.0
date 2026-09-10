CREATE SEQUENCE [GeneralLedger].[Seq_JV_T36_L2_Y2020]
    AS BIGINT
    START WITH 2961
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos contables (Journal Vouchers) del libro mayor, correspondientes al período fiscal 2020, tipo de transacción 36 y nivel 2. Inicia desde el valor 11333, sin ciclo ni caché, garantizando unicidad en la tabla de asientos del esquema GeneralLedger.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L2_Y2020';
GO
