CREATE TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationId]                           INT             NOT NULL,
    [CareGroupId]                           INT             NOT NULL,
    [HealthAdministratorId]                 INT             NOT NULL,
    [ThirdPartyId]                          INT             NULL,
    [ProductId]                             INT             NOT NULL,
    [WarehouseId]                           INT             NOT NULL,
    [Quantity]                              INT             NOT NULL,
    [ReturnedQuantity]                      INT             NOT NULL,
    [ServiceDate]                           DATETIME        NOT NULL,
    [FunctionalUnitId]                      INT             NOT NULL,
    [OrderedHealthProfessionalCode]         CHAR (20)       NULL,
    [OrderedProfessionalSpecialty]          CHAR (3)        NULL,
    [OrderedHealthProfessionalThirdPartyId] INT             NULL,
    [AuthorizationNumber]                   VARCHAR (20)    NULL,
    [LiquidationType]                       TINYINT         NOT NULL,
    [CupsEntityId]                          INT             NULL,
    [SurchargeApply]                        BIT             NOT NULL,
    [SalePrice]                             DECIMAL (18, 2) NOT NULL,
    [AverageCost]                           DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]                    NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                         DECIMAL (18, 2) NOT NULL,
    [TotalSalesPrice]                       DECIMAL (18, 2) NOT NULL,
    [GrandTotalSalesPrice]                  DECIMAL (18, 2) NOT NULL,
    [EconomicActivityId]                    INT             NULL,
    CONSTRAINT [PK_QuotationPharmaceuticalDispensingDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_CUPSEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_Quotation] FOREIGN KEY ([QuotationId]) REFERENCES [Billing].[Quotation] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de venta farmacéutica: (precio unitario - descuento) × cantidad dispensada. DECIMAL(18,2). Importe final facturado/liquidado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ((valor unitario  - Valor Descuento) * Cantidad)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal de venta: precio unitario menos descuento aplicado. DECIMAL(18,2). Base para cálculo de recargo y liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor unitario menos el descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de descuento en pesos: porcentaje de descuento aplicado al subtotal de ventas. DECIMAL(18,2). Deducción antes de recargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de rebaja/descuento aplicado al subtotal de ventas farmacéuticas. NUMERIC(5,2). Valor entre 0-100%.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento que se le va aplicar a al subtotal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario promedio del medicamento/producto en inventario. DECIMAL(18,2). Referencia de costo interno.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Costo promedio del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta del medicamento, valor individual sin descuentos. DECIMAL(18,2). Base tarifaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (SÍ/NO, 1/0) de si se aplicó recargo o sobreprecio al cálculo final. BIT. Afecta liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS (servicio/procedimiento) asociado. FK→Contract.CUPSEntity. Requerido solo si LiquidationType=2 (incluido 100% en servicio IPS).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación farmacéutica: 1=Manual Tarifario, 2=Incluido 100% en servicio IPS. TINYINT. Define forma de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización/aprobación de la dispensación. VARCHAR(20). Requerido para trazabilidad regulatoria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor) del profesional de salud que ordenó el medicamento. FK→Common.ThirdParty. Trazabilidad prescriptor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero del medico que ordena', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del profesional que prescribió (ej: 010=Medicina General). CHAR(3). Fuente: tabla Crystal INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/registro del profesional de salud prescriptor del medicamento. CHAR(20). Fuente: tabla Crystal INPROFSAL. Trazabilidad RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional/departamento para imputación contable del costo farmacéutico. FK→Payroll.FunctionalUnit. Centro de costos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional para la contabilización del costo de los productos de inventario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se dispensó (entregó) el medicamento al paciente. DATETIME. Registro de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se dispenso el medicamento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del medicamento devueltas/reintegradas al almacén. INT. Devoluciones y ajustes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se ha devuelto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades dispensadas del medicamento al paciente. INT. Unidades suministradas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén/bodega farmacéutica de origen de la dispensación. FK→Inventory.Warehouse. Gestión de inventario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen desde el cual se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento/producto farmacéutico dispensado. FK→Inventory.InventoryProduct. Trazabilidad del fármaco.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/entidad contratante (asegurador, clínica). FK→Common.ThirdParty. Facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del administrador de salud (ARS/EPS) contratante. FK→Contract.HealthAdministrator. Payer/facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención/línea de servicio contratada. FK→Contract.CareGroup. Clasificación de prestación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cotización/factura madre de esta línea de dispensación. FK→Billing.Quotation. Relación documento principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria: identificador único del detalle de dispensación farmacéutica. INT IDENTITY. Llave de registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada medicamento o producto farmacéutico incluido en una cotización de dispensación. Registra cantidades, precios, descuentos, unidad funcional, profesional que ordenó y datos de liquidación para la facturación de recetas y dispensación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociada al ítem dispensado, utilizado para clasificación tributaria o de facturación según el tipo de actividad comercial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
