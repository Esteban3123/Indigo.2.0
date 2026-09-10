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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de balance de activos fijos para un año y mes determinados, mostrando por cada bien (identificado por su placa) el saldo inicial, entradas, salidas y saldo final en valor histórico. Cruza los activos físicos con su catálogo de ítems, cuenta contable (grupo mayor del plan de cuentas) y ubicación física, aplicando filtros por rango de placa, rango de catálogo, rango de cuenta contable y rango de ubicación. Calcula el saldo inicial sumando adquisiciones de períodos anteriores al mes indicado, las entradas como adquisiciones del mes en curso, y las salidas a partir de los documentos de baja o salida de activos del mismo período. Es el procedimiento central para la reportería contable y de control de activos fijos, utilizado en auditorías de inventario, conciliación contable y seguimiento del patrimonio de la organización.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetBalance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetBalance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de balance de activos fijos para un mes/año, calculando saldo inicial, entradas, salidas y saldo final por placa, agrupado por cuenta contable, filtrado por rangos de placa, catálogo, grupo contable y ubicación.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los activos físicos deben tener Item, Catálogo de Item, Cuenta contable principal y Ubicación asociados (joins INNER).; Los rangos de filtro (placa, catálogo, grupo, ubicación) deben suministrarse para acotar el universo, sino no retornará registros.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El saldo final siempre se calcula como InitialValue + EntryValue - ExitValue.; Solo se procesan activos físicos cuya fecha de adquisición coincida exactamente con el año y mes solicitados.; El saldo inicial considera adquisiciones de años anteriores o del mismo año en meses previos al parámetro.; Las entradas corresponden a adquisiciones del mes/año exacto solicitado.; Las salidas se calculan por activo físico con base en la fecha del documento de salida en el mes/año solicitado.; El identificador del grupo contable se concatena en formato ''Number - Name'' para presentación.; Los filtros de rango usan BETWEEN inclusivo sobre Plate, Code de catálogo, Number de cuenta y Code de ubicación.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Placa de activo; Valor histórico; Adquisición de activo; Salida/baja de activo; Cuenta contable principal (PUC); Catálogo de activos; Ubicación de activos; Balance de activos fijos; Saldo inicial / entradas / salidas / saldo final', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve filas distintas con PlateCode, ProductName, InitialValue, EntryValue, ExitValue, FinalValue, MainAccountId y MainAccountNumberName, ordenadas por MainAccountId.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @InitialValue IS NULL (no hay activos adquiridos antes del mes/año para el item) → Se asigna 0 al saldo inicial; si @EntryValue IS NULL (no hay adquisiciones en el mes/año para el item) → Se asigna 0 a entradas; si @ExitValue IS NULL (no hay salidas registradas en el mes/año para el activo físico) → Se asigna 0 a salidas', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetActiveOutputDetail; FixedAsset.FixedAssetActiveOutput', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetBalance';
-- GO
