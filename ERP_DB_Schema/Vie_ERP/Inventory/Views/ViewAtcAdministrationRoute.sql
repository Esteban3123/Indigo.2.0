


CREATE VIEW [Inventory].[ViewAtcAdministrationRoute]
AS
select 
MAR.Id
,MAR.ATCId
, AR.Id AS AdministrationRouteId
, AR.Code AS AdministrationRouteCode
, AR.Name AS AdministrationRouteName, 
AR.PharmaceuticalFormId
from Inventory.ATCAdministrationRoute MAR WITH(NOLOCK)
INNER JOIN Inventory.AdministrationRoute AR WITH(NOLOCK)
ON AR.Id = MAR.AdministrationRouteId