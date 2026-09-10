CREATE TABLE [dbo].[HemodynamicMonitoringFunctionalUnits] (
    [Id]                       INT       IDENTITY (1, 1) NOT NULL,
    [IdHemodynamicMonitoringC] INT       NOT NULL,
    [UFUCODIGO]                CHAR (10) NOT NULL,
    CONSTRAINT [PK_HemodynamicMonitoringFunctionalUnits] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HemodynamicMonitoringC_ID] FOREIGN KEY ([IdHemodynamicMonitoringC]) REFERENCES [dbo].[HemodynamicMonitoringC] ([Id]),
    CONSTRAINT [FK_INUNIFUNC_UFUCODIGO] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10), identificador único de la unidad funcional o centro de atención donde se realiza monitoreo hemodinámico. Vinculado a tabla INUNIFUNC para referencia de unidades de servicio (urgencia, UCI, quirófano, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de relación con registro de monitoreo hemodinámico (tabla HemodynamicMonitoringC). Vincula este registro de unidad funcional con el monitoreo hemodinámico principal (presión arterial, frecuencia cardíaca, saturación de oxígeno).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HemodynamicMonitoringC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY) de la asociación entre monitoreo hemodinámico y unidad funcional. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los registros de monitoreo hemodinámico con las unidades funcionales donde se realiza el seguimiento. Permite saber en qué unidad funcional (sala, servicio o área clínica) se registró cada monitoreo hemodinámico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringFunctionalUnits';
