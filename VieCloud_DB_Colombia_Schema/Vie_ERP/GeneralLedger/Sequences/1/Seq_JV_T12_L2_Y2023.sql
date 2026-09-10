CREATE SEQUENCE [GeneralLedger].[Seq_JV_T12_L2_Y2023]
    AS BIGINT
    START WITH 11953
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para asientos del libro mayor general (Journal Vouchers), específicamente correspondientes al período 12 (diciembre), nivel 2 y año fiscal 2023. Inicia desde el valor 908, lo que indica que ya existían registros previos al crearse esta secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T12_L2_Y2023';
GO
