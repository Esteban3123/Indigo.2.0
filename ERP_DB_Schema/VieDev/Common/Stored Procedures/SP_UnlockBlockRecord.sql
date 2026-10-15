-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 25/05/2014
-- Description:	Sp para desbloquear registro de las diferente tabla de bloqueos
-- =============================================
CREATE PROCEDURE [Common].[SP_UnlockBlockRecord]
	@CodUser as varchar(100)
AS
BEGIN
	BEGIN TRANSACTION 

	BEGIN TRY
		--TABLAS ERP
		DELETE FROM [Common].[BlockRecord] WHERE CodUser = @CodUser
		DELETE FROM [Billing] .[BlockRecordBilling]  where CodUser = @CodUser
		DELETE FROM [Budget].[BlockRecordBudget] where CodUser = @CodUser
		DELETE FROM [Contract].[BlockRecordContract] where CodUser = @CodUser
		DELETE FROM [Cost].[BlockRecordCost] where CodUser = @CodUser
		DELETE FROM [FixedAsset].[BlockRecordFixedAsset] where CodUser = @CodUser
		DELETE FROM [GeneralLedger].[BlockRecordGeneralLedger] where CodUser = @CodUser
		DELETE FROM [InteropCost].[BlockRecordInteropCost] where CodUser = @CodUser
		DELETE FROM [Inventory].[BlockRecordInventory] where CodUser = @CodUser
		DELETE FROM [Maintenance].[BlockRecordMaintenance] where CodUser = @CodUser
		DELETE FROM [MedicalFees].[BlockRecordMedicalFees] where CodUser = @CodUser
		DELETE FROM [MixingStation].[BlockRecordMixingStation] where CodUser = @CodUser
		DELETE FROM [Payments].[BlockRecordPayments] where CodUser = @CodUser
		DELETE FROM [Payroll].[BlockRecordPayroll] where CodUser = @CodUser
		DELETE FROM [Portfolio].[BlockRecordPortfolio] where CodUser = @CodUser
		DELETE FROM [Treasury].[BlockRecordTreasury] where CodUser = @CodUser
		DELETE FROM [Treasury].[CheckBlock]  where CodUser = @CodUser
		DELETE FROM [AccountManagement].[BlockRecordAccountManagement] WHERE CodUser = @CodUser

		--TABLAS EHR
		DELETE FROM dbo.INDOCUMEN WHERE CODUSUARI = @CodUser
		UPDATE dbo.HCORDIMAG SET USUOCUREG = NULL WHERE USUOCUREG = @CodUser
		UPDATE dbo.AMBORDIMA SET USUOCUREG = NULL WHERE USUOCUREG = @CodUser

		SELECT '1' AS Resultado, 'OK' Mensaje 
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION -- devuelvo la transaccion
		SELECT  '0'  as Resultado ,  'Se ha producido un error!' + ERROR_MESSAGE() Mensaje 
	END CATCH
END