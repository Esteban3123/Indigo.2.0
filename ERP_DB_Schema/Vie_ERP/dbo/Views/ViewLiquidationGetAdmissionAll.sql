

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