CREATE TABLE [dbo].[ParamEducationFormatsVariablesD] (
    [Id]                                INT           IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsVariablesC] INT           NOT NULL,
    [Code]                              VARCHAR (3)   NOT NULL,
    [Name]                              VARCHAR (150) NOT NULL,
    [Description]                       VARCHAR (300) NOT NULL,
    [Status]                            BIT           NOT NULL,
    CONSTRAINT [PK_ParamEducationFormatsVariablesD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsVariablesD_IdParamEducationFormatsVariablesC] FOREIGN KEY ([IdParamEducationFormatsVariablesC]) REFERENCES [dbo].[ParamEducationFormatsVariablesC] ([Id])
);


GO
ALTER TABLE [dbo].[ParamEducationFormatsVariablesD] NOCHECK CONSTRAINT [FK_ParamEducationFormatsVariablesD_IdParamEducationFormatsVariablesC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado  1  =Actico     0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda   la descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda los id de parametros educacion formatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesD', @level2type = N'COLUMN', @level2name = N'Id';

