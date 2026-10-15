CREATE TABLE [Inventory].[PharmaceuticalDispensingDetail] (
    [Id]                                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingId]                INT             NOT NULL,
    [CareGroupId]                               INT             NOT NULL,
    [HealthAdministratorId]                     INT             NULL,
    [ThirdPartyId]                              INT             NULL,
    [ProductId]                                 INT             NOT NULL,
    [WarehouseId]                               INT             NOT NULL,
    [Quantity]                                  INT             NOT NULL,
    [ReturnedQuantity]                          INT             NOT NULL,
    [ServiceDate]                               DATETIME        NOT NULL,
    [FunctionalUnitId]                          INT             NOT NULL,
    [OrderedHealthProfessionalCode]             CHAR (20)       NULL,
    [OrderedProfessionalSpecialty]              CHAR (3)        NULL,
    [OrderedHealthProfessionalThirdPartyId]     INT             NULL,
    [AuthorizationNumber]                       VARCHAR (20)    NULL,
    [LiquidationType]                           TINYINT         NOT NULL,
    [CupsEntityId]                              INT             NULL,
    [SurchargeApply]                            BIT             NOT NULL,
    [SalePrice]                                 DECIMAL (18, 2) NOT NULL,
    [AverageCost]                               DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]                        NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                             DECIMAL (18, 2) NOT NULL,
    [TotalSalesPrice]                           DECIMAL (18, 2) NOT NULL,
    [GrandTotalSalesPrice]                      DECIMAL (18, 2) NOT NULL,
    [QuotationPharmaceuticalDispensingDetailId] INT             NULL,
    [EntityId]                                  INT             NULL,
    [EntityName]                                VARCHAR (250)   NULL,
    [FinalProductCost]                          DECIMAL (18, 2) NOT NULL,
    [GrossValue]                                DECIMAL (18, 2) CONSTRAINT [DF__Pharmaceu__SubTo__5C48F45D] DEFAULT ((0)) NOT NULL,
    [TaxValue]                                  DECIMAL (18, 2) CONSTRAINT [DF__Pharmaceu__TAXVa__5D3D1896] DEFAULT ((0)) NOT NULL,
    [IvaId]                                     INT             NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PharmaceuticalDispensingDetail] CHECK ([ReturnedQuantity]<=[Quantity]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_CUPSEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_GeneralLedgerIVA] FOREIGN KEY ([IvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing] FOREIGN KEY ([PharmaceuticalDispensingId]) REFERENCES [Inventory].[PharmaceuticalDispensing] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetail] FOREIGN KEY ([QuotationPharmaceuticalDispensingDetailId]) REFERENCES [Billing].[QuotationPharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty] FOREIGN KEY ([OrderedHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty1] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_CareGroup];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_CUPSEntity];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_FunctionalUnit];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_GeneralLedgerIVA];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_HealthAdministrator];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty1];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_Warehouse];


GO
CREATE NONCLUSTERED INDEX [IDX_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetailId]
    ON [Inventory].[PharmaceuticalDispensingDetail]([QuotationPharmaceuticalDispensingDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del Iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'IvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo Final del  producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';

