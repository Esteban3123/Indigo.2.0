-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[JobReference]
	-- Add the parameters for the stored procedure here
	@EmployeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select [Common].[Person].[Gender], 
	[Common].[Person].[IdentificationNumber],
	[Common].[City].[Name], 
	[Common].[Person].[FirstName], 
	[Common].[Person].[SecondName],
	[Common].[Person].[FirstLastName], 
	[Common].[Person].[SecondLastName],
	[Payroll].[ContractType].[Name],
	[Payroll].[Contract].[BasicSalary],
	[Payroll].[Contract].[JobBondingDate]
	from [Payroll].[Liquidation] 
	inner join [Payroll].[Contract]
	on [Payroll].[Liquidation].[ContractId] = [Payroll].[Contract].[Id]
	inner join [Payroll].[ContractType]
	on [Payroll].[ContractType].[Id] = [Payroll].[Contract].[ContractTypeId]
	inner join [Payroll].[Employee]
	on [Payroll].[Contract].[EmployeeId] = [Payroll].[Employee].[Id]
	inner join [Common].[ThirdParty]
	on [Common].[ThirdParty].[Id] = [Payroll].[Employee].[ThirdPartyId]
	inner join [Common].[Person] 
	on [Common].[Person].[id] = [Common].[ThirdParty].[PersonId]
	inner join [Common].[City]
	on [Common].[Person].[IdentificacionCityId] = [Common].[City].[Id]
	where [Payroll].[Liquidation].[EmployeeId] = @EmployeeId and [Payroll].[Liquidation].[RegisterStatus] = 'C'
	order by [Payroll].[Contract].[JobBondingDate] desc;

END
