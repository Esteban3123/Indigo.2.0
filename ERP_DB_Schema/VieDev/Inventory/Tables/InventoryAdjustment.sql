CREATE TABLE [Inventory].[InventoryAdjustment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [OperatingUnitId]     INT           NOT NULL,
    [DocumentDate]        DATETIME      NOT NULL,
    [AdjustmentType]      TINYINT       NOT NULL,
    [AdjustmentConceptId] INT           NULL,
    [InventoryControlId]  INT           NULL,
    [WarehouseId]         INT           NULL,
    [ThirdPartyId]        INT           NULL,
    [Description]         VARCHAR (500) NULL,
    [Status]              TINYINT       NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [ConfirmationUser]    VARCHAR (20)  NULL,
    [ConfirmationDate]    DATETIME      NULL,
    [AnnulmentUser]       VARCHAR (20)  NULL,
    [AnnulmentDate]       DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [AdmissionNumber]     CHAR (10)     NULL,
    CONSTRAINT [PK_InventoryAdjustment__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustment_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_InventoryControl] FOREIGN KEY ([InventoryControlId]) REFERENCES [Inventory].[InventoryControl] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_InventoryAdjustment_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_AdjustmentConcept];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_InventoryControl];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_ThirdParty];


GO
ALTER TABLE [Inventory].[InventoryAdjustment] NOCHECK CONSTRAINT [FK_InventoryAdjustment_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del control de inventarios, este campo solo se llena si el tipo del ajuste es de tipo Inventario Fisico    Los registros de control de inventarios se tienen que filtrar por el tipo de documentos "Control de Inventarios"', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'InventoryControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de ajuste  1 - Entrada  2 - Salida  3 - Inventario Fisico  4 - Ajuste de Inventario Custodia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustment', @level2type = N'COLUMN', @level2name = N'AdjustmentType';

