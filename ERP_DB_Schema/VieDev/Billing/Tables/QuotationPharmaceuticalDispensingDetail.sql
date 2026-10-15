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
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_CareGroup];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_CUPSEntity];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_FunctionalUnit];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_HealthAdministrator];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_InventoryProduct];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_Quotation];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_ThirdParty];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetail_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationPharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';

