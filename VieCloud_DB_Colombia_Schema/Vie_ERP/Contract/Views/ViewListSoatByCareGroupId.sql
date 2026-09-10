

CREATE VIEW [Contract].[ViewListSoatByCareGroupId]
AS

SELECT    DISTINCT    ips.Id, ips.Code, ips.Name, ips.ServiceManual, ips.ServiceClass, ips.ServiceType, ips.Presentation, ips.AuthorizationLevel, ips.ContributionsWeeks, ips.[Procedure], ips.SubattentionCode, ips.MinimunAgeUnit, 
                         ips.MinimunAge, ips.MaximumAgeUnit, ips.MaximumAge, ips.InMale, ips.InFemale, ips.ChildbirthAbortion, ips.POS, ips.ComplexityLevel, ips.PromotionAndPrevention, ips.PromotionAndPreventionActivities, 
                         ips.SurgeryArtroscopica, ips.PathologyService, ips.Status, ips.CreationUser, ips.CreationDate, ips.ModificationUser, ips.ModificationDate, cg.Id AS CareGroupId
FROM            Contract.CareGroup AS cg  with (nolock)  INNER JOIN
                         Contract.ProcedureCups AS pc  with (nolock) ON cg.ProcedureTemplateId = pc.ProceduresTemplateId INNER JOIN
                         Contract.CupsHomologation AS ch  with (nolock) ON ch.CupsEntityId = pc.CupsId INNER JOIN
                         Contract.IPSService AS ips  with (nolock) ON ch.IPSServiceId = ips.Id
WHERE        (ips.ServiceManual = 3) AND (ips.Status = 1) AND ISNULL(ips.ServiceClass,1) = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios SOAT activos de la IPS asociados a un grupo de atención contractual. Cruza el grupo de atención (CareGroup) con la plantilla de procedimientos CUPS contratados, luego aplica la homologación entre el código CUPS de la entidad y el servicio interno de la IPS, para finalmente exponer el catálogo de servicios del manual SOAT (ServiceManual = 3) que están vigentes y clasificados como clase 1. Sirve para consultar qué procedimientos y servicios SOAT están disponibles dentro de un grupo de atención específico de un contrato, apoyando la facturación, autorización y liquidación de atenciones bajo la cobertura SOAT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListSoatByCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListSoatByCareGroupId';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios IPS del manual SOAT vigentes y de clase 1 asociados a cada grupo de atención mediante la homologación CUPS de la plantilla del contrato.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en CareGroup con ProcedureTemplateId vinculado a ProcedureCups; Existe homologación CupsHomologation entre CupsEntityId y servicios IPS; El servicio IPS debe existir en el catálogo IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios IPS pertenecientes al manual tarifario SOAT (ServiceManual = 3); Solo se exponen servicios activos (Status = 1); Solo se incluyen servicios de ServiceClass = 1 (o nulo, tratado como 1); El resultado es DISTINCT: no se repiten combinaciones servicio-grupo de atención; La trazabilidad servicio↔grupo de atención se realiza vía plantilla de procedimientos y homologación CUPS', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Manual tarifario SOAT; Grupo de atención (CareGroup); Homologación CUPS; Servicio IPS; Plantilla de procedimientos contratada; Clase de servicio', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.IPSService: Solo retorna servicios cuando ServiceManual = 3 (manual SOAT), Status = 1 (activo) e ISNULL(ServiceClass,1) = 1, junto con el CareGroupId asociado', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.ProcedureCups; Contract.CupsHomologation; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListSoatByCareGroupId';
GO
