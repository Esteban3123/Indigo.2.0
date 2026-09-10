-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 07/12/2015
-- Description:	Elimino los Registros de Retroactividad (Cabecera - Detalle)
-- =============================================
CREATE PROCEDURE [Payroll].[SP_DeleteRetroactiveNoConfirm]
	@RetroactiveInitialDate date,
	@GroupId int
AS 
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY

		DECLARE @Cantidad int = 0
		DECLARE @Return int = 0

		SELECT @Cantidad = COUNT(*)
		FROM Payroll.RetroactiveC 
		WHERE InitialDateRetroactive = @RetroactiveInitialDate and IdGroup = @GroupId and [Status] = 1

		IF @Cantidad > 0 BEGIN

			-- Elimino los Detalles de Retroactividad
			DELETE FROM Payroll.RetroactiveD 
			WHERE IdRetroactiveC in (SELECT Id from Payroll.RetroactiveC where InitialDateRetroactive = @RetroactiveInitialDate and IdGroup = @GroupId and [Status] = 1)

			-- Elimino la Cabecera de Retroactividad 
			DELETE FROM Payroll.RetroactiveC WHERE  InitialDateRetroactive = @RetroactiveInitialDate and IdGroup = @GroupId and [Status] = 1

			SET @Return = 1
			RETURN @Return
				
		END ELSE BEGIN
			SET @Return = 0
			RETURN @Return
		END

	END TRY
	BEGIN CATCH
		SET @Return = 3
				RETURN @Return
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina los registros de liquidación retroactiva de nómina que aún no han sido confirmados (estado pendiente), tanto el encabezado como el detalle de conceptos, para un grupo de empleados y una fecha de inicio de retroactivo específicos. Opera sobre las tablas RetroactiveC (cabecera) y RetroactiveD (detalle de conceptos), borrando primero el detalle y luego el encabezado para mantener la integridad referencial. Se usa cuando se necesita anular o descartar un proceso de retroactividad salarial que todavía no ha sido aprobado ni aplicado en la nómina. Retorna 1 si eliminó registros, 0 si no encontró registros en estado no confirmado, y 3 si ocurrió un error.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina en cascada manual los registros de retroactividad de nómina (detalle y cabecera) que se encuentren en estado activo (Status = 1) para una fecha inicial y grupo determinados, sin requerir confirmación previa.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proporcionarse fecha inicial de retroactividad y grupo válidos; Debe existir al menos un registro en Payroll.RetroactiveC con esa fecha, grupo y Status = 1 para que ocurra la eliminación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se eliminan retroactividades con Status = 1 (activas/no confirmadas); El detalle (RetroactiveD) siempre se elimina antes que la cabecera (RetroactiveC) para preservar la integridad referencial; Los códigos de retorno son fijos: 1 = eliminado, 0 = no había registros, 3 = error', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retroactividad de nómina; Cabecera de retroactividad; Detalle de retroactividad; Grupo de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Payroll.RetroactiveD: Cuando existen cabeceras con InitialDateRetroactive=@RetroactiveInitialDate, IdGroup=@GroupId y Status=1, elimina de Payroll.RetroactiveD todas las filas cuyo IdRetroactiveC pertenezca a esas cabeceras; [DELETE] Payroll.RetroactiveC: Tras eliminar el detalle, elimina las cabeceras de Payroll.RetroactiveC con InitialDateRetroactive=@RetroactiveInitialDate, IdGroup=@GroupId y Status=1; [RETURN_RESULT] (return code): Retorna 1 si se eliminaron registros, 0 si no existían cabeceras elegibles, 3 si ocurre una excepción capturada', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en Payroll.RetroactiveC con InitialDateRetroactive y IdGroup dados y Status = 1 → Borra primero el detalle (RetroactiveD) asociado y luego la cabecera (RetroactiveC); retorna 1 else No realiza ninguna eliminación y retorna 0; si Se produce una excepción durante la ejecución (BEGIN CATCH) → Retorna 3 sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveC', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRetroactiveNoConfirm';
-- GO
