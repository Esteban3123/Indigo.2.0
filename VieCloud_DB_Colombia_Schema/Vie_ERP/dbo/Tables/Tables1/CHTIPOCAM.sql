CREATE TABLE [dbo].[CHTIPOCAM] (
    [CODTIPCAM] CHAR (3)     NOT NULL,
    [DESTIPCAM] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPOCAM] PRIMARY KEY CLUSTERED ([CODTIPCAM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y conformidad (NUMERIC 18); flag para rastrear cambios auditados, cumplimiento normativo o estado de verificación del tipo de cama en el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del tipo de cama (CHAR 40); nombre o etiqueta que identifica la categoría de cama: cama estándar, cama con control, cama de aislamiento, cama UCI, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'DESTIPCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tipo de Cama    Nota: Este campo permite identificar el tipo de cama, ejemplo Cama Estandar, Cama con Control, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'DESTIPCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'DESTIPCAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de cama; identificador único (CHAR 3) que clasifica camas hospitalarias: estándar, con control, UCI, cuidados intensivos, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'CODTIPCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'CODTIPCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'CODTIPCAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de cama hospitalaria (por ejemplo: cama general, UCI, pediátrica, etc.) utilizados para clasificar las camas disponibles en los servicios de internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM';
