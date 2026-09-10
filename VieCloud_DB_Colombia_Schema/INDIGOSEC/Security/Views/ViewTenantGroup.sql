

-- =============================================
-- Author:      Hector Rodriguez
-- Create Date: 15-04-2021
-- Description: Consulta Roles por tenant
-- =============================================
CREATE VIEW Security.ViewTenantGroup
AS
SELECT TG.TenantId, G.Id, GroupCode = G.Code, GroupName = G.[Description], G.GroupType, GroupState = G.[State] FROM
Security.TenantGroup TG (NOLOCK)
INNER JOIN Security.[Group] G (NOLOCK)
ON G.Id = TG.GroupId
