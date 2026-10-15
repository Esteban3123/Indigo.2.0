-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 21/10/2015
-- Description:	Elimina las Liquidaciones de Nomina NO Confirmadas
-- =============================================
CREATE PROCEDURE [Payroll].[SP_DeleteLiquidationNoConfirm]
	@PayrollDate date,
	@GroupId int,
	@EmployeeNit VARCHAR(50)
WITH RECOMPILE
AS 
BEGIN
	SET NOCOUNT ON

	DECLARE @PayrollDateTmp date = @PayrollDate
	DECLARE @GroupIdTmp int = @GroupId
	DECLARE @EmployeeNitTmp VARCHAR(50) =  @EmployeeNit

	BEGIN TRY

		IF @EmployeeNitTmp = '' BEGIN
			
			DELETE FROM Payroll.[Message] WHERE LiquitadionId in (SELECT Id FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp)

			DELETE FROM Payroll.LiquidationDetail WHERE PayrollId in (SELECT Id FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp)

			DELETE FROM Payroll.Liquidation WHERE RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp
		END
		ELSE BEGIN
			
			DECLARE @IdEmployee int
			DECLARE @Idliquidation int

			SELECT @IdEmployee = E.Id 
			FROM Payroll.Employee E WITH(NoLock)
			INNER JOIN Common.ThirdParty TP WITH(NoLock) ON TP.Id = E.thirdPartyId
			WHERE TP.Nit = @EmployeeNitTmp

			SELECT @Idliquidation = Id FROM Payroll.Liquidation where RegisterStatus = '' and GroupId = @GroupIdTmp and PayrollDateLiquidated = @PayrollDateTmp and EmployeeId = @IdEmployee

			DELETE FROM Payroll.[Message] where LiquitadionId = @Idliquidation

			DELETE FROM Payroll.LiquidationDetail where PayrollId = @Idliquidation

			DELETE FROM Payroll.Liquidation where Id = @Idliquidation

		END
	END TRY
	BEGIN CATCH
		RETURN 3
	END CATCH
END