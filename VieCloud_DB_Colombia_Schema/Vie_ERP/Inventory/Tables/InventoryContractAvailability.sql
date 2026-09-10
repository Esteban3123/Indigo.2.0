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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico decimal (18,2) que establece la cantidad o monto disponible en el contrato para la línea de presupuesto indicada. Usado en cálculos de disponibilidad, consumo y glosa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de disponibilidad presupuestaria. Referencia FK a [Budget].[AvailabilityDetail]. Especifica la línea de presupuesto o disponibilidad asociada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato de inventario (cabecera). Referencia FK a [Inventory].[InventoryContract]. Vincula la disponibilidad al contrato padre.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'InventoryContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de disponibilidad de contrato de inventario. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de disponibilidad de inventario por contrato. Vincula cada contrato de inventario con su detalle de disponibilidad y el valor o cantidad disponible asociado, permitiendo conocer qué recursos o artículos están disponibles según cada contrato.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContractAvailability';
