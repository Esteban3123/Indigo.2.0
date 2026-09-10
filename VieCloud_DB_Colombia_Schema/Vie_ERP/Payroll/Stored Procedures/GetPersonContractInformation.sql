-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[GetPersonContractInformation]
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la información contractual vigente y válida de un empleado a partir de su número de identificación (cédula o documento). Retorna datos clave del contrato laboral activo: número de contrato inicial, cargo desempeñado, fecha de inicio y fin del contrato, fecha de vinculación laboral, tipo de salario, salario básico, grupo de nómina, unidad funcional y centro de costo al que pertenece el trabajador. Además, incluye información tributaria y de deducciones del empleado como tipo de declarante, valor de deducción por vivienda, deducción por educación y tipo de aportante a salud. Integra las tablas de contratos, cargos, tipos de contrato, grupos de nómina, empleados, terceros, personas, unidades funcionales y centros de costo para ofrecer una vista consolidada del vínculo laboral vigente de una persona.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInformation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInformation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos contractuales y de deducciones tributarias vigentes de un empleado identificado por su número de documento, consolidando cargo, grupo, unidad funcional y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una persona en Common.Person con el número de identificación recibido.; La persona debe tener vínculo Common.ThirdParty → Payroll.Employee y al menos un contrato cuya Status = 1 y Valid = 1.; El contrato debe tener referencias válidas (INNER JOIN) a Position, ContractType, Group, FunctionalUnit y CostCenter (vía FunctionalUnit.CostCenterId).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen contratos activos y válidos (Status=1 y Valid=1).; El centro de costo se obtiene siempre a través de la unidad funcional del contrato (FunctionalUnit.CostCenterId), no directamente del contrato.; La identificación del empleado se resuelve por la persona natural asociada al ThirdParty del Employee (Person.IdentificationNumber).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato laboral; Cargo/Posición; Tipo de contrato y tipo de salario; Grupo de nómina; Unidad funcional; Centro de costo; Retención en la fuente (RTF); Deducción por vivienda; Deducción por educación; Tipo de declarante; Aportante a salud; Salario básico; Fecha de vinculación laboral (JobBondingDate)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna una fila por cada contrato del empleado con Status=1 y Valid=1, incluyendo número inicial de contrato, cargo, fechas, tipo de salario, grupo, centro de costo, unidad funcional, salario básico y datos de retención en la fuente (RTF, deducciones de vivienda y educación, tipo de declarante, aportante a salud).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Payroll.Contract.Status = 1 AND Payroll.Contract.Valid = 1 → El contrato se incluye en el resultado. else Contratos inactivos o no válidos quedan excluidos del resultset.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Position; Payroll.ContractType; Payroll.Group; Payroll.Employee; Common.ThirdParty; Common.Person; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInformation';
-- GO
