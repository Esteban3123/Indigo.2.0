CREATE TABLE [Inventory].[InventoryContractModificationAvailability] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryContractModificationId] INT             NOT NULL,
    [AvailabilityDetailId]            INT             NOT NULL,
    [Value]                           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InventoryContractModificationAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractModificationAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_InventoryContractModificationAvailability_InventoryContractModification] FOREIGN KEY ([InventoryContractModificationId]) REFERENCES [Inventory].[InventoryContractModification] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContractModificationAvailability] NOCHECK CONSTRAINT [FK_InventoryContractModificationAvailability_AvailabilityDetail];


GO
ALTER TABLE [Inventory].[InventoryContractModificationAvailability] NOCHECK CONSTRAINT [FK_InventoryContractModificationAvailability_InventoryContractModification];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la modificación del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Id';

