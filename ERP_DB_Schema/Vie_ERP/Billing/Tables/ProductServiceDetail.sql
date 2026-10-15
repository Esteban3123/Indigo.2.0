CREATE TABLE [Billing].[ProductServiceDetail] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDetailId] INT             NOT NULL,
    [ServiceOrderDetailId]             INT             NULL,
    [CUPSEntityId]                     INT             NULL,
    [ContractDescriptionsId]           INT             NULL,
    [ProductId]                        INT             NULL,
    [Price]                            DECIMAL (18, 2) NOT NULL,
    [LiquidationType]                  TINYINT         CONSTRAINT [DF__ProductSe__Liqui__6B7F7F9E] DEFAULT ((0)) NOT NULL,
    [RateType]                         TINYINT         CONSTRAINT [DF__ProductSe__RateT__6C73A3D7] DEFAULT ((0)) NOT NULL,
    [DiscountPercentage]               DECIMAL (5, 2)  CONSTRAINT [DF__ProductSe__Disco__4ADDA5E2] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductServiceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductServiceDetail_ContractDescriptions] FOREIGN KEY ([ContractDescriptionsId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_ProductServiceDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ProductServiceDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductServiceDetail_PharmaceuticalDispensingDetail] FOREIGN KEY ([PharmaceuticalDispensingDetailId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_ProductServiceDetail_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id])
);


GO
ALTER TABLE [Billing].[ProductServiceDetail] NOCHECK CONSTRAINT [FK_ProductServiceDetail_ContractDescriptions];


GO
ALTER TABLE [Billing].[ProductServiceDetail] NOCHECK CONSTRAINT [FK_ProductServiceDetail_CUPSEntity];


GO
ALTER TABLE [Billing].[ProductServiceDetail] NOCHECK CONSTRAINT [FK_ProductServiceDetail_InventoryProduct];


GO
ALTER TABLE [Billing].[ProductServiceDetail] NOCHECK CONSTRAINT [FK_ProductServiceDetail_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Billing].[ProductServiceDetail] NOCHECK CONSTRAINT [FK_ProductServiceDetail_ServiceOrderDetail];


GO
-- Índice para optimizar consultas por ServiceOrderDetailId (usado en ViewListRevenueControl)
CREATE NONCLUSTERED INDEX [IX_ProductServiceDetail_ServiceOrderDetailId]
ON [Billing].[ProductServiceDetail] ([ServiceOrderDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina el porcentaje de descuento del detalle del servicio del producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tipo de tarifa que se maneja.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tipo de liquidación que se realiza.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio del Servicio o Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripcion Relacionada de cups si existe', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden del servicio cuando se crea una orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la Dispensacion Farmaceutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Id';

