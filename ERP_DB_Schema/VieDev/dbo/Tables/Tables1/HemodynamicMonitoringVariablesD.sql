CREATE TABLE [dbo].[HemodynamicMonitoringVariablesD] (
    [Id]                                INT          IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringVariablesC] INT          NOT NULL,
    [Code]                              VARCHAR (3)  NOT NULL,
    [Name]                              VARCHAR (50) NOT NULL,
    [Description]                       VARCHAR (50) NULL,
    [Status]                            BIT          NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringVariablesD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HemodynamicMonitoringVariablesC_Id_IHemodynamicMonitoringVariablesD] FOREIGN KEY ([IdHemodynamicMonitoringVariablesC]) REFERENCES [dbo].[HemodynamicMonitoringVariablesC] ([Id])
);


GO
ALTER TABLE [dbo].[HemodynamicMonitoringVariablesD] NOCHECK CONSTRAINT [FK_HemodynamicMonitoringVariablesC_Id_IHemodynamicMonitoringVariablesD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la variable de monitoreo hemodinamico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla HemodynamicMonitoringVariablesC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringVariablesD', @level2type = N'COLUMN', @level2name = N'Id';

