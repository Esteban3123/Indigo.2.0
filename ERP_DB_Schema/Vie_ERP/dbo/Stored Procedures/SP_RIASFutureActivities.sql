
-- ====================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 17/12/2018
-- Description:	Store procedure que se encarga de registrar las actividades futuras, es decir, cuando el paciente debe volver para que le realicen de nuevo el cups
-- ====================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASFutureActivities]
	@Xml as xml,
	@PatientCode as varchar(25),
	@AdmissionNumber as varchar(10),
	@UserCode as varchar(20),
	@Source as integer,

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, RIASId int, RIASCupsDetailId int, Quantity int, RealizedQuantity int, RealizationDate datetime, Processed bit)
	
	begin try
		
		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			rc.IDRIAS as RIASId,
			0 as RIASCupsDetailId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('RealizedQuantity[1]','int') as RealizedQuantity,
			null as RealizationDate,
			0 as Processed
		from @Xml.nodes('/RIASForPatient') t(x)
		inner join dbo.RIASCUPS rc on rc.ID = t.x.value('RIASCupsId[1]','int')

		--Se obtiene la fecha de nacimiento del paciente para calcular la edad en días
		declare @AgeDatePatient datetime = (select IPFECNACI from INPACIENT where IPCODPACI = @PatientCode)

		--Variable para almacenar la edad del paciente en días
		declare @AgePatientInDays int = DATEDIFF(day, @AgeDatePatient, [Common].[GETDATE]())
		
		--Se obtiene la cantidad de registros que se van a iterar
		declare @Count int = (select count(*) from @TableXml x where x.Processed = 0)

		--Se recorren los cups que se han agregado en la orden de servicio para realizar el registro futuro
		while @Count > 0
		begin
			--Variables para obtener la información necesaria tanto del xml como del rango al que aplica el paciente
			declare @CupsCode varchar(20), @RIASCupsId int, @RIASCupsDetailId int, @Quantity int, @RealizedQuantity int, @RealizationDate as datetime,
			@AgeDaysMinimum int, @AgeDaysMaximum int, @Rule int, @Frequency int, @UnitFrequency int, @MinimumDate datetime, @MaximumDate datetime, @PeriodQuantity int

			--Variable que me identifica si tengo realizar un insert
			declare @EvaluateNextRange bit = 1

			--Se otienen los campos necesarios de la tabla temporal xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId, @RIASCupsDetailId = ta.RIASCupsDetailId, @Quantity = ta.Quantity, 
			@RealizedQuantity = ta.RealizedQuantity, @RealizationDate = ta.RealizationDate
			From @TableXml ta
			Where ta.Processed = 0

			--Se obtiene el id del rango actual y la fecha de realización del último registro
			select top 1 @RIASCupsDetailId = IDRIASCUPSD, @RealizationDate = FECHAREALIZACION
			from dbo.RIASCUPSPACIENTE 
			where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode
			order by ID desc

			--Se obtiene el rango actual al que aplica el paciente siempre y cuando el rango no requiera orden médica y la regla sea diferente a sin restricción
			select @Rule = rcd.REGLA, @UnitFrequency = rcd.UNIDADFRECUENCIA, @Frequency = rcd.FRECUENCIA, @PeriodQuantity = rcd.CANTIDADPERIODO,
			@AgeDaysMinimum = case UNIDADRANGO 
								when 1 then EDADMINIMA --Dias
					   			when 2 then EDADMINIMA * 30 --Meses a dias
								when 3 then EDADMINIMA * 365 --Años a dias
							  End,
			@AgeDaysMaximum = case UNIDADRANGO 
								when 1 then EDADMAXIMA --Dias
					   			when 2 then EDADMAXIMA * 30 --Meses a dias
								when 3 then EDADMAXIMA * 365 --Años a dias
							  End
			from dbo.RIASCUPSD rcd
			where rcd.ID = @RIASCupsDetailId and rcd.REQUIEREORDENMED = 0 and rcd.REGLA <> 1

			--Si la regla es frecuencia por rango de edad y la cantidad de veces que se realizó el cups en el rango de edad es menor a la parametrizada
			if @Rule = 2 and @RealizedQuantity < @Frequency
			begin
				--Se asigna que no evalue rango siguiente
				set @EvaluateNextRange = 0

				--la fecha minima es la actual
				set @MinimumDate = [Common].[GETDATE]()

				--La fecha maxima es la fecha en que el paciente cumple la edad del rango maximo
				set @MaximumDate = DATEADD(day, @AgeDaysMaximum, @AgeDatePatient)
			end
			
			--Si la regla es frecuencia por última fecha de realización y desde la fecha de realización de RiasCupsPaciente hasta la fecha
			--en que pase el tiempo parametrizado(1 mes, 1 trimestre, 1 semestre, 1 año) la edad del paciente se mantiene en el rango de edad actual
			if @Rule = 3
			begin
				--Se aumenta a la fecha de realización el tiempo parametrizado en el rango y esta es la fecha minima que el paciente debe volver
				set @MinimumDate = case @UnitFrequency
										when 1 then DATEADD(year, @PeriodQuantity, @RealizationDate) --Año
										when 2 then DATEADD(month, 6 * @PeriodQuantity, @RealizationDate) --Semestre
										when 3 then DATEADD(month, 3 * @PeriodQuantity, @RealizationDate) --Trimestre
										when 4 then DATEADD(month, @PeriodQuantity, @RealizationDate) --Mes
									end

				--Se calcula la cantidad de días para cuando el paciente cumpla la fecha que se acabo de calcular
				declare @ValidationDays int = DATEDIFF(day, @AgeDatePatient, @MinimumDate)

				--Se valida que el paciente este dentro del rango para cuando deba volver
				if (@ValidationDays >= @AgeDaysMinimum and @ValidationDays <= @AgeDaysMaximum)
				begin
					--Se asigna que no evalue rango siguiente
					set @EvaluateNextRange = 0

					--La fecha maxima es cuando el paciente cumpla la edad del rango maximo
					set @MaximumDate = DATEADD(day, @AgeDaysMaximum, @AgeDatePatient)
				end
			end

			--Si la frecuencia es por periodo vigente y la cantidad de veces que se realizó el cups en el periodo vigente es menor a la cantidad parametrizada
			if @Rule = 4 and @RealizedQuantity < @Frequency
			begin
				--Se asigna que no evalue rango siguiente
				set @EvaluateNextRange = 0

				--Se asigna a la fecha minima la fecha actual
				set @MinimumDate = [Common].[GETDATE]()

				--Se obtiene la fecha en que el paciente tiene la edad maxima del rango
				declare @RangeMaximumDate datetime = DATEADD(day, @AgeDaysMaximum, @AgeDatePatient)

				--Se obtiene la fecha final del periodo vigente
				declare @FrecuencyDate datetime = dbo.fnCalculateDateFrecuencyCurrentPeriod(year(@MinimumDate), @MinimumDate, @UnitFrequency)

				--Se valida cual de las dos fechas es menor
				if @FrecuencyDate < @RangeMaximumDate
				begin
					set @MaximumDate = @FrecuencyDate
				end
				else begin
					set @MaximumDate = @RangeMaximumDate
				end
			end

			--Si se debe evaluar un rango posterior
			if @EvaluateNextRange = 1
			begin
				--Se ejecuta la función recursiva para evaluar los rangos posteriores y poder obtener la información necesaria para realizar el insert
				select @RIASCupsDetailId = RiasCupsDetailId, @MinimumDate = ReturnMinimumDate, @MaximumDate = ReturnMaximumDate
				from [dbo].[fnRecursiveGetFutureRange](@RIASCupsId, @RIASCupsDetailId, @AgeDatePatient, @RealizationDate, @RealizedQuantity)

				--Si viene el id del rango lleno es porque se evaluó los rangos posteriores y se encontró uno para realizar el insert
				if @RIASCupsDetailId is not null and @RIASCupsDetailId > 0
				begin
					set @EvaluateNextRange = 0
				end
			end

			--Si se realiza un insert
			if @EvaluateNextRange = 0
			begin
				--Se inserta en la tabla RiasCupsPaciente
				insert into [dbo].[RIASCUPSPACIENTE]([IPCODPACI], [IDRIASCUPS], [CODSERIPS], [CANTIDAD], [ESTADO], [FECHAMINREALIZAR], [FECHAMAXREALIZAR],
				[IDRIASCUPSD],[ORIGEN], ACCION, CODUSUARU, FECHACREACION)
				values(@PatientCode, @RIASCupsId, @CupsCode, @Quantity, 1, @MinimumDate, @MaximumDate, @RIASCupsDetailId, @Source, 1, @UserCode, [Common].[GETDATE]())
			end

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableXml set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
			
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de actividades futuras correctamente'
		return		
	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
	end catch

END