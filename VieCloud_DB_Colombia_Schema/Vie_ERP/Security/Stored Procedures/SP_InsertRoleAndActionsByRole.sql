-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<This Sp inserts the forms in RolesForms table and adter insert the correspondent action to that form in RolFormAction,>
-- =============================================
CREATE PROCEDURE [Security].[SP_InsertRoleAndActionsByRole]
	-- Add the parameters for the stored procedure here
	@RoleId INT,
	@FormCode INT,
	@CreationUser VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	-- Insert the RolesForms
	INSERT INTO [SelfService].[RolesForms] (FormId, RoleId, Status,CreationUser, CreationDate) VALUES ((Select [Form].[Id] from [SelfService].[Form] WHERE [SelfService].[Form].[Code] = @FormCode) ,@RoleId,1,@CreationUser, GETDATE())
	
	INSERT INTO SelfService.RolFormAction(RoleId, FormActionId, [Status])
	SELECT @RoleId,  [SelfService].[FormAction].[Id], 0
	FROM [SelfService].[FormAction]
	INNER JOIN [SelfService].[Form]
	ON [SelfService].[Form].[Id] = [SelfService].[FormAction].[FormId]
	WHERE [SelfService].[Form].[Code] = @FormCode;

	-- Insert the formActions in RoleFormAction table with the parameter of role
	
  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asigna un formulario (pantalla o módulo del sistema) a un rol de seguridad y habilita automáticamente todas las acciones asociadas a ese formulario para dicho rol. Primero registra la relación entre el rol y el formulario en la tabla RolesForms, buscando el formulario por su código. Luego inserta en RolFormAction cada acción disponible del formulario vinculándola al rol indicado. Se usa para configurar permisos de acceso: cuando se le otorga a un rol el acceso a un módulo, este procedimiento garantiza que también se registren todas las acciones (botones, operaciones) de ese módulo para el rol.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_InsertRoleAndActionsByRole';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Asocia un formulario de autoservicio a un rol y registra todas las acciones de ese formulario para el rol, dejándolas inicialmente deshabilitadas.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un formulario en SelfService.Form cuyo Code coincida con el parámetro recibido; en caso contrario el FormId insertado será NULL.; Debe existir el rol al que se asocian el formulario y sus acciones.; Deben existir acciones (FormAction) asociadas al formulario para que se generen los registros en RolFormAction.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El formulario asociado al rol siempre se registra con Status=1 (activo).; Las acciones del formulario asignadas al rol siempre se registran con Status=0 (inactivas/no permitidas por defecto).; La fecha de creación del vínculo rol-formulario se establece con la fecha/hora actual del servidor (GETDATE()).; Se asignan TODAS las acciones existentes del formulario al rol, no un subconjunto.; El formulario se identifica por su Code y se traduce internamente a su Id.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Rol; Formulario; Acciones de formulario; Permisos de autoservicio', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] SelfService.RolesForms: Inserta un vínculo rol-formulario con Status=1 (activo) y CreationDate=GETDATE(), resolviendo el FormId a partir del Code del formulario.; [INSERT] SelfService.RolFormAction: Por cada acción (FormAction) ligada al formulario cuyo Code coincide, inserta un registro rol-acción con Status=0 (deshabilitada por defecto).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.Form; SelfService.FormAction', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_InsertRoleAndActionsByRole';
-- GO
