-- Ordenes Asignadas sin gestionar
CREATE VIEW [Authorization].[ViewAllottedTimeForRequests]
AS
SELECT	tp.AssignUserCode,
		SUM
		(
			ISNULL(csae.Assignment, csa.Assignment) * CASE ISNULL(csae.AssignmentUnit, csa.AssignmentUnit)
																	WHEN 1 THEN 1
																	WHEN 2 THEN 60
																	WHEN 3 THEN 1440
																END
		) AssignedMinutes
FROM [Authorization].TraceabilityPaperwork tp
JOIN
(
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 Type, 
			apce.CUPSEntityId ItemId,
			apce.ContractDescriptionId ContractDescriptionId,
			csa.Assignment, csa.AssignmentUnit
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL ContractDescriptionId,
			csa.Assignment, csa.AssignmentUnit
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
) csa ON tp.CareCenterCode = csa.CareCenterCode AND tp.Type = csa.Type AND tp.ServiceId = csa.ItemId AND ISNULL(tp.ContractDescriptionId, 0) = ISNULL(csa.ContractDescriptionId, 0)
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND tp.CareGroupId = csae.CareGroupId
WHERE tp.Status = 1
	AND ISNULL(csae.SusceptibleAuthorization, 1) = 1
	AND tp.AssignUserCode IS NOT NULL
GROUP BY tp.AssignUserCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que calcula el tiempo total asignado (en minutos) por usuario gestor para las solicitudes de autorización ambulatoria que ya tienen un usuario asignado pero aún no han sido gestionadas. Combina el portafolio de servicios CUPS y productos de inventario activos con su configuración de tiempos de asignación (en minutos, horas o días), aplicando excepciones por grupo de atención cuando corresponde. Sirve para monitorear la carga de trabajo de los autorizadores y detectar órdenes asignadas pendientes de tramitar, cruzando la trazabilidad de trámites de autorización con los tiempos configurados por sede y tipo de servicio o medicamento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAllottedTimeForRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewAllottedTimeForRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcular, por usuario asignado, el total de minutos de gestión comprometidos por trámites de autorización activos pendientes, aplicando la configuración de tiempos del portafolio y sus excepciones por grupo de atención.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de trámites en TraceabilityPaperwork con Status=1 y AssignUserCode no nulo; Configuración vigente de ConfigurationServicesAmbulatory asociada a portafolios activos (ap.Status=1); Coincidencia entre el centro de atención, tipo, ítem y descripción de contrato del trámite con el portafolio configurado', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se totalizan trámites activos (tp.Status = 1); Solo se consideran portafolios de autorización activos (ap.Status = 1); Solo se incluyen servicios susceptibles de autorización: ISNULL(csae.SusceptibleAuthorization, 1) = 1 (por defecto sí, salvo que la excepción lo niegue); Se excluyen trámites sin usuario asignado (tp.AssignUserCode IS NOT NULL); Los tiempos siempre se normalizan a minutos según la unidad (1=min, 2=hora, 3=día); El cruce entre trámite y configuración exige coincidencia de centro de atención, tipo (servicio/producto), ítem y descripción de contrato (tratando NULL como 0); La excepción por CareGroup tiene prioridad sobre la configuración base para Assignment y AssignmentUnit', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'trámite de autorización; portafolio de autorización; centro de atención; servicios ambulatorios; CUPS; producto de inventario; grupo de atención (CareGroup); tiempo asignado de gestión; excepciones de configuración; susceptibilidad de autorización', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result: Devuelve por AssignUserCode la suma de minutos asignados (Assignment * factor de unidad) considerando excepciones por CareGroup cuando existan, filtrando trámites activos susceptibles de autorización', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AssignmentUnit = 1 → El valor de Assignment se interpreta en minutos (multiplicador 1); si AssignmentUnit = 2 → El valor de Assignment se interpreta en horas y se convierte a minutos (multiplicador 60); si AssignmentUnit = 3 → El valor de Assignment se interpreta en días y se convierte a minutos (multiplicador 1440); si Existe registro en ConfigurationServicesAmbulatoryExceptions para el ConfigurationServicesAmbulatory y CareGroup del trámite → Se usan Assignment y AssignmentUnit de la excepción (csae) en lugar de los valores estándar else Se usan los valores estándar de ConfigurationServicesAmbulatory (csa); si tp.Type = 1 (servicio CUPS) → Se cruza contra AuthorizationPortfolioCUPSEntity usando CUPSEntityId y ContractDescriptionId; si tp.Type = 2 (producto de inventario) → Se cruza contra AuthorizationPortfolioInventoryProduct usando InventoryProductId, sin ContractDescriptionId', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.ConfigurationServicesAmbulatory; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatoryExceptions', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewAllottedTimeForRequests';
GO
