CREATE TABLE [Inventory].[ConsignmentInventoryRemission] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [RemissionDate]              DATETIME        NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [WarehouseId]                INT             NOT NULL,
    [MovementType]               TINYINT         CONSTRAINT [DF_ConsignmentInventoryRemission_MovementType] DEFAULT ((1)) NOT NULL,
    [RemissionNumber]            VARCHAR (50)    NOT NULL,
    [Description]                VARCHAR (200)   NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [IvaValue]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [ProductStatus]              TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [EntityCode]                 VARCHAR (20)    NULL,
    [EntityId]                   INT             NULL,
    [EntityName]                 VARCHAR (250)   NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__Consignme__Curre__669C659E] DEFAULT ((2)) NOT NULL,
    CONSTRAINT [PK_ConsignmentInventoryRemission__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentInventoryRemission_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemission_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemission_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemission_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemission_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemission] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemission_Currency];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemission] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemission_OperatingUnit];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemission] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemission_Supplier];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemission] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemission_SuppliersDistributionLines];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemission] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemission_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ConsignmentInventoryRemission__Code]
    ON [Inventory].[ConsignmentInventoryRemission]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de los productos  1 - Sin Movimientos  2 - Parcial  3 - Total  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemission', @level2type = N'COLUMN', @level2name = N'ProductStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Apertura  2 - Incremento  3 - Reposición', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemission', @level2type = N'COLUMN', @level2name = N'MovementType';

