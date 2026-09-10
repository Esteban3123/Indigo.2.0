CREATE TABLE [dbo].[INCOSNOCA] (
    [CODCONOCA]  CHAR (3)     NOT NULL,
    [DESCONOCA]  CHAR (40)    NOT NULL,
    [TIPCOSNOCA] CHAR (1)     NULL,
    [INDAUDFOR]  NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INCOSNOCA] PRIMARY KEY CLUSTERED ([CODCONOCA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de Auditoría y Formulario (NUMERIC 18), campo reservado del sistema para rastreo interno, marcas de auditoría, flags de validación o control de integridad en procesos de no calidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Costo de No Calidad (CHAR 1): clasificación que segmenta costos en Estancia (1), Medicamentos e Insumos (2), o Procedimientos y Servicios (3); usado para análisis de calidad y gestión presupuestal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'TIPCOSNOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Costos de No Calidad  1: Estancia  2: Medicamentos e Insumos  3: Procedimientos o Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'TIPCOSNOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'TIPCOSNOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del Costo de No Calidad, nombre o detalle explicativo del concepto de costo derivado de errores, complicaciones, reingresos, rehospitalizaciones o incidentes de seguridad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'DESCONOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Costos de No Calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'DESCONOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'DESCONOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Costo de No Calidad, identificador único (CHAR 3) de la categoría o tipo de costo generado por deficiencias en calidad asistencial, reembolso o servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'CODCONOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Costos de No Calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'CODCONOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA', @level2type = N'COLUMN', @level2name = N'CODCONOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de conceptos de novedades o causales de notas de crédito/débito utilizadas en facturación y contabilidad. Cada registro define un tipo de concepto con su código, descripción y clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCOSNOCA';
