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



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla o entidad origen (farmacia, unidad funcional, centro de atención) desde donde proviene el registro de devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la tabla o entidad del EHR/ERP origen que registra la devolución farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla del EHR de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (INT) de medicamento o fármaco a devolver en este detalle de devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del lote y número de serie del medicamento distribuido, vinculado a PharmaceuticalDispensingDetailBatchSerial (FK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de serie del lote de distribución farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del encabezado de devolución farmacéutica a la cual pertenece este detalle (FK a PharmaceuticalDispensingDevolution)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de devolución de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) del detalle individual de devolución de dispensación farmacéutica, clave primaria', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de dispensación farmacéutica. Registra cada ítem (lote/serial) devuelto en una devolución de medicamentos, con la cantidad devuelta y la entidad asociada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDevolutionDetail';
