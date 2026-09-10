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



GO



GO





GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [UX_ConsignmentInventoryRemissionDetailControl_EntityId_EntityName]
    ON [Inventory].[ConsignmentInventoryRemissionDetailControl]([EntityId] ASC, [EntityName] ASC);


GO
ALTER INDEX [UX_ConsignmentInventoryRemissionDetailControl_EntityId_EntityName]
    ON [Inventory].[ConsignmentInventoryRemissionDetailControl] DISABLE;


GO
CREATE NONCLUSTERED INDEX [IX_ConsignmentInventoryRemissionDetailControl_Detail_Batch_Entity]
    ON [Inventory].[ConsignmentInventoryRemissionDetailControl]([ConsignmentInventoryRemissionDetailId] ASC, [BatchSerialId] ASC, [EntityId] ASC, [EntityName] ASC)
    INCLUDE([MovementType], [Quantity], [QuantityPendingLegalization], [Value], [EntityDetailId], [OperatingUnitId], [FunctionalUnitId]);




GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-10-29
-- Description:	Se actualiza el lote del detalle cuando es una devolución de remisión de inventario en consignación
-- Modification date: 2026-09-02
-- Modified by: Oscar Sierra
-- Description: CIMA-52985 - Se completa tambien la Unidad Operativa de la devolucion, ya que
--              RemissionDevolutionAdminService no la asigna al insertar el control, lo que
--              impedia la legalizacion del Comprobante de Entrada asociado.
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

	UPDATE cirdc
		SET cirdc.OperatingUnitId = rd.OperatingUnitId
	FROM INSERTED i
	JOIN Inventory.ConsignmentInventoryRemissionDetailControl cirdc ON i.Id = cirdc.Id
	JOIN Inventory.RemissionDevolution rd ON rd.Id = cirdc.EntityId
	WHERE cirdc.EntityName = 'RemissionDevolution' AND cirdc.OperatingUnitId IS NULL
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del movimiento de inventario en consignación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que generó el movimiento de control de remisión (VARCHAR 20, auditoria)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el usuario que creo el movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional (servicio, área clínica) donde se controla el inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (centro de atención, sede) responsable del movimiento (FK → Common.OperatingUnit)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle adicional de la entidad generadora del registro de control', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle del la entidad generadora del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la entidad (proveedor, centro, empresa) que genera o participa en el documento de remisión', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la entidad generadora (código interno, NIT, RUE) en formato VARCHAR 20', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la entidad (proveedor, tercero, centro de atención) que origina el movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario o valor promedio (DECIMAL 18,2) con el cual se registró la transacción de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo promedio con el cual se realizó la transacción', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades pendientes de legalizar o regularizar del producto en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'QuantityPendingLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad pendiente de legalizar del producto usado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'QuantityPendingLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'QuantityPendingLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades del producto en el movimiento de control (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de movimiento de inventario: 1=Entrada/Ingreso, 2=Salida/Egreso (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'MovementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de movimiento  1 - Entrada, 2- Salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'MovementType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'MovementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número de serie del producto (FK → Inventory.BatchSerial)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de remisión de inventario en consignación (FK → Inventory.ConsignmentInventoryRemissionDetail)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, IDENTITY) del registro de control de remisión en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de control y movimientos del detalle de remisiones de inventario en consignación. Lleva el seguimiento de cantidades, valores, tipos de movimiento y estado de legalización pendiente por cada ítem remisionado en consigna, asociando la entidad (cliente, institución o punto de entrega) y las unidades operativas y funcionales involucradas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ConsignmentInventoryRemissionDetailControl';
