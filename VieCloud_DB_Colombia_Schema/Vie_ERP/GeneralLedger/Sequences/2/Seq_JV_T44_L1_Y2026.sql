CREATE SEQUENCE [GeneralLedger].[Seq_JV_T44_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo `bigint` para los asientos de diario (Journal Vouchers) del libro contable 1 (L1), período fiscal 2026, asociados a la tabla T44 del esquema `GeneralLedger`. Inicia desde el valor 362, incrementando de uno en uno sin ciclo ni caché, lo que sugiere que ya existen 361 registros previos para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T44_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T44_L1_Y2026';
GO
