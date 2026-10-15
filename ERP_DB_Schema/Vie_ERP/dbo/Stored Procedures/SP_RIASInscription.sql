-- Stored Procedure

-- =========================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 7/12/2018
-- Description:	Store procedure que se encarga de la inscripción de RIAS
-- =========================================================================
CREATE PROCEDURE [dbo].[SP_RIASInscription]
	@Xml as xml,
	@PatientCode as varchar(25),
	@AdmissionNumber as varchar(10),
	@UserCode as varchar(20),
	@AppointmentsId as int,
	@SourceRiasPaciente as int,
	@SourceRiasCupsPaciente as int,
	@InsertAllCupsAssociate as bit,

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, RIASId int, Quantity int, ServiceOrderDetailId int, ValueCups decimal(18,2), 
	HealthProfessionalCode varchar(20), RealizationDate datetime)
	
	--Tabla temporal en donde ingreso las diferentes cups asociados a la rias que esta en el xml
	declare @TableAssociateCupsWithRias table(Id int primary key identity, RiasCupsId int, CupsCode varchar(20), RiasId int, Quantity int, ServiceOrderDetailId int, 
	ValueCups decimal(18,2), HealthProfessionalCode varchar(20), Processed bit, RealizationDate datetime)

	begin try

		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			rc.IDRIAS as RIASId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('ServiceOrderDetailId[1]','int') as ServiceOrderDetailId,
			t.x.value('ValueCups[1]','decimal(18,2)') as ValueCups,
			t.x.value('HealthProfessionalCode[1]','varchar(20)') as HealthProfessionalCode,
			t.x.value('RealizationDate[1]','datetime') as RealizationDate
		from @Xml.nodes('/RIASForPatient') t(x)
		inner join dbo.RIASCUPS rc on rc.ID = t.x.value('RIASCupsId[1]','int')

		--Se insertan los registros nuevos
		insert into dbo.RIASXPACIENTE(IDRIAS, IPCODPACI, FECHAINCRIPCION, ESTADO, CODMOTIVOEGRE, ORIGENINSCRIPCION, USUARIO)
		select rc.IDRIAS, @PatientCode, [Common].[GETDATE](), 2, null, @SourceRiasPaciente, @UserCode
		from @TableXml tx
		inner join dbo.RIASCUPS rc on rc.ID = tx.RIASCupsId
		left join dbo.RIASXPACIENTE rp on rp.IPCODPACI = @PatientCode and rp.IDRIAS = rc.IDRIAS
		where rp.ID is null
		group by rc.IDRIAS

		--Se actualizan los registros
		update rp set rp.ESTADO = 2, rp.FECHAINCRIPCION = [Common].[GETDATE]()
		from @TableXml tx
		inner join dbo.RIASCUPS rc on rc.ID = tx.RIASCupsId
		inner join dbo.RIASXPACIENTE rp on rp.IPCODPACI = @PatientCode and rp.IDRIAS = rc.IDRIAS
		where rp.ESTADO = 1

		--Se obtiene la fecha de nacimiento del paciente para calcular la edad en días
		declare @DatePatient datetime = (select IPFECNACI from INPACIENT where IPCODPACI = @PatientCode)

		--Variable para almacenar la edad del paciente en días
		declare @AgePatientInDays int = DATEDIFF(day, @DatePatient, [Common].[GETDATE]())
		
		--Si el proceso viene desde VIE se realiza la busqueda de todos los cups asociados a la rias
		if @InsertAllCupsAssociate = 1
		begin
			--Obtengo todos los cups que esten asociados a la RIAS
			insert into @TableAssociateCupsWithRias(RiasCupsId, CupsCode, RiasId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode, Processed, RealizationDate)
			select rc.ID, rc.CODSERIPS, rc.IDRIAS, tx.Quantity, tx.ServiceOrderDetailId, tx.ValueCups, IIF(tx.HealthProfessionalCode = '', null,tx.HealthProfessionalCode), 0, tx.RealizationDate
			from @TableXml tx
			inner join dbo.RIASCUPS rc on rc.IDRIAS = tx.RIASId
			group by rc.ID, rc.CODSERIPS, rc.IDRIAS, tx.Quantity, tx.ServiceOrderDetailId, tx.ValueCups, tx.HealthProfessionalCode, tx.RealizationDate
		end
		else begin --Si el proceso se ejecuta desde agendamiento de Crystal
			--Solo se inserta un registro con el id rias cups de la cita asociada
			insert into @TableAssociateCupsWithRias(RiasCupsId, CupsCode, RiasId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode, Processed, RealizationDate)
			select tx.RIASCupsId, tx.CupsCode, rc.IDRIAS, tx.Quantity, tx.ServiceOrderDetailId, tx.ValueCups, tx.HealthProfessionalCode, 0, tx.RealizationDate
			from @TableXml tx
			inner join dbo.RIASCUPS rc on rc.ID = tx.RIASCupsId
		end

		
		--Variables que se obtienen dentro del while para realizar validaciones
		declare @CupsCode varchar(20), @RIASCupsId int, @RiasCupsDetailId int, @AgeDaysMinimum int, @AgeDaysMaximum int, @MinimumDate datetime, @MaximumDate datetime, @Rule int, 
		@UnitFrequency int, @IsCurrentRange bit, @Quantity int, @ServiceOrderDetailId int, @ValueCups decimal(18,2), @HealthProfessionalCode varchar(20), @RealizationDate datetime

		--Se obtiene la cantidad de registros que se van a iterar
		declare @Count int = (select count(*) from @TableAssociateCupsWithRias ta where ta.Processed = 0)

		while @Count > 0
		begin
			--Se resetean los valores ya que cuando encuentra registro queda asignado con el anterior
			set @CupsCode =''
			set @RIASCupsId = 0
			set @Quantity =0
			set @ServiceOrderDetailId = 0
			set @ValueCups = 0
			set @HealthProfessionalCode = ''
			set @RealizationDate = null

			--Se otienen los campos necesarios de la tabla temporal que viene del xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId, @Quantity = ta.Quantity, @ServiceOrderDetailId = ta.ServiceOrderDetailId,
			@ValueCups = ta.ValueCups, @HealthProfessionalCode = ta.HealthProfessionalCode, @RealizationDate = ta.RealizationDate 
			From @TableAssociateCupsWithRias ta
			Where ta.Processed = 0

			--Se resetean los valores ya que cuando encuentra registro queda asignado con el anterior
			set @RiasCupsDetailId = 0
			set @AgeDaysMinimum = 0
			set @AgeDaysMaximum = 0 
			set @Rule = 0
			set @UnitFrequency = 0

			--Se obtiene el rango al cual la edad del paciente aplica por rias
			select @RiasCupsDetailId = RiasCupsDetailId, @AgeDaysMinimum = AgeDaysMinimum, @AgeDaysMaximum = AgeDaysMaximum, @Rule = [Rule], @UnitFrequency = UnitFrequency
			from dbo.fnValidateCurrentRangeOrNextRange(@AgePatientInDays, @RIASCupsId)
			
			--Si se encontró rango al cual aplicar se inserta en la tabla RiasCupsPaciente
			if @RiasCupsDetailId is not null and @RiasCupsDetailId > 0
			begin
		
				--Si la edad del paciente esta dentro de un rango entonces la fecha minima es la fecha en la cual se realizó el cups
				if (@AgePatientInDays BETWEEN @AgeDaysMinimum AND @AgeDaysMaximum)
				begin					
					set @IsCurrentRange = 1
					set @MinimumDate = @RealizationDate
				end
				else begin --Si la edad del paciente tiene un rango futuro entonces la fecha minima es cuando cumpla la edad minima del rango
					set @IsCurrentRange = 0
					set @MinimumDate = DATEADD(day, @AgeDaysMinimum, @DatePatient)
				end

				--Si la regla es diferente a frecuencia por periodo vigente(4) entonces la fecha maxima es cuando el paciente cumpla la edad maxima del rango
				if @Rule <> 4
				begin
					set @MaximumDate = DATEADD(day, @AgeDaysMaximum, @DatePatient)
				end
				else begin --Si la regla es frecuencia por periodo se evalua la unidad de frecuencia para poder obtener la fecha maxima
										
					--Fecha del rango minimo
					declare @RangeMinimumDate datetime

					--Se obtiene la fecha cuando el paciente cumpla la edad minima del rango siempre y cuando sea un rango futuro
					if @IsCurrentRange = 0
					begin 
						set @RangeMinimumDate = DATEADD(day, @AgeDaysMinimum, @DatePatient)
					end
					else begin --Se obtiene la fecha de la fecha de realización del cups para cuando el rango este dentro de uno actual
						set @RangeMinimumDate = @RealizationDate
					end

					--Se obtiene la fecha cuando el paciente cumpla la edad maxima del rango
					declare @RangeMaximumDate datetime = DATEADD(day, @AgeDaysMaximum, @DatePatient)

					--Se aumenta un año a la fecha del rango maximo
					set @RangeMaximumDate = DATEADD(year, 1, @RangeMaximumDate)

					--Se obtiene el año que se calculó con la edad minima del rango
					declare @YearMinimum int = year(@RangeMinimumDate)

					--Se arma la nueva fecha dependiendo de la unidad de frecuencia para realizar la comparación
					declare @FrequencyDate datetime = dbo.fnCalculateDateFrecuencyCurrentPeriod(@YearMinimum, @RangeMinimumDate, @UnitFrequency)
						
					--Se valida si la fecha de la frecuencia es menor a la fecha del rango maximo + 1
					if @FrequencyDate < @RangeMaximumDate
					begin
						set @MaximumDate = @FrequencyDate
					end
					else begin --Si la fecha del rango maximo + 1 es menor a la fecha de la frecuencia
						set @MaximumDate = @RangeMaximumDate
					end

				end
				
				--Se verifica si el cups que estoy recorriendo existe en el xml que se esta enviando para insertar o no valores en algunos campos
				declare @CountValidate int = 0
				if @SourceRiasCupsPaciente <> 1 begin
					set @CountValidate = (select count(*) from @TableXml where CupsCode = @CupsCode)
				end 

				--Se valida si ya existe un registro
				if exists (select * from dbo.RIASCUPSPACIENTE where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode and ESTADO = 1)
				begin
					--Se actualiza el registro
					update rcp set rcp.ESTADO = IIF(@AppointmentsId is null or @AppointmentsId = 0, IIF(@CountValidate > 0, 2, 1), 1), 
					FECHAMODIFICACION = [Common].[GETDATE](), CODUSUARUMOD = @UserCode, 
					rcp.FECHAREALIZACION = IIF(@AppointmentsId is null or @AppointmentsId = 0, IIF(@IsCurrentRange = 1, @RealizationDate, null), null),
					rcp.NUMINGRES = IIF(@CountValidate > 0, @AdmissionNumber, null),
					rcp.IDDETALLEORDENSERVICIO = IIF(@CountValidate > 0, @ServiceOrderDetailId, null), 
					ACCION = 2
					from dbo.RIASCUPSPACIENTE rcp
					where rcp.IPCODPACI = @PatientCode and rcp.IDRIASCUPS = @RIASCupsId and rcp.CODSERIPS = @CupsCode and rcp.ESTADO = 1 and rcp.IDDETALLEORDENSERVICIO is null
				end
				else begin
					--Se inserta en la tabla RiasCupsPaciente
					insert into [dbo].[RIASCUPSPACIENTE]([IPCODPACI], [IDRIASCUPS], [CODSERIPS], [CANTIDAD], [ESTADO], [NUMINGRES], [FECHAMINREALIZAR], [FECHAMAXREALIZAR],
					[IDDETALLEORDENSERVICIO], [VALORCUPS], [CODPROSAL], [CODUSUARU], [IDRIASCUPSD],[ORIGEN], IDCITA, ACCION, FECHACREACION, FECHAREALIZACION)
					values(@PatientCode, @RIASCupsId, @CupsCode, @Quantity, 
					IIF(@AppointmentsId is null or @AppointmentsId = 0, IIF(@CountValidate > 0, 2, 1), 1),
					IIF(@CountValidate > 0, @AdmissionNumber, null), @MinimumDate, @MaximumDate, 
					IIF(@CountValidate > 0, @ServiceOrderDetailId, null), @ValueCups, @HealthProfessionalCode, @UserCode, @RiasCupsDetailId, @SourceRiasCupsPaciente, 
					IIF(@InsertAllCupsAssociate = 1, null, @AppointmentsId), 1, [Common].[GETDATE](), IIF(@AppointmentsId is null or @AppointmentsId = 0, IIF(@IsCurrentRange = 1, @RealizationDate, null), null))
				end

			end

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableAssociateCupsWithRias set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
				
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de inscripción correctamente'
		return		
		--select '0' as CodeMessage, 'Se realizó el proceso de inscripción correctamente' as [Message]

	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
		--select '999' as CodeMessage, ERROR_MESSAGE() as [Message]
	end catch

END