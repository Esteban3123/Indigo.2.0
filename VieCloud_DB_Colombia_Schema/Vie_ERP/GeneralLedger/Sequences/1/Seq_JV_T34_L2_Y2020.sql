CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L2_Y2020]
    AS BIGINT
    START WITH 131
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) asociados al tipo de transacción 34, libro 2 (L2) del ejercicio fiscal 2020, dentro del esquema de contabilidad general. La secuencia inicia en 723, sin ciclo ni caché, lo que indica que ya tenía registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L2_Y2020';
GO
