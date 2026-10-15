CREATE TABLE [MixingStation].[ContractExternalClientsDefinitionRate] (
    [Id]                        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractExternalClientsId] INT      NOT NULL,
    [DefinitionRateId]          INT      NOT NULL,
    [InitialDate]               DATETIME NOT NULL,
    [EndDate]                   DATETIME NOT NULL,
    CONSTRAINT [PK_ContractExternalClientsDefinitionRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClientsDefinitionRate_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDefinitionRate_DefinitionRate] FOREIGN KEY ([DefinitionRateId]) REFERENCES [Contract].[DefinitionRate] ([Id])
);


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDefinitionRate] NOCHECK CONSTRAINT [FK_ContractExternalClientsDefinitionRate_ContractExternalClients];


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDefinitionRate] NOCHECK CONSTRAINT [FK_ContractExternalClientsDefinitionRate_DefinitionRate];

