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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la definicion de la tarifa', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'Id';

