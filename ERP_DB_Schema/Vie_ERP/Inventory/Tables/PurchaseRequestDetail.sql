CREATE TABLE [Inventory].[PurchaseRequestDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseRequestId]   INT           NOT NULL,
    [InventoryProductId]  INT           NULL,
    [FixedAssetItemId]    INT           NULL,
    [OtherRequest]        VARCHAR (300) NULL,
    [TrademarkId]         INT           NULL,
    [Model]               VARCHAR (100) NULL,
    [MeasurementUnitId]   INT           NULL,
    [Quantity]            INT           CONSTRAINT [DF_PurchaseRequestDetail_Quantity] DEFAULT ((0)) NOT NULL,
    [OutstandingQuantity] INT           CONSTRAINT [DF_PurchaseRequestDetail_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    [Description]         VARCHAR (300) NULL,
    [Status]              TINYINT       CONSTRAINT [DF_PurchaseRequestDetail_Status] DEFAULT ((0)) NOT NULL,
    [ApproveObservation]  VARCHAR (300) NULL,
    [ApproveUser]         VARCHAR (20)  NULL,
    [ApproveDate]         DATETIME      NULL,
    CONSTRAINT [PK_PurchaseRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_PurchaseRequest] FOREIGN KEY ([PurchaseRequestId]) REFERENCES [Inventory].[PurchaseRequest] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetItem];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetTrademark];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_PurchaseRequest];


GO
-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2020-02-12
-- Description:	Se valida que las cantidades disponibles de la solicitud de compra concuerden con el restante de las ordenadas
-- =============================================
CREATE TRIGGER [Inventory].[tgg_PurchaseRequestDetail_ValidateQuantityes]
   ON  [Inventory].[PurchaseRequestDetail]
   AFTER INSERT, UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS
	(
		SELECT 1
		FROM INSERTED prd
		LEFT JOIN
		(
			SELECT pod.PurchaseRequestDetailId, SUM(pod.Quantity) Quantity
			FROM
			(
				SELECT pod.PurchaseRequestDetailId, pod.Quantity
				FROM Inventory.PurchaseOrder po
				JOIN Inventory.PurchaseOrderDetail pod ON po.Id = pod.PurchaseOrderId
				WHERE po.Status = 2

				UNION ALL

				SELECT pod.PurchaseRequestDetailId, pod.Quantity
				FROM FixedAsset.FixedAssetPurchaseOrder po
				JOIN FixedAsset.FixedAssetPurchaseOrderItem pod ON po.Id = pod.PurchaseOrderId
				WHERE po.Status = 2
			) pod
			GROUP BY pod.PurchaseRequestDetailId
		) pod ON prd.Id = pod.PurchaseRequestDetailId
		WHERE prd.OutstandingQuantity <> (prd.Quantity - ISNULL(pod.Quantity, 0))
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. La cantidad disponible no corresponde con lo ordenado', 1
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de aprobación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'ApproveDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien aprueba', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'ApproveUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de aprobación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'ApproveObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado, 0:Registrado,1:Aprobado, 2:Rechazado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea una solicitud este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una orden de compra de la solicitud en cuestion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo del activo fijo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la marca del activo fijo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de otra solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'OtherRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia al activo fijo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia al producto de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la solicitud de compra de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'PurchaseRequestId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id detalle de la solicitud de compra de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequestDetail', @level2type = N'COLUMN', @level2name = N'Id';

