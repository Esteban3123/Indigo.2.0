-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[JobReferenceData]
	-- Add the parameters for the stored procedure here
	@EmployeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CommonPerson.Gender as Sexo , CommonPerson.IdentificationNumber
	as NumeroIdentificacion , CommonCity.Name as NombreCiudad,
	CommonPerson.FirstName as PrimerNombre, CommonPerson.SecondName as SegundoNombre,
	CommonPerson.FirstLastName as PrimerApellido, CommonPerson.SecondLastName as SegundoApellido, 
	PayrollPosition.Name as Cargo, PayrollContractType.Name as TipoContrato , 
	PayrollContract.BasicSalary as SalarioBasico, PayrollConcept.Name as NombreBonificacion, 
	PayrollManualConcepts.QuoteValue as ValorBonificacion, PayrollContract.JobBondingDate as FechaContrato, 
	PayrollConcept.ConceptClass as ClassConcept , PayrollCompany.Name as NombreCompania, 
	PayrollBranchOffice.Telephone as Telefono 
	FROM Payroll.Contract PayrollContract left join Payroll.Employee PayrollEmploye 
	on PayrollEmploye.Id = PayrollContract.EmployeeId left join Common.ThirdParty CommonThirdParty 
	on CommonThirdParty.Id =PayrollEmploye.ThirdPartyId left join Common.Person CommonPerson 
	on CommonPerson.Id = CommonThirdParty.PersonId left join Common.City CommonCity 
	on CommonCity.Id = CommonPerson.IdentificacionCityId left join Payroll.ManualConcepts PayrollManualConcepts 
	on PayrollContract.Id = PayrollManualConcepts.ContractId left join Payroll.Concept PayrollConcept 
	on PayrollConcept.Id = PayrollManualConcepts.ConceptId left join Payroll.Position PayrollPosition 
	on PayrollPosition.Id = PayrollContract.PositionId left join Payroll.ContractType PayrollContractType 
	on PayrollContractType.Id = PayrollContract.ContractTypeId left join Payroll.[Group] PayrollGroup 
	on PayrollGroup.Id = PayrollContract.GroupId left join Payroll.Company PayrollCompany 
	on PayrollCompany.Id = PayrollGroup.CompanyId inner join Payroll.FunctionalUnit PayrollFunctionalUnit 
	on PayrollFunctionalUnit.Id = PayrollContract.FunctionalUnitId left join Payroll.BranchOffice PayrollBranchOffice 
	on PayrollBranchOffice.Id = PayrollFunctionalUnit.BranchOfficeId left join Payroll.Employee EmployeManager 
	on EmployeManager.Id = PayrollBranchOffice.PayrollManagerId left join Common.ThirdParty ThirdPartyManager 
	on ThirdPartyManager.Id = EmployeManager.ThirdPartyId left join Common.Person PersonManager 
	on PersonManager.Id = ThirdPartyManager.PersonId left join Payroll.[Contract] ContractManager 
	on EmployeManager.Id = ContractManager.EmployeeId left join Payroll.Position PositionManager 
	on PositionManager.Id =ContractManager.PositionId 
	where PayrollEmploye.Id= @EmployeeId order by PayrollContract.JobBondingDate desc
END