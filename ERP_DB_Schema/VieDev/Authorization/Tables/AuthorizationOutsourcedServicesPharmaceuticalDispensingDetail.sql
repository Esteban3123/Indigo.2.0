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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';

