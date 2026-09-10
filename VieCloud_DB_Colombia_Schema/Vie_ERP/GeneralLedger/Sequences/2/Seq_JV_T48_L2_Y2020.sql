CREATE SEQUENCE [GeneralLedger].[Seq_JV_T48_L2_Y2020]
    AS BIGINT
    START WITH 9
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para asientos contables (Journal Vouchers) del libro mayor general, correspondientes al período fiscal 2020, tabla 48, nivel 2. Inicia en 41387, incrementa de uno en uno sin ciclo ni caché, lo que indica que ya existían 41 386 registros previos al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L2_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T48_L2_Y2020';
GO
