CREATE TABLE [Inventory].[TransferOrder] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [OperatingUnitId]        INT           NOT NULL,
    [DocumentDate]           DATETIME      NOT NULL,
    [OrderType]              TINYINT       NOT NULL,
    [DispatchTo]             TINYINT       NOT NULL,
    [SourceWarehouseId]      INT           NOT NULL,
    [TargetWarehouseId]      INT           NULL,
    [TargetFunctionalUnitId] INT           NULL,
    [AdjustmentConceptId]    INT           NULL,
    [ThirdPartyId]           INT           NULL,
    [Description]            VARCHAR (300) NULL,
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
    [TransitWarehouseId]     INT           NULL,
    CONSTRAINT [PK_TransferOrder__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrder_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_TransferOrder_FunctionalUnit] FOREIGN KEY ([TargetFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_TransferOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TransferOrder_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse] FOREIGN KEY ([SourceWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse1] FOREIGN KEY ([TargetWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id]),
    CONSTRAINT [FK_TransferOrder_Warehouse2] FOREIGN KEY ([TransitWarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_AdjustmentConcept];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_FunctionalUnit];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_OperatingUnit];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_ThirdParty];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse1];


GO
ALTER TABLE [Inventory].[TransferOrder] NOCHECK CONSTRAINT [FK_TransferOrder_Warehouse2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1 - Registrado   2 - Confirmado / Entregado  3 - Anulado  4 - En Transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el concepto de movimiento de inventarios, este debe ser de tipo Traslado por consumo    Este campo solo se solicita si el tipo de la orden es Consumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica hacia donde se va a despachar los items  1 - Almacen  2 - Unidad Funcional    Este campo solo se solicita cuando el tipo de la orden es consumo ya que cuando es de tipo traslado este campo se llena por defecto en 1 - Almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'DispatchTo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de orden de traslado  1  - Traslado  2 - Consumo  3 - Traslado en Transito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrder', @level2type = N'COLUMN', @level2name = N'OrderType';

