
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 25-08-2016
-- Description:	Guarda la liquidación de impuestos
-- =============================================
CREATE PROCEDURE [Taxes].[SP_SaveTaxesLiquidation]
	@Year as int,
	@CadastralIdentification as varchar(50),
	@CadastralIdentification2 as varchar(50),
	@Address as varchar(200),
	@Address2 as varchar(200),
	@OwnerId as int,
	@PropertyType as int,
	@CodeUser as varchar(20)
AS
BEGIN
	
	SET NOCOUNT ON;
	--PropertyType = 1.Urbano, 2.Rural

	--Variable para saber si el perdio esta edificado
	declare @IsBuilt bit

	--Variable para saber el salario minimo legal vigente
	declare @SMLV decimal(18,0) = 689455

	--Impuesto predial
	declare @TaxPredial decimal(18,0)

	--Porcentaje impuesto
	declare @TaxPercentage decimal(5,2)

	--Id de la cabecera de la liquidacion de impuestos
	declare @TaxesLiquidationId int

	--Valor bomberil
	declare @BOMBERIL decimal(18,0)

	--Valor cam
	declare @CAM decimal(18,0)

	--Devuelve todos los ids que fueron guardados en la tabla TaxesLiquidationDetail para despues consultarlos con xpo
	declare @messagesIds varchar(max) = ''

	--Posición del cursor para poder armar el mensaje de ids
	declare @position int = 0

	--Id del concepto de liquidación impuesto predial
	declare @TaxesLiquidationConceptIdIMP int

	--Id del concepto de liquidación bomberil
	declare @TaxesLiquidationConceptIdBOM int

	--Id del concepto de liquidación cam
	declare @TaxesLiquidationConceptIdCAM int

	declare @IDSDetails table(Id int primary key)

	--Variables del cursor
	declare @TaxesPropertyId int
	declare @Code varchar(50)
	declare @BuiltArea decimal(18,0)
	declare @EconomicDestiny varchar(5)
	declare @Appraisal decimal(18,0)
	declare @LanArea decimal(18,0)
	declare @Index int = 0
	declare @MinIdDetail int
	declare @MaxIdDetail int

	BEGIN TRY

		--Se asigna los valores para que no arroje errores al consultar
		if @CadastralIdentification = ''
		Begin
			set @CadastralIdentification = '0'
		End

		if @CadastralIdentification2 = ''
		Begin
			set @CadastralIdentification2 = '0'
		End

		if @Address2 = ''
		Begin
			set @Address2 = 'zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz'
		End
		
		--Se valida si ya existen predios registrados
		if (select COUNT(*) from Taxes.TaxesProperty) = 0
		Begin
			select 0 as State , 'No existen predios registrados para realizar la liquidación de impuestos' Message, 0 as TaxesLiquidationId, '' as MessagesIds
			return
		End
		
		declare @Table table(Id int identity(1,1) primary key, TaxesPropertyId int, Code varchar(20), BuiltArea decimal(18,0), EconomicDestiny varchar(5), Appraisal decimal(18,0), LandArea decimal(18,0))
		insert into @Table
		select distinct tp.Id, tp.Code, tp.BuiltArea, tp.EconomicDestiny, tp.Appraisal, tp.LandArea
		from Taxes.TaxesProperty tp
		inner join Taxes.TaxesPropertyOwner tpo on tpo.TaxesPropertyId = tp.Id
		inner join Common.ThirdParty t on t.Id = tpo.ThirdPartyId
		where tp.Taxed = 1 
		and tp.Status = 1 
		and (select COUNT(*) from Taxes.TaxesProperty where t.Id = tpo.ThirdPartyId and Status = 1 and Taxed = 1) > 1
		and CAST(LTRIM(RTRIM(tp.Code)) as numeric(38,0)) >= case CAST(@CadastralIdentification as numeric(38,0)) when 0 then 0 else CAST(@CadastralIdentification as numeric(38,0)) end  and CAST(LTRIM(RTRIM(tp.Code)) as numeric(38,0)) <= case CAST(@CadastralIdentification2 as numeric(38,0)) when 0 then 9999999999999999999 else CAST(@CadastralIdentification2 as numeric(38,0)) end
		and tp.Addres >= @Address and tp.Addres <= @Address2
		and @OwnerId = case @OwnerId when 0 then @OwnerId else tpo.ThirdPartyId end
		and @PropertyType = case @PropertyType when 0 then @PropertyType else tp.[Type] end
		and tp.LandArea > 0
		and (select count(*) 
		from Taxes.TaxesLiquidationDetail tld
		inner join Taxes.TaxesLiquidation tl on tld.TaxesLiquidationId = tl.Id
		where tl.YearLiquidation = @Year and tld.TaxesInvoiceId is not null and tld.TaxesPropertyId = tp.Id) = 0

		--Se valida que hayan registros que cumplan con las condiciones enviadas desde el formulario
		if (select COUNT(*) from @Table) = 0
		Begin
			select 0 as State , 'No existen registros que cumplan con las condiciones anteriormente enviadas para realizar la liquidación de impuestos' Message, 0 as TaxesLiquidationId, '' as MessagesIds
			return
		End
		
		--Se crea la cabecera si no existe(TaxesLiquidation)
		if (select COUNT(*) from Taxes.TaxesLiquidation where YearLiquidation = @Year) = 0
		Begin
			insert into Taxes.TaxesLiquidation(YearLiquidation, Status, CreationUser, CreationDate)
			values(@Year, 1, @CodeUser, [Common].[GETDATE]())

			--Obtengo el id
			set @TaxesLiquidationId = SCOPE_IDENTITY()
		End
		Else --Si ya existe la cabecera
		Begin
			select @TaxesLiquidationId = Id from Taxes.TaxesLiquidation where YearLiquidation = @Year
		End

		--Se consulta los conceptos que van en la liquidación
		select @TaxesLiquidationConceptIdIMP = Id from Taxes.TaxesLiquidationConcept where Code = '01'
		select @TaxesLiquidationConceptIdBOM = Id from Taxes.TaxesLiquidationConcept where Code = '02'
		select @TaxesLiquidationConceptIdCAM = Id from Taxes.TaxesLiquidationConcept where Code = '03'

		--Elimino los registros que se encuentran en las tablas de liquidación de impuestos

		--Se elimina la tabla TaxesLiquidationDetailConcept
		delete from Taxes.TaxesLiquidationDetailConcept where TaxesLiquidationDetailId in 
		(select Id from Taxes.TaxesLiquidationDetail where TaxesLiquidationId = @TaxesLiquidationId)

		--Se elimina la tabla TaxesLiquidationDetail
		delete tld from Taxes.TaxesLiquidationDetail tld
		inner join Taxes.TaxesLiquidation tl on tl.Id = tld.TaxesLiquidationId
		where tl.YearLiquidation = @Year and tld.TaxesPropertyId in
		(select TaxesPropertyId from @Table)
				
		--Se declara el cursor y recorro los registros del predio con el cursor
		Declare InfoItem Cursor For 
		select TaxesPropertyId, Code, BuiltArea, EconomicDestiny, Appraisal, LandArea
		from @Table
		
		Open InfoItem
		Fetch Next From InfoItem Into @TaxesPropertyId, @Code, @BuiltArea, @EconomicDestiny, @Appraisal, @LanArea
		While @@fetch_status = 0
		Begin
			set @Index += 1
			--Se obtiene el tipo de predio si viene vacío
			if @PropertyType = 0
			Begin
				set @PropertyType = Taxes.fnGetPropertyTypeByCode(@Code)
			End
			
			--Se valida si el predio está edificado
			if ROUND((@BuiltArea * 100 / @LanArea), 0) >= 20
			Begin
				set @IsBuilt = 1
			End
			Else
			Begin
				set @IsBuilt = 0
			End
			
			--Predios urbanos edificados
			if @PropertyType = 1 and @IsBuilt = 1
			Begin
				--Se calcula el porcentaje del impuesto
				set @TaxPercentage = Taxes.fnCalculateTaxPercentage(@Appraisal, @SMLV)
			End

			--Predios urbanos no edificados
			if @PropertyType = 1 and @IsBuilt = 0
			Begin
				--NOTA: Falta verificar que es la cuota de inundación, porque todos los predios que esten por debajo de esta cuota
				--el porcentaje que se le aplica es el 8.5 y no 30

				--Se calcula el porcentaje del impuesto
				set @TaxPercentage = 30
			End

			--Predios rurales edificados
			if @PropertyType = 2 and @IsBuilt = 1
			Begin
				--Se calcula el porcentaje del impuesto
				set @TaxPercentage = 8.5
			End

			--Predios rurales no edificados
			if @PropertyType = 2 and @IsBuilt = 0
			Begin
				--Se calcula el porcentaje del impuesto
				set @TaxPercentage = 16
			End
			
			--Se calcula el impuesto predial
			set @TaxPredial = ROUND((@Appraisal * @TaxPercentage / 1000), 0)

			--Se calcula el valor BOMBERIL
			set @BOMBERIL = ROUND((@TaxPredial * 1 / 100), 0)

			--Se calcula el valor CAM
			set @CAM = ROUND((@TaxPredial * 15 / 100), 0)

			--Valor total del predio
			declare @ValueTotal decimal(18,0) = @TaxPredial + @BOMBERIL + @CAM
			
			delete from @IDSDetails

			----Inserto los detalles de liquidación por cada propietario que exista en la tabla TaxesPropertyOwner
			insert into Taxes.TaxesLiquidationDetail(TaxesLiquidationId, TaxesPropertyId, Appraisal, ThirdPartyId, RateOwner, PercentageOwner,TotalValueTax, ValueOwner, TaxesInvoiceId) output inserted.Id into @IDSDetails(Id)
			select @TaxesLiquidationId,@TaxesPropertyId,@Appraisal,ThirdPartyId, @TaxPercentage, Percentage , @ValueTotal, ROUND((@ValueTotal * Percentage / 100), -2), NULL
			from Taxes.TaxesPropertyOwner 
			where TaxesPropertyId = @TaxesPropertyId and @OwnerId = case @OwnerId when 0 then @OwnerId else ThirdPartyId end 
			
			if @Index = 1 begin
				set @MinIdDetail = (select min(Id) from @IDSDetails)
			end

			----Se inserta un detalle con el valor del Impuesto Predial
			insert into Taxes.TaxesLiquidationDetailConcept(TaxesLiquidationDetailId, TaxesLiquidationConceptId, PercentageConcept,BaseValue, Value)
			select tld.Id, @TaxesLiquidationConceptIdIMP, @TaxPercentage, @Appraisal, ROUND((tpo.Percentage * @TaxPredial /100), -2) 
			from Taxes.TaxesLiquidationDetail tld
			inner join Taxes.TaxesPropertyOwner tpo on tpo.ThirdPartyId = tld.ThirdPartyId and tpo.TaxesPropertyId = tld.TaxesPropertyId
			inner join @IDSDetails d on d.Id = tld.Id
			
			----Se inserta un detalle con el valor del BOMBERIL
			insert into Taxes.TaxesLiquidationDetailConcept(TaxesLiquidationDetailId, TaxesLiquidationConceptId, PercentageConcept,BaseValue, Value)
			select tld.Id, @TaxesLiquidationConceptIdBOM, 1, @TaxPredial, ROUND((tpo.Percentage * @BOMBERIL /100), -2) 
			from Taxes.TaxesLiquidationDetail tld
			inner join Taxes.TaxesPropertyOwner tpo on tpo.ThirdPartyId = tld.ThirdPartyId and tpo.TaxesPropertyId = tld.TaxesPropertyId
			inner join @IDSDetails d on d.Id = tld.Id

			----Se inserta un detalle con el valor del CAM
			insert into Taxes.TaxesLiquidationDetailConcept(TaxesLiquidationDetailId, TaxesLiquidationConceptId, PercentageConcept,BaseValue, Value)
			select tld.Id, @TaxesLiquidationConceptIdCAM, 15, @TaxPredial, ROUND((tpo.Percentage * @CAM /100), -2) 
			from Taxes.TaxesLiquidationDetail tld
			inner join Taxes.TaxesPropertyOwner tpo on tpo.ThirdPartyId = tld.ThirdPartyId and tpo.TaxesPropertyId = tld.TaxesPropertyId
			inner join @IDSDetails d on d.Id = tld.Id

			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @TaxesPropertyId, @Code, @BuiltArea, @EconomicDestiny, @Appraisal, @LanArea
		End
		Close InfoItem
		Deallocate InfoItem

		set @MaxIdDetail = (select max(Id) from @IDSDetails)		
		--Se asigna la ultima posición de la tabla taxesLiquidationDetail para realizar el between en el xpo
		set @messagesIds = CAST(@MinIdDetail as varchar) + ',' + CAST(@MaxIdDetail as varchar)
		
		select 1 as State , 'Se guardó correctamente' Message, @TaxesLiquidationId as TaxesLiquidationId, @messagesIds as MessagesIds

	END TRY
    BEGIN CATCH
		Close InfoItem
		Deallocate InfoItem
        select 0 as State , ERROR_MESSAGE() + ', Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) Message, 0 as TaxesLiquidationId, '' as MessagesIds
    END CATCH;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y guarda la liquidación anual de impuestos prediales (impuesto predial, bomberil y CAM) para uno o varios predios registrados en el sistema. Recibe como parámetros el año de liquidación, un rango de identificación catastral, dirección, propietario y tipo de predio (urbano o rural) para filtrar los inmuebles de TaxesProperty que estén activos, gravados y que aún no tengan factura emitida en el año indicado. Crea o reutiliza la cabecera de liquidación en TaxesLiquidation, elimina detalles previos no facturados de TaxesLiquidationDetail y TaxesLiquidationDetailConcept, y recalcula el impuesto aplicando tarifas según el avalúo catastral, el destino económico y el área del predio. Devuelve los identificadores de los registros de detalle generados para su posterior consulta, permitiendo llevar el ciclo de vida tributario del predio desde la generación hasta la facturación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTaxesLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_SaveTaxesLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la liquidación anual del impuesto predial (con bomberil y CAM) por predio y propietario, calculando tarifas según tipo de predio (urbano/rural) y si está edificado, y persistiendo cabecera, detalles y conceptos.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir predios registrados en Taxes.TaxesProperty; si no, retorna estado 0 con mensaje informativo.; Deben existir predios que cumplan filtros (Taxed=1, Status=1, propietario con más de 1 predio activo gravado, rango catastral, dirección, propietario, tipo, LandArea>0 y sin factura ya emitida en el año).; Los conceptos ''01'' (Predial), ''02'' (Bomberil) y ''03'' (CAM) deben existir en Taxes.TaxesLiquidationConcept.; El predio debe tener registros en Taxes.TaxesPropertyOwner para insertar detalles.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'SMLV se asume fijo en 689455 dentro del procedimiento (no se consulta de tabla parametrizada).; Impuesto predial = ROUND(Appraisal * Tarifa / 1000, 0).; Bomberil = 1% del impuesto predial; CAM = 15% del impuesto predial.; Valor total del predio = TaxPredial + BOMBERIL + CAM.; Solo se liquidan predios con Taxed=1, Status=1, LandArea>0 y cuyo propietario tenga más de un predio activo gravado.; No se reliquida un predio si ya tiene un detalle con TaxesInvoiceId no nulo en el año.; Solo existe una cabecera de TaxesLiquidation por año (YearLiquidation).; Los valores monetarios por propietario se redondean a la centena (ROUND(...,-2)).; El reparto por propietario se hace según Percentage de TaxesPropertyOwner.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Taxes.TaxesLiquidation: Si no existe cabecera para el año (YearLiquidation = @Year), se crea con Status=1 y usuario/fecha de creación.; [DELETE] Taxes.TaxesLiquidationDetailConcept: Antes de recalcular, elimina los conceptos cuyos detalles pertenecen a la liquidación del año.; [DELETE] Taxes.TaxesLiquidationDetail: Elimina detalles de la liquidación del año cuyo TaxesPropertyId esté en el conjunto filtrado a recalcular (purga previa al recalculo).; [INSERT] Taxes.TaxesLiquidationDetail: Por cada predio y propietario en TaxesPropertyOwner, inserta un detalle con el avalúo, tarifa, porcentaje del propietario, valor total del predio y ValueOwner = ROUND(ValueTotal*Percentage/100,-2); TaxesInvoiceId queda NULL.; [INSERT] Taxes.TaxesLiquidationDetailConcept: Inserta concepto Predial (código ''01'') con PercentageConcept=tarifa calculada, BaseValue=avalúo y Value=ROUND(Percentage*TaxPredial/100,-2) por propietario.; [INSERT] Taxes.TaxesLiquidationDetailConcept: Inserta concepto Bomberil (código ''02'') con PercentageConcept=1, BaseValue=TaxPredial y Value=ROUND(Percentage*BOMBERIL/100,-2).; [INSERT] Taxes.TaxesLiquidationDetailConcept: Inserta concepto CAM (código ''03'') con PercentageConcept=15, BaseValue=TaxPredial y Value=ROUND(Percentage*CAM/100,-2).; [RETURN_RESULT] (resultset): Devuelve State=1 con TaxesLiquidationId y rango Min,Max de IDs de detalle creados; State=0 si no hay predios, no hay registros que cumplan condiciones, o si ocurre error (devuelve ERROR_MESSAGE y línea).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CadastralIdentification o @CadastralIdentification2 vienen vacíos → Se asignan a ''0'' para que el filtro numérico no falle (0 se trata como sin límite: 0 inferior, 9999999999999999999 superior).; si @Address2 viene vacío → Se asigna ''zzzz...'' para actuar como límite superior abierto en el filtro de dirección.; si No existen predios registrados (COUNT TaxesProperty=0) → Retorna State=0 con mensaje y termina.; si @Table sin registros tras filtros → Retorna State=0 con mensaje ''No existen registros que cumplan...'' y termina.; si Existe cabecera de liquidación para el año → Reusa el TaxesLiquidationId existente. else Crea una nueva cabecera con Status=1.; si @PropertyType = 0 dentro del cursor → Se determina tipo de predio invocando Taxes.fnGetPropertyTypeByCode(@Code).; si ROUND(BuiltArea*100/LandArea,0) >= 20 → Se marca el predio como edificado (@IsBuilt=1). else Se marca como no edificado (@IsBuilt=0).; si PropertyType=1 (urbano) y edificado → Tarifa = Taxes.fnCalculateTaxPercentage(Appraisal, SMLV).; si PropertyType=1 (urbano) y no edificado → Tarifa fija = 30 por mil.; si PropertyType=2 (rural) y edificado → Tarifa fija = 8.5 por mil.; si PropertyType=2 (rural) y no edificado → Tarifa fija = 16 por mil.; si @OwnerId = 0 → No filtra por propietario (incluye todos los propietarios del predio).; si Error en TRY → CATCH cierra y desaloja el cursor y retorna State=0 con ERROR_MESSAGE y línea.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveTaxesLiquidation';
-- GO
