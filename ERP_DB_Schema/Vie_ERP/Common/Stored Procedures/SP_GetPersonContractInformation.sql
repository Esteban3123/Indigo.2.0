-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:[Payroll].[GetPersonContractInfomation]	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonContractInformation]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	SELECT Contract.Id,
	Contract.InitialContractNumber,
	Position.Name as PositionName,
	Contract.ContractInitialDate,
	Contract.JobBondingDate,
	Contract.ContractEndingDate,
	Contract.BasicSalary,
	ContractType.SalaryType,
	CostCenter.Name as CostCenterName,
	FunctionalUnit.Name as FunctionalUnitName,
	[Group].Name as GroupName,
	Employee.ProcedureTypeRTF,
	Person.HousingType,
	Employee.HousingDeductionValue,
	Employee.EducationDeductionValue,
	Employee.DeclarantType,
	Employee.HealthContributorRTF
	FROM Payroll.Contract	
	INNER JOIN Payroll.FunctionalUnit
	ON Contract.FunctionalUnitId = FunctionalUnit.Id
	INNER JOIN Payroll.Position
	ON Contract.PositionId = Payroll.Position.Id
	INNER JOIN Payroll.ContractType
	ON Contract.ContractTypeId = ContractType.Id
	INNER JOIN Payroll.Employee 
	ON Payroll.Contract.EmployeeId = Payroll.Employee.Id
	INNER JOIN Payroll.CostCenter
	ON Employee.CostCenterId = CostCenter.Id
	INNER JOIN Common.ThirdParty
	ON Employee.ThirdPartyId = ThirdParty.Id
	INNER JOIN Common.Person
	ON ThirdParty.PersonId = Person.Id
	
	inner join Payroll.[Group]
	on Payroll.[Group].Id = Payroll.Contract.GroupId
	WHERE (Person.IdentificationNumber = @IdentificationNumber AND Payroll.Contract.Valid = 1 and Contract.Status = 1)
END