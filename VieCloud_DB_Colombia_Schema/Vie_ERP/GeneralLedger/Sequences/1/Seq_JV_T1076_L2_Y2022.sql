CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1076_L2_Y2022]
    AS BIGINT
    START WITH 628
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para asientos de diario (Journal Voucher) asociados al libro mayor (GeneralLedger), específicamente para la tabla o lote del centro de costo/entidad T1076, nivel contable L2, correspondiente al ejercicio fiscal 2022. Inicia en 628, incrementando de uno en uno sin caché.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L2_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1076_L2_Y2022';
GO
