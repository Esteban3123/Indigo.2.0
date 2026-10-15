-- =============================================
-- Author:		Nicolas Pulido
-- Create date: 21/Marzo/2017
-- Description:	Genera el informe de balance de activos fijos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportFixedAssetBalance]
	@Year int,
	@Month int,
	@parameterInitialPlate varchar(50),
	@parameterFinalPlate varchar(50),
	@parameterInitialCatalog varchar(20),
	@parameterFinalCatalog varchar(20),
	@parameterInitialGroup varchar(50),
	@parameterFinalGroup varchar(50),
	@parameterInitialLocation varchar(20),
	@parameterFinalLocation varchar(20)

AS
BEGIN
	
	--Id del activo fisico
	declare @PhisicalAssetId int
	--Código de la placa
	declare @PlateCode as varchar(50)
	--Nombre del producto
	declare @ProductName as varchar(300)
	--Valor saldo inicial
	declare @InitialValue as decimal = 0
	--Valor entradas
	declare @EntryValue as decimal = 0
	--Valor salidas
	declare @ExitValue as decimal = 0
	--Valor saldo final
	declare @FinalValue as decimal = 0
	--Id del grupo(MainAccountId)
	declare @MainAccountId as int
	--Numero del grupo(MainAccountNumber)
	declare @MainAccountNumber as varchar(50)
	--Nombre del grupo(MainAccountName)
	declare @MainAccountName as varchar(100)
	--Code - Name del grupo(MainAccountCodeName)
	declare @MainAccountNumberName varchar(160)
	--Id del item
	declare @ItemId as int = 0
	--Tabla en la que se guardan los datos del reporte
	declare @FixedAssetBalanceHeader table(Id int identity(1,1), PlateCode varchar(50), ProductName varchar(300), InitialValue numeric(20,4), EntryValue numeric(20,4), ExitValue numeric(20,4), FinalValue numeric(20,4), MainAccountId int, MainAccountNumberName varchar(160))
	--Tabla temporal con los Id de los items
	declare @tmpItemIds table(Id int identity(1,1), ItemId int)
	--Tabla temporal con los id de los activos fisicos
	declare @tmpPhysicalAssetIds table(Id int identity(1,1), PhysicalAssetId int)
	
	insert into @tmpPhysicalAssetIds
	select fapa.Id from FixedAsset.FixedAssetPhysicalAsset fapa
	inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
	inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
	inner join GeneralLedger.MainAccounts ma on ma.Id = fapa.MainAccountId
	inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
	where YEAR(fapa.AdquisitionDate) = @Year 
	AND MONTH(fapa.AdquisitionDate) = @Month 
	and fapa.Plate between @parameterInitialPlate and @parameterFinalPlate 
	and faic.Code between @parameterInitialCatalog and @parameterFinalCatalog 
	and ma.Number between @parameterInitialGroup and @parameterFinalGroup 
	and fal.Code between @parameterInitialLocation and @parameterFinalLocation
	
	-- se declara el cursor item_cursor que va a contener los ids de los items
	DECLARE PhysicalAsset_cursor CURSOR FOR   
	select PhysicalAssetId from @tmpPhysicalAssetIds

	OPEN PhysicalAsset_cursor  
	FETCH NEXT FROM PhysicalAsset_cursor   
	INTO @PhisicalAssetId

	WHILE @@FETCH_STATUS = 0  
	BEGIN   

		--Codigo de placa - nombre producto
		select @PlateCode = fapa.Plate, @ProductName = fai.[Description] from FixedAsset.FixedAssetPhysicalAsset fapa
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		where fapa.Id = @PhisicalAssetId
		--Listado de items por id
		delete @tmpItemIds
		insert into @tmpItemIds(ItemId)
		select fapa.ItemId from FixedAsset.FixedAssetPhysicalAsset fapa
		inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
		where fapa.Id = @PhisicalAssetId
		-- se declara el cursor item_cursor que va a contener los ids de los items
		DECLARE Item_cursor CURSOR FOR   
		select ItemId from @tmpItemIds
		
		OPEN Item_cursor  
		FETCH NEXT FROM Item_cursor   
		INTO @ItemId

		WHILE @@FETCH_STATUS = 0  
		BEGIN   

			--Valor inicial por itemid
			select @InitialValue = SUM(HistoricalValue) from FixedAsset.FixedAssetPhysicalAsset where YEAR(AdquisitionDate) <= @Year and MONTH(AdquisitionDate) < @Month and ItemId = @ItemId
			--Valor entradas por itemid
			select @EntryValue = SUM(HistoricalValue) from FixedAsset.FixedAssetPhysicalAsset where YEAR(AdquisitionDate) = @Year and MONTH(AdquisitionDate) = @Month and ItemId = @ItemId
			--Valor salidas por item
			select @ExitValue = SUM(faaod.SalesValue) from FixedAsset.FixedAssetActiveOutputDetail faaod 
			inner join FixedAsset.FixedAssetActiveOutput faao on faao.Id = faaod.FixedAssetActiveOutputId
			where YEAR(faao.DocumentDate) = @Year and MONTH(faao.DocumentDate) = @Month and faaod.PhysicalAssetId = @PhisicalAssetId

			
			--Id, Number y Name del grupo
			select @MainAccountId = ma.Id, @MainAccountNumber = ma.Number, @MainAccountName = ma.[Name] from FixedAsset.FixedAssetPhysicalAsset fapa
			inner join GeneralLedger.MainAccounts ma on ma.Id = fapa.MainAccountId
			where YEAR(fapa.AdquisitionDate) = @Year and MONTH(fapa.AdquisitionDate) = @Month and fapa.ItemId = @ItemId

			set @MainAccountNumberName = @MainAccountNumber + ' - ' + @MainAccountName

			if (@InitialValue IS NULL)
			begin
				set @InitialValue = 0
			end
			if (@EntryValue IS NULL)
			begin
				set @EntryValue = 0
			end
			if (@ExitValue IS NULL)
			begin
				set @ExitValue = 0
			end

			set @FinalValue = @InitialValue + @EntryValue - @ExitValue
			insert into @FixedAssetBalanceHeader(PlateCode,ProductName,InitialValue,EntryValue,ExitValue,MainAccountId,MainAccountNumberName,FinalValue) values(@PlateCode,@ProductName,@InitialValue,@EntryValue,@ExitValue,@MainAccountId,@MainAccountNumberName,@FinalValue)

			FETCH NEXT FROM Item_cursor   
			INTO @ItemId
		END   
		CLOSE Item_cursor;  
		DEALLOCATE Item_cursor;

	FETCH NEXT FROM PhysicalAsset_cursor   
	INTO @PhisicalAssetId
	END
	CLOSE PhysicalAsset_cursor;  
	DEALLOCATE PhysicalAsset_cursor;
	select distinct PlateCode, ProductName, InitialValue, EntryValue, ExitValue, FinalValue, MainAccountId, MainAccountNumberName from @FixedAssetBalanceHeader order by MainAccountId
END