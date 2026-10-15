CREATE TABLE [MixingStation].[ContractExternalClientsDetail] (
    [Id]                        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractExternalClientsId] INT     NOT NULL,
    [Type]                      TINYINT NOT NULL,
    [ProductId]                 INT     NULL,
    [AtcId]                     INT     NULL,
    [SupplieId]                 INT     NULL,
    [CUPSEntityId]              INT     NULL,
    [SuppliedBy]                TINYINT NOT NULL,
    CONSTRAINT [PK_ContractExternalClientsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClientsDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDetail] NOCHECK CONSTRAINT [FK_ContractExternalClientsDetail_ATC];


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDetail] NOCHECK CONSTRAINT [FK_ContractExternalClientsDetail_ContractExternalClients];


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDetail] NOCHECK CONSTRAINT [FK_ContractExternalClientsDetail_CUPSEntity];


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDetail] NOCHECK CONSTRAINT [FK_ContractExternalClientsDetail_InventoryProduct];


GO
ALTER TABLE [MixingStation].[ContractExternalClientsDetail] NOCHECK CONSTRAINT [FK_ContractExternalClientsDetail_InventorySupplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suministrado por:  0. No Aplica  1. Cliente  2. Cliente y Central Mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SuppliedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:   1: Medicamento  2: Insumo  3: Producto de tipo otro  4: Servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Id';

