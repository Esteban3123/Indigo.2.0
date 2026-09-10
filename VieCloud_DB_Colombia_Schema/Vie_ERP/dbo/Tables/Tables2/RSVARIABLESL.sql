CREATE TABLE [dbo].[RSVARIABLESL] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRSVARIABLE] INT           NOT NULL,
    [CODIGO]       VARCHAR (50)  NOT NULL,
    [NOMBRE]       VARCHAR (100) NOT NULL,
    [DESCRIPCION]  VARCHAR (500) NULL,
    [ESTADO]       BIT           NOT NULL,
    CONSTRAINT [PK_RSVARIABLESL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVARIABLESL_RSVARIABLES] FOREIGN KEY ([IDRSVARIABLE]) REFERENCES [dbo].[RSVARIABLES] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del item de lista; bit booleano (1=activo, 0=inactivo); control de disponibilidad en RIPS, recetas, procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Activo o Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario, nota o observación adicional del campo; texto descriptivo hasta 500 caracteres; aclaraciones de uso clínico o administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del item de la lista desplegable; etiqueta visible en formularios; identificación legible del valor (ej: tipo de diagnóstico, categoría de procedimiento, unidad funcional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre  Item de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único alfanumérico del item de lista; identificador técnico para integraciones RIPS, facturación, recetas; clave de referencia en consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Item de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la variable padre (FK a RSVARIABLES); relaciona el item a su lista maestra; agrupa items por tipo (ej: diagnósticos, procedimientos, centros de atención).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Variable padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY); clave primaria clustered; secuencial para cada registro en la tabla de items de lista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado de valores o ítems posibles para cada variable de resultados o indicadores del sistema de reportes. Permite parametrizar las opciones válidas que puede tomar una variable (por ejemplo, los niveles de una escala, categorías de respuesta o clasificaciones).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESL';
