-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetRecoveryAccountEmail]
	-- Add the parameters for the stored procedure here
	 @UserCode varchar(20),
	 @Container varchar(50),
	 @Security varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @sql nvarchar(max)

    -- Insert statements for procedure here
	set @sql = 'SELECT '+@Container+ '.[Common].[Email].[Email], '+@Container+'.[Common].[ThirdParty].[Name],'+@Security+'.[SelfService].[PortalUser].[UserCode], 
	'+@Security+'.[SelfService].[PortalUser].[Password]
	FROM '+@Security+'.[SelfService].[PortalUser]
	INNER JOIN '+@Security+'.[SelfService].[PortalUserCompany] 
	ON '+@Security+'.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN '+@Container+'.[Payroll].[Employee]  
	ON '+@Container+'.[Payroll].[Employee].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN '+@Container+'.[Common].[ThirdParty]
	ON '+@Container+'.[Common].[ThirdParty].[ID] = '+@Container+'.[Payroll].[Employee].[ThirdPartyId]
	INNER JOIN '+@Container+'.[Common].[Person]
	ON '+@Container+'.[Common].[Person].[ID] = '+@Container+'.[Common].[ThirdParty].[PersonId]
	INNER JOIN '+@Container+'.[Common].[Email]
	ON '+@Container+'.[Common].[Email].[IdPerson]= '+@Container+'.[Common].[Person].[Id] 
	WHERE '+@Security+'.[SelfService].[PortalUser].[UserCode] = '''+@UserCode+''';';

	exec(@sql)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el correo electrónico y los datos de acceso de un usuario del portal de autoservicio a partir de su código de usuario, con el fin de permitir el proceso de recuperación de contraseña o cuenta. Combina dinámicamente información de múltiples bases de datos (contenedor de datos y base de seguridad) cruzando el usuario del portal con el empleado, la persona, el tercero y su dirección de correo electrónico registrada. Se utiliza cuando un usuario olvida sus credenciales y el sistema necesita enviarle un enlace o información de recuperación al email asociado a su cuenta. Construye SQL dinámico en tiempo de ejecución usando los parámetros @Container (base de datos de nómina/común) y @Security (base de datos de seguridad/portal), lo que implica que las tablas reales tocadas se resuelven en runtime.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetRecoveryAccountEmail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetRecoveryAccountEmail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera el correo electrónico, nombre del tercero y credenciales del usuario del portal de autoservicio para flujos de recuperación de cuenta, cruzando datos entre bases de seguridad y de nómina.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las bases/contenedores indicados deben existir y ser accesibles por el contexto de ejecución.; El usuario del portal debe estar vinculado a una compañía (PortalUserCompany), a un empleado de nómina, a un tercero, una persona y un correo registrado para retornar fila.; El UserCode debe corresponder exactamente a un PortalUser existente.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de usuarios del portal cuyo empleado tenga tercero, persona y correo asociados (uso de INNER JOIN en toda la cadena).; El filtro siempre se aplica por UserCode exacto del PortalUser.; Las bases de datos de Security y del contenedor (Common/Payroll) son parametrizables en tiempo de ejecución mediante SQL dinámico.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Recuperación de cuenta; Empleado de nómina; Tercero; Persona; Correo electrónico; Credenciales (UserCode/Password)', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Cuando PortalUser.UserCode coincide con el código suministrado y existen relaciones completas con PortalUserCompany→Employee→ThirdParty→Person→Email, se devuelve Email, Name del tercero, UserCode y Password.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.SelfService.PortalUser; Security.SelfService.PortalUserCompany; Common.Payroll.Employee; Common.Common.ThirdParty; Common.Common.Person; Common.Common.Email', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetRecoveryAccountEmail';
-- GO
