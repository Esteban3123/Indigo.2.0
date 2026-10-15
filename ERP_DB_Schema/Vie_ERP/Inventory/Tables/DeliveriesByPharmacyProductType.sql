CREATE TABLE [Inventory].[DeliveriesByPharmacyProductType] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [HCFARMEPDId]         INT          NOT NULL,
    [ProductType]         BIT          NOT NULL,
    [Quantity]            INT          NOT NULL,
    [ConcentrationByUnit] VARCHAR (20) NOT NULL,
    [TotalWeight]         VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_DeliveriesByPharmacyProductType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeliveriesByPharmacyProductType_HCFARMEPD] FOREIGN KEY ([HCFARMEPDId]) REFERENCES [dbo].[HCFARMEPD] ([ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso Total (Cantidades * Concentracion)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'TotalWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentracion por cada unidad de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ConcentrationByUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de producto seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de producto: False o 0 = Producto Transformado
																					True o 1 = Producto Comercial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'ProductType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla HCFARMEPD desde donde se realizo la solicitud a farmacia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'HCFARMEPDId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DeliveriesByPharmacyProductType', @level2type = N'COLUMN', @level2name = N'Id';

