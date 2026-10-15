-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-03-08
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una convenio de nomina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveAgreement]
    @AgreementXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Payroll].[SP_SaveAgreement_Output]
		@AgreementXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 		
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 
			@Id as Id, 
			@Code as Code
END