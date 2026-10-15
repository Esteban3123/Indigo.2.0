CREATE TABLE [MixingStation].[StabilityTableDetailDilutionReferencesReconstitution] (
    [Id]                                   INT IDENTITY (1, 1) NOT NULL,
    [StabilityTableDetailDilutionId]       INT NOT NULL,
    [StabilityTableDetailReconstitutionId] INT NOT NULL,
    CONSTRAINT [PK_StabilityTableDetailDilutionReferencesReconstitution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_StabilityTableDetailDilutionReferencesReconstitution_StabilityTableDetailDilution] FOREIGN KEY ([StabilityTableDetailDilutionId]) REFERENCES [MixingStation].[StabilityTableDetailDilution] ([Id]),
    CONSTRAINT [FK_StabilityTableDetailDilutionReferencesReconstitution_StabilityTableDetailReconstitution] FOREIGN KEY ([StabilityTableDetailReconstitutionId]) REFERENCES [MixingStation].[StabilityTableDetailReconstitution] ([Id])
);

