CREATE TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] (
    [Id]                                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDevolutionId]        INT           NOT NULL,
    [PharmaceuticalDispensingDetailBatchSerialId] INT           NOT NULL,
    [Quantity]                                    INT           NOT NULL,
    [EntityId]                                    INT           NULL,
    [EntityName]                                  VARCHAR (250) NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDevolutionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDetailBatchSerial] FOREIGN KEY ([PharmaceuticalDispensingDetailBatchSerialId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolution] FOREIGN KEY ([PharmaceuticalDispensingDevolutionId]) REFERENCES [Inventory].[PharmaceuticalDispensingDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolution];


GO
CREATE NONCLUSTERED INDEX [IDX_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolutionId]
    ON [Inventory].[PharmaceuticalDispensingDevolutionDetail]([PharmaceuticalDispensingDevolutionId] ASC)
    INCLUDE([PharmaceuticalDispensingDetailBatchSerialId]);


GO
-- =============================================
-- Author:		JOHAN SEBASTIAN CUELLAR 
-- Create date: 01/03/2022
-- Description:	Trigger para que genere error cuando hayan datos inconsistentes
-- =============================================
CREATE TRIGGER [Inventory].[ValidateDuplicateDevolution]
   ON [Inventory].[PharmaceuticalDispensingDevolutionDetail]
   AFTER INSERT,UPDATE
AS 
BEGIN

	Declare @DevolutionId Int
	Select TOP 1 @DevolutionId = PharmaceuticalDispensingDevolutionId from INSERTED

	If exists (SELECT 1
		FROM Inventory.PharmaceuticalDispensingDevolution d (NOLOCK)			
		JOIN Inventory.PharmaceuticalDispensingDevolutionDetail dd (NOLOCK) on dd.PharmaceuticalDispensingDevolutionId = d.Id
		WHERE d.Id = @DevolutionId
		GROUP by dd.PharmaceuticalDispensingDevolutionId, dd.PharmaceuticalDispensingDetailBatchSerialId 
		having count(*) > 1)
	BEGIN
		THROW 51000, 'Existen detalles de la devolucion duplicados', 1	
	END

END
GO



-- =============================================
-- Author:		Giovanny Plazas L 
-- Create date: 20/05/2022
-- Description:	Trigger para que genere error cuando se haga devoluciones a un almacen de tipo consiganicion a uno normal y viceversa
-- =============================================
CREATE TRIGGER [Inventory].[ValidateWareHouseConsignment]
   ON [Inventory].[PharmaceuticalDispensingDevolutionDetail]
   AFTER INSERT,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	Declare @DevolutionId Int,
			@WarehouseType varchar(max)
	Select TOP 1 @DevolutionId = PharmaceuticalDispensingDevolutionId from INSERTED

	If exists (select  1
				from Inventory.PharmaceuticalDispensingDevolution pdd
				inner join Inventory.Warehouse wd on wd.Id = pdd.WarehouseId
				inner join INSERTED pddd on pdd.Id = pddd.PharmaceuticalDispensingDevolutionId
				inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on pddd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
				inner join Inventory.PhysicalInventory pin on pin.Id = pddbs.PhysicalInventoryId
				inner join Inventory.InventoryProduct ipro on ipro.Id = pin.ProductId
				inner join Inventory.PharmaceuticalDispensingDetail pddes on pddes.Id = pddbs.PharmaceuticalDispensingDetailId
				inner join Inventory.Warehouse wdis on wdis.Id = pddes.WarehouseId
				inner join Inventory.PharmaceuticalDispensing p on p.Id = pddes.PharmaceuticalDispensingId
				left join Inventory.BatchSerial bs on bs.Id = pin.BatchSerialId
				where pdd.Status = 2 and pdd.WarehouseId <> pddes.WarehouseId and wd.WarehouseConsignment <> wdis.WarehouseConsignment )
	BEGIN
			SELECT top 1 @WarehouseType= concat('Se esta intentando devolver al Almacen: ', w.Name,' el cual es tipo ',iif(w.WarehouseConsignment=1,'Consignación','no es de Consignación'),'; el Tipo de almacen es diferente al que hizo la dispensación')
			FROM Inventory.PharmaceuticalDispensingDevolution pdd
			JOIN Inventory.Warehouse w ON pdd.WarehouseId=w.Id
			WHERE pdd.Id=@DevolutionId;

		THROW 51000,@WarehouseType , 1	;
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla del EHR de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de serie del lote de distribución farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de devolución de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

