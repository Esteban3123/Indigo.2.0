CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L1_Y2023]
    AS BIGINT
    START WITH 1101
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del libro 1 (L1) correspondiente a la tabla 34 (T34) del ejercicio fiscal 2023, dentro del esquema de contabilidad general. La secuencia inicia en 2076, no es cíclica y no usa caché, garantizando unicidad en la numeración de registros contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L1_Y2023';
GO
