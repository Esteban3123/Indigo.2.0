CREATE SEQUENCE [GeneralLedger].[Seq_JV_T32_L1_Y2020]
    AS BIGINT
    START WITH 196
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) del libro mayor, correspondientes al tipo 32, libro 1 (L1) del ejercicio fiscal 2020. Inicia en 372, lo que indica que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T32_L1_Y2020';
GO
