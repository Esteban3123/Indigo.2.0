CREATE SEQUENCE [GeneralLedger].[Seq_JV_T20_L1_Y2020]
    AS BIGINT
    START WITH 129
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Secuencia numérica del esquema `GeneralLedger` que genera identificadores únicos correlativos para asientos de diario (Journal Vouchers) del tipo 20, libro 1 (L1), correspondientes al año 2020. Inicia en 20504, incrementa de uno en uno sin ciclo ni caché, lo que indica que los primeros 20503 registros de esa combinación ya fueron emitidos previamente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2020';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T20_L1_Y2020';
GO
