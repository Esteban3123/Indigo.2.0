CREATE TABLE [Authorization].[AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationOutsourcedServicesId]     INT             NOT NULL,
    [CareGroupId]                           INT             NOT NULL,
    [HealthAdministratorId]                 INT             NULL,
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
    CONSTRAINT [PK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_AuthorizationOutsourcedServices] FOREIGN KEY ([AuthorizationOutsourcedServicesId]) REFERENCES [Authorization].[AuthorizationOutsourcedServices] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_CUPSEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el ((valor unitario  - Valor Descuento) * Cantidad)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor unitario menos el descuento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento que se le va aplicar a al subtotal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Costo promedio del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio de venta del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero del medico que ordena', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la especialidad el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Funcional para la contabilización del costo de los productos de inventario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se dispenso el medicamento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de items que se ha devuelto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen desde el cual se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';

