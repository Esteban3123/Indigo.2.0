CREATE TABLE [dbo].[HemodynamicMonitoringGroupsVariables] (
    [Id]                                INT IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringC]          INT NOT NULL,
    [IdHemodynamicMonitoringVariablesC] INT NOT NULL,
    [Visible]                           BIT CONSTRAINT [DF__Hemodynam__Visib__482CED17] DEFAULT ((0)) NOT NULL,
    [Obligatory]                        BIT CONSTRAINT [DF__Hemodynam__Oblig__49211150] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringGroupsVariables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HemodynamicMonitoringC_Id_] FOREIGN KEY ([IdHemodynamicMonitoringC]) REFERENCES [dbo].[HemodynamicMonitoringC] ([Id]),
    CONSTRAINT [FK_HemodynamicMonitoringC_Id_IdHemodynamicMonitoringVariablesC] FOREIGN KEY ([IdHemodynamicMonitoringVariablesC]) REFERENCES [dbo].[HemodynamicMonitoringVariablesC] ([Id])
);


GO
ALTER TABLE [dbo].[HemodynamicMonitoringGroupsVariables] NOCHECK CONSTRAINT [FK_HemodynamicMonitoringC_Id_];


GO
ALTER TABLE [dbo].[HemodynamicMonitoringGroupsVariables] NOCHECK CONSTRAINT [FK_HemodynamicMonitoringC_Id_IdHemodynamicMonitoringVariablesC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para poner obligatoria la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna para poner visible el variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Visible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cabecera del Grupo de Hemodinamia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Id';

