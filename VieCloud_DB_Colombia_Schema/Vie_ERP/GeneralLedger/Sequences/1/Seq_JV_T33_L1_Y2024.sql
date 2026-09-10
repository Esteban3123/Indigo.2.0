CREATE SEQUENCE [GeneralLedger].[Seq_JV_T33_L1_Y2024]
    AS BIGINT
    START WITH 957
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para asientos contables (Journal Vouchers) del libro mayor (GeneralLedger), correspondientes al tipo de transacción T33, libro L1 y ejercicio fiscal 2024. Inicia en 3592 y no reinicia al alcanzar el máximo, garantizando unicidad sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T33_L1_Y2024';
GO
