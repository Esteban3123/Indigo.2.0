CREATE SEQUENCE [GeneralLedger].[Seq_JV_T34_L1_Y2021]
    AS BIGINT
    START WITH 767
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos para asientos de diario (Journal Vouchers) del libro mayor general, específicamente para la tabla o lote identificado como T34, nivel L1, correspondiente al año fiscal 2021. La secuencia inicia en 1290, lo que indica que ya existían registros previos al crearla.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L1_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T34_L1_Y2021';
GO
