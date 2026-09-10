

-- ====================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 14/01/2019
-- Description:	Store procedure que se encarga de reversar el proceso de RIAS, se elimina el registro futuro y se nulean los campos que se asignan cuando se inscribe
-- ====================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASAnnulment]
	@Xml as xml,
	@PatientCode as varchar(25),
	@UserCode as varchar(20),

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, Processed bit)
	
	begin try
		
		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			0 as Processed
		from @Xml.nodes('/RIASForPatient') t(x)
				
		--Se obtiene la cantidad de registros que se van a iterar
		declare @Count int = (select count(*) from @TableXml x where x.Processed = 0)

		--Se recorren los cups que se eliminaron de la rejilla y que manejen rias
		while @Count > 0
		begin
			--Variables para obtener la información del xml
			declare @CupsCode varchar(20), @RIASCupsId int
			
			--Se otienen los campos necesarios de la tabla temporal xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId
			From @TableXml ta
			Where ta.Processed = 0
			
			--Se elimina el registro de la actividad futura
			delete from dbo.RIASCUPSPACIENTE where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode and ESTADO = 1

			--Se obtiene el id del registro de la inscripción
			declare @InscriptionId int = (select top 1 ID 
			from dbo.RIASCUPSPACIENTE 
			where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode and ESTADO = 2
			order by ID desc)

			--Se actualizan los campos que se realizó en la inscripción
			update dbo.RIASCUPSPACIENTE set ESTADO = 1, NUMINGRES = null, FECHAREALIZACION = null, IDDETALLEORDENSERVICIO = null, VALORCUPS = null, CODPROSAL = null where ID = @InscriptionId

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableXml set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
			
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de anulación correctamente'
		return		
	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que anula o reversa la inscripción de actividades RIAS (Rutas Integrales de Atención en Salud) para un paciente. Recibe un XML con los códigos CUPS y los identificadores de RIAS a anular, elimina los registros de actividades futuras pendientes y revierte los datos de inscripción (número de ingreso, fecha de realización, detalle de orden de servicio, valor y profesional de salud) dejando el registro en estado pendiente nuevamente. Se utiliza cuando se deshace una inscripción o atención RIAS ya registrada para un paciente, garantizando la consistencia del historial de rutas de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASAnnulment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIASAnnulment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa la inscripción de un paciente a actividades de RIAS, eliminando el registro de actividad futura y restableciendo el registro de inscripción a su estado original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener nodos /RIASForPatient con CupsCode y RIASCupsId; Debe existir un paciente identificado por el código recibido; Deben existir registros en RIASCUPSPACIENTE con estado 1 (futuro) y/o estado 2 (inscrito) para el paciente, RIASCupsId y CupsCode dados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se elimina la actividad futura cuando su estado es 1 (pendiente/futura); Solo se reversa la inscripción cuando su estado es 2 (inscrita/realizada); Al revertir la inscripción se restablece el estado a 1 y se limpian los campos operativos (ingreso, fecha de realización, detalle de orden, valor y profesional); El reverso aplica únicamente al paciente, RIAS-CUPS y código de servicio recibidos por XML; Si existen múltiples inscripciones en estado 2, se reversa la de mayor ID (la más reciente); Cada combinación CupsCode/RIASCupsId del XML se procesa una sola vez (Processed=1); Los errores no propagan excepción al cliente; se devuelven vía parámetros de salida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); Paciente; CUPS (procedimientos en salud); Inscripción a ruta de atención; Anulación/reverso de inscripción; Actividad futura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] dbo.RIASCUPSPACIENTE: Para cada CUPS del XML: elimina el registro de actividad futura del paciente (IPCODPACI=@PatientCode, IDRIASCUPS, CODSERIPS) cuyo ESTADO = 1; [UPDATE] dbo.RIASCUPSPACIENTE: Sobre el registro de inscripción más reciente (mayor ID) con ESTADO=2 del paciente/RIASCups/CupsCode: cambia ESTADO a 1 y deja en NULL NUMINGRES, FECHAREALIZACION, IDDETALLEORDENSERVICIO, VALORCUPS y CODPROSAL, revirtiendo la inscripción; [RETURN_RESULT] OUTPUT: Si todo fue correcto devuelve CodeMessage=''0'' con mensaje de éxito; si ocurre una excepción devuelve CodeMessage=''999'' con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen registros en la tabla temporal con Processed = 0 (Count > 0) → Itera procesando cada CUPS: elimina actividad futura y revierte la inscripción else Termina el proceso y retorna mensaje de éxito; si Ocurre una excepción durante el proceso → Asigna CodeMessage=''999'' y devuelve el mensaje de error de SQL Server else Asigna CodeMessage=''0'' y mensaje de anulación correcta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIASAnnulment';
-- GO
