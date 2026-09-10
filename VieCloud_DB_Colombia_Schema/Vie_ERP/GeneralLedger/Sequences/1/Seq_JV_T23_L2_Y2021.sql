CREATE SEQUENCE [GeneralLedger].[Seq_JV_T23_L2_Y2021]
    AS BIGINT
    START WITH 5868
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos y secuenciales de tipo `bigint` para los asientos contables (Journal Vouchers) correspondientes al tipo 23, nivel 2, del ejercicio fiscal 2021, dentro del esquema de contabilidad general. La secuencia inicia en 199208, lo que indica registros previos ya existentes para ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T23_L2_Y2021';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T23_L2_Y2021';
GO
