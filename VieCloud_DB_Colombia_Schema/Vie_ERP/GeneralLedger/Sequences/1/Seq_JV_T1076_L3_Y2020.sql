CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1076_L3_Y2020]
    AS BIGINT
    START WITH 60
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor general, específicamente para la tabla o partición correspondiente al tipo de transacción T1076, nivel 3 (L3), del ejercicio fiscal 2020. La secuencia inicia en 60, incrementando de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L3_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L3_Y2020';
GO
