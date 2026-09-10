CREATE TABLE [MixingStation].[StabilityTableDetailDilutionReferencesReconstitution] (
    [Id]                                   INT IDENTITY (1, 1) NOT NULL,
    [StabilityTableDetailDilutionId]       INT NOT NULL,
    [StabilityTableDetailReconstitutionId] INT NOT NULL,
    CONSTRAINT [PK_StabilityTableDetailDilutionReferencesReconstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailDilutionReferencesReconstitution_StabilityTableDetailDilution] FOREIGN KEY ([StabilityTableDetailDilutionId]) REFERENCES [MixingStation].[StabilityTableDetailDilution] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilutionReferencesReconstitution_StabilityTableDetailReconstitution] FOREIGN KEY ([StabilityTableDetailReconstitutionId]) REFERENCES [MixingStation].[StabilityTableDetailReconstitution] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre los detalles de dilución y los detalles de reconstitución de las tablas de estabilidad en la estación de mezclas. Vincula qué referencias de reconstitución aplican a cada dilución registrada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de relación dilución-reconstitución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de dilución de la tabla de estabilidad al que pertenece esta asociación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailDilutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailDilutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de reconstitución de la tabla de estabilidad vinculado con la dilución correspondiente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailReconstitutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'StabilityTableDetailDilutionReferencesReconstitution', @level2type = N'COLUMN', @level2name = N'StabilityTableDetailReconstitutionId';
