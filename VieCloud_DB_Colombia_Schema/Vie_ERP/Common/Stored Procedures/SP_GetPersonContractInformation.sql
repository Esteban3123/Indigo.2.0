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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la información contractual vigente de un empleado a partir de su número de identificación (cédula o documento). Retorna los datos del contrato activo: número de contrato, cargo, fechas de inicio y vinculación, fecha de finalización, salario básico, tipo de salario, centro de costo, unidad funcional y grupo de nómina. También incluye datos laborales y de deducciones del empleado como tipo de procedimiento de retención en la fuente, tipo de vivienda, valores de deducción por vivienda y educación, tipo de declarante y tipo de aportante en salud. Integra las tablas de contrato, cargo, tipo de contrato, centro de costo, unidad funcional, grupo de nómina, empleado, tercero y persona para devolver una vista completa del vínculo laboral activo de la persona buscada.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonContractInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonContractInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la información contractual y de nómina vigente de una persona (contrato, cargo, salario, centro de costo, unidad funcional, grupo y datos para retención en la fuente) a partir de su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona en Common.Person cuyo número de identificación coincida con el parámetro de entrada.; La persona debe estar vinculada a un tercero, y éste a un empleado de nómina con contrato vigente.; El contrato debe tener Valid = 1 y Status = 1 para ser retornado.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan contratos marcados como válidos (Valid = 1) y con estado activo (Status = 1).; La identificación de la persona se resuelve siguiendo la cadena Persona → Tercero → Empleado → Contrato.; Todo contrato retornado debe tener obligatoriamente unidad funcional, cargo, tipo de contrato, empleado, centro de costo, tercero, persona y grupo asociados (uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato laboral; Cargo/Posición; Tipo de contrato; Salario básico; Tipo de salario; Centro de costo; Unidad funcional; Grupo de nómina; Empleado; Tipo de vivienda; Deducción por vivienda; Deducción por educación; Tipo de declarante (renta); Aportante a salud (RTF); Procedimiento RTF (retención en la fuente)', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Contract: Cuando Person.IdentificationNumber coincide con el parámetro y Contract.Valid = 1 y Contract.Status = 1, se retorna el conjunto de datos contractuales y laborales del empleado junto con parámetros de retención en la fuente y deducciones (vivienda, educación).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.FunctionalUnit; Payroll.Position; Payroll.ContractType; Payroll.Employee; Payroll.CostCenter; Common.ThirdParty; Common.Person; Payroll.Group', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonContractInformation';
-- GO
