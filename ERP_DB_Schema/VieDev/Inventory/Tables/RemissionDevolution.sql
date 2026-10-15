CREATE TABLE [Inventory].[RemissionDevolution] (
    [Id]                              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                            VARCHAR (20)  NOT NULL,
    [RemissionDate]                   DATETIME      NOT NULL,
    [OperatingUnitId]                 INT           NOT NULL,
    [DevolutionType]                  TINYINT       NOT NULL,
    [WarehouseId]                     INT           NOT NULL,
    [RemissionEntranceId]             INT           NULL,
    [RemissionOutputId]               INT           NULL,
    [Description]                     VARCHAR (300) NOT NULL,
    [Status]                          TINYINT       NOT NULL,
    [CreationUser]                    VARCHAR (20)  NOT NULL,
    [CreationDate]                    DATETIME      NOT NULL,
    [ModificationUser]                VARCHAR (20)  NULL,
    [ModificationDate]                DATETIME      NULL,
    [ConfirmationUser]                VARCHAR (20)  NULL,
    [ConfirmationDate]                DATETIME      NULL,
    [AnnulmentUser]                   VARCHAR (20)  NULL,
    [AnnulmentDate]                   DATETIME      NULL,
    [TimeStamp]                       ROWVERSION    NOT NULL,
    [ConsignmentInventoryRemissionId] INT           NULL,
    CONSTRAINT [PK_RemissionDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionDevolution_ConsignmentInventoryRemission] FOREIGN KEY ([ConsignmentInventoryRemissionId]) REFERENCES [Inventory].[ConsignmentInventoryRemission] ([Id]),
    CONSTRAINT [FK_RemissionDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_RemissionDevolution_RemissionEntrance] FOREIGN KEY ([RemissionEntranceId]) REFERENCES [Inventory].[RemissionEntrance] ([Id]),
    CONSTRAINT [FK_RemissionDevolution_RemissionOutput] FOREIGN KEY ([RemissionOutputId]) REFERENCES [Inventory].[RemissionOutput] ([Id]),
    CONSTRAINT [FK_RemissionDevolution_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionDevolution] NOCHECK CONSTRAINT [FK_RemissionDevolution_ConsignmentInventoryRemission];


GO
ALTER TABLE [Inventory].[RemissionDevolution] NOCHECK CONSTRAINT [FK_RemissionDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[RemissionDevolution] NOCHECK CONSTRAINT [FK_RemissionDevolution_RemissionEntrance];


GO
ALTER TABLE [Inventory].[RemissionDevolution] NOCHECK CONSTRAINT [FK_RemissionDevolution_RemissionOutput];


GO
ALTER TABLE [Inventory].[RemissionDevolution] NOCHECK CONSTRAINT [FK_RemissionDevolution_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de devolucion del registro  1 - Entrada  2 - Salida  3 - Inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolution', @level2type = N'COLUMN', @level2name = N'DevolutionType';

