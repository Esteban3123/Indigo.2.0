-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_GetInterviewers]
	-- Add the parameters for the stored procedure here
	@Code varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.Id AS EmployeeId, P.FirstName AS FirstName, P.SecondName AS SecondName, P.FirstLastName AS FirstLastName, P.SecondLastName AS SecondLastName FROM [StaffPick].[CallForStaff] CFS
	INNER JOIN [StaffPick].[CallForStaffBossInterview] CFSBI ON CFS.Id = CFSBI.IdCallForStaff
	INNER JOIN [Payroll].[Employee] E ON CFSBI.IdEmployee = E.Id
	INNER JOIN [Common].[ThirdParty] T ON E.ThirdPartyId = T.Id
	INNER JOIN [Common].[Person] P ON T.PersonId = P.Id
	WHERE CFS.Code=@Code

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dado el código de una convocatoria de selección de personal, retorna la lista de entrevistadores (jefes) asignados a esa convocatoria. Consulta la convocatoria en CallForStaff, cruza con las entrevistas de jefes registradas en CallForStaffBossInterview, y obtiene el nombre completo de cada entrevistador desde los registros de empleado, tercero y persona. Se usa en el proceso de reclutamiento para identificar quiénes están designados como entrevistadores en una convocatoria específica.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_GetInterviewers';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'PROCEDURE', @level1name = N'SP_GetInterviewers';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la lista de empleados que participan como entrevistadores (jefes) en una convocatoria de selección de personal identificada por su código.', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una convocatoria de selección con el código proporcionado en CallForStaff; Los empleados entrevistadores deben estar vinculados a un ThirdParty y este a una Person para poder retornar los nombres', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan entrevistadores que tienen cadena completa Empleado → Tercero → Persona (INNER JOIN); si falta algún eslabón el entrevistador se omite; Solo se consideran entrevistadores registrados como ''jefe'' en CallForStaffBossInterview', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Convocatoria de selección de personal; Entrevistador / Jefe entrevistador; Empleado; Tercero; Persona', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] StaffPick.CallForStaff: Cuando CFS.Code coincide con el código recibido, retorna los entrevistadores (Id y nombres) asociados a esa convocatoria mediante CallForStaffBossInterview', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'StaffPick.CallForStaff; StaffPick.CallForStaffBossInterview; Payroll.Employee; Common.ThirdParty; Common.Person', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'StaffPick', @level1type=N'PROCEDURE', @level1name=N'SP_GetInterviewers';
-- GO
