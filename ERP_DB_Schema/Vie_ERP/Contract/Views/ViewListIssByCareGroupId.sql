



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