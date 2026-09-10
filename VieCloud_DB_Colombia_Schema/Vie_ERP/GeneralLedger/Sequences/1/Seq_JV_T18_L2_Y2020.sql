CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L2_Y2020]
    AS BIGINT
    START WITH 6262
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos o líneas de diario contable (Journal Voucher) correspondientes al tipo 18, nivel 2, del ejercicio fiscal 2020, en el esquema de libro mayor general (`GeneralLedger`). La secuencia inicia en 87507, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L2_Y2020';
GO
