-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_DeleteRole]
	-- Add the parameters for the stored procedure here
	@RoleId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	IF (NOT EXISTS (SELECT * FROM [SelfService].[RolFormAction] where RoleId = @RoleId) AND NOT EXISTS (SELECT * FROM [SelfService].[RolePortalUser] where RoleId = @RoleId))
	BEGIN
		DELETE FROM [SelfService].[Role] WHERE Id = @RoleId;
		--return 1;
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina un rol de acceso del portal de autoservicio, identificado por su ID. Antes de borrar, verifica que el rol no esté en uso: comprueba que no tenga acciones de formulario asignadas (RolFormAction) ni usuarios del portal asociados (RolePortalUser). Solo si el rol está completamente desvinculado procede con la eliminación en la tabla de roles (Role). Sirve para dar de baja perfiles de permisos sin romper integridad referencial en la seguridad del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRole';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteRole';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina un rol del módulo de autoservicio solo si no tiene dependencias activas en permisos de formularios ni en asignaciones a usuarios del portal.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rol no debe tener registros asociados en SelfService.RolFormAction; El rol no debe estar asignado a ningún usuario en SelfService.RolePortalUser', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se elimina un rol que tenga permisos sobre acciones de formularios asignados; Nunca se elimina un rol que esté asignado a algún usuario del portal; La eliminación de roles es física (DELETE), no lógica', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Rol; Permisos de formulario; Usuario del portal de autoservicio', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] SelfService.Role: Cuando no existen filas en RolFormAction ni en RolePortalUser para el RoleId, se elimina el rol de SelfService.Role.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS en RolFormAction AND NOT EXISTS en RolePortalUser para el RoleId → Se ejecuta DELETE sobre SelfService.Role else No se realiza ninguna acción (el rol se conserva)', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.RolFormAction; SelfService.RolePortalUser', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteRole';
-- GO
