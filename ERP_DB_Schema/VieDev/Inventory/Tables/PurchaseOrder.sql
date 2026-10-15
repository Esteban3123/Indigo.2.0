CREATE TABLE [Inventory].[PurchaseOrder] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [OrderType]                  TINYINT         NOT NULL,
    [IsBasedContract]            BIT             NOT NULL,
    [ContractId]                 INT             NULL,
    [DocumentDate]               DATETIME        NOT NULL,
    [DeliveredDate]              DATETIME        NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [WarehouseId]                INT             NULL,
    [Description]                VARCHAR (300)   NOT NULL,
    [PaymentMethod]              VARCHAR (200)   NOT NULL,
    [DeliveryMethod]             VARCHAR (200)   NULL,
    [DeliveryPlace]              VARCHAR (200)   NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [DiscountValue]              DECIMAL (18, 2) NOT NULL,
    [IvaValue]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [OsteosynthesisEquipment]    BIT             CONSTRAINT [DF_PurchaseOrder_OsteosynthesisEquipment] DEFAULT ((0)) NOT NULL,
    [OsteosynthesisEquipmentId]  INT             NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__PurchaseO__Curre__24AEA6B0] DEFAULT ((1)) NOT NULL,
    [TypePaymentMethod]          TINYINT         NULL,
    [FunctionalUnitRequestId]    INT             NULL,
    [DaysTerm]                   INT             NULL,
    CONSTRAINT [PK_PurchaseOrder__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrder_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_FunctionalUnit] FOREIGN KEY ([FunctionalUnitRequestId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_InventoryContract] FOREIGN KEY ([ContractId]) REFERENCES [Inventory].[InventoryContract] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_PurchaseOrder_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_Currency];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_FunctionalUnit];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_InventoryContract];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_OperatingUnit];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_Supplier];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_SuppliersDistributionLines];


GO
ALTER TABLE [Inventory].[PurchaseOrder] NOCHECK CONSTRAINT [FK_PurchaseOrder_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la forma de pago 1-Contado ,                         2-Credito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrder', @level2type = N'COLUMN', @level2name = N'TypePaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrder', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de la orden  1 - Productos  2 - Servicios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrder', @level2type = N'COLUMN', @level2name = N'OrderType';

