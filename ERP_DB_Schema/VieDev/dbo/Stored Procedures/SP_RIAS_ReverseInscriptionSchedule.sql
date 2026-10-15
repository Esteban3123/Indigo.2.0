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