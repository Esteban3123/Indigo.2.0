CREATE TABLE [MixingStation].[CampaignRawMaterial] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusId] INT             NOT NULL,
    [ProductId]                    INT             NULL,
    [AtcId]                        INT             NULL,
    [SupplieId]                    INT             NULL,
    [ExpendQuantity]               DECIMAL (18, 6) NOT NULL,
    [MeasurementUnitId]            INT             NOT NULL,
    [ProductValidationId]          INT             NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [BatchSerialId]                INT             NULL,
    CONSTRAINT [PK_CampaignRawMaterial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CampaignRawMaterial_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_InventoryProduct] FOREIGN KEY ([ProductValidationId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id]),
    CONSTRAINT [FK_CampaignRawMaterial_Supplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_ATC];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_Product];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_RequestPackageDetailStatus];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_Supplie];




GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_ATC];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_InventoryProduct];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_Product];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_RequestPackageDetailStatus];


GO
ALTER TABLE [MixingStation].[CampaignRawMaterial] NOCHECK CONSTRAINT [FK_CampaignRawMaterial_Supplie];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de los Productos entregados de Campaign Validation y utilizados en la Gestion de Materia ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductValidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida en la que se gasto el producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de materia prima gastada en el detalle del paquete', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ExpendQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del sumistro cuando el detalle del paquete es por suministro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del atc cuando el detalle del paquete es por atc', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del producto cuando el detalle del paquete es por producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica el packete en concreto que se esta produciendo ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CampaignRawMaterial', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';

