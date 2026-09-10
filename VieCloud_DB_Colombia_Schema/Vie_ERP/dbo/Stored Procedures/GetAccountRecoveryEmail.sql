-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountRecoveryEmail]
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el correo electrónico de recuperación de cuenta para un usuario del portal de autoservicio, dado su código de usuario. Recorre la cadena: usuario del portal → empresa asociada → empleado de nómina → tercero → persona → email registrado, devolviendo también el nombre del empleado y las credenciales (usuario y contraseña) del portal. Se utiliza en el flujo de recuperación o restablecimiento de contraseña del portal de empleados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetAccountRecoveryEmail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'GetAccountRecoveryEmail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos de contacto y credenciales de un usuario del portal de autoservicio a partir de su código de usuario, para flujos de recuperación de cuenta vía correo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en PortalUser con el UserCode suministrado; El usuario debe estar vinculado a una empresa vía PortalUserCompany; El registro debe tener un Employee asociado con un ThirdParty y Person válidos; La persona debe tener un correo registrado en Common.Email', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información de usuarios cuyo empleado tenga ThirdParty, Person y al menos un Email registrado (INNER JOINs); Si el usuario no tiene correo asociado a su persona, no se devuelve ningún registro; Un usuario sin vínculo a empresa o sin empleado asociado no es recuperable por este flujo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Recuperación de cuenta; Credenciales (UserCode/Password); Empleado de nómina; Tercero; Persona; Correo electrónico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SelfService.PortalUser: Cuando UserCode coincide y existe la cadena completa de joins (PortalUserCompany→Employee→ThirdParty→Person→Email), retorna Email, Name del tercero, UserCode y Password', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.PortalUser; SelfService.PortalUserCompany; Payroll.Employee; Common.ThirdParty; Common.Person; Common.Email', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'GetAccountRecoveryEmail';
-- GO
