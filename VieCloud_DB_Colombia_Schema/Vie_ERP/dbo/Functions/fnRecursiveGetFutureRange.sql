

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula recursivamente el siguiente rango de edad futuro que aplica a un paciente según las reglas de elegibilidad RIAS-CUPS, para determinar cuándo debe volver a realizarse un servicio o procedimiento de salud. Recibe la fecha de nacimiento del paciente, la fecha y cantidad de realizaciones previas del servicio, y el identificador del rango actual para buscar el siguiente rango posterior en la tabla RIASCUPSD. Según la regla del rango (por edad, por última fecha de realización o por periodo vigente), calcula las fechas mínima y máxima en que el paciente debería retornar para la próxima atención, apoyándose en la función fnCalculateDateFrecuencyCurrentPeriod para periodos vigentes. Si el rango siguiente tampoco aplica (por ejemplo, el paciente aún no alcanza la edad o el tiempo parametrizado supera el rango), se llama a sí misma recursivamente hasta encontrar el rango correcto o agotar las opciones; esta lógica es la base del motor de actividades futuras o próximas citas en la Ruta Integral de Atención en Salud (RIAS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnRecursiveGetFutureRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnRecursiveGetFutureRange';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcular recursivamente el siguiente rango de edad aplicable a un paciente dentro de una RIAS-CUPS y determinar las fechas mínima y máxima en que debería volver a realizarse la actividad, según la regla de frecuencia configurada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir parametrización en RIASCUPSD para el RiasCups indicado, con ESTADO=1, REGLA distinta de 1 y REQUIEREORDENMED=0; Se requiere fecha de nacimiento del paciente para calcular edades en días; Para Rule=3 se requiere fecha de realización previa válida del CUPS; Para Rule=4 se requiere la cantidad de realizaciones del CUPS en el periodo vigente; UNIDADRANGO debe ser 1 (Días), 2 (Meses) o 3 (Años); UnitFrequency debe ser 1-4 para Rule=3', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles de RIASCUPSD con ESTADO = 1, REQUIEREORDENMED = 0 y REGLA <> 1; Se excluye explícitamente el rango cuyo ID coincida con el parámetro de detalle a ignorar; Solo se evalúan rangos cuya edad mínima en días sea mayor a la edad actual del paciente (rangos futuros); Se selecciona siempre el rango futuro más cercano (ORDER BY AgeDaysMinimum, TOP 1); La conversión de unidades de edad asume meses=30 días y años=365 días; El límite superior de edad en días se calcula sumando un periodo extra (día/mes/año) al máximo configurado; Cuando no aplica ninguna regla evaluable o no hay rango, igual se inserta una fila (posiblemente con NULLs) en la tabla resultante; La recursión termina al encontrar un rango cuya validación temporal cabe dentro de la edad máxima o cuando no hay más rangos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Edad del paciente; RIAS (Ruta Integral de Atención en Salud); CUPS; Actividades futuras / próximas citas; Frecuencia por rango de edad; Frecuencia por última fecha de realización; Frecuencia por periodo vigente; Orden médica; Periodo vigente (año/semestre/trimestre/mes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableRange: Cuando se identifica un rango futuro válido y se calculan ReturnMinimumDate/ReturnMaximumDate según la regla, se inserta una fila con los datos del rango y las fechas calculadas; [INSERT] @TableRange: Cuando Rule=3 y ValidationDate supera la edad máxima del rango, se inserta el resultado de la llamada recursiva excluyendo el rango actual; [INSERT] @TableRange: Cuando Rule=4 con RealizationQuantity>=Frequency y el inicio del nuevo periodo supera la edad máxima del rango, se inserta el resultado de la llamada recursiva al siguiente rango; [RETURN_RESULT] @TableRange: Si no existe siguiente rango futuro, retorna la tabla con una fila de valores nulos para los campos de rango y fechas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No se encuentra un rango futuro válido (RiasCupsDetailIdReturn IS NULL o <= 0) → Se retorna la tabla con valores nulos/por defecto sin calcular fechas else Se evalúan las reglas según el tipo de regla del rango; si Rule = 2 (frecuencia por rango de edad) → Fecha mínima = nacimiento + AgeDaysMinimum; Fecha máxima = nacimiento + AgeDaysMaximum; si Rule = 3 (frecuencia por última fecha de realización) y ValidationDate (RealizationDate + periodo según UnitFrequency) > fecha en que cumple edad máxima → Se invoca recursivamente la función excluyendo el rango actual para evaluar el siguiente rango y se retorna else Si ValidationDate > fecha de edad mínima, ReturnMinimumDate = ValidationDate; en caso contrario ReturnMinimumDate = fecha en que cumple edad mínima; si Rule = 4 (frecuencia por periodo vigente) y RealizationQuantity < Frequency → Si la fecha final del periodo vigente es menor a la fecha de edad máxima, ReturnMaximumDate = fecha final del periodo vigente; sino se mantiene la fecha de edad máxima; si Rule = 4 y RealizationQuantity >= Frequency y fecha inicial del nuevo periodo < fecha en que cumple edad máxima → ReturnMinimumDate = inicio del nuevo periodo; ReturnMaximumDate = mínimo entre fin del nuevo periodo y fecha de edad máxima else Se invoca recursivamente la función para evaluar el siguiente rango y se retorna; si UnitFrequency en Rule 3 → 1=Año (DATEADD year), 2=Semestre (6 meses * PeriodQuantity), 3=Trimestre (3 meses * PeriodQuantity), 4=Mes (DATEADD month); si UNIDADRANGO del detalle → 1=Días (edad tal cual), 2=Meses (edad*30), 3=Años (edad*365); el máximo se incrementa en 1 día/30/365 respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fnRecursiveGetFutureRange; dbo.fnCalculateDateFrecuencyCurrentPeriod', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnRecursiveGetFutureRange';
GO
