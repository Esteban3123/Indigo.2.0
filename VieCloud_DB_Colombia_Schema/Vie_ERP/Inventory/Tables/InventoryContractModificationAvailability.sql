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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor decimal (18,2) establecido para la disponibilidad; cantidad, monto o porcentaje de disponibilidad asignada en la modificación del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que referencia el detalle de disponibilidad presupuestaria; vincula a [Budget].[AvailabilityDetail]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que referencia la cabecera/encabezado de la modificación del contrato de inventario; vincula a [Inventory].[InventoryContractModification]', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la modificación del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de disponibilidad en modificación de contrato de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la disponibilidad de stock o capacidad asociada a cada modificación de contrato de inventario, indicando el detalle de disponibilidad y el valor o cantidad disponible para cada modificación contractual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractModificationAvailability';
