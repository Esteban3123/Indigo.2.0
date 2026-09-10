CREATE SEQUENCE [GeneralLedger].[Seq_JV_T38_L1_Y2021]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del libro 1 (L1) correspondiente al tipo de transacción 38 (T38) del ejercicio fiscal 2021, dentro del esquema de Libro Mayor (GeneralLedger). La secuencia inicia en 322, sin ciclo ni caché, lo que sugiere continuidad desde registros previos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T38_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T38_L1_Y2021';
GO
