CREATE SEQUENCE [GeneralLedger].[Seq_JV_T5_L1_Y2022]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos secuenciales de tipo bigint para asientos de diario (Journal Vouchers) del tipo 5, libro 1, correspondientes al año 2022, dentro del esquema de contabilidad general. Inicia en 58207, incrementando de uno en uno sin ciclo ni caché, lo que sugiere que ya existían registros previos al crear la secuencia.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T5_L1_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T5_L1_Y2022';
GO
