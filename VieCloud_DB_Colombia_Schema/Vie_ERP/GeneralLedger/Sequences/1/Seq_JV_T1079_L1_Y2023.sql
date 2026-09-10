CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1079_L1_Y2023]
    AS BIGINT
    START WITH 312
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales únicos de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro 1 (L1) del período fiscal 2023, correspondientes a la entidad o unidad contable T1079 dentro del esquema de Contabilidad General. La secuencia inicia en 312, lo que indica registros previos ya generados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1079_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1079_L1_Y2023';
GO
