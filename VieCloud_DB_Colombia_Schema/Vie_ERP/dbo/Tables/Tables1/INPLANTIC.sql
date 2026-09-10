CREATE TABLE [dbo].[INPLANTIC] (
    [CODPLANTI] CHAR (5)     NOT NULL,
    [DESPLANTI] CHAR (30)    NOT NULL,
    [INDIDMENU] CHAR (3)     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INPLANTIL] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría numérico (NUMERIC 18) que registra la trazabilidad y control de cambios en la plantilla dentro del sistema Indigo Vie Cloud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del menú en Indigo (CHAR 3), referencia a la estructura de navegación y permisos del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDIDMENU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDIDMENU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDIDMENU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la plantilla (CHAR 30), define el nombre, propósito o etiqueta de la plantilla para usuarios finales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'DESPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'DESPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'DESPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la plantilla (CHAR 5, PK), identificador principal para Indigo Crystal.Net, referencia en formularios y configuraciones del ERP/EHR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Plantilla para Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'CODPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de plantillas o formatos del sistema. Registra los tipos de formularios, plantillas o esquemas utilizados en la configuración de menús y auditoría de formularios clínicos o administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC';
