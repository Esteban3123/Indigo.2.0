

-- ============================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 08/01/2019
-- Description:	Store procedure que se encarga de realizar todo el proceso de rias, se ejecuta primero el proceso de inscripción y después el proceso de actividades futuras
-- ============================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASProcess]
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
		exec dbo.SP_RIASInscription @Xml, @PatientCode, @AdmissionNumber, @UserCode, 0, 2, 4, 1, @CodeMessageInscription output, @MessageInscription output
		
		--Si hay error ejecutando el sp de inscripción
		if @CodeMessageInscription = '999'
		begin
			set @CodeMessage = '999'
			set @Message = @MessageInscription
			return
		end
		
		--Se ejecuta el proceso de actividades futuras
		exec dbo.SP_RIASFutureActivities @Xml, @PatientCode, @AdmissionNumber, @UserCode, 4, @CodeMessageFutureActivities output, @MessageFutureActivities output
		
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal que ejecuta el proceso completo de RIAS (Rutas Integrales de Atención en Salud) para un paciente y un ingreso específico. Orquesta en secuencia dos subprocesos: primero realiza la inscripción del paciente en la ruta de atención (llamando a SP_RIASInscription) y luego registra las actividades futuras de seguimiento (llamando a SP_RIASFutureActivities). Recibe los datos del paciente (código/cédula), número de ingreso, usuario que opera y un XML con la información de la ruta; retorna un código y mensaje indicando si el proceso fue exitoso o si ocurrió un error en alguna etapa. Se usa para garantizar que el paciente quede inscrito y con sus actividades programadas dentro del modelo de atención de RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASProcess';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASProcess';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta el proceso completo de RIAS ejecutando primero la inscripción del paciente y luego la programación de actividades futuras, devolviendo un código/mensaje unificado de éxito o error.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe ser interpretable por los sub-procesos de inscripción y de actividades futuras de RIAS; Deben existir el paciente y la admisión referenciados, así como el usuario que ejecuta la operación; Los procedimientos dependientes SP_RIASInscription y SP_RIASFutureActivities deben estar disponibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El proceso de actividades futuras solo se ejecuta si la inscripción a RIAS finalizó sin error; Cualquier error en cualquiera de las dos etapas se propaga con código ''999'' y el mensaje original del sub-proceso; El éxito total se reporta únicamente cuando ambas etapas (inscripción y actividades futuras) terminan correctamente, devolviendo código ''0''; Toda excepción no controlada se traduce a código ''999'' con el mensaje del motor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Inscripción a RIAS; Actividades futuras programadas; Paciente; Admisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SP_RIASInscription: Siempre invoca primero la inscripción de RIAS con parámetros fijos (0, 2, 4, 1) para registrar al paciente en el modelo RIAS; [RETURN_RESULT] dbo.SP_RIASFutureActivities: Cuando la inscripción no devuelve código ''999'', invoca el proceso de actividades futuras con parámetro fijo 4 para programar las actividades del paciente; [RETURN_RESULT] N/A: Cuando ambas etapas finalizan sin código ''999'', retorna código ''0'' y mensaje ''Se realizó el proceso de RIAS correctamente''; [RETURN_RESULT] N/A: Cuando ocurre una excepción capturada por CATCH, retorna código ''999'' con el texto de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de retorno de la inscripción de RIAS = ''999'' → Aborta el proceso devolviendo código ''999'' y el mensaje recibido del sub-proceso de inscripción else Continúa con la ejecución del proceso de actividades futuras; si Código de retorno de actividades futuras = ''999'' → Aborta el proceso devolviendo código ''999'' y el mensaje recibido del sub-proceso de actividades futuras else Finaliza con código ''0'' y mensaje de éxito; si Se produce una excepción en el bloque TRY → Devuelve código ''999'' y como mensaje el resultado de ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SP_RIASInscription; dbo.SP_RIASFutureActivities', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASProcess';
-- GO
