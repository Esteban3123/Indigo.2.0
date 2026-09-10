CREATE SEQUENCE [GeneralLedger].[Seq_JV_T18_L1_Y2025]
    AS BIGINT
    START WITH 8
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro mayor general, correspondientes al tipo 18, libro 1 y ejercicio fiscal 2025. Inicia desde el valor 344605, lo que indica registros contables previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2025';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T18_L1_Y2025';
GO
