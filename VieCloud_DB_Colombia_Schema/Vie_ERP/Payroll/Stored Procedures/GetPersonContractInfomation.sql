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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la información contractual activa y vigente de un empleado identificado por su número de documento (cédula o identificación). Integra datos de contrato laboral, cargo, tipo de contrato, grupo de nómina, centro de costo, unidad funcional y deducciones del empleado (vivienda, educación, salud), cruzando desde la persona hasta su vínculo contractual vigente. Se usa típicamente para consultar el detalle laboral y de nómina de un trabajador a partir de su número de identificación, por ejemplo en procesos de gestión de personal, liquidación de nómina o auditoría contractual.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInfomation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonContractInfomation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información contractual vigente y datos de retención en la fuente de un empleado a partir de su número de identificación personal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El número de identificación recibido debe corresponder a una persona registrada en Common.Person vinculada vía ThirdParty a un Employee con contrato; El contrato del empleado debe tener Status = 1 y Valid = 1 para ser retornado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone contratos activos y válidos (Status=1 y Valid=1); contratos inactivos o anulados quedan ocultos; La identificación se busca contra Common.Person, no contra el empleado directamente, garantizando la cadena Person→ThirdParty→Employee→Contract; El centro de costo se obtiene indirectamente a través de la unidad funcional del contrato, no de un campo directo del contrato', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contrato laboral; Cargo/Posición; Tipo de contrato; Grupo de nómina; Unidad funcional; Centro de costo; Salario básico; Retención en la fuente (RTF); Deducción por vivienda; Deducción por educación; Tipo de declarante; Aportante de salud; Identificación de persona', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Contract: Retorna datos contractuales (número inicial, fechas, salario básico, cargo, tipo de contrato, grupo, unidad funcional, centro de costo) y datos fiscales del empleado (deducciones de vivienda/educación, tipo declarante, aportante salud, tipo procedimiento RTF) solo cuando Contract.Status=1 AND Contract.Valid=1 AND Person.IdentificationNumber=@IdentificationNumber', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Position; Payroll.ContractType; Payroll.Group; Payroll.Employee; Common.ThirdParty; Common.Person; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonContractInfomation';
-- GO
