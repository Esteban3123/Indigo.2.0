

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
	, THP.Id AS ThirdPartyPatientId -- Tercero paciente
	, rcd.PatientQuotaResponsibleThirdPartyId  -- Responsable cuota paciente
	, rcd.ThirdPartyId -- Tercero del folio
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del tablero de liquidación de admisiones: consolida en una sola consulta todos los datos necesarios para el proceso de liquidación de ingresos hospitalarios (urgencias, hospitalización, consulta externa). Combina información del episodio de ingreso del paciente (fecha, tipo, causa, cama, estado), datos demográficos y de afiliación del paciente (cédula, nombre, tipo de documento, estrato, nivel de copago y cuota moderadora), la sede o centro de atención, la unidad funcional de ingreso y egreso, la entidad pagadora o aseguradora (EPS/ARS), el grupo de atención del contrato, el diagnóstico de egreso (CIE-10) y los valores del folio de facturación (total facturado, valor a cargo del paciente, descuentos, bonos, valor a cargo de la entidad). También expone el control de ingresos (RevenueControl/RevenueControlDetail), el tipo de grupo de atención para determinar cómo se distribuye el cobro entre paciente y entidad, anticipos del portafolio disponibles para cruce, y el número de ítems por folio. Sirve como fuente principal para el dashboard y los reportes de liquidación, permitiendo visualizar el estado financiero de cada admisión, identificar saldos pendientes por paciente o entidad, y gestionar la facturación y el recaudo por episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationDashboard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationDashboard';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un tablero consolidado de admisiones hospitalarias activas tipo 1 cuyo control de ingresos posee un único folio detalle activo, listas para liquidación, junto con datos del paciente, entidad, folio, descuentos y anticipos de cartera.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe existir en ADINGRESO con paciente, nivel, centro de atención y unidad funcional válidos (joins INNER).; Debe existir un RevenueControl asociado por número de admisión y al menos un RevenueControlDetail en estado 1.; El RevenueControlDetail debe tener un CareGroup válido y al menos una distribución de orden de servicio con CUPSEntity asociado.; El estado del ingreso (IESTADOIN) debe ser '' '' (en blanco) o ''P''.; El tipo de ingreso (TIPOINGRE) debe ser 1.; El RevenueControl debe contener exactamente un único RevenueControlDetail con Status=1 (validado por EXISTS + GROUP BY HAVING COUNT=1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen admisiones de tipo 1 (TIPOINGRE=1).; Solo se muestran admisiones cuyo control de ingresos posee exactamente un detalle activo (Status=1).; Las admisiones con estado distinto a '' '' o ''P'' se excluyen del tablero.; Los CareGroup tipo 3 manejan el descuento del paciente como suma de distribuciones, los demás usan el descuento directo del detalle.; Para CareGroup tipo 3 el valor a cargo de la entidad siempre es 0.; Los anticipos de cartera asociados solo se muestran si están en estado 2, con saldo y valor positivos y coinciden con el número de admisión.; Si no se ha registrado fecha de alta, se asume una fecha de salida ficticia 10 minutos después del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión / Ingreso hospitalario; Paciente; Liquidación; Folio de facturación (RevenueControl/Detail); Centro de atención; Unidad funcional; Nivel de atención y copagos (contributivo, subsidiado, vinculado, moderador); Plan de beneficios; Autorización; Egreso / Alta del paciente; Diagnóstico (CUPS/diagnóstico de egreso); CareGroup (grupo de atención); Entidad de salud / EPS; Tercero responsable; Cuota del paciente y descuentos; Bonos (Voucher); Anticipos de cartera (PortfolioAdvance); Tratamiento especial; Cama / estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve solo admisiones con IESTADOIN in ('' '',''P'') AND TIPOINGRE=1 AND rcd.Status=1 y cuyo RevenueControl tenga exactamente un detalle activo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EGR.FECALTPAC IS NULL → FECALTPAC se calcula como la fecha de ingreso + 10 minutos else FECALTPAC toma la fecha real de alta del paciente desde HCREGEGRE; si cgf.CareGroupType = 3 → TotalPatientWithDiscount = TotalFolio - TotalPatientWithDiscount - ValueVoucher; TotalEntity = 0; PatientDiscount se calcula como suma de GrandTotalDiscount de la distribución else TotalEntity = TotalFolio - TotalPatientWithDiscount - ValueVoucher; PatientDiscount toma el valor directo de rcd.PatientDiscount; TotalPatientWithDiscount usa ValueVoucher si es >0, si no rcd.TotalPatientWithDiscount; si Valor calculado de TotalPatientWithDiscount > 0 → Se busca y concatena el primer PortfolioAdvance del tercero con Value>0, Balance>0, Status=2 y AdmissionNumber coincidente else PortfolioAdvanceConcat queda en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREGEGRE; Contract.HealthAdministrator; Contract.CareGroup; Contract.CUPSEntity; Billing.RevenueControl; Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; POrtfolio.PortfolioAdvance; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationDashboard';
GO
