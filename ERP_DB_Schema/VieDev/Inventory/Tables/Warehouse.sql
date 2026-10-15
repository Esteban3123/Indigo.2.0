CREATE TABLE [Inventory].[Warehouse] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [Prefix]                        VARCHAR (4)   NOT NULL,
    [CostCenterId]                  INT           NOT NULL,
    [LoanThirdPartyDebitAccountId]  INT           NOT NULL,
    [LoanThirdPartyCreditAccountId] INT           NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  VARCHAR (20)  CONSTRAINT [DF_Warehouse_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                  DATETIME      CONSTRAINT [DF_Warehouse_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [VirtualStore]                  BIT           CONSTRAINT [DF_Warehouse_VirtualStore] DEFAULT ((0)) NOT NULL,
    [SupplierId]                    INT           CONSTRAINT [DF_Warehouse_SupplierId] DEFAULT ((5129)) NOT NULL,
    [WarehouseConsignment]          BIT           CONSTRAINT [DF__Warehouse__Wareh__0FCAFA68] DEFAULT ((0)) NOT NULL,
    [CustodyStore]                  BIT           CONSTRAINT [DF_Warehouse_CustodyStore] DEFAULT ((0)) NOT NULL,
    [CodeCenterAttention]           CHAR (10)     NULL,
    [TransitStore]                  BIT           CONSTRAINT [DF_Warehouse_TransitStore] DEFAULT ((0)) NOT NULL,
    [ControlStore]                  BIT           CONSTRAINT [DF_Warehouse_ControlStore] DEFAULT ((0)) NOT NULL,
    [WareHouseType]                 TINYINT       CONSTRAINT [DF__Warehouse__WareH__7DD4055A] DEFAULT ((0)) NOT NULL,
    [HandleRestrictedProducts]      BIT           CONSTRAINT [DF_Warehouse_HandleRestrictedProducts] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Warehouse__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Warehouse_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_Warehouse_MainAccounts] FOREIGN KEY ([LoanThirdPartyDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Warehouse_MainAccounts1] FOREIGN KEY ([LoanThirdPartyCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_Warehouse_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_CostCenter];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_MainAccounts];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_MainAccounts1];


GO
ALTER TABLE [Inventory].[Warehouse] NOCHECK CONSTRAINT [FK_Warehouse_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el almacen tiene restricciones por productos, es decir que solo maneja algunos productos en especfico. Cuando este campo esta en 1 entonces se llena la tabla WarehouseRestrictedConditions', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'HandleRestrictedProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de almacen :                  (0, "Ninguna"))                  (1, "Almacén Virtual"))                  (2, "Almacén de Consignación"))                  (3, "Almacén de Custodia"))                  (4, "Almacén de Transito"))                  (5, "Almacén de Control"))                  (6, "Almacén de Remanentes"))', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Warehouse', @level2type = N'COLUMN', @level2name = N'WareHouseType';

