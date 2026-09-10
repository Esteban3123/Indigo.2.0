-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetJobReference]
	-- Add the parameters for the stored procedure here
	@EmployeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	DISTINCT CommonPerson.Gender as Sexo, 
	CommonPerson.IdentificationNumber as NumeroIdentificacion, 
	CommonCity.Name as NombreCiudad,
	CommonPerson.FirstName as PrimerNombre, 
	CommonPerson.SecondName as SegundoNombre,
	CommonPerson.FirstLastName as PrimerApellido, 
	CommonPerson.SecondLastName as SegundoApellido, 
	PayrollPosition.Name as Cargo, 
	PayrollContractType.Name as TipoContrato , 
	PayrollContract.BasicSalary as SalarioBasico, 
	'' as NombreBonificacion, 
	0 as ValorBonificacion, 
	PayrollContract.JobBondingDate as FechaContrato, 
	'001' as ClassConcept, 
	PayrollCompany.Name as NombreCompania, 
	PayrollBranchOffice.Telephone as Telefono 
	FROM Payroll.Contract PayrollContract 
	INNER join Payroll.Employee PayrollEmploye on PayrollEmploye.Id = PayrollContract.EmployeeId 
	INNER join Common.ThirdParty CommonThirdParty on CommonThirdParty.Id =PayrollEmploye.ThirdPartyId 
	INNER join Common.Person CommonPerson on CommonPerson.Id = CommonThirdParty.PersonId 
	LEFT join Payroll.Position PayrollPosition on PayrollPosition.Id = PayrollContract.PositionId 
	left join Payroll.ContractType PayrollContractType on PayrollContractType.Id = PayrollContract.ContractTypeId 
	left join Payroll.[Group] PayrollGroup on PayrollGroup.Id = PayrollContract.GroupId 
	left join Payroll.Company PayrollCompany on PayrollCompany.Id = PayrollGroup.CompanyId 
	inner join Payroll.FunctionalUnit PayrollFunctionalUnit on PayrollFunctionalUnit.Id = PayrollContract.FunctionalUnitId 
	left join Payroll.BranchOffice PayrollBranchOffice on PayrollBranchOffice.Id = PayrollFunctionalUnit.BranchOfficeId 
	left join Common.City CommonCity on CommonCity.Id =  CommonPerson.IdentificacionCityId
	left join Payroll.Employee EmployeManager on EmployeManager.Id = PayrollBranchOffice.PayrollManagerId 
	left join Common.ThirdParty ThirdPartyManager on ThirdPartyManager.Id = EmployeManager.ThirdPartyId 
	left join Common.Person PersonManager on PersonManager.Id = ThirdPartyManager.PersonId 
	left join Payroll.[Contract] ContractManager on EmployeManager.Id = ContractManager.EmployeeId 
	left join Payroll.Position PositionManager on PositionManager.Id =ContractManager.PositionId 
	where PayrollEmploye.Id= @EmployeeId AND PayrollContract.Valid = 1 and PayrollContract.Status = 1
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la información de referencia laboral de un empleado específico, identificado por su ID interno. Consolida datos personales del empleado (nombre completo, número de identificación, sexo, ciudad), información contractual vigente (tipo de contrato, cargo, salario básico, fecha de vinculación) y datos de la empresa y sede a la que pertenece (nombre de la compañía, teléfono de la sucursal). Se utiliza para generar certificados laborales, referencias de empleo o consultas de nómina, combinando información de las tablas de contratos, empleados, personas, cargos, tipos de contrato, grupos de nómina, unidades funcionales, sucursales y ciudades, filtrando únicamente contratos activos y válidos del empleado consultado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetJobReference';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetJobReference';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene los datos de referencia laboral de un empleado (datos personales, cargo, contrato, salario, compañía y sucursal) a partir de su contrato vigente y activo.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener al menos un contrato con Valid = 1 y Status = 1 para retornar filas.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran contratos válidos y activos (Valid = 1 y Status = 1).; El concepto de clase retornado es siempre la constante ''001''.; El nombre de bonificación se retorna vacío y su valor en 0 (no calcula bonificaciones).; El resultado se entrega como filas DISTINCT para evitar duplicados por joins.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Cargo; Tipo de contrato; Salario básico; Bonificación; Compañía; Sucursal; Unidad funcional; Identificación de persona; Ciudad de identificación; Jefe/Manager de nómina', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve datos personales, cargo, tipo de contrato, salario básico, fecha de contrato, compañía y teléfono de sucursal sólo cuando PayrollContract.Valid = 1 AND PayrollContract.Status = 1 para el EmployeeId indicado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Employee; Common.ThirdParty; Common.Person; Payroll.Position; Payroll.ContractType; Payroll.Group; Payroll.Company; Payroll.FunctionalUnit; Payroll.BranchOffice; Common.City', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetJobReference';
-- GO
