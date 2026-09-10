CREATE SEQUENCE [GeneralLedger].[Seq_JV_T16_L1_Y2020]
    AS BIGINT
    START WITH 96
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos consecutivos de tipo bigint para los asientos contables (Journal Vouchers) del libro mayor, específicamente para el tipo de transacción 16, libro 1, correspondiente al año fiscal 2020. Inicia desde el valor 1581, lo que sugiere que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T16_L1_Y2020';
GO
