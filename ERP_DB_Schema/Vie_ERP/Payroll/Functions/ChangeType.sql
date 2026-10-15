-- =============================================
-- Author:      Juan Pablo Daza Medina 
-- Create Date: 2024-12-24
-- Description: OBTIENE LA ABREVIATURA POR EL TIPO DE CAMBIO
-- =============================================
CREATE FUNCTION [Payroll].[ChangeType]
(
    @employeeId Int,
	@payrollDateLiquidated Date
	


)
RETURNS VARCHAR
AS
BEGIN
	Declare @ChangeType VARCHAR
	--Razones de modificacion de contrato
	Declare @ModificationC int = IsNull((Select C.ContractModificationReasonId From Payroll.VerifyAutoliquidationFile VAF
				join Payroll.Contract C on C.EmployeeId = VAF.EmployeeId
				WHERE VAF.EmployeeId = @employeeId AND  C.RetirementDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2))and C.RetirementDate <= @payrollDateLiquidated
				AND C.ContractModificationReasonId IS NOT NULL),0)

	--Novedades
	Declare @Novelty int = IsNull((SELECT N.TypeNovelty FROM Payroll.VerifyAutoliquidationFile VA 
							JOIN Payroll.Novelty N ON VA.EmployeeId = N.EmployeeId 
							WHERE @employeeId =Va.EmployeeId AND  N.RealDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2)) AND N.RealDate <= @payrollDateLiquidated
							AND n.TypeNovelty = 3 and (n.LicenseClass = 1 OR n.LicenseClass = 2)),0)

	--Retiro 
	Declare @Exclusion BIT = IsNull((SELECT 1 FROM Payroll.VerifyAutoliquidationFile VA 
									  JOIN Payroll.ContractLiquidation CL ON CL.EmployeeId = VA.EmployeeId
									  WHERE @employeeId =Va.EmployeeId AND CL.RetirementDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2)) 
									  AND CL.RetirementDate <= @payrollDateLiquidated), 0)
	

	If @ModificationC <> 0
	BEGIN
		SET @ChangeType = CASE 
								WHEN @ModificationC = 2  THEN 'OC'
								WHEN @ModificationC = 3 THEN 'SA'
								ELSE ''
						  END
		
		RETURN @ChangeType
	END
	IF @Novelty  <> 0 
	BEGIN
		SET @ChangeType = CASE 
							WHEN @Novelty = 1 THEN 'IN'
							WHEN @Novelty = 3 THEN 'PE'
							ELSE ''
							END
		RETURN @ChangeType
	END

	IF @Exclusion  <> 0 
	BEGIN
		SET @ChangeType = 'EX'
		RETURN @ChangeType
	END


	--select top 100* from Payroll.VerifyAutoliquidationFile
	--select * from Payroll.Novelty
	--select * from Payroll.ContractLiquidation

	--select top 1000 * from Payroll.Liquidation
	RETURN @ChangeType
END