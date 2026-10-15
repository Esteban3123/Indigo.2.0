-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 2015-11-26
-- Description:	Procedimiento ejecutado para cerrar un ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_CloseAdmission]
	@AdmissionNumber varchar(20),
	@ContainerNameCrystal varchar(50),
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult BIT,
			@MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_CloseAdmission_Output] 
		@AdmissionNumber, 
		@ContainerNameCrystal, 
		@CodeUser,
		--------------------------------------------
		@StatusResult OUTPUT, 
		@MessageResult OUTPUT

	SELECT	@StatusResult AS StatusResult,
			@MessageResult AS MessageResult	
END