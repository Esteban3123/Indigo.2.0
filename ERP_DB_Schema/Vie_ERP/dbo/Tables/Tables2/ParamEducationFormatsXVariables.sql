CREATE TABLE [dbo].[ParamEducationFormatsXVariables] (
    [Id]                                INT IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsC]          INT NOT NULL,
    [IdParamEducationFormatsVariablesC] INT NOT NULL,
    [Required]                          BIT NOT NULL,
    [Status]                            BIT NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsXVariables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsC] FOREIGN KEY ([IdParamEducationFormatsC]) REFERENCES [dbo].[ParamEducationFormatsC] ([Id]),
    CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsVariablesC] FOREIGN KEY ([IdParamEducationFormatsVariablesC]) REFERENCES [dbo].[ParamEducationFormatsVariablesC] ([Id])
);


GO
ALTER TABLE [dbo].[ParamEducationFormatsXVariables] NOCHECK CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsC];


GO
ALTER TABLE [dbo].[ParamEducationFormatsXVariables] NOCHECK CONSTRAINT [FK_ParamEducationFormatsXVariables_ParamEducationFormatsVariablesC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  estado  1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Requerido  1  = Si       0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Required';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la identidad  "ID" de los parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsXVariables', @level2type = N'COLUMN', @level2name = N'Id';

