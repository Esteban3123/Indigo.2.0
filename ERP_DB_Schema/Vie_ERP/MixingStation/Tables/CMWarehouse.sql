CREATE TABLE [MixingStation].[CMWarehouse] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdMixingStation] INT     NOT NULL,
    [IdWarehouse]     INT     NOT NULL,
    [WarehouseType]   TINYINT NOT NULL,
    [StateWH]         BIT     NOT NULL,
    CONSTRAINT [PK_CMWarehouse] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMWarehouse_CMConfiguration] FOREIGN KEY ([IdMixingStation]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMWarehouse_WareHouse] FOREIGN KEY ([IdWarehouse]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMWarehouse] NOCHECK CONSTRAINT [FK_CMWarehouse_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMWarehouse] NOCHECK CONSTRAINT [FK_CMWarehouse_WareHouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'StateWH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de almacen:  1-Materia Prima Stock  2-Almacén  3-En Proceso  4-Terminado  5-Inventario de Control  6-Inventario de Remanente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'WarehouseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del maestro de almacenes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdWarehouse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de Estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'IdMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMWarehouse', @level2type = N'COLUMN', @level2name = N'Id';

