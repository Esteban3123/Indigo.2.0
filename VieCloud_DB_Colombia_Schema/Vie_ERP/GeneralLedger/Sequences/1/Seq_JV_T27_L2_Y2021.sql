CREATE SEQUENCE [GeneralLedger].[Seq_JV_T27_L2_Y2021]
    AS BIGINT
    START WITH 3
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al tipo de transacción 27, nivel 2, del ejercicio fiscal 2021, en el esquema de contabilidad general. La secuencia inicia en 84, no es cíclica y no usa caché, garantizando unicidad en la generación de llaves.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T27_L2_Y2021';
GO
