

-- ============================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 08/01/2019
-- Description:	Store procedure que se encarga de realizar todo el proceso de rias, se ejecuta primero el proceso de inscripción y después el proceso de actividades futuras
-- ============================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASProcess_ManagementRias]
	@Xml as xml,
	@PatientCode as varchar(25),
	@AdmissionNumber as varchar(10),
	@UserCode as varchar(20),

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	
	--Variables en donde se almacena el resultado del proceso de inscripción de RIAS
	declare @CodeMessageInscription varchar(20), @MessageInscription varchar(max)

	--Variables en donde se almacena el resultado del proceso de actividades futuras
	declare @CodeMessageFutureActivities varchar(20), @MessageFutureActivities varchar(max)
	
	begin try

		--Se ejecuta el proceso de inscripción de RIAS
		exec dbo.SP_RIASInscription @Xml, @PatientCode, @AdmissionNumber, @UserCode, 0, 2, 1, 1, @CodeMessageInscription output, @MessageInscription output
		
		--Si hay error ejecutando el sp de inscripción
		if @CodeMessageInscription = '999'
		begin
			set @CodeMessage = '999'
			set @Message = @MessageInscription
			return
		end
		
		--Se ejecuta el proceso de actividades futuras
		exec dbo.SP_RIASFutureActivities @Xml, @PatientCode, @AdmissionNumber, @UserCode, 1, @CodeMessageFutureActivities output, @MessageFutureActivities output
		
		--Si hay error ejecutando el sp de actividades futuras
		if @CodeMessageFutureActivities = '999'
		begin
			set @CodeMessage = '999'
			set @Message = @MessageFutureActivities
			return
		end
													
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de RIAS correctamente'
		return		
	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el proceso completo de RIAS (Rutas Integrales de Atención en Salud) para un paciente en un ingreso específico. Primero realiza la inscripción del paciente en la ruta de atención invocando SP_RIASInscription, y luego genera las actividades futuras pendientes a través de SP_RIASFutureActivities. Si cualquiera de los dos subprocesos falla, detiene la ejecución y retorna el error correspondiente. Recibe como entrada un XML con los datos de la ruta, el código o cédula del paciente, el número de ingreso o admisión, y el usuario que ejecuta la operación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASProcess_ManagementRias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASProcess_ManagementRias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta el proceso RIAS ejecutando primero la inscripción del paciente y luego el registro de actividades futuras, deteniéndose si alguno falla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información de RIAS, código de paciente, número de admisión y código de usuario; Los procedimientos dbo.SP_RIASInscription y dbo.SP_RIASFutureActivities deben existir y aceptar la firma invocada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las actividades futuras nunca se ejecutan si la inscripción RIAS falló; El código de error estándar para fallo es ''999'' y para éxito es ''0''; Cualquier excepción no controlada se transforma en CodeMessage=''999'' con el mensaje de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Inscripción a RIAS; Actividades futuras de RIAS; Paciente; Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando SP_RIASInscription retorna CodeMessage=''999'', se propaga el error con CodeMessage=''999'' y el mensaje de inscripción, abortando el flujo; [RETURN_RESULT] N/A: Cuando SP_RIASFutureActivities retorna CodeMessage=''999'', se propaga el error con CodeMessage=''999'' y el mensaje de actividades futuras; [RETURN_RESULT] N/A: Cuando ambos sub-procesos terminan sin error, se retorna CodeMessage=''0'' y mensaje ''Se realizó el proceso de RIAS correctamente''; [RETURN_RESULT] N/A: Cuando ocurre una excepción capturada en el TRY/CATCH, se retorna CodeMessage=''999'' y el texto de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodeMessageInscription = ''999'' (falla la inscripción RIAS) → Asigna CodeMessage=''999'' con el mensaje de inscripción y retorna sin ejecutar actividades futuras else Continúa ejecutando SP_RIASFutureActivities; si @CodeMessageFutureActivities = ''999'' (falla actividades futuras) → Asigna CodeMessage=''999'' con el mensaje de actividades futuras y retorna else Retorna éxito con CodeMessage=''0''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SP_RIASInscription; dbo.SP_RIASFutureActivities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess_ManagementRias';
-- GO
