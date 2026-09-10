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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera la carta o referencia laboral de un empleado identificado por su ID, consolidando sus datos personales (nombre completo, género, número de identificación, ciudad de expedición del documento), el tipo de contrato laboral, el salario básico y la fecha de vinculación. Cruza las liquidaciones de nómina con el contrato, el tipo de contrato, el empleado, el tercero y la persona para obtener un perfil laboral completo. Retorna únicamente registros con estado de liquidación confirmado (''C'') y ordena los resultados por la fecha de vinculación más reciente, lo que permite presentar siempre el vínculo contractual vigente o más actual del trabajador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'JobReference';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'JobReference';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el historial laboral de un empleado (datos personales, ciudad, tipo de contrato, salario y fecha de vinculación) a partir de sus liquidaciones confirmadas, ordenado de la vinculación más reciente a la más antigua.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe existir y tener liquidaciones registradas con RegisterStatus = ''C'' (confirmado/cerrado); Las liquidaciones deben estar asociadas a un contrato vigente o histórico con tipo de contrato definido; El empleado debe estar vinculado a un ThirdParty con Person y ciudad de identificación válida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran liquidaciones con RegisterStatus = ''C''; El resultado se ordena por fecha de vinculación laboral (JobBondingDate) descendente; Solo retorna registros donde existan todos los joins (empleado con tercero, persona y ciudad de identificación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Liquidación de nómina; Contrato laboral; Tipo de contrato; Salario básico; Fecha de vinculación laboral; Persona/Tercero; Ciudad de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con datos personales, ciudad, tipo de contrato, salario básico y fecha de vinculación cuando Liquidation.EmployeeId coincide con el parámetro y Liquidation.RegisterStatus = ''C''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Contract; Payroll.ContractType; Payroll.Employee; Common.ThirdParty; Common.Person; Common.City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'JobReference';
-- GO
