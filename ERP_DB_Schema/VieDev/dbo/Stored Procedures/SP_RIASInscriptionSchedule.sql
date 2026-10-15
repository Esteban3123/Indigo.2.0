-- =========================================================================
-- Author:		Rafael Eduardo Patiño cabrera
-- Create date: 17/12/2018
-- Description:	Store procedure que se encarga de la inscripción de RIAS desde el proceso de agendamiento
-- =========================================================================
CREATE PROCEDURE [dbo].[SP_RIASInscriptionSchedule]
	@PatientCode as varchar(25),
	@IDCita as int,
	@IdRiasCups as int,
	@CUPS as varchar(20),
	@UserCode as varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	begin try

	
	    --si el paciente no esta en la RIAS ejecutamos proceso de inscripcion
		if not exists(select RP.ID from RIASXPACIENTE RP inner join RIASCUPS  RC on RP.IDRIAS = RC.IDRIAS where RP.IPCODPACI = @PatientCode and RC.ID = @IdRiasCups  )
		begin
			--Tabla para almacenar la info para registrar las inscripciones de RIAS
			declare @XmlRiasInscription table(CupsCode varchar(20), RIASCupsId int, Quantity int, ServiceOrderDetailId int, ValueCups decimal(18,2), HealthProfessionalCode varchar(20), RealizationDate datetime)		
		
			--Se insertan los datos a la tabla de xml para inscribir las RIAS
			insert into @XmlRiasInscription(CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode,RealizationDate)
			select @CUPS, @IdRiasCups, 1,null, null, null,[Common].[GETDATE]() 
		
				--Si hay datos en la tabla xml para inscribir las rias se consume el sp
			if EXISTS(select * from @XmlRiasInscription)
			begin
				--Variable xml para enviar al sp
				declare @GenerateXml xml = (select CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode,RealizationDate from @XmlRiasInscription as RIASForPatient For Xml Auto, Elements)
				--Variables en donde se almacena el resultado de la inscripción de RIAS
				declare @CodeMessageInscription varchar(20), @MessageInscription varchar(max)
				--Se ejecuta el sp de inscripción
				exec dbo.SP_RIASInscription @GenerateXml, @PatientCode, null, @UserCode,@IDCita ,  3, 3, 1, @CodeMessageInscription output, @MessageInscription output

				--Si hay error ejecutando el sp
				if @CodeMessageInscription = '999'
				begin
					select '999' as CodeMessage, @MessageInscription as [Message]
					return
				end
			end
		end

			
	    --proceso de insert o Update
		--Se verifica, si existe un registro (empezar por el registro mas nuevo) con el mismo paciente y mismo CUPS y misma RIAS y estado sin realizar y fecha max para realizar mayor a la actual
		if exists(select * from RIASCUPSPACIENTE where IPCODPACI = @PatientCode and IDRIASCUPS = @IdRiasCups and CODSERIPS = @CUPS and ESTADO = 1 and FECHAMAXREALIZAR > [Common].[GETDATE]() )
		begin
		    declare @IDcitaTmp as int
			declare @IDRIASCUPSPACIENTE as int
			select top 1 @IDRIASCUPSPACIENTE = ID, @IDcitaTmp = IDCITA  from RIASCUPSPACIENTE where IPCODPACI = @PatientCode and IDRIASCUPS = @IdRiasCups and CODSERIPS = @CUPS and ESTADO = 1 and FECHAMAXREALIZAR > [Common].[GETDATE]() order by FECHACREACION desc 
			--si no tiene cita se relaciona el idcita creada al campo IdCita
			if @IDcitaTmp is null
			begin
			print 'paso'
				update 	RIASCUPSPACIENTE set IDCITA = @IDCita , ACCION = 2, CODUSUARUMOD = @UserCode,FECHAMODIFICACION = getdate() where ID = @IDRIASCUPSPACIENTE 
				select '0' as CodeMessage, 'Se realizó el proceso de inscripción correctamente' as [Message]
				return				
			end else begin 
			    --si tiene cita relacionada
				--si la fecha de la cita no ha pasado se genera el mensaje (restrictivo) “El paciente ya tiene una cita con la actividad xxxx (CUPS xxxx), RIAS xxx, para la fecha xxx” 
				if exists(select * from AGASICITA where CODAUTONU = @IDcitaTmp and IDRIASCUPS  = @IdRiasCups  and convert(date,FECHORAIN) > convert(date,[Common].[GETDATE]()))
				begin 
					declare @FechaCita as datetime
					declare @NombreActividad as varchar(200)
					declare @NombreRias as varchar(200)
					declare @NombreCups as varchar(200)
					
					select top 1 @NombreRias = R.NOMBRE , @NombreActividad = rtrim(ltrim(Act.DESACTMED)), @FechaCita = C.FECHORAIN from AGASICITA C inner join AGACTIMED Act on C.CODACTMED = Act.CODACTMED   
					inner join RIASCUPS RC on RC.ID = C.IDRIASCUPS 
					inner join RIAS R on R.ID = RC.IDRIAS 
					where CODAUTONU = @IDcitaTmp and c.IDRIASCUPS is not null and convert(date,FECHORAIN) >= convert(date,[Common].[GETDATE]())

					select top 1 @NombreCups = rtrim(ltrim(DESSERIPS))  from INCUPSIPS where codserips = @cups 
					
					select '999' as CodeMessage, 'El paciente ya tiene una cita con la actividad: ' + @NombreActividad + ' (CUPS: ' + @NombreCups + '), RIAS: ' + @NombreRias + ', para la fecha: ' + convert(varchar(20), @FechaCita,103) + ' ' +  convert(varchar(20), @FechaCita,108)  as [Message]
					return

				end else begin
					--si la fecha de la cita ya pasó se ejecuta el proceso de insert
					goto procesoInsert
				end
			end 
		end else begin 
			--proceso de insert
			goto procesoInsert
		end
	   	

		--proceso de insercion a la tabla RIAS CUPS x Paciente
		procesoInsert:
			delete from @XmlRiasInscription
			--Se insertan los datos a la tabla de xml para inscribir las RIAS
			insert into @XmlRiasInscription(CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode,RealizationDate)
			select @CUPS, @IdRiasCups, 1,null, null, null,[Common].[GETDATE]()  
		
			--Si hay datos en la tabla xml para inscribir las rias se consume el sp
			if EXISTS(select * from @XmlRiasInscription)
			begin
				--Variable xml para enviar al sp
				set @GenerateXml  = (select CupsCode, RIASCupsId, Quantity, ServiceOrderDetailId, ValueCups, HealthProfessionalCode,RealizationDate from @XmlRiasInscription as RIASForPatient For Xml Auto, Elements)
				--Variables en donde se almacena el resultado de la inscripción de RIAS
				set @CodeMessageInscription = null
				set @MessageInscription = null
				--Se ejecuta el sp de inscripción
				exec dbo.SP_RIASInscription @GenerateXml, @PatientCode, null, @UserCode,@IDCita,3, 3,0, @CodeMessageInscription output, @MessageInscription output

				--Si hay error ejecutando el sp
				if @CodeMessageInscription = '999'
				begin
					select '999' as CodeMessage, @MessageInscription as [Message]
					return
				end
			end

		
		select '0' as CodeMessage, 'Se realizó el proceso de inscripción correctamente' as [Message]

	end try
	begin catch		
		select '999' as CodeMessage, ERROR_MESSAGE() as [Message]
	end catch

END