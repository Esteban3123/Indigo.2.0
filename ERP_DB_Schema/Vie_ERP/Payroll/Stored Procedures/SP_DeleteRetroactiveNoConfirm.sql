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