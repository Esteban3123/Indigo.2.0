CREATE VIEW [Authorization].[ViewListCUPSSusceptibles]
AS

	select CONCAT(pcc.Id, csa.Id, ISNULL(csae.Id, 0)) Id, 
	ce.Id CUPSEntityId, ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName, 
	pcc.CareCenterCode,
	ISNULL(csae.CareGroupId, 0) CareGroupId, ISNULL(csae.SusceptibleAuthorization, 1) SusceptibleAuthorization, 
	pce.ContractDescriptionId, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName, p.TypePortfolio, ce.ServiceType
	from [Authorization].AuthorizationPortfolioCUPSEntity pce
	inner join [Authorization].AuthorizationPortfolioCareCenter pcc on pcc.AuthorizationPortfolioId = pce.AuthorizationPortfolioId
	inner join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioCUPSEntityId = pce.Id
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pce.AuthorizationPortfolioId
	inner join Contract.CUPSEntity ce on ce.Id = pce.CUPSEntityId
	left join [Authorization].ConfigurationServicesAmbulatoryExceptions csae on csae.ConfigurationServicesAmbulatoryId = csa.Id
	left join Contract.ContractDescriptions cd on cd.Id = pce.ContractDescriptionId
	where p.Status = 1 
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios CUPS (procedimientos, exámenes y consultas) que están configurados como susceptibles de autorización en el proceso ambulatorio, cruzando el portafolio de autorizaciones activo con los centros de atención habilitados, la configuración de tiempos ambulatorios y sus excepciones por grupo de atención. Para cada combinación devuelve el código y descripción del CUPS, el centro de atención, el grupo de atención, si el servicio requiere autorización (susceptible o no), y el tipo de descripción de contrato asociado. Sirve como base para que el módulo de autorizaciones ambulatorias determine qué servicios deben pasar por proceso de autorización según la sede, el portafolio vigente y las reglas de excepción configuradas por contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListCUPSSusceptibles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListCUPSSusceptibles';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios CUPS configurados como susceptibles de autorización ambulatoria en portafolios activos, incluyendo sus centros de atención, excepciones por grupo de atención y descripción de contrato asociada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen portafolios de autorización con Status = 1 (activos); Los CUPS deben estar vinculados al portafolio vía AuthorizationPortfolioCUPSEntity; Debe existir configuración ambulatoria (ConfigurationServicesAmbulatory) asociada al CUPS del portafolio; El portafolio debe tener al menos un centro de atención asociado en AuthorizationPortfolioCareCenter', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen CUPS pertenecientes a portafolios activos (Status = 1); Cada fila representa la combinación de un CUPS del portafolio con un centro de atención y opcionalmente una excepción por grupo de atención; Si no hay excepción configurada, el servicio se considera susceptible de autorización por defecto (SusceptibleAuthorization = 1); El CareGroupId = 0 representa la ausencia de excepción específica de grupo de atención; La descripción de contrato es opcional (LEFT JOIN con ContractDescriptions)', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Portafolio de autorización; Centro de atención; Servicios ambulatorios; Susceptibilidad de autorización; Grupo de atención; Excepciones de configuración; Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un Id sintético construido como CONCAT(AuthorizationPortfolioCareCenter.Id, ConfigurationServicesAmbulatory.Id, ISNULL(ConfigurationServicesAmbulatoryExceptions.Id, 0)) por cada combinación CUPS-centro-configuración-excepción', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.Status = 1 → Solo se incluyen portafolios de autorización activos; los inactivos se excluyen del resultado; si No existe ConfigurationServicesAmbulatoryExceptions para la configuración (LEFT JOIN nulo) → Se asume CareGroupId = 0 y SusceptibleAuthorization = 1 (susceptible por defecto) else Se toman los valores CareGroupId y SusceptibleAuthorization definidos en la excepción del grupo de atención', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioCareCenter; Authorization.ConfigurationServicesAmbulatory; Authorization.AuthorizationPortfolio; Contract.CUPSEntity; Authorization.ConfigurationServicesAmbulatoryExceptions; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListCUPSSusceptibles';
GO
