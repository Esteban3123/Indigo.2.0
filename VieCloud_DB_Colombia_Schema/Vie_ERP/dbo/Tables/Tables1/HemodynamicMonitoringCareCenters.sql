CREATE TABLE [dbo].[HemodynamicMonitoringCareCenters] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringC] INT       NOT NULL,
    [CODCENATE]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringCareCenters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ADCENATEN_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HemodynamicMonitoringC_Id_HemodynamicMonitoringCareCenters] FOREIGN KEY ([IdHemodynamicMonitoringC]) REFERENCES [dbo].[HemodynamicMonitoringC] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o institución prestadora de servicios de salud donde se realiza el monitoreo hemodinámico; referencia a tabla ADCENATEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o registro maestro de monitoreo hemodinámico; clave foránea que vincula con HemodynamicMonitoringC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY) de la relación entre monitoreo hemodinámico y centro de atención; clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los registros de monitoreo hemodinámico con los centros de atención habilitados para realizarlos. Permite saber en qué sedes o centros de atención está disponible o se registró cada protocolo de monitoreo hemodinámico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringCareCenters';
