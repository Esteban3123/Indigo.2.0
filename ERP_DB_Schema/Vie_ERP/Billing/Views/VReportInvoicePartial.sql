








CREATE VIEW [Billing].[VReportInvoicePartial]
AS
	WITH Cte_MasterAccount AS (
			SELECT	rcd.Id RevenueControlDetailId,
			rc.Id  RevenueControlId,
			iif(rcd.IsMasterAccount=2,0,SUM(sodd.SubTotalPatientSalesPrice)) CopaymentValue,
			iif(rcd.IsMasterAccount=2,0,sum(sodd.DeductibleValue))DeductibleValue,
			rcd.IsMasterAccount,
			case rcd.IsMasterAccount 
			WHEN 2 THEN iif(ld.ApplyDeductible=1,sum(sodd.SubTotalSalesPrice + sodd.DeductibleValue - sodd.GrandTotalDiscount + sodd.InsurerCoveredValue),sum(sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount))
			WHEN 4 THEN	IIF(SUM(sodd.SubTotalSalesPrice) = SUM(sodd.SubTotalPatientSalesPrice),0,sum(  sodd.SubTotalSalesPrice - sodd.SubTotalPatientSalesPrice - sodd.DeductibleValue - sodd.GrandTotalDiscount))	
			ELSE 0 END Coinsurance,
			iif(ISNULL(ld.ApplyDiscount,lda.ApplyDiscount) in (1,3),ISNULL(ld.DiscountValueorPercentage,lda.DiscountValueorPercentage),0) DiscountPercentage
	FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sodd.RevenueControlDetailId = rcd.Id
	LEFT JOIN Billing.ServiceOrderDetailDistribution sodd1 WITH(NOLOCK) ON sodd1.ServiceOrderDetailId = sodd.ServiceOrderDetailId AND sodd1.RevenueControlDetailId = rcd.RevenueControlDetailMasterId
	JOIN Billing.RevenueControl rc WITH(NOLOCK) on rcd.RevenueControlId= rc.Id
	JOIN Billing.LiquidationData lda WITH(NOLOCK) ON lda.AdmissionNumber = rc.AdmissionNumber
	LEFT JOIN Billing.LiquidationDataSeparated ld WITH(NOLOCK) on rc.AdmissionNumber= ld.AdmissionNumber
	where (rcd.IsMasterAccount <> 0 AND ((rcd.RevenueControlDetailMasterId IS NOT NULL AND  sodd1.Id IS NOT NULL) OR
			(rcd.RevenueControlDetailMasterId IS  NULL AND  sodd1.Id IS  NULL))) 
	GROUP by rcd.id, rcd.IsMasterAccount,ld.ApplyDeductible,rc.Id,ld.ApplyDiscount,ld.DiscountValueorPercentage,
			lda.ApplyDiscount,lda.DiscountValueorPercentage,rcd.RevenueControlDetailMasterId			
			)


SELECT	distinct rcd.Id,
		ic.Code + ' - ' + ic.Name AS InvoiceCategory,
		----------------------- CLIENTE -----------------------
		tp.Nit + '-' + tp.DigitVerification as CustomerNit, 
		tp.Nit as NitWithOutDig,
		ISNULL(tp.Name,INP.IPNOMCOMP) as CustomerName, 
		(select top 1 Addresss from Common.Address as Ad where Ad.IdPerson = tp.PersonId) as CustomerAddress,
		(select top 1 Phone from Common.Phone as Ph where Ph.IdPerson = tp.PersonId) as CustomerPhone, 
		cg.Code + ' - ' + cg.Name AS CareGroup, 
		cg.CareGroupType, 
		ha.HealthEntityCode, 
		iif(cg.CareGroupType= 3 and rcd.RevenueControlDetailMasterId is null,'',concat(ha.Code,' - ',ha.Name) )  As DescriptionHealthAdministrator, 
		C.ContractNumber + ' - ' + C.ContractName AS Contract, 
		C.PrintingMode,
		cg.InvoiceDeadlines as Term,
		----------------------  PACIENTE ----------------------
		rc.PatientCode, 
		td.NOMBRE IdentificationName,
		RTRIM(INP.IPNOMCOMP) AS PatientName,
		RTRIM(INP.IPPRINOMB) PatientFirstName,
		RTRIM(INP.IPSEGNOMB) PatientSecondName,
		RTRIM(INP.IPPRIAPEL) PatientFirstLastName,
		RTRIM(INP.IPSEGAPEL) PatientSecondLastName,
		INP.IPTIPOPAC AS PatientType,
		INP.IPTIPOAFI AffiliateType,
		RTRIM(AN.NIVCODIGO) + ' - ' + RTRIM(AN.NIVDESCRI) AS PatientLevel, 
		RTRIM(INP.IPDIRECCI) AS PatientAddress, 
		RTRIM(INP.IPTELEFON) AS PatientTelephoneNumber, 
		INP.IPTELMOVI as PatientPhoneMovil,
		INP.CORELEPAC PatientEmail,
		dbo.Edad(INP.IPFECNACI, AD.IFECHAING) AS PatientAge, 		
		----------------------  ADMISIóN ----------------------
		AD.TIPOINGRE as AdmissionType, 
		rc.AdmissionNumber,
		AD.IFECHAING AS AdmissionDate, 
		rcd.OutputDate AS EgressDate,
		AD.IAUTORIZA AS AuthorizationNumber,
		AD.FECHA ProcessingDate, 
		AD.LINEA ProcessLine, 
		AD.ENTIDAD PacientEntity, 
		AD.REGIMEN PacientRegimen,
		AD.ESTADO AffiliateStatus,
		AD.CONFIRMADOERP ERPConfirm, 
		AD.REPORTAIPS IPSReport,
		RTRIM(CAI.NOMCENATE) as CODCENATE, 
		CASE WHEN UFI.UFUCODIGO IS NULL THEN RTRIM(UF.UFUCODIGO) + ' - ' + RTRIM(UF.UFUDESCRI) ELSE RTRIM(UFI.UFUCODIGO) + ' - ' + RTRIM(UFI.UFUDESCRI) END  as UFUIGRMED,
		CASE WHEN UFE.UFUCODIGO IS NULL THEN RTRIM(UF.UFUCODIGO) + ' - ' + RTRIM(UF.UFUDESCRI) ELSE RTRIM(UFE.UFUCODIGO) + ' - ' + RTRIM(UFE.UFUDESCRI) END  as UFUEGRMED,
		--------------------- DATOS FOLIO ---------------------
		rcd.FolioType as DocumentType,
		'' OutputDiagnosis,
		rcd.TotalFolio AS SubTotalService, 
		rcd.PatientDiscount ThirdPartyDiscountValue, 
		rcd.TotalPatientSalesPrice, 
		rcd.PatientDiscount,
				ISNULL((Select Sum(ipa.[Value]) 
		From [Billing].[InvoicePortfolioAdvance] ipa (nolock)
		JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
		where ipa.InvoiceId = i.Id AND pa.ThirdPartyBeneficiaryId IS NOT NULL), 0) AS TotalThirdPartyPortfolioAdvance,
		(rcd.TotalFolio - rcd.TotalPatientWithDiscount - (ISNULL((SELECT SUM(ipa.Value)
				FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
				JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
				WHERE ipa.InvoiceId = i.Id AND tp.Id = pa.ThirdPartyId AND pa.ThirdPartyBeneficiaryId IS NOT NULL), 0))) ThirdPartySalesValue,    
		U.UserCode, 
		SUBSTRING(PU.FirstName + ' ' + PU.FirstLastName, 1 , 15)  AS UserFullName, 		
		rcd.Status,
		isnull(cm1.Coinsurance,0)	CoinsuranceInsurance,
		isnull(cm1.CopaymentValue,0) CopaymentValueInsurance,
		isnull(cm1.DeductibleValue,0) DeductibleValueInsurance,
		isnull(cm2.Coinsurance,0) PatientCoinsurance,
		isnull(cm2.CopaymentValue,0) PatientCopaymentValue ,
		isnull(cm2.DeductibleValue,0) PatientDeductibleValue,
		rcd. IsMasterAccount,
		case rcd.IsMasterAccount
			when 2 then cm1.DiscountPercentage
			ELSE iif(cm3.ApplyDiscount in (1,3),cm3.DiscountValueorPercentage,0)
		END DataDiscountPercentage,
		RTRIM(CAI.CODIPSSEC) CenterAttentionCode,
		RTRIM(CAI.DIRCENATE) CenterAttentionAddress,
		RTRIM(CAI.NUMTELCEN) CenterAttentionPhone,
		rcd.TotalFolio as GrandTotalSalesPrice, -- este campo incluye dto cuando liquida cuenta madre
		rcd.PatientDiscount as GrandTotalDiscount, -- campo donde se guarda el dto cuando liquida cuenta madre
		cast(1 as NUMERIC(20,2)) TRMValue, -- por defecto 1 debido a que los valores estan en la moneda oficial
		rcd.TotalPatientWithDiscount,
		rcd.CreationDate
FROM Billing.RevenueControl rc WITH (NOLOCK) 
JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rc.Id = rcd.RevenueControlId
JOIN Common.ThirdParty AS tp WITH (NOLOCK)  ON rcd.ThirdPartyId = tp.Id 
JOIN Common.Person as cp  WITH (NOLOCK) on cp.Id = tp.PersonId 
JOIN Contract.CareGroup AS cg  WITH (NOLOCK) ON rcd.CareGroupId = cg.Id 
INNER JOIN dbo.INPACIENT AS INP  WITH (NOLOCK) ON rc.PatientCode = INP.IPCODPACI 
INNER JOIN  dbo.ADTIPOIDENTIFICA td WITH (NOLOCK) ON INP.IPTIPODOC = td.CODIGO
INNER JOIN dbo.ADINGRESO AS AD WITH (NOLOCK)  ON AD.NUMINGRES = rc.AdmissionNumber 
INNER JOIN dbo.ADCENATEN as CAI with (NOLOCK) ON ad.CODCENATE = CAI.CODCENATE
LEFT JOIN Billing.Invoice i ON i.RevenueControlDetailId = rcd.Id
LEFT JOIN Contract.HealthAdministrator AS ha  WITH (NOLOCK) ON  rcd.HealthAdministratorId = ha.Id
LEFT JOIN Common.ThirdParty tp2 WITH (NOLOCK) on ha.ThirdPartyId = tp2.Id 
LEFT JOIN Contract.Contract AS C WITH (NOLOCK) ON cg.ContractId = C.Id 
LEFT JOIN Billing.InvoiceCategories as ic WITH (NOLOCK)  ON rcd.InvoiceCategoryId = ic.Id 
LEFT JOIN dbo.INUNIFUNC as UFI with (NOLOCK) ON ad.UFUINGMED = ufi.UFUCODIGO  -- Obtinene UF de Ingreso del medico de la HC
LEFT JOIN dbo.INUNIFUNC as UFE with (NOLOCK) ON ad.UFUEGRMED = ufE.UFUCODIGO -- Obtinene UF de Egreso del medico de la HC
LEFT JOIN dbo.INUNIFUNC as UF with (NOLOCK) ON ad.UFUCODIGO = uf.UFUCODIGO   -- Se enlaza para cuando las unidades funcionales de ingreso y egreso medico estan vacias - ejemplo laboratorios, imagenes que no tienen HC
LEFT OUTER JOIN dbo.CHREGEGRE as EG  WITH (NOLOCK) ON EG.NUMINGRES = rc.AdmissionNumber 
LEFT JOIN Security.[UserInt] AS U ON U.UserCode = rcd.CreationUser
LEFT JOIN Security.PersonInt AS PU ON U.IdPerson = PU.Id 
LEFT JOIN DBO.ADNIVELES AS AN  WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO
LEFT JOIN Cte_MasterAccount cm1 on rcd.RevenueControlId=cm1.RevenueControlId and cm1.IsMasterAccount=2 --Entidad
LEFT JOIN Cte_MasterAccount cm2 on rcd.RevenueControlId=cm2.RevenueControlId and cm2.IsMasterAccount=4 --Paciente
LEFT JOIN Billing.LiquidationData cm3 WITH(NOLOCK) on cm3.AdmissionNumber = rc.AdmissionNumber
LEFT JOIN Common.Customer cus WITH(NOLOCK) on cus.ThirdPartyId =  tp.Id