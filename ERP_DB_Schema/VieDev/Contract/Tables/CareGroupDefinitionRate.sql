CREATE TABLE [Contract].[CareGroupDefinitionRate] (
    [Id]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]      INT      NOT NULL,
    [DefinitionRateId] INT      NOT NULL,
    [InitialDate]      DATETIME NOT NULL,
    [EndDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_CareGroupDefinitionRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupDefinitionRate_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_CareGroupDefinitionRate_DefinitionRate] FOREIGN KEY ([DefinitionRateId]) REFERENCES [Contract].[DefinitionRate] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroupDefinitionRate] NOCHECK CONSTRAINT [FK_CareGroupDefinitionRate_CareGroup];


GO
ALTER TABLE [Contract].[CareGroupDefinitionRate] NOCHECK CONSTRAINT [FK_CareGroupDefinitionRate_DefinitionRate];

