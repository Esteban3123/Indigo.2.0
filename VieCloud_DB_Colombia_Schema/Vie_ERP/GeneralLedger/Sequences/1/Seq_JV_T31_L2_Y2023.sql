CREATE SEQUENCE [GeneralLedger].[Seq_JV_T31_L2_Y2023]
    AS BIGINT
    START WITH 21
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos del libro mayor (Journal Vouchers) correspondientes al tipo de transacción 31, nivel 2, del año fiscal 2023. La secuencia inicia en 4, no se reinicia y no usa caché, garantizando unicidad en la numeración de esos comprobantes contables.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T31_L2_Y2023';
GO
