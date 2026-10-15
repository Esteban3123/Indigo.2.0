CREATE TABLE [Inventory].[RemissionOutput] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [RemissionDate]    DATETIME      NOT NULL,
    [OperatingUnitId]  INT           CONSTRAINT [DF_RemissionOutput_OperatingUnitId] DEFAULT ((9)) NOT NULL,
    [CustomerId]       INT           NOT NULL,
    [WarehouseId]      INT           NOT NULL,
    [Description]      VARCHAR (300) NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [ProductStatus]    TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RemissionOutput__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionOutput_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_RemissionOutput_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_RemissionOutput_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionOutput] NOCHECK CONSTRAINT [FK_RemissionOutput_Customer];


GO
ALTER TABLE [Inventory].[RemissionOutput] NOCHECK CONSTRAINT [FK_RemissionOutput_OperatingUnit];


GO
ALTER TABLE [Inventory].[RemissionOutput] NOCHECK CONSTRAINT [FK_RemissionOutput_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado de los productos  1 - Sin Movmientos  2 - Parcial  3 - Total  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutput', @level2type = N'COLUMN', @level2name = N'ProductStatus';

