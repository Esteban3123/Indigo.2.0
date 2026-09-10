

CREATE VIEW [Authorization].[ViewListPortfolioServices]
AS

	select pce.Id, 
	pce.AuthorizationPortfolioId, p.Code AuthorizationCode, p.Name AuthorizationName, p.Code + ' - ' + p.Name AuthorizationCodeName,
	pce.AuthorizationGroupId, g.Code AuthorizationGroupCode, g.Name AuthorizationGroupName, g.Code + ' - ' + g.Name AuthorizationGroupCodeName,
	ce.Id CUPSEntityId, ce.Code CUPSEntityCode, ce.Description CUPSEntityName, ce.Code + ' - ' + ce.Description CUPSEntityCodeName,
	IIF(csa.Id is null, 0, 1) SelectOption,
	csa.Id ConfigurationServicesAmbulatoryId,
	cd.Id ContractDescriptionId, cd.Code + ' - ' + cd.Name ContractDescriptionCodeName
	from [Authorization].AuthorizationPortfolioCUPSEntity pce
	inner join [Authorization].AuthorizationPortfolio p on p.Id = pce.AuthorizationPortfolioId
	inner join [Authorization].AuthorizationGroup g on g.Id = pce.AuthorizationGroupId
	inner join Contract.CUPSEntity ce on ce.Id = pce.CUPSEntityId
	left join [Authorization].ConfigurationServicesAmbulatory csa on csa.AuthorizationPortfolioCUPSEntityId = pce.Id
	left join Contract.ContractDescriptions cd on cd.Id = pce.ContractDescriptionId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de servicios del portafolio de autorización, que combina los procedimientos CUPS habilitados por entidad con su grupo de autorización, el portafolio al que pertenecen y, opcionalmente, la descripción de contrato asociada. Para cada servicio indica si ya tiene configuración ambulatoria activa (tiempos y unidades permitidos), lo que permite saber si el ítem está listo para operar en ese flujo. Se usa en procesos de autorización de servicios ambulatorios para consultar, filtrar y seleccionar qué procedimientos o servicios están disponibles dentro de cada portafolio y grupo de autorización, facilitando la parametrización y gestión de autorizaciones de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListPortfolioServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListPortfolioServices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el portafolio de servicios CUPS autorizables, mostrando portafolio, grupo de autorización, servicio CUPS, descripción contractual y si tiene configuración ambulatoria asociada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de AuthorizationPortfolioCUPSEntity debe tener AuthorizationPortfolioId, AuthorizationGroupId y CUPSEntityId válidos (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan filas con portafolio, grupo de autorización y CUPSEntity existentes (INNER JOIN).; ConfigurationServicesAmbulatory y ContractDescriptions son opcionales en el resultado (LEFT JOIN).; La columna SelectOption es binaria (0/1) y refleja la existencia de configuración ambulatoria para el ítem.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Portafolio de autorización; Grupo de autorización; CUPS (Clasificación Única de Procedimientos en Salud); Servicios ambulatorios; Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListPortfolioServices: Devuelve la lista de servicios CUPS de cada portafolio enriquecida con códigos y nombres concatenados (Code + '' - '' + Name) y un indicador SelectOption=1 cuando existe ConfigurationServicesAmbulatory asociada, 0 en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si csa.Id IS NULL (no existe ConfigurationServicesAmbulatory ligada al AuthorizationPortfolioCUPSEntity) → SelectOption = 0 else SelectOption = 1', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolio; Authorization.AuthorizationGroup; Contract.CUPSEntity; Authorization.ConfigurationServicesAmbulatory; Contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListPortfolioServices';
GO
