





-- =============================================
-- Author:      Hector Rodriguez
-- Create Date: 15-04-2021
-- Description: Consulta Tenants por usuario
-- =============================================
CREATE VIEW [Security].[ViewTenantUsers]
AS

SELECT
	Security.TenantUsers.TenantId AS Id
   ,Security.Tenant.Name
   ,Security.TenantUsers.UserId
   ,Security.TenantUsers.ManageCompany
FROM Security.Tenant WITH (NOLOCK) 
INNER JOIN Security.TenantUsers WITH (NOLOCK) ON Security.Tenant.Id = Security.TenantUsers.TenantId
WHERE Tenant.Status = 2 --Tenant Activo
	AND Security.TenantUsers.State = 1 --Activo'
