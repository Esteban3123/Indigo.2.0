CREATE TABLE [dbo].[ParamEducationFormatsCareCenters] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC] INT       NOT NULL,
    [CODCENATE]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsCareCenters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsCareCenters_ParamEducationFormatsC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id])
);


GO
ALTER TABLE [dbo].[ParamEducationFormatsCareCenters] NOCHECK CONSTRAINT [FK_ParamEducationFormatsCareCenters_ParamEducationFormatsC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsCareCenters', @level2type = N'COLUMN', @level2name = N'Id';

