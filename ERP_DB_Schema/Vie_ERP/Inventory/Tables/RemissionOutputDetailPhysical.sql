CREATE TABLE [Inventory].[RemissionOutputDetailPhysical] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionOutputDetailId] INT NOT NULL,
    [PhysicalInventoryId]     INT NOT NULL,
    [Quantity]                INT NOT NULL,
    [OutstandingQuantity]     INT NOT NULL,
    CONSTRAINT [PK_RemissionOutputDetailPhysical] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionOutputDetailPhysical_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_RemissionOutputDetailPhysical_RemissionOutputDetail] FOREIGN KEY ([RemissionOutputDetailId]) REFERENCES [Inventory].[RemissionOutputDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionOutputDetailPhysical] NOCHECK CONSTRAINT [FK_RemissionOutputDetailPhysical_PhysicalInventory];


GO
ALTER TABLE [Inventory].[RemissionOutputDetailPhysical] NOCHECK CONSTRAINT [FK_RemissionOutputDetailPhysical_RemissionOutputDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetailPhysical', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetailPhysical', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetailPhysical', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetailPhysical', @level2type = N'COLUMN', @level2name = N'RemissionOutputDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetailPhysical', @level2type = N'COLUMN', @level2name = N'Id';

