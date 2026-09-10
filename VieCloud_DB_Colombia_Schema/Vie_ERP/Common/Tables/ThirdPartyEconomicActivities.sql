CREATE TABLE [Common].[ThirdPartyEconomicActivities] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [ThirdPartyId]       INT NOT NULL,
    [EconomicActivityId] INT NOT NULL,
    [Defect]             BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ThirdPartyEconomicActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyEconomicActivities_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ThirdPartyEconomicActivities_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyEconomicActivities] NOCHECK CONSTRAINT [FK_ThirdPartyEconomicActivities_EconomicActivity];




GO
ALTER TABLE [Common].[ThirdPartyEconomicActivities] NOCHECK CONSTRAINT [FK_ThirdPartyEconomicActivities_EconomicActivity];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si la actividad económica será seleccionada por defecto. Valores: 0=No es predeterminada, 1=Sí es predeterminada. Relevante para terceros (proveedores, entidades, prestadores) con múltiples actividades económicas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Defect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la actividad economica sera marcada "Por Defecto"', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Defect';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Defect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la actividad económica vinculada. Referencia a [Common].[EconomicActivity]. Clasifica el tipo de operación económica del tercero (ej: farmacia, laboratorio, clínica, distribuidora de medicamentos).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Actividad economica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del tercero o entidad externa. Referencia a [Common].[ThirdParty]. Puede ser proveedor, acreedor, prestador de servicios de salud, o entidad contratante en el sistema RIPS y facturación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) del registro de asociación entre tercero y actividad económica. Clave primaria de la tabla [Common].[ThirdPartyEconomicActivities].', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de actividades económicas asociadas a terceros (proveedores, contratistas, empresas). Permite vincular uno o varios códigos de actividad económica (CIIU) a cada tercero, indicando cuál es la actividad por defecto.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities';
