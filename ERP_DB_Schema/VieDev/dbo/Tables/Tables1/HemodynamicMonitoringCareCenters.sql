CREATE TABLE [dbo].[HemodynamicMonitoringCareCenters] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringC] INT       NOT NULL,
    [CODCENATE]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringCareCenters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ADCENATEN_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HemodynamicMonitoringC_Id_HemodynamicMonitoringCareCenters] FOREIGN KEY ([IdHemodynamicMonitoringC]) REFERENCES [dbo].[HemodynamicMonitoringC] ([Id])
);


GO
ALTER TABLE [dbo].[HemodynamicMonitoringCareCenters] NOCHECK CONSTRAINT [FK_ADCENATEN_CODCENATE];


GO
ALTER TABLE [dbo].[HemodynamicMonitoringCareCenters] NOCHECK CONSTRAINT [FK_HemodynamicMonitoringC_Id_HemodynamicMonitoringCareCenters];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'Id';

