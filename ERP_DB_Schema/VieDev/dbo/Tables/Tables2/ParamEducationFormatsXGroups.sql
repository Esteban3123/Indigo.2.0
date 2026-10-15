CREATE TABLE [dbo].[ParamEducationFormatsXGroups] (
    [Id]                            INT IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC]      INT NOT NULL,
    [IdParamEducationFormatsGroups] INT NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsXGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsGroups] FOREIGN KEY ([IdParamEducationFormatsGroups]) REFERENCES [dbo].[ParamEducationFormatsGroups] ([Id]),
    CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsVariablesC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id])
);


GO
ALTER TABLE [dbo].[ParamEducationFormatsXGroups] NOCHECK CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsGroups];


GO
ALTER TABLE [dbo].[ParamEducationFormatsXGroups] NOCHECK CONSTRAINT [FK_ParamEducationFormatsXGroups_ParamEducationFormatsVariablesC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXGroups', @level2type = N'COLUMN', @level2name = N'Id';

