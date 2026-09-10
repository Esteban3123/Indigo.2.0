

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que relaciona los códigos ATC (clasificación anatómica terapéutica) de medicamentos con sus vías de administración permitidas, combinando el catálogo de vías (oral, intravenosa, intramuscular, tópica, etc.) con la tabla de asociación ATC-vía. Para cada combinación expone el identificador del ATC, el código y nombre de la vía de administración, y la forma farmacéutica asociada. Se utiliza en farmacia y prescripción clínica para consultar rápidamente por qué rutas puede administrarse un medicamento según su clasificación ATC.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAtcAdministrationRoute';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewAtcAdministrationRoute';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las vías de administración asociadas a cada código ATC junto con los datos descriptivos de la vía y su forma farmacéutica.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen relaciones ATC-vía cuya vía de administración exista en el catálogo (INNER JOIN).; Las lecturas se realizan con NOLOCK, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ATC (clasificación anatómica terapéutica); Vía de administración; Forma farmacéutica; Medicamentos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve únicamente las relaciones ATC-vía de administración cuya vía existe en el catálogo (INNER JOIN sobre AdministrationRoute.Id = ATCAdministrationRoute.AdministrationRouteId).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATCAdministrationRoute; Inventory.AdministrationRoute', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewAtcAdministrationRoute';
GO
