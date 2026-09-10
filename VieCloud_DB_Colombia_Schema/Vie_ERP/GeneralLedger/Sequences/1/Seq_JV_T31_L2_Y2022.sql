CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L2_Y2022]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos o líneas de diario contable (Journal Voucher) asociados al tipo de transacción 31, nivel 2, del ejercicio fiscal 2022, dentro del esquema de contabilidad general. No tiene ciclo ni caché, garantizando unicidad estricta.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2022';
GO
