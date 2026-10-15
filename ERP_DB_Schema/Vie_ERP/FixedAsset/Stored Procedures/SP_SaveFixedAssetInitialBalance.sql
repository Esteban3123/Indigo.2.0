




-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_SaveFixedAssetInitialBalance] 
	@FixedAssetInitialBalanceItemPartsDetailBookXml as Xml,
	@FixedAssetInitialBalanceItemPartsXml as Xml,
	@FixedAssetInitialBalanceItemDetailBookXml as Xml,
	@FixedAssetInitialBalanceItemXml as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemPartsDetailBook
	declare @FixedAssetInitialBalanceItemPartsDetailBookTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemParts
	declare @FixedAssetInitialBalanceItemPartsTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItemDetailBook
	declare @FixedAssetInitialBalanceItemDetailBookTemp table(Id int)
	--Tabla temporal para obtener el listado de ids de eliminados de FixedAssetInitialBalanceItem
	declare @FixedAssetInitialBalanceItemTemp table(Id int)

	Begin try
	
		--Se obtienen los ids de los xml

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemPartsDetailBook)
		insert into @FixedAssetInitialBalanceItemPartsDetailBookTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemPartsDetailBookXml.nodes('/ListDeleteFixedAssetInitialBalanceItemPartsDetailBook') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemParts)
		insert into @FixedAssetInitialBalanceItemPartsTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemPartsXml.nodes('/ListDeleteFixedAssetInitialBalanceItemParts') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItemDetailBook)
		insert into @FixedAssetInitialBalanceItemDetailBookTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemDetailBookXml.nodes('/ListDeleteFixedAssetInitialBalanceItemDetailBook') t(x)

		--Se obtiene los detalles del xml(FixedAssetInitialBalanceItem)
		insert into @FixedAssetInitialBalanceItemTemp
		select 
		t.x.value('Id[1]','int') as Id
		from @FixedAssetInitialBalanceItemXml.nodes('/ListDeleteFixedAssetInitialBalanceItem') t(x)

		
		--Se eliminan los registros en cascada

		--Eliminación FixedAssetInitialBalanceItemPartsDetailBook
		delete [FixedAsset].[FixedAssetInitialBalanceItemPartsDetailBook] where Id in (select Id from @FixedAssetInitialBalanceItemPartsDetailBookTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItemParts
		delete [FixedAsset].[FixedAssetInitialBalanceItemParts] where Id in (select Id from @FixedAssetInitialBalanceItemPartsTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItemDetailBook
		delete [FixedAsset].[FixedAssetInitialBalanceItemDetailBook] where Id in (select Id from @FixedAssetInitialBalanceItemDetailBookTemp where Id > 0)

		--Eliminación FixedAssetInitialBalanceItem
		delete [FixedAsset].[FixedAssetInitialBalanceItem] where Id in (select Id from @FixedAssetInitialBalanceItemTemp where Id > 0)


		select 0 as CodeMessage, 'Se eliminó correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END