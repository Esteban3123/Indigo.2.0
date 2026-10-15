CREATE TABLE [dbo].[HemodynamicMonitoringFunctionalUnits] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringC] INT       NOT NULL,
    [UFUCODIGO]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringFunctionalUnits] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HemodynamicMonitoringC_ID] FOREIGN KEY ([IdHemodynamicMonitoringC]) REFERENCES [dbo].[HemodynamicMonitoringC] ([Id]),
    CONSTRAINT [FK_INUNIFUNC_UFUCODIGO] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HemodynamicMonitoringFunctionalUnits] NOCHECK CONSTRAINT [FK_HemodynamicMonitoringC_ID];


GO
ALTER TABLE [dbo].[HemodynamicMonitoringFunctionalUnits] NOCHECK CONSTRAINT [FK_INUNIFUNC_UFUCODIGO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla HemodynamicMonitoringC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'Id';

