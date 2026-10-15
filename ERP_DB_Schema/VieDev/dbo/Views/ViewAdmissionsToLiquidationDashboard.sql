




CREATE VIEW [dbo].[ViewAdmissionsToLiquidationDashboard]
  
AS

	SELECT DISTINCT LTRIM(RTRIM(ING.NUMINGRES)) AS AdmissionCode, ING.NUMINGRES AS AdmissionCodeWithOutTrim, ING.IFECHAING AS AdmissionDate
	, ING.TIPOINGRE AS AdmissionType, ING.GENCAREGROUP AS AdmissionCaregroupId
	, ING.ICAUSAING AS AdmissionReason, ING.ITIPORIES AS AdmissionRiskType, CONCAT(LTRIM(RTRIM(CEN.CODCENATE)), ' - ', LTRIM(RTRIM(CEN.NOMCENATE))) AS AdmissionCentAtencCodeName
	, CONCAT(LTRIM(RTRIM(UFU.UFUCODIGO)), ' - ', LTRIM(RTRIM(UFU.UFUDESCRI))) AS AdmissionUniFuncCodeName, ING.IINGREPOR AS PlaceEntry, LTRIM(RTRIM(COALESCE (ING.CODPANATE, ''))) AS BenefitPlan
	, COALESCE (ING.IAUTORIZA, '') AS AuthorizationNumber, COALESCE (ING.IPTELEFON, '') AS ResponsiblePhone, ING.IPRNOMBRE AS ResponsibleName, PAT.IPCODPACI AS PatientCode, PAT.IPFECNACI AS PatientBirth
	, PAT.IPSEXOPAC AS PatientGenus, COALESCE (PAT.IPESTRATO, '0') AS PatientEstrato, PAT.IPTIPOPAC AS PatientType, PAT.IPTIPOAFI AS PatientAfiliation, PAT.IPTIPODOC AS PatientDocumentType
	, NIV.NIVCODIGO AS NivelCode, LTRIM(RTRIM(NIV.NIVDESCRI)) AS NivelName, NIV.NIVPORCMO AS NivelModeratorSharePercentage, NIV.NIVPORCOP AS NivelCoPayContribPercentage
	, NIV.NIVPORSUB AS NivelCoPaySubsiPercentage, NIV.NIVPORVIN AS NivelCoPayVincuPercentage, NIV.NIVSISBEN AS NivelSisben, NIV.TOPEVECMO AS NivelModeratorShareTop
	, NIV.TOPEVECOP AS NivelCoPayContribTop, NIV.TOPEVESUB AS NivelCoPaySubsibTop, NIV.TOPEVEVIN AS NivelCoPayVincuTop, NIV.TOPANUCMO AS NivelModeratorShareTopYear
	, NIV.TOPANUCOP AS NivelCoPayContribTopYear, NIV.TOPANUSUB AS NivelCoPaySubsibTopYear, NIV.TOPANUVIN AS NivelCoPayVincuTopYear, ING.IESTADOIN AS Status
	, ING.GENCONENTITY AS HealthAdministratorId, PAT.GENCONENTITY AS PatientEntityId, PAT.IPTELMOVI AS PatientPhone
	, PAT.GENCAREGROUP AS PatientCareGroupId, LTRIM(RTRIM(PAT.IPNOMCOMP)) AS PatientName, LTRIM(RTRIM(ING.CODICAMHO)) AS BedStay, ING.ILIQUIDAC AS LiquidationType, '- Ingresos' AS StatusName
	, ING.GENCONENTITY as EntityId, COALESCE(PHA.Code, '') AS PatientEntityCode, COALESCE(PHA.Name, '') AS PatientEntityName, COALESCE(AHA.Code, '') AS EntityCode, COALESCE(AHA.Code, '') AS EntityName
	, CONCAT(CG.Code, ' - ', CG.Name) AS AdmissionCareGroupCodeName, CASE WHEN EGR.FECALTPAC IS NULL THEN DATEADD(MINUTE, 10, ING.IFECHAING)  ELSE EGR.FECALTPAC  END AS FECALTPAC
	, CONCAT(LTRIM(RTRIM(EGF.UFUCODIGO)), ' - ', LTRIM(RTRIM(EGF.UFUDESCRI))) AS UniFuncEgre
	, ING.TRATAESPECIA as TratamientoEspecial, rc.Id As RevenueControlId
	
	, (case cgf.CareGroupType 
		when 3 then (rcd.TotalFolio - rcd.TotalPatientWithDiscount - rcd.ValueVoucher) 
		else 
			case when rcd.ValueVoucher > 0 then rcd.ValueVoucher else rcd.TotalPatientWithDiscount end
		end) As TotalPatientWithDiscount
	, (case cgf.CareGroupType when 3 then 0 else (rcd.TotalFolio - rcd.TotalPatientWithDiscount - rcd.ValueVoucher) end) As TotalEntity
	, rcd.TotalFolio As TotalFolio

	, (case cgf.CareGroupType 
		when 3 then 
			(select sum(sodd.GrandTotalDiscount) from Billing.ServiceOrderDetailDistribution sodd with(nolock)
			where sodd.RevenueControlDetailId = rcd.Id)
		else 
			rcd.PatientDiscount
		end) As PatientDiscount

	, concat(ltrim(rtrim(PAT.IPCODPACI)), ' - ',LTRIM(RTRIM(PAT.IPNOMCOMP))) As PatientCodeName
	, LTRIM(RTRIM(CEN.CODCENATE)) As CODCENATE
	, THP.Id AS ThirdPartyPatientId	
	, concat(cgf.Code, ' - ', cgf.[Name]) As CareGroupCodeName
	, cgf.CareGroupType As FolioCareGroupType
	, (select top 1 concat(ce.Code, ' - ', ce.[Description]) from Billing.ServiceOrderDetailDistribution sodd with(nolock)
		inner join Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
		inner join [Contract].CUPSEntity ce with(nolock) on sod.CUPSEntityId = ce.Id
		where sodd.RevenueControlDetailId = rcd.Id And ce.Id is not null) As CupsEntityCodeName
	, rcd.Id As RevenueControlDetailId
	, rcd.FolioOrder
	, rcd.FolioType
	, rcd.CareGroupId As CareGroupFolioId
	, ING.CODDIAEGR As Diagnostico
	, concat(dia.CODDIAGNO, ' - ', dia.NOMDIAGNO) As DiagnosticoCodeName
	, (select count(1) from Billing.ServiceOrderDetailDistribution sodd with(nolock) 
		where sodd.RevenueControlDetailId = rcd.Id) As NoItems	
	, case when (case cgf.CareGroupType 
		when 3 then (rcd.TotalFolio - rcd.TotalPatientWithDiscount - rcd.ValueVoucher) 
		else 
			case when rcd.ValueVoucher > 0 then rcd.ValueVoucher else rcd.TotalPatientWithDiscount end
		end) > 0 then
			(select top 1 concat(pa.Id,';',pa.code,';',pa.Balance,';',pa.ThirdPartyId,';',pa.MainAccountId,';',coalesce(pa.CostCenterId, 0),';',format(pa.DocumentDate, 'dd/MM/yyyy hh:mm:ss')) from POrtfolio.PortfolioAdvance pa with(nolock) 
			where pa.ThirdPartyId = THP.Id And pa.[Value] > 0 And pa.Balance > 0 And pa.[Status] = 2 And pa.AdmissionNumber = ING.NUMINGRES)  
		else 
			null end As PortfolioAdvanceConcat
			
	FROM dbo.ADINGRESO AS ING with(nolock)
	INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
	INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
	INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
	INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
	LEFT JOIN dbo.INENTIDAD INE with(nolock) ON ING.CODENTIDA = INE.CODENTIDA
	left join dbo.INDIAGNOS dia with(nolock) on ing.CODDIAEGR = dia.CODDIAGNO
	LEFT JOIN Contract.HealthAdministrator AS PHA WITH(NOLOCK) ON PAT.GENCONENTITY = PHA.Id
	LEFT JOIN Contract.HealthAdministrator AS AHA WITH(NOLOCK) ON ING.GENCONENTITY = AHA.Id
	LEFT JOIN Contract.CareGroup AS CG WITH(NOLOCK) ON PAT.GENCAREGROUP = CG.Id
	LEFT JOIN dbo.HCREGEGRE AS EGR WITH(NOLOCK) ON EGR.NUMINGRES = ING.NUMINGRES
	LEFT JOIN dbo.INUNIFUNC AS EGF WITH(NOLOCK) ON EGF.UFUCODIGO = EGR.UFUCODIGO
	LEFT JOIN Billing.RevenueControl rc with(nolock) ON rc.AdmissionNumber = LTRIM(RTRIM(ING.NUMINGRES))
	inner join Billing.RevenueControlDetail rcd with(nolock) on rcd.RevenueControlId = rc.Id
	inner join Contract.CareGroup cgf with(nolock) on rcd.CareGroupId = cgf.Id	
	inner join (select sodd.RevenueControlDetailId, sod.* 
			from Billing.ServiceOrderDetailDistribution sodd with(nolock)
			inner join Billing.ServiceOrderDetail sod on sodd.ServiceOrderDetailId = sod.Id
			inner join [Contract].CUPSEntity ce with(nolock) on sod.CUPSEntityId = ce.Id
		) sod1 on sod1.RevenueControlDetailId = rcd.Id			
	LEFT OUTER JOIN Common.ThirdParty THP  with (nolock) ON rc.PatientCode = THP.Nit
	where (ING.IESTADOIN = ' ' or ING.IESTADOIN = 'P') 
		--And (select count(1) from Billing.RevenueControlDetail rcd where rcd.RevenueControlId = rc.Id) = 1
		And EXISTS (select rcd2.RevenueControlId from Billing.RevenueControlDetail rcd2 where rcd2.RevenueControlId = rc.Id AND rcd2.Status = 1 GROUP BY rcd2.RevenueControlId HAVING COUNT(rcd2.RevenueControlId) = 1)
		And ING.TIPOINGRE = 1
		AND rcd.Status = 1