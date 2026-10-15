-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 2015-11-26
-- Description:	Procedimiento ejecutado para cerrar un ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_CloseAdmission_Output]
	@AdmissionNumber VARCHAR(20),
	@ContainerNameCrystal VARCHAR(50),
	@CodeUser AS VARCHAR(20),
	@StatusResult BIT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		
		/***********************************************  VALIDACIONES ***********************************************/

		EXEC [Billing].[SP_CloseAdmissionValidations_Output] @AdmissionNumber, @StatusResult OUT, @MessageResult OUT

		IF @StatusResult = 0
		BEGIN
			RETURN
		END

		/*********************************************** ACTUALIZACION ***********************************************/

		UPDATE ing 
			SET IESTADOIN = IIF(rcd.Id IS NULL, 'C', 'F'),
				CODUSUMOD = @CodeUser,
				FECREGMOD = Common.GETDATE(),
				IJUSTIFIC = 'El ingreso fue cerrado por el usuario ' + @CodeUser                                                                                                                                                                                                                   
		FROM [dbo].[ADINGRESO] ing
		LEFT JOIN Billing.RevenueControl rc ON ing.NUMINGRES = rc.AdmissionNumber
		LEFT JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
		WHERE ing.NUMINGRES = @AdmissionNumber

		SELECT	@StatusResult = 1, 
				@MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT	@StatusResult = 0, 
				@MessageResult = 'SP_CloseAdmission_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END