

CREATE View [dbo].[ViewLiquidationGetAdmissionAll]	
As

with rcd_cte as(
SELECT  RevenueControlId, Id,rcd.CreationDate
from Billing.RevenueControlDetail rcd WITH(NOLOCK)
where IsMasterAccount in(1,3)
GROUP BY RevenueControlId,Id,rcd.CreationDate
),
 master_cte as(
				SELECT	cte.RevenueControlId,
						cte.Id, 
						max(iif(rcd.IsMasterAccount=2,i.InvoiceDate,NULL)) DateTRMInsurer,
						max(iif(rcd.IsMasterAccount=4,i.InvoiceDate,NULL)) DateTRMpatient,
						max(cte.CreationDate) CreationDate
				from rcd_cte cte 
				left JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on cte.Id = rcd.RevenueControlDetailMasterId AND rcd.Status=2 
				left JOIN Billing.Invoice i WITH(NOLOCK) on rcd.Id= i.RevenueControlDetailId
				GROUP BY cte.RevenueControlId,cte.Id
			
)

	Select Distinct Ltrim(Rtrim(Ing.NUMINGRES)) As AdmissionCode
		, Ing.NUMINGRES As AdmissionCodeWithOutTrim
		, Ing.IFECHAING As AdmissionDate, Ing.TIPOINGRE As AdmissionType
		, Ing.GENCAREGROUP As AdmissionCaregroupId
		, Ing.ICAUSAING As AdmissionReason
		, Ing.ITIPORIES As AdmissionRiskType
		, Concat(Ltrim(Rtrim(Cen.CODCENATE)), ' - ', Ltrim(Rtrim(Cen.NOMCENATE))) As AdmissionCentAtencCodeName
		, Concat(Ltrim(Rtrim(Ufu.UFUCODIGO)), ' - ', Ltrim(Rtrim(Ufu.UFUDESCRI))) AS AdmissionUniFuncCodeName
		, Ing.IINGREPOR As PlaceEntry
		, Ltrim(Rtrim(Coalesce(Ing.CODPANATE, ''))) As BenefitPlan
		, Coalesce(Ing.IAUTORIZA, '') As AuthorizationNumber
		, Coalesce(Ing.IPTELEFON, '') As ResponsiblePhone
		, Ing.IPRNOMBRE As ResponsibleName
		, Pat.IPCODPACI As PatientCode
		, Pat.IPFECNACI As PatientBirth
		, Pat.IPSEXOPAC As PatientGenus
		, Coalesce(Pat.IPESTRATO, '0') As PatientEstrato
		, Pat.IPTIPOPAC As PatientType
		, Pat.IPTIPOAFI As PatientAfiliation
		, Pat.IPTIPODOC As PatientDocumentType
		, Niv.NIVCODIGO As NivelCode
		, Ltrim(Rtrim(Niv.NIVDESCRI)) As NivelName
		, Niv.NIVPORCMO As NivelModeratorSharePercentage
		, Niv.NIVPORCOP As NivelCoPayContribPercentage
		, Niv.NIVPORSUB As NivelCoPaySubsiPercentage
		, Niv.NIVPORVIN As NivelCoPayVincuPercentage
		, Niv.NIVSISBEN As NivelSisben
		, Niv.TOPEVECMO As NivelModeratorShareTop
		, Niv.TOPEVECOP As NivelCoPayContribTop
		, Niv.TOPEVESUB As NivelCoPaySubsibTop
		, Niv.TOPEVEVIN As NivelCoPayVincuTop
		, Niv.TOPANUCMO As NivelModeratorShareTopYear
		, Niv.TOPANUCOP As NivelCoPayContribTopYear
		, Niv.TOPANUSUB As NivelCoPaySubsibTopYear
		, Niv.TOPANUVIN As NivelCoPayVincuTopYear
		, Ing.IESTADOIN As [Status]
		, Ing.GENCONENTITY As HealthAdministratorId
		, Concat(Aha.Code, ' - ', Aha.[Name]) As AdmissionHealthAdministratorCodeName
		, Pat.GENCONENTITY As PatientEntityId
		, Pat.IPTELMOVI As PatientPhone
		, Pat.GENCAREGROUP As PatientCareGroupId
		, Ltrim(Rtrim(Pat.IPNOMCOMP)) As PatientName
		, Ltrim(Rtrim(Ing.CODICAMHO)) As BedStay
		, ISNULL(Cg.LiquidationType, 0) As LiquidationType
		, (Case Ing.IESTADOIN 
				When ' ' Then 'Abierto' 
				When 'P' Then 'Parcial' 
				When 'F' Then 'Facturado' 
				When 'C' Then 'Cerrado'
				When 'A' Then 'Anulado'
				WHEN 'B' Then 'Bloqueado'
				Else '' End) As StatusName
		, Ing.GENCONENTITY As EntityId
		, Coalesce(Pha.Code, '') As PatientEntityCode
		, Coalesce(Pha.[Name], '') As PatientEntityName
		, Coalesce(Aha.Code, '') As EntityCode
		, Coalesce(Aha.[Name], '') As EntityName
		, Concat(Cg.Code, ' - ', Cg.[Name]) as CareGroupCodeName
		, Cg.CareGroupType
		, Concat(CgA.Code, ' - ', CgA.[Name]) As AdmissionCareGroupCodeName
		, ISNULL(Cg.CareGroupType, CgA.CareGroupType) As CareGroupTypePatient
		, CgA.TypeLiquidationEmergencyStays As AdmissionTypeLiquidationEmergencyStays
		, Egr.FECALTPAC
		, Concat(Ltrim(Rtrim(Egf.UFUCODIGO)), ' - ', Ltrim(Rtrim(Egf.UFUDESCRI))) As UniFuncEgre
		, Ing.TRATAESPECIA As TratamientoEspecial
		, Rc.Id As RevenueControlId
		, Tpp.Id As ThirdPartyPatientId
		, Concat(Tpp.Nit, ' - ', Tpp.[Name]) As PatientEntity
		, (Select Top 1 SMLV From GeneralLedger.CompanySettings) As SMLV
		, ISNULL(CgA.ApplyRIAS, 0) AdmissionCareGroupApplyRIAS
		, Aha.ThirdPartyId HealthAdministratorThirdPartyId
		, cont.Id ContractId
		, Concat(cont.Code, ' - ', cont.ContractName) As CareGroupContractCodeName
		, IIF(RCD.RevenueControlId IS NOT NULL, 1, 0) As IsMasterAccount
		, RCD.Id as MasterAccountId
		, COALESCE(RCD.DateTRMInsurer,RCD.DateTRMpatient,RCD.CreationDate) DateTRM
	From dbo.ADINGRESO As Ing With(Nolock)
	Inner Join dbo.INPACIENT As Pat With(Nolock) On Ing.IPCODPACI = Pat.IPCODPACI
	LEFT Join dbo.ADNIVELES As Niv With(Nolock) On Pat.NIVCODIGO = Niv.NIVCODIGO
	Inner Join dbo.ADCENATEN As Cen With(Nolock) On Ing.CODCENATE = Cen.CODCENATE
	Inner Join dbo.INUNIFUNC As Ufu With(Nolock) On Ing.UFUCODIGO = Ufu.UFUCODIGO
	Left Join [Contract].HealthAdministrator As Pha With(Nolock) On Pat.GENCONENTITY = Pha.Id
	Left Join [Contract].HealthAdministrator As Aha With(Nolock) On Ing.GENCONENTITY = Aha.Id
	Left Join [Contract].CareGroup As Cg With(Nolock) On Pat.GENCAREGROUP = Cg.Id
	Left Join [Contract].CareGroup As CgA With(Nolock) On Ing.GENCAREGROUP = CgA.Id
	left join [Contract].[Contract] as cont With(Nolock) on cont.Id = CgA.ContractId
	Left Join dbo.HCREGEGRE As Egr With(Nolock) On Egr.NUMINGRES = Ing.NUMINGRES
	Left Join dbo.INUNIFUNC As Egf With(Nolock) On Egf.UFUCODIGO = Egr.UFUCODIGO
	Left Join Billing.RevenueControl Rc With(Nolock) On Rc.AdmissionNumber = Ltrim(Rtrim(Ing.NUMINGRES))
	Left Join master_cte RCD on RCD.RevenueControlId = Rc.Id
	Left Join Common.ThirdParty Tpp With(Nolock) On Tpp.Nit = Ltrim(Rtrim(Pat.IPCODPACI))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida toda la información necesaria para el proceso de liquidación de admisiones (ingresos) de pacientes. Integra datos del episodio de ingreso (urgencias, hospitalización, consulta externa), información demográfica del paciente, nivel de afiliación con sus topes y porcentajes de cuotas moderadoras y copagos, centro de atención, unidad funcional, administradora de salud (EPS/aseguradora) tanto del ingreso como del paciente, grupo de atención, contrato vigente, datos del egreso y estado de facturación (control de ingresos, cuenta maestra y fecha de TRM aplicada). Sirve como fuente principal para los módulos de liquidación, facturación y reportería de cuentas por cobrar, permitiendo conocer en un solo resultado el estado de cada admisión (abierto, parcial, facturado, cerrado, anulado, bloqueado), si ya tiene cuenta maestra generada y qué entidad pagadora es responsable del cobro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewLiquidationGetAdmissionAll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewLiquidationGetAdmissionAll';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de admisiones con datos de paciente, nivel, centro, unidad funcional, administradora, grupo de atención, contrato, control de ingresos y cuenta maestra para soportar procesos de liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La admisión debe existir en ADINGRESO con paciente asociado en INPACIENT (INNER JOIN obligatorio).; Deben existir el centro de atención (ADCENATEN) y la unidad funcional (INUNIFUNC) referenciados por la admisión.; Para considerar cuenta maestra, en RevenueControlDetail debe haber registros con IsMasterAccount IN (1,3).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La unión paciente-entidad usa IPCODPACI como NIT en Common.ThirdParty para resolver el tercero del paciente.; El RevenueControl se enlaza por AdmissionNumber con LTRIM/RTRIM del NUMINGRES de la admisión.; El SMLV reportado es siempre el primer registro de GeneralLedger.CompanySettings (TOP 1 sin ORDER BY).; Los nombres concatenados (centro, unidad funcional, administradora, grupo de atención, contrato) siguen el formato ''Código - Nombre''.; LiquidationType, ApplyRIAS, BenefitPlan, AuthorizationNumber, ResponsiblePhone, PatientEstrato y campos de entidad se exponen con valores por defecto (0 o cadena vacía) cuando son NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión/Ingreso; Paciente; Nivel de atención; Copago/Cuota moderadora; Topes de copago (evento y anual); Régimen de afiliación (contributivo, subsidiado, vinculado); Sisbén; Estrato; Centro de atención; Unidad funcional; Egreso; Administradora de salud (EPS); Grupo de atención (CareGroup); Contrato; Plan de beneficios; Autorización; Cuenta maestra; Control de ingresos (RevenueControl); Factura; TRM aseguradora/paciente; Liquidación; RIAS; SMLV; Tratamiento especial; Liquidación de estancias de urgencias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewLiquidationGetAdmissionAll: Devuelve una fila DISTINCT por admisión enriquecida con datos clínicos, administrativos y financieros para liquidación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IsMasterAccount IN (1,3) en RevenueControlDetail → Se considera registro candidato a cuenta maestra y se agrupa por RevenueControlId/Id en rcd_cte.; si RevenueControlDetail.IsMasterAccount=2 y Status=2 → La fecha de factura asociada se toma como DateTRMInsurer (TRM aseguradora).; si RevenueControlDetail.IsMasterAccount=4 y Status=2 → La fecha de factura asociada se toma como DateTRMpatient (TRM paciente).; si RCD.RevenueControlId IS NOT NULL → Se marca la admisión como cuenta maestra (IsMasterAccount=1). else IsMasterAccount=0.; si Ing.IESTADOIN → Se traduce el código de estado a nombre: '' ''=Abierto, ''P''=Parcial, ''F''=Facturado, ''C''=Cerrado, ''A''=Anulado, ''B''=Bloqueado; cualquier otro valor retorna cadena vacía.; si Cg.CareGroupType IS NULL → Se usa CgA.CareGroupType como CareGroupTypePatient (fallback al grupo de atención de la admisión).; si Selección de DateTRM → Se prioriza DateTRMInsurer; si es NULL se usa DateTRMpatient; si también es NULL se usa CreationDate del detalle.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.Invoice; Billing.RevenueControl; GeneralLedger.CompanySettings; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; Contract.HealthAdministrator; Contract.CareGroup; Contract.Contract; dbo.HCREGEGRE; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLiquidationGetAdmissionAll';
GO
