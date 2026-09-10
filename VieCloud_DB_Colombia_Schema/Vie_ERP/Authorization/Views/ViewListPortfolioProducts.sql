

CREATE VIEW [Authorization].[ViewListPortfolioProducts]
AS

	select pip.Id, 
	pip.AuthorizationPortfolioId, p.Code AuthorizationCode, p.Name AuthorizationName, p.Code + ' - ' + p.Name AuthorizationCodeName,
	pip.AuthorizationGroupId, g.Code AuthorizationGroupCode, g.Name AuthorizationGroupName, g.Code + ' - ' + g.Name AuthorizationGroupCodeName,
	ip.Id InventoryProductId, ip.Code InventoryProductCode, ip.Name InventoryProductName, ip.Code + ' - ' + ip.Name InventoryProductCodeName,
	IIF(csa.Id is null, 0, 1) SelectOption,
	csa.Id ConfigurationServicesAmbulatoryId
	from [Authorization].AuthorizationPortfolioInventoryProduct pip
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pip.AuthorizationPortfolioId
	inner join [Authorization].AuthorizationGroup g on g.Id = pip.AuthorizationGroupId
	inner join Inventory.InventoryProduct ip on ip.Id = pip.InventoryProductId
	left join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioInventoryProductId = pip.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de productos de inventario (medicamentos, insumos y dispositivos médicos) asociados a portafolios y grupos de autorización. Combina el catálogo de productos con su portafolio de autorización correspondiente, el grupo de autorización al que pertenecen, y si tienen configuración de tiempos ambulatorios vigente. La columna SelectOption indica si el producto ya tiene una configuración de servicios ambulatorios activa (1) o no (0), lo que facilita la selección y gestión durante el proceso de autorización de servicios ambulatorios. Se usa principalmente en pantallas de configuración y reportería de portafolios autorizables por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListPortfolioProducts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListPortfolioProducts';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un listado los productos de inventario asociados a portafolios y grupos de autorización, indicando si cada uno ya cuenta con configuración de servicios ambulatorios.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro en AuthorizationPortfolioInventoryProduct debe tener referencias válidas a un AuthorizationPortfolio, un AuthorizationGroup y un InventoryProduct.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos del portafolio que tengan portafolio, grupo de autorización y producto de inventario válidos (INNER JOIN con AuthorizationPortfolio, AuthorizationGroup e InventoryProduct).; La existencia de configuración de servicios ambulatorios es opcional (LEFT JOIN), no excluye al producto del listado.; Se entregan códigos y nombres concatenados (''Code - Name'') para portafolio, grupo y producto, facilitando su despliegue en interfaces de selección.; SelectOption es un indicador binario (0/1) de si el producto del portafolio ya tiene configuración ambulatoria asignada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Portafolio de autorización; Grupo de autorización; Producto de inventario; Configuración de servicios ambulatorios', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna una fila por cada producto del portafolio de autorización con sus datos de portafolio, grupo, producto de inventario y un flag SelectOption que indica si tiene configuración ambulatoria (1) o no (0).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si csa.Id IS NULL (no existe configuración de servicios ambulatorios asociada al producto del portafolio) → SelectOption = 0 else SelectOption = 1', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioInventoryProduct; Authorization.AuthorizationPortfolio; Authorization.AuthorizationGroup; Inventory.InventoryProduct; Authorization.ConfigurationServicesAmbulatory', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioProducts';
GO
