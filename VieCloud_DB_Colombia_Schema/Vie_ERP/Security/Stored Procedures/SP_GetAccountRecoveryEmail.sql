-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetAccountRecoveryEmail]
	-- Add the parameters for the stored procedure here
	@UserCode varchar(20)  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Common].[Email].[Email], [Common].[ThirdParty].[Name],[SelfService].[PortalUser].[UserCode], 
	[SelfService].[PortalUser].[Password]
	FROM [SelfService].[PortalUser]
	INNER JOIN [SelfService].[PortalUserCompany] 
	ON [SelfService].[PortalUser].[Id] = [SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN [Payroll].[Employee]  
	ON [Payroll].[Employee].[Id] = [SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN [Common].[ThirdParty]
	ON [Common].[ThirdParty].[ID] = [Payroll].[Employee].[ThirdPartyId]
	INNER JOIN [Common].[Person]
	ON [Common].[Person].[ID] = [Common].[ThirdParty].[PersonId]
	INNER JOIN [Common].[Email]
	ON [Common].[Email].[IdPerson]= [Common].[Person].[Id] 
	WHERE [SelfService].[PortalUser].[UserCode] = @UserCode;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el correo electrónico de recuperación de cuenta para un usuario del portal de autoservicio, dado su código de usuario. Recorre la cadena: usuario del portal → empresa asignada → empleado de nómina → tercero → persona → correo electrónico registrado, devolviendo además el nombre del empleado, el código de usuario y su contraseña. Se utiliza en el flujo de recuperación o restablecimiento de contraseña del portal de empleados, para identificar a qué dirección de correo enviar el enlace de recuperación.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetAccountRecoveryEmail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetAccountRecoveryEmail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera el correo electrónico, nombre del tercero y credenciales asociadas a un usuario del portal de autoservicio para flujos de recuperación de cuenta.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en el portal de autoservicio con el código suministrado; El usuario debe estar vinculado a una empresa vía PortalUserCompany; El empleado asociado debe tener un Tercero y una Persona con al menos un correo registrado', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de usuarios cuyo código coincide exactamente con el parámetro recibido; Requiere la cadena completa de relaciones PortalUser→PortalUserCompany→Employee→ThirdParty→Person→Email; si falta cualquier eslabón no devuelve filas; Puede devolver múltiples filas si la persona tiene varios correos o el usuario está vinculado a varias empresas', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Recuperación de cuenta; Credenciales (UserCode/Password); Correo electrónico de persona; Empleado; Tercero', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result set: Cuando UserCode coincide, retorna Email, Name del tercero, UserCode y Password del usuario del portal', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.PortalUser; SelfService.PortalUserCompany; Payroll.Employee; Common.ThirdParty; Common.Person; Common.Email', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetAccountRecoveryEmail';
-- GO
