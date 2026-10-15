-- =========================================================================
-- Author:		Rafael Eduardo patiño
-- Create date: 10/12/2018
-- Description:	se encarga de la inscripción de RIAS CUPS x Paciente desde plan de manejo al guarda la HC
-- =========================================================================
CREATE PROCEDURE [dbo].[SP_RIASInscriptionMedicalOrder]
	@Xml as xml,
	@PatientCode as varchar(25),
	@UserCode as varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, quantity int,Folio int, RIASId int, Processed bit)

	
	begin try

		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			t.x.value('Quantity[1]','int') as quantity,
			t.x.value('Folio[1]','int') as Folio,
			rc.IDRIAS as RIASId,
			0 as Processed
		from @Xml.nodes('/RIASForPatient') t(x)
		inner join dbo.RIASCUPS rc on rc.ID = t.x.value('RIASCupsId[1]','int')
		
	
		--Se obtiene la fecha de nacimiento del paciente para calcular la edad en días
		declare @DatePatient datetime = (select IPFECNACI from INPACIENT where IPCODPACI = @PatientCode)

		--Variable para almacenar la edad del paciente en días
		declare @AgePatientInDays int = DATEDIFF(day, @DatePatient, [Common].[GETDATE]())
	
		--Variables que se obtienen dentro del while para realizar validaciones
		declare @CupsCode varchar(20)
		declare @RIASCupsId int
		declare @RiasCupsDetailId int
		declare @Quantity as int
		declare @Folio as char(10)
		declare @AgeDaysMinimum int
		declare @AgeDaysMaximum int
		declare @MinimumDate datetime
		declare @MaximumDate datetime
		declare @Count int = (select count(*) from @TableXml ta where ta.Processed = 0)

		
		while @Count > 0
		begin
			--Se otienen los campos necesarios de la tabla temporal que viene del xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId , @Quantity = quantity , @Folio = Folio 
			From @TableXml ta
			Where ta.Processed = 0

				

			--Se obtiene el rango al cual la edad del paciente aplica por rias
			select @RiasCupsDetailId = ID , @AgeDaysMinimum = EDADMINIMA_DIAS , @AgeDaysMaximum = EDADMAXIMA_DIAS 
			from dbo.fnValidaRangoEdadRIASCUPS(@AgePatientInDays, @RIASCupsId) where REQUIEREORDENMED = 0
			
			print '@AgePatientInDays: ' + cast(@AgePatientInDays as varchar(50))
			print '@RIASCupsId: ' + cast(@RIASCupsId as varchar(50))
			print '@RiasCupsDetailId: ' + cast(@RiasCupsDetailId as varchar(50))
			

			--Si se encontró rango al cual aplicar se inserta en la tabla RiasCupsPaciente
			if @RiasCupsDetailId is not null and @RiasCupsDetailId > 0
			begin
				--Si la edad del paciente esta dentro de un rango entonces la fecha minima es la actual, y la fecha maxima es cuando el paciente cumpla la edad maxima del rango
				if (@AgePatientInDays BETWEEN @AgeDaysMinimum AND @AgeDaysMaximum)
				begin
					set @MinimumDate = [Common].[GETDATE]()
					set @MaximumDate = DATEADD(day, @AgeDaysMaximum, @DatePatient)
				end
			

				if exists(select * from RIASCUPSPACIENTE where IPCODPACI = @PatientCode  AND CODSERIPS = @CupsCode  AND IDRIASCUPS = @RIASCupsId  AND [Common].[GETDATE]() < FECHAMAXREALIZAR AND ESTADO = 1 )
				begin
					update dbo.RIASCUPSPACIENTE set FOLIO = @Folio , IDCITA = null, FECHAREALIZACION = null, ACCION = 2, CODUSUARUMOD = @UserCode,FECHAMODIFICACION = getdate()  where IPCODPACI = @PatientCode  AND CODSERIPS = @CupsCode  AND IDRIASCUPS = @RIASCupsId AND [Common].[GETDATE]() < FECHAMAXREALIZAR AND ESTADO = 1 
				end else begin
				   INSERT INTO dbo.RIASCUPSPACIENTE([IPCODPACI],[IDRIASCUPS],[CODSERIPS],[CANTIDAD],[ESTADO],[FOLIO],[NUMINGRES],[FECHAMINREALIZAR],[FECHAMAXREALIZAR],[FECHAREALIZACION],[IDCITA],[IDDETALLEORDENSERVICIO],[IDFACTURA],[VALORCUPS],[CODPROSAL],[CODUSUARU],[IDRIASCUPSD],[ORIGEN],[ACCION],[FECHACREACION])
				   VALUES(@PatientCode ,@RIASCupsId,@CupsCode,@Quantity,1,@Folio,NULL,@MinimumDate,@MaximumDate,@MinimumDate,NULL,NULL,NULL,NULL,NULL,@UserCode,@RiasCupsDetailId,2,1,[Common].[GETDATE]() )	
				end

			
			end

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableXml  set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
			
							
		select '0' as CodeMessage, 'Se realizó el proceso de inscripción correctamente' as [Message]

	end try
	begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() as [Message]
	end catch

END