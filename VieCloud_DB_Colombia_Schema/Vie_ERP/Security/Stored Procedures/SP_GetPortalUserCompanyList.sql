-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetPortalUserCompanyList]
	-- Add the parameters for the stored procedure here
	 @Container varchar(50),
	 @Security varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @sql nvarchar(max)
    -- Insert statements for procedure here
	set @sql = 'SELECT '
	+@Security+ '.[SelfService].[PortalUser].[Id], ' 
	+@Security+ '.[SelfService].[Role].[Id]  as RoleId, '
	+@Security+ '.[SelfService].[PortalUser].[UserCode], ' 
	+@Security+ '.[SelfService].[PortalUser].[Status], ' 
	+@Container+ '.Common.ThirdParty.Name
	FROM ' +@Security+ '.[SelfService].[PortalUser] 
	INNER JOIN ' +@Security+ '.[SelfService].[RolePortalUser] 
	on ' +@Security+ '.[SelfService].[RolePortalUser].[PortalUserId] = ' +@Security+ '.[SelfService].[PortalUser].[Id]  
	INNER JOIN ' +@Security+ '.[SelfService].[Role]
	on ' +@Security+ '.[SelfService].[RolePortalUser].[RoleId] = ' +@Security+ '.[SelfService].[Role].[Id]  
	INNER JOIN ' +@Security+ '.[SelfService].[PortalUserCompany] 
	ON ' +@Security+ '.[SelfService].[PortalUser].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[PortalUserId] 
	INNER JOIN ' + @Container+ '.[Payroll].[Employee]
	on ' +@Container+ '.[Payroll].[Employee].[Id] = ' +@Security+ '.[SelfService].[PortalUserCompany].[EmployeeId] 
	INNER JOIN ' +@Container+ '.[Common].[ThirdParty] 
	on ' +@Container+ '.[Payroll].[Employee].[ThirdPartyId] =' +@Container+ '.[Common].[ThirdParty].[Id]'

	--PRINT @sql

	exec(@sql)
	
END

--exec [Security].[SP_GetPortalUserCompanyList] 'VIE08'
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la lista de usuarios del portal de autoservicio junto con las empresas a las que tienen acceso, cruzando información de seguridad (usuarios, roles y asignación de empresas por usuario) con datos del contenedor de nómina (empleados y terceros). Recibe como parámetros el nombre de la base de datos de seguridad y la base de datos contenedora, construyendo la consulta de forma dinámica para operar sobre múltiples bases de datos en tiempo de ejecución. Se usa para administrar y visualizar qué usuario del portal está asociado a qué empresa/empleado, incluyendo su código de usuario, estado y rol asignado. Toca las entidades de usuario portal, rol, asignación empresa-usuario, empleado y tercero (persona natural o jurídica).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortalUserCompanyList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetPortalUserCompanyList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los usuarios del portal de autoservicio junto con su rol, estado y nombre del tercero asociado, cruzando bases de datos de seguridad y de la compañía (contenedor).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los nombres de base de datos recibidos para ''Security'' y ''Container'' deben existir y ser accesibles desde la sesión.; Las bases referenciadas deben contener los esquemas SelfService, Payroll y Common con las tablas requeridas.; Cada PortalUser debe tener al menos un registro en RolePortalUser, PortalUserCompany y un Employee con ThirdParty para aparecer en el resultado (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna usuarios del portal que tengan rol asignado, compañía asignada y un empleado con tercero válido (todos los joins son INNER).; La consulta opera sobre dos bases de datos distintas (seguridad y contenedor de negocio) enlazadas vía SQL dinámico.; No filtra por estado del usuario; retorna cualquier Status existente.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Usuario de portal de autoservicio; Rol; Empresa/Compañía del usuario; Empleado (nómina); Tercero', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto con Id de usuario portal, RoleId, UserCode, Status y nombre del tercero, ejecutando SQL dinámico construido con los nombres de bases de datos parametrizados.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.SelfService.PortalUser; Security.SelfService.RolePortalUser; Security.SelfService.Role; Security.SelfService.PortalUserCompany; Container.Payroll.Employee; Container.Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetPortalUserCompanyList';
-- GO
