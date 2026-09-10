

-- =============================================
-- Author:      Hector Rodriguez
-- Create Date: 15-04-2021
-- Description: Consulta Roles por tenant
-- =============================================
CREATE VIEW Security.ViewTenantRoll
AS
SELECT TR.TenantId, R.Id, R.RollCode, RollName = R.[Description], R.RollType FROM
Security.TenantRoll TR (NOLOCK)
INNER JOIN Security.Roll R (NOLOCK)
ON R.Id = TR.RollId
