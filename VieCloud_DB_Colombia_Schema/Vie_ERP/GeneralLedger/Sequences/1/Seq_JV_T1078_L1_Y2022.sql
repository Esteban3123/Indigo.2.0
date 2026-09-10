CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1078_L1_Y2022]
    AS BIGINT
    START WITH 118966
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla o lote correspondiente al tipo T1078, libro 1 (L1), del ejercicio fiscal 2022. La secuencia inicia en 118966, lo que sugiere continuidad respecto a registros contables preexistentes de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L1_Y2022';
GO
