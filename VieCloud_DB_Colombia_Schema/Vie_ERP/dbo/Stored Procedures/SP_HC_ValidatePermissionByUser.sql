CREATE proc [dbo].[SP_HC_ValidatePermissionByUser]
@userCode char(20)
as
select indidmenu as code from SEGpermiu where codusuari = @userCode and indidmenu = '255' union
select indidmenu as code from SEGpermiu where codusuari = @userCode and indidmenu = '256'
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si un usuario tiene permiso de acceso a las opciones de menú 255 y 256 del sistema de historia clínica. Consulta la tabla de permisos por usuario (SEGpermiu) usando el código de usuario recibido como parámetro y retorna los identificadores de menú a los que tiene acceso. Se utiliza para controlar la visibilidad o habilitación de funcionalidades específicas en los módulos de historia clínica, según el perfil de seguridad del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ValidatePermissionByUser';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si un usuario tiene asignados permisos específicos de menú (códigos 255 y/o 256) dentro del esquema de seguridad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en la tabla de permisos de seguridad; Los códigos de menú 255 y 256 deben estar definidos en el catálogo de permisos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se evalúan los permisos con códigos de menú ''255'' y ''256'', cualquier otro permiso es ignorado; El resultado contiene a lo sumo dos filas (una por cada código de menú evaluado); El UNION elimina duplicados, garantizando códigos únicos en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'permisos de usuario; seguridad; menú de aplicación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SEGpermiu: Devuelve el código de menú ''255'' si el usuario lo tiene asignado, unido con el código ''256'' si también lo tiene asignado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGpermiu', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ValidatePermissionByUser';
-- GO
