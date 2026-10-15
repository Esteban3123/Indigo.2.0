




-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-05-27
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliación bancaria
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveBankReconciliation]
    @BankReconciliationXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(200),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Treasury].[SP_SaveBankReconciliation_Output] @BankReconciliationXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END