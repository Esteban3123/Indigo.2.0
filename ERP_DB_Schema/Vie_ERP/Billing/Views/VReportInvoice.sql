







CREATE VIEW [Billing].[VReportInvoice]
AS
WITH CTE_INGRESO AS (
	SELECT 
        AD.NUMINGRES,
        td.SIGLA,
        td.NOMBRE AS IdentificationName,

        -- Datos del paciente
        RTRIM(INP.IPNOMCOMP) AS IPNOMCOMP,
        RTRIM(INP.IPPRINOMB) AS IPPRINOMB,
        RTRIM(INP.IPSEGNOMB) AS IPSEGNOMB,
        RTRIM(INP.IPPRIAPEL) AS IPPRIAPEL,
        RTRIM(INP.IPSEGAPEL) AS IPSEGAPEL,
        INP.IPTIPOAFI,
        INP.IPDIRECCI,
        INP.IPTELEFON,
        INP.IPTELMOVI,
        INP.CORELEPAC,

        -- Ingreso
        AD.TIPOINGRE,
        AD.IAUTORIZA,
        AD.FECHA,
        AD.LINEA,
        AD.ENTIDAD,
        AD.REGIMEN,
        AD.ESTADO,
        AD.CONFIRMADOERP,
        AD.REPORTAIPS,

        -- Centro de atención
        CAI.CODCENATE,
        CAI.NOMCENATE,
        CAI.CODIPSSEC,
        CAI.DIRCENATE,
        CAI.NUMTELCEN,

        -- Unidades funcionales
        CASE 
            WHEN AD.UFUINGMED IS NULL 
                THEN CONCAT(RTRIM(UF.UFUCODIGO), ' - ', RTRIM(UF.UFUDESCRI))
                ELSE CONCAT(RTRIM(UFI.UFUCODIGO), ' - ', RTRIM(UFI.UFUDESCRI))
        END AS UFUIGRMED,
        CASE 
            WHEN AD.UFUEGRMED IS NULL 
                THEN CONCAT(RTRIM(UF.UFUCODIGO), ' - ', RTRIM(UF.UFUDESCRI))
                ELSE CONCAT(RTRIM(UFE.UFUCODIGO), ' - ', RTRIM(UFE.UFUDESCRI))
        END AS UFUEGRMED,
        fur.NUMSOA,
        dbo.Edad(INP.IPFECNACI, AD.IFECHAING) AS PatientAge,
        CASE 
            WHEN INP.NIVCODIGO IS NULL 
                THEN '' 
                ELSE CONCAT(RTRIM(AN.NIVCODIGO), ' - ', RTRIM(AN.NIVDESCRI))
        END AS PatientLevel

    FROM dbo.ADINGRESO AD WITH (NOLOCK)
	JOIN dbo.INPACIENT INP WITH (NOLOCK) ON AD.IPCODPACI = INP.IPCODPACI 
    JOIN dbo.ADTIPOIDENTIFICA td WITH (NOLOCK) ON INP.IPTIPODOC = td.CODIGO
    JOIN dbo.ADCENATEN CAI WITH (NOLOCK) ON AD.CODCENATE = CAI.CODCENATE
    LEFT JOIN dbo.INUNIFUNC UF WITH (NOLOCK) ON AD.UFUCODIGO = UF.UFUCODIGO
    LEFT JOIN dbo.INUNIFUNC UFI WITH (NOLOCK) ON AD.UFUINGMED = UFI.UFUCODIGO
    LEFT JOIN dbo.INUNIFUNC UFE WITH (NOLOCK) ON AD.UFUEGRMED = UFE.UFUCODIGO
    LEFT JOIN dbo.CHREGEGRE EG WITH (NOLOCK) ON EG.NUMINGRES = AD.NUMINGRES
    LEFT JOIN (
        SELECT 
            MAX(NUMSOA) AS NUMSOA, 
            NUMINGRES, 
            NUMDOCVIC
        FROM dbo.ADFURIPSU 
        WHERE NUMSOA IS NOT NULL
        GROUP BY NUMINGRES, NUMDOCVIC
    ) fur 
        ON fur.NUMINGRES = AD.NUMINGRES 
        AND fur.NUMDOCVIC = AD.IPCODPACI
    LEFT JOIN dbo.ADNIVELES AN WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO
), 

Cte_MasterAccount AS (
	SELECT
		rcd.Id AS RevenueControlDetailId,
		rc.Id AS RevenueControlId,
		CASE 
			WHEN rcd.IsMasterAccount = 2 THEN 0
			ELSE SUM(sodd.SubTotalPatientSalesPrice)
		END AS CopaymentValue,
		CASE 
			WHEN rcd.IsMasterAccount = 2 THEN 0
			ELSE SUM(sodd.DeductibleValue)
		END AS DeductibleValue,
		rcd.IsMasterAccount,
		CASE rcd.IsMasterAccount 
			WHEN 2 THEN
				CASE 
					WHEN ld.ApplyDeductible = 1 THEN
						SUM(sodd.SubTotalSalesPrice + sodd.DeductibleValue - sodd.GrandTotalDiscount + sodd.InsurerCoveredValue)
					ELSE
						SUM(sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount)
				END
			WHEN 4 THEN
				CASE 
					WHEN SUM(sodd.SubTotalSalesPrice) = SUM(sodd.SubTotalPatientSalesPrice) THEN 0
					ELSE SUM(sodd.SubTotalSalesPrice - sodd.SubTotalPatientSalesPrice - sodd.DeductibleValue - sodd.GrandTotalDiscount)
				END
			ELSE 0
		END AS Coinsurance,
		CASE 
			WHEN ISNULL(ld.ApplyDiscount, lda.ApplyDiscount) IN (1, 3) THEN
				ISNULL(ld.DiscountValueorPercentage, lda.DiscountValueorPercentage)
			ELSE 0
		END AS DiscountPercentage
	FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON sodd.RevenueControlDetailId = rcd.Id
	LEFT JOIN Billing.ServiceOrderDetailDistribution sodd1 WITH (NOLOCK) ON sodd1.ServiceOrderDetailId = sodd.ServiceOrderDetailId
		AND sodd1.RevenueControlDetailId = rcd.RevenueControlDetailMasterId
	JOIN Billing.RevenueControl rc WITH (NOLOCK) ON rcd.RevenueControlId = rc.Id
	JOIN Billing.LiquidationData lda WITH (NOLOCK) ON lda.AdmissionNumber = rc.AdmissionNumber
	LEFT JOIN Billing.LiquidationDataSeparated ld WITH (NOLOCK) ON rc.AdmissionNumber = ld.AdmissionNumber
	WHERE 
		rcd.IsMasterAccount <> 0
		AND (
			(rcd.RevenueControlDetailMasterId IS NOT NULL AND sodd1.Id IS NOT NULL)
			OR
			(rcd.RevenueControlDetailMasterId IS NULL AND sodd1.Id IS NULL)
		)
	GROUP BY 
		rcd.Id,
		rc.Id,
		rcd.IsMasterAccount,
		rcd.RevenueControlDetailMasterId,
		ld.ApplyDeductible,
		ld.ApplyDiscount,
		ld.DiscountValueorPercentage,
		lda.ApplyDiscount,
		lda.DiscountValueorPercentage		
),

Cte_Advanced AS (
	SELECT
		vpm.InvoiceId,
		SUM(vpm.[Value]) AS [Value],
		STRING_AGG(
			CASE vpm.PaymentMethodTypes
				WHEN 1 THEN 'Efectivo'
				WHEN 2 THEN 'Cheque'
				WHEN 3 THEN 'Tarjeta Crédito'
				WHEN 4 THEN 'Consignación bancaria'
			END,
			' - '
		) AS PaymentMethodTypes,
		vpm.CurrencyId
	FROM Billing.ViewPaymentMethods vpm WITH (NOLOCK)
	WHERE vpm.AccountReceivableType NOT IN (4, 6)
	GROUP BY vpm.InvoiceId, vpm.CurrencyId
),

Cte_CompanySettings AS (
	SELECT TOP 1 *
	FROM GeneralLedger.CompanySettings
),

Cte_PromissoryNote AS (	
	SELECT 
		ar.InvoiceId,
		ar.Id AS AccountReceivableId
	FROM Portfolio.AccountReceivable AS ar WITH (NOLOCK)
	WHERE ar.AccountReceivableType = 4
		AND ar.Balance > 0

	UNION ALL

	SELECT 
		ic.InvoiceId,
		ar.Id AS AccountReceivableId
	FROM Billing.InvoiceCopay ic WITH (NOLOCK)
	JOIN Billing.BasicBilling bb WITH (NOLOCK) ON ic.BasicBillingId = bb.Id
	JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON ar.InvoiceId = bb.InvoiceId
	WHERE ar.Balance > 0
		AND bb.Status = 2
)

SELECT	distinct 
		i.Id,
		i.OperatingUnitId OperatingUnitId,
		i.InvoiceDate,
		i.InvoiceExpirationDate,
		i.InvoiceNumber,
		i.ElectronicInvoiceNumber AS ElectronicInvoiceNumber,
		i.CutType,
		ISNULL(i.Observation, rcd.Observation) AS Observation,

		--------------------- CLIENTE ---------------------
		CONCAT(tp.Nit, '-', tp.DigitVerification) AS Nit,
		tp.Nit AS NitWithOutDig,
		ISNULL(tp.Name, ad.IPNOMCOMP) AS [Name],
		(	SELECT TOP 1 Addresss 
			FROM Common.[Address] Ad WITH (NOLOCK) 
			WHERE Ad.IdPerson = tp.PersonId
		) AS ThirdPartyAddress,
		(	SELECT TOP 1 Phone 
			FROM Common.Phone Ph WITH (NOLOCK) 
			WHERE Ph.IdPerson = tp.PersonId
		) AS ThirdPartyPhone,
		CONCAT(cg.Code, ' - ', cg.[Name]) AS CareGroup,
		cg.CareGroupType,
		ha.HealthEntityCode,
		IIF(cg.CareGroupType=3 AND rcd.RevenueControlDetailMasterId IS NULL,'',CONCAT(ha.Code, ' - ', ha.[Name]))  AS DescriptionHealthAdministrator,
		CONCAT(c.ContractNumber, ' - ', c.ContractName) AS [Contract],
		c.PrintingMode,
		c.CreationDate AS ContractDate,
		cd.InitialDate AS ContractInitialDate,
		cd.EndDate AS ContractEndDate,

		-------------------- CONTRATO CAPITA --------------------
		i.CapitationInitialDate,
		i.CapitationEndDate,
		i.CapitationlPatientsAmount,
		i.CapitationPatientValue,
		i.TotalValue,
		i.TotalInvoice,

		--------------------  PACIENTE --------------------
		ad.SIGLA AS IdentificationTypeCode,
		ad.IdentificationName,
		i.PatientCode,
		ad.IPNOMCOMP PatientName,
		ad.IPPRINOMB PatientFirstName,
		ad.IPSEGNOMB PatientSecondName,
		ad.IPPRIAPEL PatientFirstLastName,
		ad.IPSEGAPEL PatientSecondLastName,
		CASE i.DocumentType
			WHEN 3 THEN 'Particular'
			ELSE
				CASE 
					WHEN cg.EntityType = 9 THEN 
						CONCAT
						(
							'Especiales o de Excepción',
							CASE i.PatientAffiliatedType											
								WHEN 2 THEN ' beneficiario'
								ELSE ' cotizante'
							END
						)
					WHEN cg.EntityType = 5 THEN 'Tomador/Amparado ARL'
					WHEN cg.EntityType = 13 AND cg.ConceptToBill = 13 THEN 'Tomador/Amparado SOAT'
					ELSE
						CASE i.PatientType
							WHEN 1 THEN
								CONCAT
								(
									'Contributivo',
									CASE i.PatientAffiliatedType									
										WHEN 2 THEN ' beneficiario'
										WHEN 3 THEN ' adicional'
										ELSE ' cotizante'
									END
								)
							WHEN 2 THEN 'Subsidiado'
							WHEN 4 THEN 'Particular'
							ELSE 'Sin régimen'
						END 
			END
		END as PatientType, 
		ad.IPTIPOAFI AS AffiliateType,
		AD.PatientLevel,
		RTRIM(ad.IPDIRECCI) AS PatientAddress,
		RTRIM(ad.IPTELEFON) AS PatientTelephoneNumber,
		ad.IPTELMOVI AS PatientPhoneMovil,
		ad.CORELEPAC AS PatientEmail,
		AD.PatientAge,		

		--------------------  ADMISIóN --------------------
		AD.TIPOINGRE AS TypeAdmission,
		i.AdmissionNumber,
		i.InitialDate AS AdmissionDate,
		i.OutputDate AS EgressDate,
		AD.IAUTORIZA AS AuthorizationNumber,
		AD.FECHA AS ProcessingDate,
		AD.LINEA AS ProcessLine,
		AD.ENTIDAD AS PacientEntity,
		AD.REGIMEN AS PacientRegimen,
		AD.ESTADO AS AffiliateStatus,
		AD.CONFIRMADOERP AS ERPConfirm,
		AD.REPORTAIPS AS IPSReport,
		ad.CODCENATE,
		RTRIM(ad.NOMCENATE) AS CenterAttentionName,
		RTRIM(ad.CODIPSSEC) AS CenterAttentionCode,
		RTRIM(ad.DIRCENATE) AS CenterAttentionAddress,
		RTRIM(ad.NUMTELCEN) AS CenterAttentionPhone,
		AD.UFUIGRMED,
		AD.UFUEGRMED,

		------------------ DATOS FACTURA ------------------
		IIF(i.InvoiceCategoryId IS NULL, '', CONCAT(ic.Code, ' - ', ic.[Name])) AS InvoiceCategory,
		i.DocumentType AS DocumentType,
		IIF(ISNULL(i.OutputDiagnosis, '') = '', '', CONCAT(RTRIM(DIAG.CODDIAGNO) ,' - ', RTRIM(DIAG.NOMDIAGNO))) AS OutputDiagnosis,

		-------------------------CAMBIO DE MONEDA--------------------
		(i.TotalInvoice + i.ThirdPartyDiscountValue) AS SubTotalService,
		i.ThirdPartyDiscountValue,
		i.TotalPatientSalesPrice,
		i.PatientDiscount,
		IIF(cg.CareGroupType = 3 OR sb.LiquidateMasterAccount = 1,(i.ThirdPartySalesValue - ISNULL(i.TaxDevolutionValue,0)), i.TotalPatientWithDiscount)
			- ISNULL(
				(SELECT SUM(ipa.[Value]) 
				FROM [Billing].[InvoicePortfolioAdvance] ipa WITH(NOLOCK)
				JOIN Common.ThirdParty t ON t.Nit = i.PatientCode
				JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId AND pa.ThirdPartyId = t.Id
				WHERE ipa.InvoiceId = i.Id), 0
			) AS TotalPatientAccountReceivable,
		ISNULL(
			(SELECT SUM(K.Value) 
			FROM (SELECT ipa.Value
			FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
			JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
			WHERE ipa.InvoiceId = i.Id AND tp.Id = pa.ThirdPartyId AND pa.ThirdPartyBeneficiaryId IS NOT NULL
				
			UNION ALL
				
			SELECT ipa.Value				
			FROM Billing.InvoicePortfolioAdvance ipa
			JOIN Portfolio.PortfolioAdvance pa ON pa.Id = ipa.PortfolioAdvanceId
			JOIN Treasury.CashReceiptDetails crd ON crd.Id = pa.CashReceiptDetailId
			JOIN Treasury.CashReceiptConcepts crc ON crc.Id = crd.IdCashReceiptConcept
			WHERE ipa.InvoiceId = i.Id AND tp.Id = pa.ThirdPartyId AND 
			crc.IsFixedAmountInvoiceAdvance = 1 AND crd.ThirdPartyBeneficiaryId IS NULL AND i.DocumentType = 4 ) K),0) AS TotalThirdPartyPortfolioAdvance,
		(i.ThirdPartySalesValue - 
				ISNULL((SELECT SUM(ipa.Value)
				FROM Billing.InvoicePortfolioAdvance ipa WITH(NOLOCK)
				JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) ON pa.Id = ipa.PortfolioAdvanceId
				WHERE ipa.InvoiceId = i.Id AND tp.Id = pa.ThirdPartyId AND pa.ThirdPartyBeneficiaryId IS NOT NULL),0)) ThirdPartySalesValue,
		U.UserCode,
		SUBSTRING(PU.FirstName + ' ' + PU.FirstLastName, 1 , 15) AS FullNameUser,
		i.[Status],
		CASE i.DocumentType
			WHEN 1 THEN 'Factura EAPB con Contrato'
			WHEN 2 THEN 'Factura EAPB sin Contrato'
			WHEN 3 THEN 'Factura Particular'
			WHEN 4 THEN 'Factura Capitada'
			WHEN 5 THEN 'Control de Capitacion'
			ELSE ''
		END AS DocumentTypeName,
		AD.NUMSOA AS PolicyNumber,
		cd. PermanentObservationOfTheInvoice,
		cpn.AccountReceivableId As IdPay, --Pagare Asociado

		----------------  DATOS RESOLUCION ----------------
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.ResolutionNumber) AS ResolutionNumber,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.ResolutionDate) AS ResolutionDate,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.InvoicePrefix) AS ResolutionInvoicePrefix,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.InitialInvoice) AS ResolutionInitialInvoice,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.FinalInvoice) AS ResolutionFinalInvoice,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.InitialDate) AS ResolutionInitialDate,
		IIF(i.BillingAuthorizationId IS NULL, NULL, ba.FinalDate) AS ResolutionFinalDate,
		IIF(i.BillingAuthorizationId IS NULL, NULL, DATEDIFF(MONTH, ba.InitialDate, ba.FinalDate)) AS ResolutionExpiration,

		------------ DATOS FACTURA ELECTRONICA ------------
		IIF(i.InvoiceExpirationDate IS NULL, 0, DATEDIFF(DAY, i.InvoiceDate, i.InvoiceExpirationDate)) AS Term,
		IIF
		(
			(i.InvoiceValue - ISNULL(i.TaxDevolutionValue,0)) = ValueAdv,
			'Contado',
			'Crédito'
		) as PaymentMethod,
		ISNULL(vpm.PaymentMethodTypes,'Acuerdo mutuo') PaymentMeans,
		-----------------------------------------------
		IIF(i.CareGroupId IS NULL, NULL, cg.LiquidationType) AS LiquidationType,
		CASE i.DocumentType
			WHEN 3 THEN 'Particular'
			ELSE
				CASE
					WHEN cg.ConceptToBill IN (1, 3, 4, 17) THEN 'Plan de beneficios en salud financiado con UPC'
					WHEN cg.ConceptToBill IN (10, 12, 16) THEN 'Cobertura entidad territorial, recursos de oferta'
					WHEN cg.ConceptToBill IN (14, 15) THEN 'Cobertura ADRES'
					WHEN cg.ConceptToBill = 2 THEN 'Plan complementario en Salud'
					WHEN cg.ConceptToBill = 5 THEN 'Plan medicina prepagada'
					WHEN cg.ConceptToBill = 6 THEN 'Otras Pólizas en salud'
					WHEN cg.ConceptToBill = 7 THEN 'Particular'
					WHEN cg.ConceptToBill = 8 THEN 'Cobertura Salud Pública'
					WHEN cg.ConceptToBill = 9 THEN 'Cobertura Régimen Especial o Excepción'
					WHEN cg.ConceptToBill = 11 THEN 'Cobertura ARL'
					WHEN cg.ConceptToBill = 13 THEN 'Cobertura Póliza SOAT'
					WHEN cg.ConceptToBill = 18 THEN 'Presupuesto máximo'
					WHEN cg.ConceptToBill = 19 THEN 'Urgencias población migrante'
					WHEN cg.ConceptToBill = 20 THEN 'Prima EPS / EOC, no asegurados SOAT'
					WHEN cg.ConceptToBill = 21 THEN 'Cobertura Fondo Nacional de Salud de las Personas Privadas de la Libertad'
					ELSE
						CASE i.PatientType
							WHEN 1 THEN 'Cobertura Salud Pública'
							WHEN 2 THEN 'Cobertura Salud Pública'
							WHEN 4 THEN 'Particular'
							ELSE 'Cobertura Salud Pública'
						END
				END
		END AS CoverageDescription,
		-----------------------------------------------
		i.CUFE,
		i.QR,
		CASE ed.Status
			WHEN 0 THEN 'Erronea'
			WHEN 1 THEN 'Registrada'
			WHEN 2 THEN 'Enviada'
			WHEN 3 THEN 'Valida'
			WHEN 4 THEN 'Invalida'
			WHEN 88 THEN 'Envio en Proceso'
			WHEN 99 THEN 'Validación en Proceso'
			ELSE 'Procesando...'
		END StatusDIAN,
		ed.ValidationDate,
		cu.Id CurrencyId,
		cu.[Name] as CurrencyName,

		----------CUENTA MADRE-------------------------------------
		i.ValueTax,
		[Common].[CurrencyConverterByModule](ISNULL(cm1.Coinsurance, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS CoinsuranceInsurance,
		[Common].[CurrencyConverterByModule](ISNULL(cm1.CopaymentValue, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS CopaymentValueInsurance,
		[Common].[CurrencyConverterByModule](ISNULL(cm1.DeductibleValue, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS DeductibleValueInsurance,
		[Common].[CurrencyConverterByModule](ISNULL(cm2.Coinsurance, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS PatientCoinsurance,
		[Common].[CurrencyConverterByModule](ISNULL(cm2.CopaymentValue, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS PatientCopaymentValue,
		[Common].[CurrencyConverterByModule](ISNULL(cm2.DeductibleValue, 0), cs.OfficialCurrencyId, ISNULL(i.CurrencyId, cs.OfficialCurrencyId), NULL, 'Invoice', i.InvoiceDate) AS PatientDeductibleValue,
		rcd.IsMasterAccount,
		i.TaxDevolutionValue,
		CASE rcd.IsMasterAccount
			WHEN 2 THEN cm1.DiscountPercentage
			ELSE IIF(cm3.ApplyDiscount in (1,3),cm3.DiscountValueorPercentage,0)
		END DataDiscountPercentage,
		i.InvoiceNumber AS InternalInvoiceNumber,
		ep.ConditionSalesCodeName,
		i.IsElectronicTicket
FROM Billing.Invoice i WITH (NOLOCK) 
JOIN Common.ThirdParty tp WITH (NOLOCK)  ON i.ThirdPartyId = tp.Id
JOIN Cte_CompanySettings cs WITH (NOLOCK) ON 1=1
JOIN Billing.SettingsBilling sb WITH (NOLOCK) ON sb.IdOperatingUnit = i.OperatingUnitId
JOIN Common.Currency cu on cu.Id = COALESCE(i.CurrencyId,cs.OfficialCurrencyId)
LEFT JOIN Contract.CareGroup cg  WITH (NOLOCK) ON i.CareGroupId = cg.Id 
LEFT JOIN Contract.HealthAdministrator ha  WITH (NOLOCK) ON i.HealthAdministratorId = ha.Id 
LEFT JOIN Contract.[Contract] c WITH (NOLOCK) ON ISNULL(i.ContractId, cg.ContractId) = c.Id 
LEFT JOIN Contract.ContractDetail cd WITH (NOLOCK) ON c.Id = cd.ContractId AND cd.ValidRecord = 1
LEFT JOIN Billing.InvoiceCategories ic WITH (NOLOCK)  ON i.InvoiceCategoryId = ic.Id 
LEFT JOIN CTE_INGRESO AD on AD.NUMINGRES = i.AdmissionNumber
LEFT JOIN dbo.INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = i.OutputDiagnosis
LEFT JOIN Security.[User] U ON U.UserCode = i.InvoicedUser 
LEFT JOIN Security.Person PU ON U.IdPerson = PU.Id
LEFT JOIN Billing.BillingAuthorization ba WITH (NOLOCK) ON i.BillingAuthorizationId = ba.ID
LEFT JOIN Billing.RevenueControlDetail rcd WITH (nolock) ON rcd.Id = i.RevenueControlDetailId
LEFT JOIN Cte_MasterAccount cm1 ON rcd.RevenueControlId=cm1.RevenueControlId AND cm1.IsMasterAccount=2 AND rcd.RevenueControlDetailMasterId IS NOT NULL  --Entidad
LEFT JOIN Cte_MasterAccount cm2 ON rcd.RevenueControlId=cm2.RevenueControlId AND cm2.IsMasterAccount=4 --Paciente
LEFT JOIN Billing.LiquidationData cm3 WITH (NOLOCK) ON cm3.AdmissionNumber = i.AdmissionNumber
OUTER APPLY
(
	SELECT TOP 1 EntityId, ed.EntityName, ValidationDate, [Status]
	FROM Billing.ElectronicDocument ed WITH (NOLOCK)
	WHERE ed.EntityName = 'Invoice' AND ed.EntityId = i.Id
	ORDER BY ValidationDate DESC
) ed
OUTER APPLY 
(
	SELECT sum(subvpm.ValueAdv) ValueAdv, STRING_AGG(subvpm.PaymentMethodTypes,'-') PaymentMethodTypes
	FROM
	(	
		SELECT Common.CurrencyConverterByModule
		(
			ISNULL(cte_a.[Value], 0),
			ISNULL(cte_a.CurrencyId,cte_cs.OfficialCurrencyId),
			ISNULL(i.CurrencyId,cte_cs.OfficialCurrencyId),
			NULL,'Invoice',CAST(Common.GETDATE() AS DATE)
		) ValueAdv,
		cte_a.PaymentMethodTypes
		FROM Cte_Advanced cte_a 
		Join Cte_CompanySettings cte_cs ON 1=1
		WHERE cte_a.InvoiceId = i.Id
	) subvpm
) vpm
OUTER APPLY ( SELECT TOP 1 cpn.AccountReceivableId
			  FROM Cte_PromissoryNote cpn
			  WHERE cpn.InvoiceId = i.Id
			) cpn
OUTER APPLY
(
	SELECT TOP 1 CONCAT(cs.Code,' - ', cs.Name) ConditionSalesCodeName	
	FROM Billing.ElectronicsProperties ep
	LEFT JOIN Billing.ConditionSales cs ON cs.Id = ep.ConditionSalesId
	WHERE ep.EntityName = 'Invoice' AND ep.EntityId = i.Id
) ep
GO