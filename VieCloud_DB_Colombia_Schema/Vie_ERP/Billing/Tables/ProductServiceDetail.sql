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



GO



GO



GO



GO



GO
-- Índice para optimizar consultas por ServiceOrderDetailId (usado en ViewListRevenueControl)
CREATE NONCLUSTERED INDEX [IX_ProductServiceDetail_ServiceOrderDetailId]
ON [Billing].[ProductServiceDetail] ([ServiceOrderDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje decimal (DECIMAL 5,2, default=0) de descuento aplicado al precio del producto o servicio en este detalle. Reduce el valor facturableantes de liquidación a asegurador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina el porcentaje de descuento del detalle del servicio del producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico (TINYINT, default=0) que establece la clase de tarifa aplicada: lista, contratada, regulada, particular. Determina el valor final a cobrar al paciente o asegurador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo de tarifa que se maneja.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico (TINYINT, default=0) que define el tipo de liquidación: forma en que se cobra/paga al prestador (por evento, por unidad, por paquete, por capítación). Usado en RIPS y contratación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo de liquidación que se realiza.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) del producto o servicio antes de aplicar descuentos. Base para cálculo de factura, glosa y liquidación a terceros (asegurador, paciente).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio del Servicio o Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, opcional) que referencia el identificador del producto en inventario (medicamento, insumo, dispositivo médico). Nullable si el detalle es un servicio sin producto físico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, opcional) que referencia la descripción contractual del CUPS asociado. Vincula el detalle al contrato y tarifa acordada con el asegurador o cliente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripcion Relacionada de cups si existe', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ContractDescriptionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, opcional) que referencia la entidad CUPS (código único de procedimientos en salud). Identifica el procedimiento, servicio o producto según catalogación de RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del CUPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, opcional) que referencia el detalle de la orden de servicio cuando se genera orden de atención (consulta, procedimiento, examen, imagen). Nullable si no hay orden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden del servicio cuando se crea una orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que referencia el detalle de dispensación farmacéutica (medicamento, fármaco entregado al paciente). Obligatorio en farmacia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la Dispensacion Farmaceutica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle de producto/servicio en facturación. Clave primaria que rastrea cada línea de cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos y servicios facturados: relaciona cada ítem dispensado (medicamento o servicio de salud) con su orden, contrato, tarifa y descuento aplicado en el proceso de liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductServiceDetail';
