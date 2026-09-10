-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SelfService].[AuthenticateUser]
	-- Add the parameters for the stored procedure here
	@UserCode Varchar(20)
AS
BEGIN
	DECLARE @QueryResult int;
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  SET @QueryResult = (SELECT COUNT(*)
	  FROM [SelfService].[PortalUser]
	  INNER JOIN [SelfService].[PortalUserCompany]
	  ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId]
	  INNER JOIN [Payroll].[Contract]
	  ON  [SelfService].[PortalUserCompany].[EmployeeId] = [Payroll].[Contract].[EmployeeId]
	  WHERE (UserCode = @UserCode AND [Payroll].[Contract].[Valid] = 1 AND [Payroll].[Contract].[Status] = 1))
	  print @QueryResult

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de autenticación del portal de autoservicio para empleados. Verifica si un usuario, identificado por su código de usuario, existe en el portal y además tiene un contrato laboral vigente y activo en nómina. Combina los datos del usuario del portal, su vinculación con la empresa y su contrato laboral para determinar si el acceso debe ser permitido. Retorna un conteo: si es mayor a cero, el usuario está habilitado para ingresar al portal de autoservicio.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'PROCEDURE', @level1name = N'AuthenticateUser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'PROCEDURE', @level1name = N'AuthenticateUser';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Verifica si un código de usuario del portal de autoservicio corresponde a un empleado con contrato vigente y activo, devolviendo el conteo de coincidencias.', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en PortalUser y estar vinculado a una empresa en PortalUserCompany; El empleado asociado debe tener al menos un contrato en Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran autenticables usuarios cuyo contrato esté marcado como válido (Valid=1) y activo (Status=1); La autenticación requiere que exista la cadena completa: PortalUser → PortalUserCompany → Contract; No valida contraseña ni credenciales criptográficas; solo verifica existencia del UserCode con contrato vigente', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Empresa/Compañía del usuario; Empleado; Contrato laboral vigente; Autenticación', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (stdout): Imprime (PRINT) la cantidad de registros encontrados que cumplen UserCode=@UserCode AND Contract.Valid=1 AND Contract.Status=1', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.PortalUser; SelfService.PortalUserCompany; Payroll.Contract', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'SelfService', @level1type=N'PROCEDURE', @level1name=N'AuthenticateUser';
-- GO
