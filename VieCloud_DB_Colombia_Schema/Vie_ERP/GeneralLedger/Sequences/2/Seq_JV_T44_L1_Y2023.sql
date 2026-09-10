CREATE SEQUENCE [GeneralLedger].[Seq_JV_T44_L1_Y2023]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro 1 (L1), tipo 44 (T44), correspondientes al ejercicio fiscal 2023, dentro del esquema de Contabilidad General. La secuencia inicia en 181, lo que indica registros previos ya creados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T44_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T44_L1_Y2023';
GO
