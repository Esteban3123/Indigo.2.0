CREATE TABLE [Inventory].[PurchaseOrderAvailability] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]      INT             NOT NULL,
    [AvailabilityDetailId] INT             NOT NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PurchaseOrderAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_PurchaseOrderAvailability_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [Inventory].[PurchaseOrder] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor decimal (18,2) que se establece para la disponibilidad de presupuesto en la orden de compra; representa cantidad, monto o porcentaje según el detalle asociado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se establece para la disponibilidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de disponibilidad de presupuesto [Budget].[AvailabilityDetail]; vincula configuración de disponibilidad presupuestaria a la orden de compra.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la disponibilidad de presupuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de la orden de compra [Inventory].[PurchaseOrder]; referencia la OC madre a la cual se asigna disponibilidad presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'PurchaseOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única (INT IDENTITY) del registro de disponibilidad presupuestaria en orden de compra; clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la disponibilidad presupuestaria o de recursos asociada a cada orden de compra, vinculando la orden con el detalle de disponibilidad y el valor comprometido o disponible.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderAvailability';
