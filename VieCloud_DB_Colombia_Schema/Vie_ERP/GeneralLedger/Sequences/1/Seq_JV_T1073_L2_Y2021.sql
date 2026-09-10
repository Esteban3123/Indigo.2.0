CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1073_L2_Y2021]
    AS BIGINT
    START WITH 1265
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al libro de nivel 2 (L2) de la entidad o centro de costo T1073, correspondientes al año fiscal 2021, dentro del esquema de Contabilidad General. Inicia en 1265, lo que indica registros previos ya creados para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1073_L2_Y2021';
GO
