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
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-10-29
-- Description:	Se actualiza el lote del detalle cuando es una devolución de remisión de inventario en consignación
-- =============================================
CREATE TRIGGER [Inventory].[tgg_UpdatebatchSerial]
   ON  [Inventory].[ConsignmentInventoryRemissionDetailControl] 
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE cirdc 
		SET cirdc.BatchSerialId = cirdbs.BatchSerialId
	FROM INSERTED i
	JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON i.Id = cirdc.Id
	JOIN Inventory.RemissionDevolutionDetail rdd ON cirdc.EntityDetailId = rdd.Id
	JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cirdc.ConsignmentInventoryRemissionDetailId = cirdbs.ConsignmentInventoryRemissionDetailId AND rdd.ConsignmentInventoryRemissionDetailBatchSerialId = cirdbs.Id
	WHERE cirdc.EntityName = 'RemissionDevolution'
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el usuario que creo el movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle del la entidad generadora del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo promedio con el cual se realizó la transacción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente de legalizar del producto usado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'QuantityPendingLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de movimiento  1 - Entrada, 2- Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Id';

