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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (0/1) que marca si la variable de monitoreo hemodinámico es obligatoria o requerida en el grupo. Valores: 0=No obligatoria, 1=Obligatoria. Controla si el usuario debe completar/registrar esta variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para poner obligatoria la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Obligatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (0/1) que controla la visibilidad de la variable en la interfaz del grupo de monitoreo hemodinámico. Valores: 0=Oculta, 1=Visible. Determina si la variable se muestra al usuario en pantalla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Visible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para poner visible el variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Visible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Visible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la variable de monitoreo hemodinámico específica. Referencia a [HemodynamicMonitoringVariablesC]. Vincula cada variable (presión arterial, frecuencia cardíaca, gasto cardíaco, etc.) al grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringVariablesC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del grupo de monitoreo hemodinámico contenedor. Referencia a [HemodynamicMonitoringC]. Agrupa múltiples variables de hemodinamia asociadas a un protocolo o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cabecera del Grupo de Hemodinamia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'IdHemodynamicMonitoringC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY) de la asociación variable-grupo. Clave primaria clustering. Generado automáticamente, sin replicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa las variables de monitoreo hemodinámico que pertenecen a cada grupo de seguimiento clínico, indicando cuáles variables son visibles y cuáles son de registro obligatorio dentro del formulario de monitoreo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HemodynamicMonitoringGroupsVariables';
