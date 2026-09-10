CREATE SEQUENCE [GeneralLedger].[Seq_JV_T25_L2_Y2021]
    AS BIGINT
    START WITH 7727
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) del tipo 25, libro 2 (L2), correspondientes al año fiscal 2021, dentro del esquema de Contabilidad General. La secuencia inicia en 48, lo que indica que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T25_L2_Y2021';
GO
