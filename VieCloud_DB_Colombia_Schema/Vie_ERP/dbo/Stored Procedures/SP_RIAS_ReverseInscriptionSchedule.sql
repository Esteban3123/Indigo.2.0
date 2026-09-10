-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 20/12/2018
-- Description:	Sp para reversar los registros de inscripcion de RIAS CUPS X Paciente desde el aegndaiento cuando se cancela una cita
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_ReverseInscriptionSchedule] 
	@IDCita as int,
	@IdRiasCups as int,
	@UserCode as varchar(20)
AS
BEGIN

	SET NOCOUNT ON;

	begin try

		declare @IDRIASCUPSPACIENTE as int
		declare @Accion as Int
		
		--si existe registro por esa cita y para esa RIAS CUPS
		if exists(select * from RIASCUPSPACIENTE where IDCITA = @IDCita  AND IDRIASCUPS = @IdRiasCups )
		begin
			--garantizamos que se tome el ultimo registro para la cita y riascups
			select top 1 @IDRIASCUPSPACIENTE = ID , @Accion = ACCION  from RIASCUPSPACIENTE where IDCITA = @IDCita  AND IDRIASCUPS = @IdRiasCups order by FECHACREACION Desc
						
			if @Accion =1
			begin
				--si la accion sobre el registro fue una insercion prpcedemos a borrar el registro y que la cita se esta cancelando 
				delete from RIASCUPSPACIENTE where ID = @IDRIASCUPSPACIENTE 
			end else
			begin
				--si la accion sobre el registro fue una actualizacion, se procede a dejar NULL el campo IDCITA
				update RIASCUPSPACIENTE set IDCITA = NULL, CODUSUARU = @UserCode, FECHAMODIFICACION = [Common].[GETDATE]()  where ID = @IDRIASCUPSPACIENTE 
			end
		end

		select '0' as CodeMessage, 'Se realizó el proceso de reversión correctamente' as [Message]
		
    end try
	begin catch		
		select '999' as CodeMessage, ERROR_MESSAGE() as [Message]
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa o deshace la inscripción de un servicio RIAS-CUPS asociado a una cita de agendamiento cuando dicha cita es cancelada. Recibe el identificador de la cita, el código del servicio RIAS-CUPS y el usuario que ejecuta la acción. Si el registro de inscripción fue creado originalmente como una inserción nueva, lo elimina por completo; si fue el resultado de una actualización, simplemente deja el campo de cita en nulo para conservar el historial. Se utiliza para mantener la integridad de las inscripciones de rutas integrales de atención en salud (RIAS) cuando el paciente cancela o se anula su cita programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revertir la inscripción de un paciente en una RIAS CUPS generada desde el agendamiento cuando la cita asociada se cancela, eliminando el registro si fue creado por inserción o desligando la cita si fue una actualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en RIASCUPSPACIENTE asociado a la cita y a la RIAS CUPS indicadas para que se ejecute alguna acción; El registro debe tener un valor de ACCION que permita distinguir entre inserción (1) y actualización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se afecta el último registro (más reciente por FECHACREACION) para la combinación cita/RIAS CUPS; Si el registro provino de una inserción se elimina físicamente; si provino de una actualización se preserva el historial desligando la cita; Toda excepción se captura y se devuelve como CodeMessage=''999'' con el mensaje de error, sin propagar; El resultado siempre se entrega como un conjunto con columnas CodeMessage y Message', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIAS (Rutas Integrales de Atención en Salud); CUPS; Paciente; Cita; Inscripción; Cancelación de cita; Reversión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] dbo.RIASCUPSPACIENTE: Cuando existe registro para la cita y RIAS CUPS y el último registro tiene ACCION=1 (inserción), se elimina dicho registro; [UPDATE] dbo.RIASCUPSPACIENTE: Cuando existe registro para la cita y RIAS CUPS y el último registro NO tiene ACCION=1, se actualiza poniendo IDCITA=NULL y registrando CODUSUARU y FECHAMODIFICACION=Common.GETDATE() para conservar el histórico; [RETURN_RESULT] (resultset): Al finalizar sin error retorna CodeMessage=''0'' y mensaje de éxito; ante excepción retorna CodeMessage=''999'' con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en RIASCUPSPACIENTE para la cita y la RIAS CUPS indicadas → Se selecciona el último registro (por FECHACREACION desc) y se evalúa su acción para revertirlo else No se ejecuta operación de reversión sobre los datos; si El último registro tiene ACCION = 1 (inserción original) → Se elimina el registro de RIASCUPSPACIENTE else Se actualiza el registro dejando IDCITA en NULL y registrando usuario y fecha de modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.RIASCUPSPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_ReverseInscriptionSchedule';
-- GO
