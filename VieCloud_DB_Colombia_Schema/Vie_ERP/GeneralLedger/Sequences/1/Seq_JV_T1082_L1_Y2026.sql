CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1082_L1_Y2026]
    AS BIGINT
    START WITH 1
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo BIGINT para los registros de asientos contables (Journal Vouchers) asociados al libro contable L1 del período fiscal 2026, dentro del esquema GeneralLedger. La secuencia comienza en 1 con incremento de 1 y sin caché, garantizando valores consecutivos sin huecos para la entidad T1082.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1082_L1_Y2026';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1082_L1_Y2026';
GO
