CREATE TABLE [dbo].[SEGmodulu] (
    [indmodulo] CHAR (2)  NOT NULL,
    [indmoddes] CHAR (80) NOT NULL,
    CONSTRAINT [PK_SEGmodulu] PRIMARY KEY CLUSTERED ([indmodulo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del módulo en INDIGO (CHAR 80), etiqueta legible del componente funcional del sistema (ej: Facturación, RIPS, Pacientes, Laboratorio, Farmacia, Urgencias, Hospitalizacion). Términos relacionados: descripción de módulo, nombre del componente, funcionalidad del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmoddes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmoddes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmoddes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del módulo en INDIGO (CHAR 2), identificador único de la funcionalidad del sistema ERP/EHR; clave primaria. Equivalentes: código de módulo, identificador de módulo, módulo ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmodulo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmodulo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmodulo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de módulos del sistema de seguridad. Registra los módulos funcionales disponibles en el ERP/EHR, identificados por un código y su nombre descriptivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu';
