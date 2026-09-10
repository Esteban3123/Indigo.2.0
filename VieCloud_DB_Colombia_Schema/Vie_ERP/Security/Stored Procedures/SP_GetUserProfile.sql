-- Stored Procedure

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetUserProfile]
	-- Add the parameters for the stored procedure here
	@UserCode varchar(20),
	@Container varchar(50),
	@Security varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @sql nvarchar(max)
    -- Insert statements for procedure here
	set @sql = 'SELECT ' +@Container+ '.[Common].[ThirdParty].[Name], ' +@Container+ '.[Payroll].[Employee].[Id] as EmployeeId,'
	+@Security+'.[SelfService].[RolePortalUser].[RoleId], ' +@Container+ '.[Common].[Person].[IdentificationNumber]
	FROM ' +@Security+'.[SelfService].[PortalUser]
	INNER JOIN '+@Security+'.[SelfService].[PortalUserCompany] 
	ON '+@Security+'.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN ' +@Container+ '.[Payroll].[Employee]  
	ON '+@Container+'.[Payroll].[Employee].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[EmployeeId] 	
	INNER JOIN ' +@Container+ '.[Common].[ThirdParty]
	ON ' +@Container+'.[Common].[ThirdParty].[ID] = ' +@Container+'.[Payroll].[Employee].[ThirdPartyId]
	INNER JOIN ' +@Security+ '.[SelfService].[RolePortalUser]
	ON ' +@Security+ '.[SelfService].[PortalUser].[Id] = '+@Security+'.[SelfService].[RolePortalUser].[PortalUserId] 
	INNER JOIN ' +@Container+ '.[Common].[Person]
	ON ' +@Container+ '.[Common].[Person].[Id] = ' +@Container+ '.[Common].[ThirdParty].[PersonId]
	WHERE '+@Security+'.[SelfService].[PortalUser].[UserCode] = '''+@UserCode+''';';
	print @sql
	exec(@sql)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el perfil completo de un usuario del portal de autoservicio a partir de su código de usuario. Combina información de seguridad (roles y acceso del usuario) con datos del empleado (identificador de empleado) y datos personales (nombre completo y número de identificación/cédula). Utiliza SQL dinámico construyendo la consulta en tiempo de ejecución con los parámetros de base de datos contenedor (@Container) y base de datos de seguridad (@Security), lo que permite operar sobre múltiples instancias o esquemas en runtime. Se usa típicamente al iniciar sesión o cargar el perfil del usuario en el portal, para conocer su nombre, su documento de identidad, su rol asignado y su vínculo como empleado.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetUserProfile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetUserProfile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el perfil de un usuario del portal de autoservicio (nombre, identificación, empleado y rol) cruzando información entre las bases de seguridad y de negocio a partir de su código de usuario.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los nombres de bases de datos/contenedores recibidos como parámetros deben existir y contener los esquemas y tablas referenciados (SelfService, Payroll, Common).; El código de usuario suministrado debe corresponder a un PortalUser existente para obtener resultados.; Se construye y ejecuta SQL dinámico concatenando parámetros, por lo que estos deben ser confiables (riesgo de inyección SQL si no se controlan).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El usuario del portal debe tener al menos una compañía asociada (PortalUserCompany) con un empleado vinculado para retornar resultados.; El empleado debe estar vinculado a un Tercero, y el Tercero a una Persona, para producir filas (joins INNER).; El usuario del portal debe tener al menos un rol asignado (RolePortalUser) para producir filas.; La consulta opera de forma cross-database, combinando datos del contenedor de negocio y del contenedor de seguridad parametrizados en tiempo de ejecución.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal (autoservicio); Empleado; Tercero; Persona; Rol de usuario de portal; Compañía asociada al usuario', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SelfService.PortalUser: Cuando PortalUser.UserCode coincide con el código recibido, retorna el nombre del tercero, el Id del empleado, el RoleId del usuario de portal y el número de identificación de la persona asociada.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.PortalUser; SelfService.PortalUserCompany; Payroll.Employee; Common.ThirdParty; SelfService.RolePortalUser; Common.Person', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetUserProfile';
-- GO
