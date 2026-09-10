CREATE SEQUENCE [GeneralLedger].[Seq_JV_T10_L2_Y2023]
    AS BIGINT
    START WITH 48392
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al libro mayor general, específicamente para el tipo de transacción 10, libro 2 del año 2023. Inicia en 7 593 868, reflejando registros previos ya existentes para ese periodo contable.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T10_L2_Y2023';
GO
