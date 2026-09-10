CREATE SEQUENCE [GeneralLedger].[Seq_JV_T17_L2_Y2024]
    AS BIGINT
    START WITH 970
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del tipo 17, libro 2, correspondientes al año 2024, dentro del esquema de Contabilidad General. La secuencia inicia en 1680, lo que indica registros previos ya existentes para ese período contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L2_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T17_L2_Y2024';
GO
