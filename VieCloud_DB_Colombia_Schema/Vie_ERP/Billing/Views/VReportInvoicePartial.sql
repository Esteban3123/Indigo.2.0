

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de facturación parcial por folio (cuenta parcial o cuenta madre) que consolida la información financiera, clínica y administrativa necesaria para imprimir o exportar una factura o documento equivalente en el ciclo de facturación. Integra el control de ingresos (RevenueControl y RevenueControlDetail) con la distribución financiera de servicios facturados (ServiceOrderDetailDistribution) para calcular copago, deducible, coaseguro y descuento aplicados tanto al asegurador como al paciente según los parámetros de liquidación pactados (LiquidationData y LiquidationDataSeparated). Expone datos del cliente o entidad pagadora (NIT, nombre, dirección, contrato, grupo de atención, administradora de salud), datos del paciente (cédula, nombre completo, tipo de afiliado, nivel, dirección, teléfono, correo, edad), datos de la admisión (número de ingreso, fecha de ingreso, fecha de egreso, tipo de ingreso, autorización, centro de atención, unidad funcional de ingreso y egreso) y datos del folio (tipo de documento, subtotal de servicios, valor a cargo del tercero, valor a cargo del paciente, anticipos de cartera, estado del folio, usuario liquidador y porcentaje de descuento aplicado). Sirve como fuente principal para los reportes e impresión de facturas parciales, prefacturas y cuentas de cobro a aseguradoras, EPS, empresas y pacientes particulares.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoicePartial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoicePartial';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la información necesaria para imprimir/reportar facturas parciales (cuentas madre/hija), unificando datos del cliente, paciente, admisión, centro de atención, folio, descuentos, copagos, coaseguros, deducibles y anticipos por tercero.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe Billing.RevenueControl con su RevenueControlDetail asociado.; El paciente referenciado (rc.PatientCode) existe en dbo.INPACIENT y su tipo de documento en dbo.ADTIPOIDENTIFICA.; La admisión (rc.AdmissionNumber) existe en dbo.ADINGRESO y su centro de atención en dbo.ADCENATEN.; El tercero responsable (rcd.ThirdPartyId) existe en Common.ThirdParty con persona asociada en Common.Person.; El grupo de cuidado (rcd.CareGroupId) existe en Contract.CareGroup.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TRMValue siempre se devuelve como 1 porque los valores se manejan en moneda oficial.; GrandTotalSalesPrice siempre equivale a rcd.TotalFolio (incluye descuento cuando se liquida la cuenta madre).; GrandTotalDiscount siempre equivale a rcd.PatientDiscount (descuento de cuenta madre).; Solo se incluyen detalles con rcd.IsMasterAccount <> 0 dentro del CTE de cuenta madre.; CopaymentValue y DeductibleValue de la entidad (IsMasterAccount=2) son siempre 0; los valores no nulos pertenecen al paciente (IsMasterAccount=4).; TotalThirdPartyPortfolioAdvance solo suma anticipos cuya PortfolioAdvance.ThirdPartyBeneficiaryId NO es nulo.; ThirdPartySalesValue resta del total del folio el valor a cargo del paciente y los anticipos del tercero responsable (mismo ThirdPartyId).; OutputDiagnosis siempre se devuelve como cadena vacía (no se calcula).; UserFullName se trunca a 15 caracteres.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportInvoicePartial: Devuelve una fila por RevenueControlDetail (DISTINCT) con datos de cliente, paciente, admisión, folio y desglose financiero para emitir facturas parciales.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rcd.IsMasterAccount = 2 (cuenta entidad) → CopaymentValue y DeductibleValue se fuerzan a 0; Coinsurance se calcula según ld.ApplyDeductible: si=1 suma SubTotalSalesPrice + DeductibleValue - GrandTotalDiscount + InsurerCoveredValue; en otro caso SubTotalSalesPrice - GrandTotalDiscount. else Para IsMasterAccount=4 (paciente) se calcula Coinsurance solo si SubTotalSalesPrice <> SubTotalPatientSalesPrice; otros valores=0.; si rcd.IsMasterAccount = 4 y SUM(SubTotalSalesPrice) = SUM(SubTotalPatientSalesPrice) → Coinsurance se establece en 0. else Coinsurance = SubTotalSalesPrice - SubTotalPatientSalesPrice - DeductibleValue - GrandTotalDiscount.; si rcd.IsMasterAccount NO está en (2,4) → Coinsurance = 0 en el CTE.; si ISNULL(ld.ApplyDiscount, lda.ApplyDiscount) IN (1,3) → DiscountPercentage toma el valor de DiscountValueorPercentage (priorizando LiquidationDataSeparated sobre LiquidationData). else DiscountPercentage = 0.; si cg.CareGroupType = 3 AND rcd.RevenueControlDetailMasterId IS NULL → DescriptionHealthAdministrator se muestra vacío (''''). else Se muestra concat(ha.Code,'' - '',ha.Name).; si rcd.RevenueControlDetailMasterId IS NOT NULL → debe existir sodd1.Id (registro coincidente en distribución de la cuenta madre); si IS NULL → sodd1.Id debe ser NULL → Solo entonces el detalle entra en el cálculo del CTE Cte_MasterAccount.; si UFI.UFUCODIGO IS NULL (no hay UF de ingreso médico) → UFUIGRMED toma UF.UFUCODIGO+UFUDESCRI por defecto. else Toma UFI.UFUCODIGO+UFUDESCRI.; si UFE.UFUCODIGO IS NULL (no hay UF de egreso médico) → UFUEGRMED toma UF por defecto. else Toma UFE.; si rcd.IsMasterAccount = 2 al elegir DataDiscountPercentage → Usa cm1.DiscountPercentage (descuento calculado para la entidad). else Usa LiquidationData (cm3) si ApplyDiscount IN (1,3); de lo contrario 0.; si tp.Name IS NULL → CustomerName se reemplaza por INP.IPNOMCOMP (nombre del paciente). else Usa tp.Name.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.RevenueControl; Billing.LiquidationData; Billing.LiquidationDataSeparated; Billing.Invoice; Billing.InvoicePortfolioAdvance; Billing.InvoiceCategories; Portfolio.PortfolioAdvance; Common.ThirdParty; Common.Person; Common.Address; Common.Phone; Common.Customer; Contract.CareGroup; Contract.HealthAdministrator; Contract.Contract; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGEGRE; dbo.ADNIVELES; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartial';
GO
