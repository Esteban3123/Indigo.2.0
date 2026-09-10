CREATE SEQUENCE [GeneralLedger].[Seq_JV_T36_L2_Y2023]
    AS BIGINT
    START WITH 16714
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo `bigint` para los asientos contables (Journal Vouchers) del libro mayor, correspondientes a la tabla 36, libro 2 del año fiscal 2023. La secuencia inicia en 40261, incrementa de uno en uno sin ciclo ni caché, lo que garantiza valores únicos y auditables para registros contables de ese período específico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L2_Y2023';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T36_L2_Y2023';
GO
