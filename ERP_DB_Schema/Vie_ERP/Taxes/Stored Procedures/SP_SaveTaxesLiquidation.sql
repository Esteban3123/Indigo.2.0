
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