


-- =============================================
-- Author:      <HECTOR RODRIGUEZ RUBIANO>
-- Create Date: <17/01/2022>
-- Description: <Lista CPC activos sin hijos>
-- =============================================
CREATE VIEW [Budget].[ViewListCPCCatalog]
AS

SELECT CPCC.[Id], CPCC.[Code], CPCC.[Name] FROM [Budget].[CPCCatalog] CPCC WITH(NOLOCK)
WHERE CPCC.[Status] = 1
AND CPCC.ID NOT IN
(SELECT DISTINCT CPCCatalogOwnerId FROM [Budget].[CPCCatalog] WITH(NOLOCK) WHERE [Status] = 1
AND CPCCatalogOwnerId IS NOT NULL)