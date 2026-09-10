-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetRolePortalUser]
	-- Add the parameters for the stored procedure here
	@UserCode Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [SelfService].[PortalUser].[Id]
	  ,[SelfService].[PortalUser].[UserCode]
      ,[SelfService].[RolePortalUser].[PortalUserId]
      ,[SelfService].[RolePortalUser].[RoleId]
	  ,[SelfService].[Role].[Name]
	  FROM [SelfService].[RolePortalUser]
	  INNER JOIN [SelfService].[PortalUser]
	  ON [SelfService].[PortalUser].[Id] = [SelfService].[RolePortalUser].[PortalUserId]
	  INNER JOIN [SelfService].[Role]
	  ON [SelfService].[Role].[Id] = [SelfService].[RolePortalUser].[RoleId]
	  where PortalUser.UserCode = @UserCode  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los roles asignados a un usuario del portal de autoservicio a partir de su código de usuario. Combina la información del usuario del portal, la relación usuario-rol y el catálogo de roles para devolver qué perfiles de acceso tiene habilitados ese usuario. Se utiliza para validar los permisos o perfiles de seguridad de un usuario al momento de autenticarse o gestionar su acceso en el portal.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetRolePortalUser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetRolePortalUser';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los roles del portal de autoservicio asignados a un usuario identificado por su código de usuario.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en PortalUser identificado por su UserCode.; Debe existir al menos una asignación en RolePortalUser para retornar filas.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna roles asociados a un usuario existente en PortalUser (INNER JOIN).; Solo retorna asignaciones cuyo rol exista en la tabla Role (INNER JOIN).; El filtro se realiza por código de usuario, no por identificador interno.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario del portal de autoservicio; Rol de acceso; Asignación rol-usuario', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SelfService.RolePortalUser: Cuando PortalUser.UserCode coincide con el código recibido, retorna el cruce de PortalUser, RolePortalUser y Role con id de usuario, código, ids de relación y nombre del rol.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.RolePortalUser; SelfService.PortalUser; SelfService.Role', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRolePortalUser';
-- GO
