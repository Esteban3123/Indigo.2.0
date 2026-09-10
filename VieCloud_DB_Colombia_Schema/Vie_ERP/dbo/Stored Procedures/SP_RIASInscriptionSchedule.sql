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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que automatiza la inscripción de un paciente a una Ruta Integral de Atención en Salud (RIAS) durante el proceso de agendamiento de citas. Verifica si el paciente ya está enrolado en la RIAS correspondiente al código CUPS solicitado; si no lo está, lo inscribe llamando al procedimiento SP_RIASInscription. Luego gestiona la programación del servicio CUPS dentro de la ruta del paciente (tabla RIASCUPSPACIENTE): si ya existe una actividad pendiente sin cita asignada la vincula a la nueva cita, si ya tiene una cita futura la bloquea con un mensaje restrictivo, y si no hay pendientes vigentes registra una nueva programación. Es el punto de entrada RIAS desde el módulo de agendamiento, coordinando enrolamiento, validación de duplicados de citas y trazabilidad de actividades RIAS por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASInscriptionSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASInscriptionSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Inscribe al paciente en una RIAS desde el agendamiento y gestiona la programación del CUPS asociado: vincula citas pendientes, bloquea duplicados con cita futura o registra nueva programación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, la cita, la RIAS-CUPS y el CUPS deben existir y ser válidos.; Debe existir el procedimiento dbo.SP_RIASInscription para delegar la inscripción.; La función [Common].[GETDATE]() debe estar disponible para obtener la fecha actual del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se inscribe dos veces a un paciente en la misma RIAS: primero verifica RIASXPACIENTE/RIASCUPS antes de llamar a SP_RIASInscription.; Solo se considera una programación pendiente como vigente si ESTADO=1 y FECHAMAXREALIZAR > fecha actual.; Cuando hay múltiples registros pendientes vigentes, siempre se opera sobre el más reciente (ORDER BY FECHACREACION DESC).; No se permite programar una nueva actividad si el paciente ya tiene cita futura para la misma RIAS-CUPS.; Toda actualización de RIASCUPSPACIENTE registra usuario modificador y fecha de modificación.; Los errores siempre se retornan con CodeMessage=''999'' y los éxitos con CodeMessage=''0''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Inscripción de paciente en RIAS; CUPS (código de servicio); Cita médica / agendamiento; Actividad médica; Programación de actividades RIAS por paciente; Validación de duplicidad de citas; Trazabilidad de modificaciones (usuario y fecha)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.RIASCUPSPACIENTE: Cuando existe un registro pendiente (ESTADO=1, FECHAMAXREALIZAR > fecha actual) para el paciente/CUPS/RIAS y NO tiene IDCITA asignada, se asocia la cita actual y se marca ACCION=2 con usuario y fecha de modificación.; [RETURN_RESULT] RESULT: Si SP_RIASInscription retorna CodeMessage=''999'', se devuelve el mismo código y mensaje de error y se termina la ejecución.; [RETURN_RESULT] RESULT: Si el paciente ya tiene una cita futura (FECHORAIN > hoy) para la misma RIAS-CUPS, se devuelve CodeMessage=''999'' con mensaje restrictivo indicando actividad, CUPS, RIAS y fecha de la cita existente.; [RETURN_RESULT] RESULT: Cuando la inscripción y/o programación se completan sin errores, se devuelve CodeMessage=''0'' con mensaje ''Se realizó el proceso de inscripción correctamente''.; [RAISERROR] RESULT: Cualquier excepción capturada en el bloque CATCH retorna CodeMessage=''999'' con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El paciente no está inscrito en la RIAS asociada al IdRiasCups (no existe en RIASXPACIENTE join RIASCUPS) → Se ejecuta SP_RIASInscription con parámetros (3,3,1) para realizar la inscripción inicial en la RIAS. else Se omite la inscripción inicial y se pasa directo al proceso de programación.; si Existe registro en RIASCUPSPACIENTE pendiente (ESTADO=1) y vigente (FECHAMAXREALIZAR > hoy) para el paciente/CUPS/RIAS → Se evalúa si tiene cita asociada para decidir entre actualizar, bloquear o reinsertar. else Se ejecuta el proceso de inserción (procesoInsert) llamando a SP_RIASInscription con flag final 0.; si El registro pendiente vigente no tiene IDCITA (IDcitaTmp IS NULL) → Se actualiza RIASCUPSPACIENTE asignando la nueva cita y ACCION=2; retorna éxito. else Se valida si la cita existente es futura.; si La cita asociada al registro pendiente está en fecha futura (FECHORAIN > hoy en AGASICITA) → Se retorna error ''999'' con mensaje restrictivo de duplicidad de cita. else Se ejecuta el proceso de inserción (procesoInsert) creando una nueva programación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SP_RIASInscription', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASXPACIENTE; dbo.RIASCUPS; dbo.RIASCUPSPACIENTE; dbo.AGASICITA; dbo.AGACTIMED; dbo.RIAS; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASInscriptionSchedule';
-- GO
