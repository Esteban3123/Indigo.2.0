

CREATE VIEW [Authorization].[ViewListProductsSusceptibles]
AS

	select CONCAT(pro.Id, '-', pcc.CareCenterCode) Row, 
	pro.Id InventoryProductId, pro.Code InventoryProductCode, pro.Name InventoryProductName, pro.Code + ' - ' + pro.Name InventoryProductCodeName, pcc.CareCenterCode
	,p.TypePortfolio, pt.Class as ClassProduct
	from [Authorization].AuthorizationPortfolioInventoryProduct pip
	inner join [Authorization].AuthorizationPortfolioCareCenter pcc on pcc.AuthorizationPortfolioId = pip.AuthorizationPortfolioId
	inner join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioInventoryProductId = pip.Id
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pip.AuthorizationPortfolioId
	inner join Inventory.InventoryProduct pro on pro.Id = pip.InventoryProductId
	Inner JOIN Inventory.ProductType pt ON pro.ProductTypeId = pt.Id
	where p.Status = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de productos de inventario (medicamentos, insumos o dispositivos médicos) que están habilitados para ser autorizados en el proceso ambulatorio, cruzados con los centros de atención donde aplican. Combina el portafolio de autorización activo, la configuración de tiempos ambulatorios y el catálogo de productos para identificar qué ítems son susceptibles de autorización por sede. Cada fila representa la combinación única de un producto y un centro de atención habilitado, incluyendo el código y nombre del producto para facilitar su selección o validación en el flujo de autorizaciones ambulatorias.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListProductsSusceptibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListProductsSusceptibles';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos de inventario configurados como servicios ambulatorios susceptibles de autorización, junto con los centros de atención habilitados, restringido a portafolios de autorización activos.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El portafolio de autorización debe tener Status = 1 (activo); El producto de inventario debe estar asociado al portafolio vía AuthorizationPortfolioInventoryProduct; El producto debe tener una configuración de servicio ambulatorio en ConfigurationServicesAmbulatory; El portafolio debe tener al menos un centro de atención asociado en AuthorizationPortfolioCareCenter', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen productos pertenecientes a portafolios de autorización activos (Status = 1); Solo se exponen productos que tienen configuración ambulatoria registrada (INNER JOIN con ConfigurationServicesAmbulatory); El identificador de fila combina el producto y el código del centro de atención, garantizando granularidad por sede; El campo InventoryProductCodeName concatena código y nombre del producto separados por '' - ''', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Portafolio de autorización; Producto de inventario; Centro de atención; Servicios ambulatorios; Autorización de servicios', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación producto-centro de atención, identificada por Row = CONCAT(InventoryProductId, ''-'', CareCenterCode)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.Status = 1 → Incluye el producto/centro en el resultado else Excluye los portafolios inactivos del listado', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioInventoryProduct; Authorization.AuthorizationPortfolioCareCenter; Authorization.ConfigurationServicesAmbulatory; Authorization.AuthorizationPortfolio; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListProductsSusceptibles';
GO
