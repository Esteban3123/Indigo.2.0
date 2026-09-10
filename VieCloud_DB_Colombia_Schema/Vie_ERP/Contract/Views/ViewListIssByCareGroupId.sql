

CREATE VIEW [Contract].[ViewListIssByCareGroupId]
AS
SELECT   DISTINCT     ips.Id, ips.Code, ips.Name, ips.ServiceManual, ips.ServiceClass, ips.ServiceType, ips.Presentation, ips.AuthorizationLevel, ips.ContributionsWeeks, ips.[Procedure], ips.SubattentionCode, ips.MinimunAgeUnit, 
                         ips.MinimunAge, ips.MaximumAgeUnit, ips.MaximumAge, ips.InMale, ips.InFemale, ips.ChildbirthAbortion, ips.POS, ips.ComplexityLevel, ips.PromotionAndPrevention, ips.PromotionAndPreventionActivities, 
                         ips.SurgeryArtroscopica, ips.PathologyService, ips.Status, ips.CreationUser, ips.CreationDate, ips.ModificationUser, ips.ModificationDate, cg.Id AS CareGroupId
FROM            Contract.CareGroup AS cg with (nolock) INNER JOIN
                         Contract.ProcedureCups AS pc with (nolock) ON cg.ProcedureTemplateId = pc.ProceduresTemplateId INNER JOIN
                         Contract.CupsHomologation AS ch with (nolock) ON ch.CupsEntityId = pc.CupsId INNER JOIN
                         Contract.IPSService AS ips with (nolock) ON ch.IPSServiceId = ips.Id
WHERE        (ips.ServiceManual <= 2) AND (ips.Status = 1) AND ISNULL(ips.ServiceClass,1) = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios de salud de la IPS (procedimientos, exámenes y prestaciones activos, de manual tarifario básico y clase de servicio estándar) que están contratados dentro de un grupo de atención específico. Para construir el resultado, vincula el grupo de atención del contrato con la plantilla de procedimientos CUPS contratados, luego homologa esos códigos CUPS al catálogo interno de servicios de la IPS. Se usa para consultar qué servicios están disponibles o habilitados para facturar bajo un grupo de atención contractual determinado, filtrando sólo servicios vigentes y de clasificación básica.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListIssByCareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListIssByCareGroupId';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios IPS activos, de manual y clase válidos, asociados a un grupo de cuidado a través de la plantilla de procedimientos y la homologación de CUPS.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe relación entre CareGroup.ProcedureTemplateId y ProcedureCups.ProceduresTemplateId; Existen homologaciones en CupsHomologation que enlazan CupsEntityId con servicios IPS', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se exponen servicios IPS activos (Status=1); Sólo se exponen servicios cuyo ServiceManual sea menor o igual a 2; Sólo se exponen servicios cuya ServiceClass sea 1 (o nula, tratada como 1); Los resultados son únicos (DISTINCT) por combinación servicio-grupo de cuidado; Las consultas usan NOLOCK, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de cuidado; Servicio IPS; Procedimiento CUPS; Homologación de CUPS; Plantilla de procedimientos; Nivel de autorización; Manual de servicios; Clase de servicio; POS; Promoción y prevención', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.IPSService: Devuelve servicios IPS sólo cuando ServiceManual <= 2, Status = 1 y ServiceClass (con default 1 si nulo) = 1, junto al CareGroupId asociado', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Contract.ProcedureCups; Contract.CupsHomologation; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIssByCareGroupId';
GO
