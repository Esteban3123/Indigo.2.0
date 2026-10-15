

-- ========================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 7/12/2018
-- Description:	Función que me retorna el rango futuro por el cual aplica la edad del paciente, esta función
--				se ejecuta en el proceso de actividades futuras que es la que me indica cuando el paciente debe volver
-- ========================================================================================================================
CREATE FUNCTION [dbo].[fnRecursiveGetFutureRange]
(	
	@RiasCupsId int, --Id del riascups para realizar la consulta del rango
	@RiasCupsDetailId int, --Id del rango que no se va a tener en cuenta para consultar rangos posteriores
	@BirthDatePatient datetime, --Fecha de nacimiento del paciente
	@RealizationDate datetime, --Fecha de realización del cups en la inscripción
	@RealizationQuantity int --Cantidad que se ha realizado el cups
)
RETURNS @TableRange TABLE 
(
	RiasCupsDetailId int, 
	RiasCupsId int, 
	[Rule] int, 
	Frequency int, 
	UnitFrequency int, 
	PeriodQuantity int, 
	AgeDaysMinimum int, 
	AgeDaysMaximum int,
	AgeMinimum int,
	AgeMaximum int,
	UnitRangeAge varchar(20),
	RequiredMedicalOrder bit,
	ReturnMinimumDate datetime,
	ReturnMaximumDate datetime
)
AS
Begin
	
	--Variables para realizar el insert a la tabla que se retorna con los datos del rango
	declare @RiasCupsDetailIdReturn int, @Rule int, @Frequency int, @UnitFrequency int, @PeriodQuantity int, @AgeDaysMinimum int, @AgeDaysMaximum int,
	@AgeMinimum int, @AgeMaximum int, @UnitRangeAge varchar(20), @RequiredMedicalOrder bit, @ReturnMinimumDate datetime, @ReturnMaximumDate datetime

	--Se obtiene la edad del paciente en días para poder realizar la consulta del rango
	declare @AgeDaysPatient int = datediff(day, @BirthDatePatient, getdate())

	--Se obtienen los datos del rango al cual se va evaluar
	select  top 1 @RiasCupsDetailIdReturn = tmp.RiasCupsDetailId, @Rule = tmp.[Rule], @Frequency = tmp.Frequency, @UnitFrequency = tmp.UnitFrequency,
	@PeriodQuantity = tmp.PeriodQuantity, @AgeDaysMinimum = tmp.AgeDaysMinimum, @AgeDaysMaximum = tmp.AgeDaysMaximum, @AgeMinimum = tmp.AgeMinimum,
	@AgeMaximum = tmp.AgeMaximum, @UnitRangeAge = tmp.UnitRangeAge, @RequiredMedicalOrder = tmp.RequiredMedicalOrder
	from 
	(
		select
			ID as RiasCupsDetailId,
			IDRIASCUPS as RiasCupsId,
			REGLA as [Rule],
			FRECUENCIA as Frequency,
			UNIDADFRECUENCIA as UnitFrequency,
			CANTIDADPERIODO as PeriodQuantity,
			case UNIDADRANGO when 1 then EDADMINIMA      --Dia
					   			when 2 then (EDADMINIMA * 30)  --Meses a dias
								when 3 then (EDADMINIMA * 365)  --años a Dias
			End as AgeDaysMinimum,
			case UNIDADRANGO when 1 then EDADMAXIMA  + 1    --Dia
					   			when 2 then (EDADMAXIMA * 30) + 30 --Meses a dias
								when 3 then (EDADMAXIMA * 365) + 365 --años a Dias
			End as AgeDaysMaximum,
			EDADMINIMA as AgeMinimum,
			EDADMAXIMA as AgeMaximum,
			case UNIDADRANGO when 1 then 'Dias'
								when 2 then 'Meses'
								when 3 then 'Años'	END as UnitRangeAge,
			REQUIEREORDENMED as RequiredMedicalOrder
		from RIASCUPSD where IDRIASCUPS = @RiasCupsId and REQUIEREORDENMED = 0 and REGLA <> 1 and ID <> @RiasCupsDetailId and ESTADO = 1
	) as tmp
	where (tmp.AgeDaysMinimum > @AgeDaysPatient)
	order by tmp.AgeDaysMinimum

	--Si viene el rango asignado se valida ese rango, si no viene el rango asignado es porque no hay mas rangos para evaluar
	if @RiasCupsDetailIdReturn is not null and @RiasCupsDetailIdReturn > 0
	begin

		--Si la regla es frecuencia por rango de edad
		if @Rule = 2
		begin
			--La fecha minima es cuando el paciente cumpla la edad del rango minimo
			set @ReturnMinimumDate = DATEADD(day, @AgeDaysMinimum, @BirthDatePatient) 

			--La fecha maxima es cuando el paciente cumpla la edad del rango maximo
			set @ReturnMaximumDate = DATEADD(day, @AgeDaysMaximum, @BirthDatePatient) 
		end

		--Si la regla es frecuencia por última fecha de realización
		if @Rule = 3
		begin
			--Se aumenta a la fecha de realización el tiempo parametrizado en el rango
			declare @ValidationDate datetime = case @UnitFrequency
													when 1 then DATEADD(year, @PeriodQuantity, @RealizationDate) --Año
													when 2 then DATEADD(month, 6 * @PeriodQuantity, @RealizationDate) --Semestre
													when 3 then DATEADD(month, 3 * @PeriodQuantity, @RealizationDate) --Trimestre
													when 4 then DATEADD(month, @PeriodQuantity, @RealizationDate) --Mes
											   end

			--La fecha maxima es cuando el paciente cumpla la edad del rango maximo
			set @ReturnMaximumDate = DATEADD(day, @AgeDaysMaximum, @BirthDatePatient)

			--Si la fecha cuando cumple el tiempo parametrizado es mayor a la fecha en que el paciente cumple la edad del rango maximo se continua evaluando el siguiente rango
			if @ValidationDate > @ReturnMaximumDate
			begin
				--Se vuelve a consumir esta funcion para evaluar el siguiente rango
				insert into @TableRange
				select top 1 *
				from [dbo].[fnRecursiveGetFutureRange](@RiasCupsId, @RiasCupsDetailIdReturn, @BirthDatePatient, @RealizationDate, @RealizationQuantity)
				return
			end

			--Se establece la fecha cuando el paciente cumpla la edad del rango minimo
			set @ReturnMinimumDate = DATEADD(day, @AgeDaysMinimum, @BirthDatePatient)

			--Si la fecha cuando cumple el tiempo parametrizado es mayor a la fecha cuando el paciente cumple la edad del rango minimo
			if @ValidationDate > @ReturnMinimumDate
			begin 
				set @ReturnMinimumDate = @ValidationDate
			end
		end
		
		--Si la regla es frecuencia por periodo vigente
		if @Rule = 4
		begin
			--La fecha minima es cuando el paciente cumpla la edad del rango minimo
			set @ReturnMinimumDate = DATEADD(day, @AgeDaysMinimum, @BirthDatePatient)
			
			--Se obtiene la fecha final del periodo vigente
			declare @FrecuencyDate datetime = dbo.fnCalculateDateFrecuencyCurrentPeriod(year(@ReturnMinimumDate), @ReturnMinimumDate, @UnitFrequency)

			--Se obtiene la fecha cuando el paciente cumple la edad del rango maximo
			set @ReturnMaximumDate = DATEADD(day, @AgeDaysMaximum, @BirthDatePatient)

			--Si la cantidad de veces que se ha realizado el cups en el periodo es menor a la cantidad parametrizada
			if @RealizationQuantity < @Frequency
			begin
				--Si la fecha del periodo vigente es menor a la fecha maxima del rango se asigna, sino se deja la edad del rango maximo
				if @FrecuencyDate < @ReturnMaximumDate
				begin
					set @ReturnMaximumDate = @FrecuencyDate
				end
			end
			else if @RealizationQuantity >= @Frequency begin --Si la cantidad de veces que se ha realizado el cups en el periodo es >= a la cantidad parametrizada
				--Se obtiene la fecha inicial del nuevo periodo
				set @FrecuencyDate = CONVERT(datetime,DATEADD(dd,-(DAY(DATEADD(month, 1, @FrecuencyDate))-1),DATEADD(month, 1, @FrecuencyDate)))

				--Si la fecha inicial del nuevo periodo es menor a la fecha de cuando el paciente cumple la edad del rango maximo
				if @FrecuencyDate < @ReturnMaximumDate
				begin
					--La fecha minima es la fecha en que empieza el nuevo periodo
					set @ReturnMinimumDate = @FrecuencyDate

					--Se obtiene la fecha final del nuevo periodo
					set @FrecuencyDate = dbo.fnCalculateDateFrecuencyCurrentPeriod(year(@FrecuencyDate), @FrecuencyDate, @UnitFrequency)

					--Si la fecha final del nuevo periodo es menor a la fecha en que el paciente cumple la edad del rango maximo se asigna, si no se deja la que esta
					if @FrecuencyDate < @ReturnMaximumDate
					begin
						set @ReturnMaximumDate = @FrecuencyDate
					end
				end
				else begin --Si la fecha inicial del nuevo periodo es mayor se evalua el siguiente rango
					--Se vuelve a consumir esta funcion para evaluar el siguiente rango
					insert into @TableRange
					select top 1 *
					from [dbo].[fnRecursiveGetFutureRange](@RiasCupsId, @RiasCupsDetailIdReturn, @BirthDatePatient, @RealizationDate, @RealizationQuantity)
					return
				end
			end 						
		end

	end

	--Se retorna el rango con el que se realiza el insert en el sp de actividades futuras
	insert into @TableRange
	values(@RiasCupsDetailIdReturn, @RiasCupsId, @Rule, @Frequency, @UnitFrequency, @PeriodQuantity, @AgeDaysMinimum, @AgeDaysMaximum, @AgeMinimum, 
	@AgeMaximum, @UnitRangeAge, @RequiredMedicalOrder, @ReturnMinimumDate, @ReturnMaximumDate)
	return
End