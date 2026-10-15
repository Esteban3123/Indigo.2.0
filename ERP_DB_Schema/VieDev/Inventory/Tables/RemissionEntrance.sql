CREATE TABLE [Inventory].[RemissionEntrance] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [RemissionDate]              DATETIME        NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [WarehouseId]                INT             NOT NULL,
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
    [CurrencyId]                 INT             CONSTRAINT [DF__Remission__Curre__74957F3A] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RemissionEntrance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionEntrance_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RemissionEntrance_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_RemissionEntrance_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_RemissionEntrance_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_RemissionEntrance_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionEntrance] NOCHECK CONSTRAINT [FK_RemissionEntrance_Currency];


GO
ALTER TABLE [Inventory].[RemissionEntrance] NOCHECK CONSTRAINT [FK_RemissionEntrance_OperatingUnit];


GO
ALTER TABLE [Inventory].[RemissionEntrance] NOCHECK CONSTRAINT [FK_RemissionEntrance_Supplier];


GO
ALTER TABLE [Inventory].[RemissionEntrance] NOCHECK CONSTRAINT [FK_RemissionEntrance_SuppliersDistributionLines];


GO
ALTER TABLE [Inventory].[RemissionEntrance] NOCHECK CONSTRAINT [FK_RemissionEntrance_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de los productos  1 - Sin Movmientos  2 - Parcial  3 - Total  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntrance', @level2type = N'COLUMN', @level2name = N'ProductStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de  la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntrance', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';

