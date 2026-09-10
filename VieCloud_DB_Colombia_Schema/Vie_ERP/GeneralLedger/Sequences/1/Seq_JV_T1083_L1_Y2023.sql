CREATE SEQUENCE [GeneralLedger].[Seq_JV_T1083_L1_Y2023]
    AS BIGINT
    START WITH 94
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores secuenciales de tipo BIGINT para los asientos de diario (Journal Vouchers) del libro 1 (L1) correspondiente al período fiscal 2023, asociados a la transacción o tipo de documento T1083 dentro del esquema de Contabilidad General. La secuencia inicia en 94, lo que indica que ya se habían registrado 93 entradas previas al momento de su creación.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L1_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T1083_L1_Y2023';
GO
