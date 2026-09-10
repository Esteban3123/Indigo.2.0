-- Stored Procedure

-- =============================================
-- Author:		<Sebastian Martinez,,Name>
-- Create date: <03-09-2019,,>
-- Description:	<This Store Procedure return the profle information of an user,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_UserProfile]
	-- Add the parameters for the stored procedure here
	@UserCode varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/*SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>*/
	SELECT [Common].[ThirdParty].[Name], [Payroll].[Employee].[Id] as EmployeeId,
	[SelfService].[RolePortalUser].[RoleId], [Common].[Person].[IdentificationNumber]
	FROM [SelfService].[PortalUser]
	INNER JOIN [SelfService].[PortalUserCompany] 
	ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN [Payroll].[Employee]  
	ON [Payroll].[Employee].[Id] = [SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN [Common].[ThirdParty]
	ON [Common].[ThirdParty].[ID] = [Payroll].[Employee].[ThirdPartyId]
	INNER JOIN [SelfService].[RolePortalUser]
	ON [SelfService].[PortalUser].[Id] = [SelfService].[RolePortalUser].[PortalUserId] 
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[Id] = [Common].[ThirdParty].[PersonId]
	WHERE [SelfService].[PortalUser].[UserCode] = @UserCode;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna el perfil completo de un usuario del portal de autoservicio a partir de su código de usuario. Combina datos del usuario del portal con su empresa asociada, el empleado de nómina vinculado, el tercero correspondiente y la persona natural, para obtener el nombre, número de identificación (cédula o documento), identificador de empleado y rol asignado en el portal. Se utiliza para autenticación, control de acceso y presentación del perfil del usuario logueado en el portal de autoservicio.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_UserProfile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_UserProfile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el perfil de un usuario del portal de autoservicio, retornando su nombre, identificación, empleado asociado y rol vinculado.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en el portal de autoservicio identificado por su código de usuario; El usuario debe tener vinculación con una compañía y un empleado; El empleado debe tener un tercero asociado y este una persona natural; El usuario debe tener al menos un rol asignado en el portal', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan usuarios que tengan simultáneamente compañía, empleado, tercero, persona y rol asignados (uso de INNER JOIN excluye registros incompletos); Si el usuario tiene múltiples compañías o roles, se generan múltiples filas (producto cartesiano de las relaciones); El filtrado se hace estrictamente por UserCode exacto', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Empleado; Tercero; Persona; Rol de usuario; Compañía del usuario; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando UserCode coincide y existen los joins requeridos (PortalUser→PortalUserCompany→Employee→ThirdParty→Person y PortalUser→RolePortalUser), retorna nombre del tercero, EmployeeId, RoleId e IdentificationNumber', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.PortalUser; SelfService.PortalUserCompany; Payroll.Employee; Common.ThirdParty; SelfService.RolePortalUser; Common.Person', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_UserProfile';
-- GO
