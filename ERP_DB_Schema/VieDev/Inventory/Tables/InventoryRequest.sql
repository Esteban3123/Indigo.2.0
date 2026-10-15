CREATE TABLE [Inventory].[InventoryRequest] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [OperatingUnitId]        INT           NOT NULL,
    [DocumentDate]           DATETIME      NOT NULL,
    [RequestType]            TINYINT       NOT NULL,
    [TargetFunctionalUnitId] INT           NULL,
    [MovementType]           TINYINT       NULL,
    [SourceWarehouseId]      INT           NULL,
    [TargetWarehouseId]      INT           NULL,
    [Observation]            VARCHAR (300) NULL,
    [Status]                 TINYINT       NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [ConfirmationUser]       VARCHAR (20)  NULL,
    [ConfirmationDate]       DATETIME      NULL,
    [AnnulmentUser]          VARCHAR (20)  NULL,
    [AnnulmentDate]          DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InventoryRequest__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryRequest_FunctionalUnit] FOREIGN KEY ([TargetFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_InventoryRequest_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryRequest_TargetWarehouse] FOREIGN KEY ([TargetWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_InventoryRequest_Warehouse] FOREIGN KEY ([SourceWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryRequest] NOCHECK CONSTRAINT [FK_InventoryRequest_FunctionalUnit];


GO
ALTER TABLE [Inventory].[InventoryRequest] NOCHECK CONSTRAINT [FK_InventoryRequest_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryRequest] NOCHECK CONSTRAINT [FK_InventoryRequest_TargetWarehouse];


GO
ALTER TABLE [Inventory].[InventoryRequest] NOCHECK CONSTRAINT [FK_InventoryRequest_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento, el cual solo se habilita si el tipo de solicitud es de Almacen  1 - Consumo  2 - Traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequest', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de la solicitud  1 - Unidad Funcional  2 - Almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryRequest', @level2type = N'COLUMN', @level2name = N'RequestType';

