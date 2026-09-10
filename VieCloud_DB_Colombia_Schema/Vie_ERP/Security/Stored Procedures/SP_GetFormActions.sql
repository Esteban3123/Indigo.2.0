-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetFormActions]
	-- Add the parameters for the stored procedure here
	@FormId INT,
	@RoleId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT [SelfService].[RolFormAction].[Id] as RolFormActionId
	,[SelfService].[FormAction].[FormId]
	,[SelfService].[FormAction].[ActionId]
	,[SelfService].[RolFormAction].[Status]
	,[SelfService].[Action].[Id]
	,[SelfService].[Action].[Name]
	,[SelfService].[Action].[Code]
	,[SelfService].[Action].[CreationUser]
	,[SelfService].[Action].[CreationDate]
	,[SelfService].[Action].[ModificationUser]
	,[SelfService].[Action].[ModificationDate]
	FROM [SelfService].[FormAction]
	INNER JOIN [SelfService].[Action]
	ON [SelfService].[FormAction].[ActionId] = [SelfService].[Action].[Id]
	INNER JOIN [SelfService].[RolFormAction] 
	ON [SelfService].[RolFormAction].[FormActionId] = [SelfService].[FormAction].[Id] 
	WHERE ([SelfService].[FormAction].[FormId] = @FormId AND [SelfService].[RolFormAction].[RoleId] = @RoleId)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las acciones permitidas sobre un formulario específico para un rol de usuario determinado dentro del módulo de autoservicio. Combina las tablas de acciones disponibles por formulario (FormAction y Action) con las asignaciones de permisos por rol (RolFormAction), filtrando por el identificador del formulario y el identificador del rol recibidos como parámetros. Se utiliza para controlar y verificar qué operaciones (como crear, editar, eliminar, consultar) puede ejecutar un rol sobre un formulario dado, implementando la seguridad basada en roles del sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetFormActions';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetFormActions';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las acciones disponibles y su estado de permiso para un formulario y rol específicos dentro del módulo de autoservicio.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en FormAction asociados al formulario indicado; Deben existir asignaciones en RolFormAction para el rol indicado vinculadas al FormAction', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven acciones que tengan a la vez relación en FormAction (formulario-acción) y en RolFormAction (rol-formAction); Filtra simultáneamente por formulario y rol, excluyendo acciones no asignadas al rol; Usa INNER JOIN, por lo que omite acciones sin permiso configurado para el rol', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autoservicio; Formulario; Acción; Rol; Permiso de acción por rol', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando FormAction.FormId coincide con el formulario y RolFormAction.RoleId coincide con el rol, retorna las acciones con su estado de permiso (Status)', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.FormAction; SelfService.Action; SelfService.RolFormAction', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormActions';
-- GO
