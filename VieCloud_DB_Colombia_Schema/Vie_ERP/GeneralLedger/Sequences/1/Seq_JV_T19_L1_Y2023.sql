CREATE SEQUENCE [GeneralLedger].[Seq_JV_T19_L1_Y2023]
    AS BIGINT
    START WITH 10488
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos de diario (Journal Vouchers) correspondientes al tipo 19, libro 1 del año 2023, dentro del esquema de contabilidad general (`GeneralLedger`). Inicia en 684, lo que indica que ya existen 683 registros previos para esa combinación de tipo, libro y período fiscal.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T19_L1_Y2023';
GO
