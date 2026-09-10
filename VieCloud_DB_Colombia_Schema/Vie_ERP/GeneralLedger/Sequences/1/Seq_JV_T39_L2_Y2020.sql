CREATE SEQUENCE [GeneralLedger].[Seq_JV_T39_L2_Y2020]
    AS BIGINT
    START WITH 6740
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla de tipo 39, nivel 2, correspondientes al ejercicio fiscal 2020. La secuencia inicia en 470, indicando que ya existían registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T39_L2_Y2020';
GO
