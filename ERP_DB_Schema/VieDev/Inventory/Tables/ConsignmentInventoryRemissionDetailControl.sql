CREATE TABLE [Inventory].[ConsignmentInventoryRemissionDetailControl] (
    [Id]                                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsignmentInventoryRemissionDetailId] INT             NOT NULL,
    [BatchSerialId]                         INT             NULL,
    [MovementType]                          TINYINT         NOT NULL,
    [Quantity]                              INT             NOT NULL,
    [QuantityPendingLegalization]           INT             CONSTRAINT [DF_ConsignmentInventoryRemissionDetailControl_OutstandingLegalizedQuantity] DEFAULT ((0)) NOT NULL,
    [Value]                                 DECIMAL (18, 2) CONSTRAINT [DF_ConsignmentInventoryRemissionDetailControl_Value] DEFAULT ((0)) NOT NULL,
    [EntityId]                              INT             NOT NULL,
    [EntityCode]                            VARCHAR (20)    NOT NULL,
    [EntityName]                            VARCHAR (250)   NOT NULL,
    [EntityDetailId]                        INT             NULL,
    [OperatingUnitId]                       INT             NULL,
    [FunctionalUnitId]                      INT             NULL,
    [CreationUser]                          VARCHAR (20)    NOT NULL,
    [CreationDate]                          DATETIME        NOT NULL,
    CONSTRAINT [PK_ConsignmentInventoryRemissionDetailControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_ConsignmentInventoryRemissionDetail] FOREIGN KEY ([ConsignmentInventoryRemissionDetailId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetail] ([Id]),
    CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailControl] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_BatchSerial];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailControl] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_ConsignmentInventoryRemissionDetail];


GO
ALTER TABLE [Inventory].[ConsignmentInventoryRemissionDetailControl] NOCHECK CONSTRAINT [FK_ConsignmentInventoryRemissionDetailControl_OperatingUnit];


GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetailControl_EntityId_EntityName]
    ON [Inventory].[ConsignmentInventoryRemissionDetailControl]([EntityId] ASC, [EntityName] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada, 2- Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'MovementType';

