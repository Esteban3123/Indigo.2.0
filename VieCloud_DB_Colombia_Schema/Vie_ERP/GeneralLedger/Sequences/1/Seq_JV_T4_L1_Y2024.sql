CREATE SEQUENCE [GeneralLedger].[Seq_JV_T4_L1_Y2024]
    AS BIGINT
    START WITH 7
    INCREMENT BY 1
    MINVALUE 0
    NO CACHE;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genera identificadores únicos correlativos de tipo bigint para los asientos de diario (Journal Vouchers) correspondientes al Tipo 4, Libro 1 del año 2024, dentro del esquema de Contabilidad General. La secuencia inicia en 687545, incrementa de uno en uno y no tiene ciclo, garantizando unicidad para los registros contables de ese período.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L1_Y2024';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'SEQUENCE', @level1name=N'Seq_JV_T4_L1_Y2024';
GO
