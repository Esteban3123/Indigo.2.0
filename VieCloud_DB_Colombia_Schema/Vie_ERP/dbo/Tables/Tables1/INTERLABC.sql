CREATE TABLE [dbo].[INTERLABC] (
    [AUTO]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_INTERLABC] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);






GO
CREATE NONCLUSTERED INDEX [_dta_index_INTERLABC_7_585821199__K1_K2]
    ON [dbo].[INTERLABC]([AUTO] ASC, [ORDEN_INDIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABC__ORDEN_INDIGO__INC__AUTO]
    ON [dbo].[INTERLABC]([ORDEN_INDIGO] ASC)
    INCLUDE([AUTO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de orden de laboratorio: concatenación del código del paciente (cédula/identificación) con número de folio secuencial. Clave para rastrear solicitudes de análisis clínicos, exámenes de laboratorio y resultados en el sistema Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de incremento automático (IDENTITY). Clave primaria técnica de la tabla INTERLABC para garantizar unicidad de registros de órdenes de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC', @level2type = N'COLUMN', @level2name = N'AUTO';


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de integración con laboratorio externo: registra el vínculo entre las órdenes de laboratorio generadas en Indigo y el sistema externo de laboratorio, permitiendo rastrear qué órdenes fueron enviadas o sincronizadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABC';
