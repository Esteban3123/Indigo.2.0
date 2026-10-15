CREATE TABLE [Inventory].[InventoryContractAvailability] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryContractId]  INT             NOT NULL,
    [AvailabilityDetailId] INT             NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InventoryContractAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContractAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_InventoryContractAvailability_InventoryContract] FOREIGN KEY ([InventoryContractId]) REFERENCES [Inventory].[InventoryContract] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContractAvailability] NOCHECK CONSTRAINT [FK_InventoryContractAvailability_AvailabilityDetail];


GO
ALTER TABLE [Inventory].[InventoryContractAvailability] NOCHECK CONSTRAINT [FK_InventoryContractAvailability_InventoryContract];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Id';

