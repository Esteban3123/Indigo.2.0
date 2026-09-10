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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de venta con descuento aplicado: (precio unitario - valor descuento) × cantidad dispensada. DECIMAL(18,2). Usado en liquidación farmacéutica y facturación de servicios externalizados.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ((valor unitario  - Valor Descuento) * Cantidad)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario menos descuento aplicado. DECIMAL(18,2). Subtotal antes de aplicar cantidad en detalle de dispensación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor unitario menos el descuento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del descuento calculado: porcentaje de descuento × subtotal de venta. DECIMAL(18,2). Aplicado en liquidación de servicios farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento aplicado al subtotal. NUMERIC(5,2). Rango 0-100%. Usado en ajuste de precios de dispensación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento que se le va aplicar a al subtotal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio unitario del producto farmacéutico en inventario. DECIMAL(18,2). Referencia para análisis de margen y costo de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Costo promedio del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta del producto en la dispensación. DECIMAL(18,2). Base para cálculo de total antes de descuentos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si se aplicó recargo/sobrecosto al precio de venta del medicamento. 1=Sí, 0=No. Afecta cálculo final.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS (Clasificación Única de Procedimientos en Salud) vinculado. Requerido solo si LiquidationType=2 (incluido 100% en servicio IPS). FK a Contract.CUPSEntity.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de liquidación farmacéutica: 1=Manual Tarifario, 2=Incluido 100% en servicio IPS. TINYINT. Determina contabilización y generación RIPS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de autorización del servicio farmacéutico externalizado. VARCHAR(20). Clave para auditoría y trámite ante EPS/administrador.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor) asociado al profesional de salud que ordenó el medicamento. FK a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero del medico que ordena', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del profesional prescriptor. CHAR(3). Ej: 101=Medicina General, 121=Pediatría. Usado en RIPS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de salud que prescribió/ordenó el medicamento. CHAR(20). Origen: tabla INPROFSAL (Crystal). PII sensible.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional destino para contabilización de costo de inventario. FK a Payroll.FunctionalUnit. Asocia gasto farmacéutico a área clínica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional para la contabilización del costo de los productos de inventario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de dispensación del medicamento al paciente. DATETIME. Marca momento de entrega y auditoría temporal.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se dispenso el medicamento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del medicamento devueltas/no utilizadas. INT. Ajusta inventario y liquidación de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se ha devuelto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades de medicamento dispensadas. INT. Base para cálculo de totales y control de inventario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén/depósito de origen de la dispensación. FK a Inventory.Warehouse. Trazabilidad de existencias.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen desde el cual se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto/medicamento dispensado. FK a Inventory.InventoryProduct. Vincula farmaco a detalle de orden.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor/entidad) que realiza la dispensación externalizada. FK a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero contratista (EPS/administrador de salud) para este servicio. FK a Contract.HealthAdministrator. Obligatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo/plan de atención contratado. FK a Contract.CareGroup. Agrupa servicios por póliza/contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador cabecera de la autorización/cotización de servicios farmacéuticos externalizados. FK a Authorization.AuthorizationOutsourcedServices. Agrupa detalles.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de dispensación farmacéutica. INT IDENTITY. PK del registro en tabla de auditoría y liquidación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de dispensación farmacéutica de servicios tercerizados autorizados. Registra cada medicamento o producto dispensado en el marco de una autorización de servicio externo, incluyendo cantidades, precios, descuentos, unidad funcional y profesional que ordenó la dispensación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail';
