CREATE TABLE [Inventory].[InventoryControl] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [OperatingUnitId]  INT          NOT NULL,
    [DocumentDate]     DATETIME     NOT NULL,
    [WarehouseId]      INT          NOT NULL,
    [DocumentType]     TINYINT      NOT NULL,
    [Import]           BIT          CONSTRAINT [DF_InventoryControl_Import] DEFAULT ((0)) NOT NULL,
    [Status]           TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [ConfirmationUser] VARCHAR (20) NULL,
    [ConfirmationDate] DATETIME     NULL,
    [AnnulmentUser]    VARCHAR (20) NULL,
    [AnnulmentDate]    DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    [AdmissionNumber]  CHAR (10)    NULL,
    CONSTRAINT [PK_InventoryControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryControl_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryControl_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryControl] NOCHECK CONSTRAINT [FK_InventoryControl_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryControl] NOCHECK CONSTRAINT [FK_InventoryControl_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el estado del registro  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControl', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo del documento  1 - Saldo Inicial  2 - Inventario Físico  3 - Inventario en Custodia  4 - Inventario de Control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControl', @level2type = N'COLUMN', @level2name = N'DocumentType';

