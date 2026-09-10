CREATE TABLE [dbo].[INTERERRO] (
    [ORDEN_INDIGO] VARCHAR (20)  NOT NULL,
    [CODSERIPS]    VARCHAR (15)  NOT NULL,
    [OBSERVACION]  VARCHAR (250) NULL,
    CONSTRAINT [PK_INTERERRO] PRIMARY KEY CLUSTERED ([ORDEN_INDIGO] ASC, [CODSERIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de error detectado en la interfaz de integración; texto descriptivo del problema o incidencia ocurrida durante la sincronización de datos (VARCHAR 250, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Error Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (Institución Prestadora de Salud); identificador único del servicio o procedimiento según nomenclatura RIPS (VARCHAR 15, parte de clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden de servicio concatenada: código del paciente + número de folio; identificador único de la orden en el sistema Indigo Vie Cloud (VARCHAR 20, parte de clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Oder de Servicio Concatenado codigo Paciente y Nuemero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de errores de interoperabilidad: guarda las órdenes de Indigo que fallaron al integrarse con sistemas externos, junto con el servicio (CUPS) involucrado y la observación o mensaje de error generado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERERRO';
