-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[GetPersonContractInfomation]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[Contract].[InitialContractNumber],
	[Payroll].[Position].[Name] as PositionName,	
	[Payroll].[Contract].[ContractInitialDate],
	[Payroll].[ContractType].[SalaryType],
	[Payroll].[Group].[Name] as GroupName,	
	[Payroll].[CostCenter].[Name] as CostCenterName,
	[Payroll].[Contract].[JobBondingDate],
	[Payroll].[Contract].[ContractEndingDate],
	[Payroll].[Contract].[BasicSalary],
	[Payroll].[FunctionalUnit].[Name] as FunctionalUnitName,
	[Payroll].[Employee].[ProcedureTypeRTF],
	[Payroll].[Employee].[HousingDeductionValue],
	[Payroll].[Employee].[EducationDeductionValue],
	[Payroll].[Employee].[DeclarantType],
	[Payroll].[Employee].[HealthContributorRTF]
	FROM [Payroll].[Contract]
	INNER JOIN [Payroll].[Position]
	ON [Payroll].[Position].[Id] = [Payroll].[Contract].[PositionId] 
	INNER JOIN [Payroll].[ContractType]
	ON [Payroll].[ContractType].[Id] =[Payroll].[Contract].[ContractTypeId]
	INNER JOIN [Payroll].[Group]
	ON [Payroll].[Contract].[GroupId] = [Payroll].[Group].[Id]
	INNER JOIN [Payroll].[Employee]
	ON [Payroll].[Contract].[EmployeeId] = [Payroll].[Employee].[Id]
	INNER JOIN [Common].[ThirdParty]
	ON [Common].[ThirdParty].[Id] = [Payroll].[Employee].[ThirdPartyId]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[ThirdParty].[PersonId]
	INNER JOIN [Payroll].[FunctionalUnit] 
	ON [Payroll].[FunctionalUnit].[Id] = [Payroll].[Contract].[FunctionalUnitId] 	
	INNER JOIN [Payroll].[CostCenter]
	ON [Payroll].[FunctionalUnit].[CostCenterId] = [Payroll].[CostCenter].[Id]
	WHERE ([Common].[Person].[IdentificationNumber]=@IdentificationNumber
	AND [Payroll].[Contract].[Status] = 1 AND [Payroll].[Contract].[Valid] = 1);
END