CREATE PROCEDURE [Admissions].[ListGroupsbyTenant]
@HISContainer VARCHAR(50),
@CodigoConsultar VARCHAR(50)

AS
BEGIN
	SET NOCOUNT ON;

if @CodigoConsultar = '' begin

		
		SELECT distinct r.Code as 'Codigo', r.[description] as 'Descripcion' from Security.Containers c
				inner join Security.TenantContainer b on  b.ContainerId = c.Id 
				inner join Security.Tenant t on  t.Id = b.TenantId 
				inner join Security.TenantGroup g on  g.TenantId = t.Id 
				inner join Security.[Group] r on  r.Id  = g.GroupId 
			where c.HISContainer = @HISContainer 

end else

		SELECT distinct r.Code as 'Codigo', r.[description] as 'Descripcion' from Security.Containers c
				inner join Security.TenantContainer b on  b.ContainerId = c.Id 
				inner join Security.Tenant t on  t.Id = b.TenantId 
				inner join Security.TenantGroup g on  g.TenantId = t.Id 
				inner join Security.[Group] r on  r.Id  = g.GroupId 
		where c.HISContainer = @HISContainer and r.Code = @CodigoConsultar

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los grupos de seguridad asociados a un tenant (empresa u organización) dentro del sistema, filtrando por el contenedor HIS (instancia del sistema hospitalario). Recibe como parámetros el identificador del contenedor HIS y, opcionalmente, el código de un grupo específico a buscar: si no se indica código, devuelve todos los grupos del tenant; si se indica, filtra por ese grupo en particular. Sirve para la gestión de perfiles y roles de acceso por organización, permitiendo saber qué grupos de usuarios están habilitados para un tenant dado en el ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'ListGroupsbyTenant';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'ListGroupsbyTenant';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los grupos de seguridad (código y descripción) asociados a un contenedor HIS, opcionalmente filtrados por código de grupo.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un contenedor en Security.Containers cuyo HISContainer coincida con el parámetro recibido; Las relaciones Containers→TenantContainer→Tenant→TenantGroup→Group deben estar pobladas para retornar resultados', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se filtra por c.HISContainer = @HISContainer en ambas ramas; El resultado siempre se devuelve con DISTINCT, evitando duplicados por la cadena de joins entre tenants/containers/grupos; Solo se exponen las columnas Code (alias Codigo) y Description (alias Descripcion) del grupo', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contenedor HIS; Tenant (organización); Grupo de seguridad; Perfiles/roles de acceso', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Security.Group: Cuando el código a consultar viene vacío, devuelve DISTINCT Code y Description de todos los grupos vinculados al HISContainer; si se proporciona código, restringe adicionalmente con r.Code = @CodigoConsultar', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodigoConsultar = '''' → Retorna todos los grupos distintos asociados al HISContainer sin filtro adicional por código de grupo else Retorna los grupos asociados al HISContainer filtrando además por r.Code = @CodigoConsultar', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.Containers; Security.TenantContainer; Security.Tenant; Security.TenantGroup; Security.Group', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'ListGroupsbyTenant';
-- GO
