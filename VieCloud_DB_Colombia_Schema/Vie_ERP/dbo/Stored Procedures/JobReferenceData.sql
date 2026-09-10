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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la ficha de referencia laboral completa de un empleado a partir de su identificador interno. Integra datos personales (nombre, documento de identidad, género, ciudad de expedición), información contractual (cargo, tipo de contrato, salario básico, fecha de vinculación) y bonificaciones o conceptos manuales de nómina registrados para ese contrato. También recupera la empresa y el teléfono de la sucursal asociada al empleado, combinando los módulos de nómina (Contract, Employee, Position, ContractType, Group, Company, ManualConcepts, Concept) con los maestros comunes de personas y ciudades. Se usa principalmente para generar reportes o fichas de referencia de un empleado en el contexto de nómina y recursos humanos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'JobReferenceData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'JobReferenceData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la hoja de datos laborales y personales del empleado (identificación, cargo, contrato, salario, bonificaciones manuales, compañía y sede) para fines de referencia o certificación laboral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe existir en Payroll.Employee con el Id recibido; Para obtener datos personales y de ciudad, el empleado debe estar enlazado a un ThirdParty y este a una Person; Para mostrar bonificación, deben existir registros en Payroll.ManualConcepts asociados al contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan filas asociadas al empleado solicitado (filtro por PayrollEmploye.Id); El resultado se ordena de forma descendente por fecha de vinculación laboral (JobBondingDate); Requiere obligatoriamente unidad funcional asociada al contrato (INNER JOIN con Payroll.FunctionalUnit); contratos sin unidad funcional son excluidos; El resto de relaciones (persona, ciudad, cargo, tipo de contrato, conceptos manuales, compañía, sede, gerente) son opcionales (LEFT JOIN), permitiendo filas con datos parciales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Contrato laboral; Tipo de contrato; Cargo; Salario básico; Bonificación (concepto manual de nómina); Concepto de nómina; Compañía; Sede/Sucursal; Unidad funcional; Gerente de sede; Fecha de vinculación laboral; Identificación personal; Ciudad de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Contract: Cuando PayrollEmploye.Id = @EmployeeId, retorna los contratos del empleado ordenados por JobBondingDate descendente (contrato más reciente primero)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Employee; Common.ThirdParty; Common.Person; Common.City; Payroll.ManualConcepts; Payroll.Concept; Payroll.Position; Payroll.ContractType; Payroll.Group; Payroll.Company; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReferenceData';
-- GO
