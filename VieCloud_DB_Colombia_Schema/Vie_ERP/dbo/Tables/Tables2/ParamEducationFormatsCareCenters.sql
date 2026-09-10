CREATE TABLE [dbo].[ParamEducationFormatsCareCenters] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC] INT       NOT NULL,
    [CODCENATE]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsCareCenters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsCareCenters_ParamEducationFormatsC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, clínica o institución de salud). Identificador único que vincula los formatos educativos a centros específicos. Búsqueda: centro atención, IPS, unidad funcional, institución salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del parámetro de formato educativo. Referencia a la tabla ParamEducationFormatsC que contiene definiciones de formatos educativos en el sistema. Tipo: INT, clave foránea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) y consecutivo secuencial de la tabla. Clave primaria con IDENTITY(1,1). Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formatos de educación parametrizados con los centros de atención habilitados para usarlos. Permite controlar qué formatos educativos están disponibles en cada sede o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters';
