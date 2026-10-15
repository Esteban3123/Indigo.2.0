CREATE TABLE [ClinicalParameters].[HistoryVariablesList] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [IdHistoryVariables] INT           NOT NULL,
    [Code]               VARCHAR (3)   NOT NULL,
    [Name]               VARCHAR (100) NOT NULL,
    [Description]        VARCHAR (100) NOT NULL,
    [Status]             BIT           NOT NULL,
    CONSTRAINT [PK_HistoryVariablesList] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la lista de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la lista de variables históricas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de lista de variables historicas', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo de la tabla HistoryVariables', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'IdHistoryVariables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'ClinicalParameters', @level1type = N'TABLE', @level1name = N'HistoryVariablesList', @level2type = N'COLUMN', @level2name = N'Id';

