CREATE TABLE [dbo].[ParamEducationFormatsVariablesC] (
    [Id]                            INT           IDENTITY (1, 1) NOT NULL,
    [IdParamEducationFormatsGroups] INT           NOT NULL,
    [Code]                          VARCHAR (3)   NOT NULL,
    [Name]                          VARCHAR (150) NULL,
    [ToolTip]                       VARCHAR (150) NULL,
    [Type]                          INT           NOT NULL,
    [ManejoDecimal]                 BIT           NULL,
    [CreationUser]                  CHAR (20)     NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              CHAR (20)     NULL,
    [ModificationDate]              DATETIME      NULL,
    CONSTRAINT [PK_ParamEducationFormatsVariablesC] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParamEducationFormatsVariablesC_ParamEducationFormatsGroups] FOREIGN KEY ([IdParamEducationFormatsGroups]) REFERENCES [dbo].[ParamEducationFormatsGroups] ([Id])
);


GO
ALTER TABLE [dbo].[ParamEducationFormatsVariablesC] NOCHECK CONSTRAINT [FK_ParamEducationFormatsVariablesC_ParamEducationFormatsGroups];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de la modificación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien modifica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario de creación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si maneja decimal el numérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ManejoDecimal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Boolean  2 - String  3 - Numérico  4 - Lista  5 - Fecha (dd/mm/yyyy)  6 - Fecha (mm/yyyy)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Tooltip de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'ToolTip';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Nombre de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Tiene relación con la tabla ParamEducationFormatsGroups', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'IdParamEducationFormatsGroups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsVariablesC', @level2type = N'COLUMN', @level2name = N'Id';

