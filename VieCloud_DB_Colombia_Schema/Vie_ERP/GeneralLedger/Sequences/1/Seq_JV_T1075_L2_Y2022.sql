CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1075_L2_Y2022]
    AS BIGINT
    START WITH 168
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Vouchers) asociados al tipo de transacción T1075, nivel 2 (L2), correspondientes al año fiscal 2022, dentro del esquema de Contabilidad General. La secuencia inicia en 168, lo que indica registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1075_L2_Y2022';
GO
