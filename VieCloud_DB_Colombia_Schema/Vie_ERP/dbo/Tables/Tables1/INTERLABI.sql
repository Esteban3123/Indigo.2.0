CREATE TABLE [dbo].[INTERLABI] (
    [AUTO]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_INTERLABI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de orden laboratorial en Indigo Vie Cloud, concatenación de código del paciente (cédula/identificación) y número de folio/secuencia de la solicitud de examen o análisis clínico. Clave de búsqueda para rastrear requisiciones de laboratorio, RIPS y resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Indigo  Paciente concatenado con el numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autonumérica con identidad incremental (IDENTITY 1,1). Identificador técnico interno del registro de interfaz de laboratorio, usado para relaciones de tablas y auditoría interna del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico Identity', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de integración con laboratorio externo: almacena la correspondencia entre órdenes de exámenes de laboratorio generadas en Indigo y su identificador interno de seguimiento, permitiendo rastrear cada orden enviada al laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABI';
