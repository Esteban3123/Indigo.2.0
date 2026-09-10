CREATE TABLE [dbo].[INTERPDETA] (
    [AUTO]      INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [NUMEFOLIO] CHAR (10) NULL,
    [ORDTIP]    CHAR (3)  NULL,
    [AUTOPATP]  INT       NOT NULL,
    CONSTRAINT [PK_INTERPDETA] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDICE_INTERPDETA]
    ON [dbo].[INTERPDETA]([NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de patrón o plantilla de interpretación (sin documentación disponible en Crystal ni desarrolladores). Posible FK a tabla de patrones diagnósticos o de respuesta estándar en laboratorio/imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTOPATP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTOPATP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTOPATP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden de interpretación: AMB (Ambulatorio) o INT (Intrahospitalario). Clasifica el contexto de atención donde se realiza el examen o procedimiento diagnosticado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Orden AMB-Ambulatorio    INT-Intrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'ORDTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o documento de la orden/solicitud de interpretación. Referencia externa para trazabilidad administrativa y búsqueda de solicitudes en exámenes (laboratorio, imagen, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Servicio IPS (Institución Prestadora de Salud), clasificación RIPS. Identifica la unidad funcional o departamento (laboratorio, imagenología, urgencias) que genera la interpretación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo vinculado a la tabla INTERPCABE (FK implícita). Referencia a la cabecera de interpretación de estudios/exámenes. Agrupador de detalles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del consecutivo de INTERPCABE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) que sirve como clave primaria interna de la interpretación detallada. Auditoría y trazabilidad del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de interpretaciones o líneas de servicios asociadas a un proceso de facturación o liquidación, vinculando cada servicio (CUPS) con su concepto de cobro y el encabezado del pago o patrón correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERPDETA';
