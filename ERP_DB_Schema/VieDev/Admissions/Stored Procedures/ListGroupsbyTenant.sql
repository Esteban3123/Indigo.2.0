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