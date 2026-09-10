CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1078_L3_Y2022]
    AS BIGINT
    START WITH 118966
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) correspondientes al nivel 3 (L3) del período fiscal 2022, asociados a la entidad o centro de costo T1078 dentro del esquema de contabilidad general. El valor inicial de 118966 refleja la continuidad de registros preexistentes para dicho período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L3_Y2022';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1078_L3_Y2022';
GO
