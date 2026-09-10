-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_GetSubordinateVacationRequestDetail]
	-- Add the parameters for the stored procedure here
	@VacationRequestDetailId INT
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [HumanTalent].[VacationRequestDetail].[Id]
	,[HumanTalent].[VacationRequestDetail].[Comments]
	,[HumanTalent].[VacationRequest].[Id] AS VacationRequestId
	,[HumanTalent].[VacationRequest].[InitialDateVacation]
	,[HumanTalent].[VacationRequest].[EndDateVacation]
	,[HumanTalent].[VacationRequest].[DaysRequest]
	,[HumanTalent].[VacationRequest].[CreationDate]
	,[Common].[ThirdParty].[Name]
	,[Payroll].[Position].[Name] AS PositionName
	,[Common].[Person].[IdentificationNumber]
	FROM [HumanTalent].[VacationRequestDetail]
	INNER JOIN [HumanTalent].[VacationRequest]
	ON [HumanTalent].[VacationRequestDetail].[IdVacationRequest] = [HumanTalent].[VacationRequest].[Id]
	INNER JOIN [Payroll].[Employee]
	ON [HumanTalent].[VacationRequest].[EmployeeId] = [Payroll].[Employee].[Id]
	INNER JOIN [Common].[ThirdParty]
	ON [Payroll].[Employee].[ThirdPartyId] = [Common].[ThirdParty].[Id]
	INNER JOIN [Payroll].[Contract]
	ON [Payroll].[Contract].[EmployeeId] = [Payroll].[Employee].[Id]
	INNER JOIN [Payroll].[Position]
	ON [Payroll].[Position].[Id] = [Payroll].[Contract].[PositionId]
	INNER JOIN Common.Person 
	ON [Common].[ThirdParty].[PersonId] = [Common].[Person].[Id]
	WHERE ([HumanTalent].[VacationRequestDetail].[Id] = @VacationRequestDetailId AND [Payroll].[Contract].[Status] = 1 AND [Payroll].[Contract].[Valid] = 1);
    
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle completo de un registro de seguimiento (historial de estado) de una solicitud de vacaciones de un subordinado, identificado por el ID del detalle de solicitud. Combina la información del cambio de estado (comentarios) con los datos de la solicitud de vacaciones (fechas de inicio y fin, días solicitados, fecha de creación), el nombre del empleado, su cargo actual y su número de identificación (cédula), consultando únicamente contratos laborales activos y vigentes. Se utiliza para que un jefe o responsable de talento humano consulte el estado y contexto completo de un paso específico dentro del proceso de aprobación o rechazo de vacaciones de un empleado subordinado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de una solicitud de vacaciones de un subordinado, incluyendo datos del empleado, su identificación y el cargo asociado a su contrato vigente.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El detalle de solicitud de vacaciones debe existir y estar vinculado a una solicitud (VacationRequest) con empleado, tercero, persona, contrato y cargo relacionados.; El empleado debe tener un contrato con Status = 1 y Valid = 1 para que se retorne información.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve información cuando el contrato del empleado está en estado activo (Status=1) y vigente (Valid=1); si no, el resultado es vacío.; Si el empleado tiene múltiples contratos activos y válidos, podría retornarse más de una fila (no se restringe a un único contrato).', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de vacaciones; Detalle de solicitud de vacaciones; Empleado; Contrato laboral; Cargo (Position); Tercero; Persona / Identificación', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HumanTalent.VacationRequestDetail: Retorna el detalle de la solicitud de vacaciones unido a la solicitud, empleado, tercero, persona, contrato y cargo, filtrando por el Id del detalle y exigiendo contrato activo y válido.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HumanTalent.VacationRequestDetail; HumanTalent.VacationRequest; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position; Common.Person', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetSubordinateVacationRequestDetail';
-- GO
