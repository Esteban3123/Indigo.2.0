
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2024-02-12
-- Description:	Vista encargada de obetner la informacion de la seccion de otros procedimientos para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoOtrosServiciosRIPS]
AS
		WITH Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay 
									FROM Billing.InvoiceCopay ic WITH(NOLOCK)
									JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.id
									JOIN Billing.Invoice i WITH(NOLOCK) on bb.InvoiceId =i.id
									GROUP by ic.InvoiceId, i.InvoiceNumber),

			Cte_GeneralData as (select	i.InvoiceNumber,
										i.InvoiceDate,
										i.id InvoiceId,
										i.AdmissionNumber,
										i.PatientCode,
										i.DocumentType,
										id.InvoicedQuantity,
										id.GrandTotalSalesPrice,
										id.SubTotalPatientSalesPrice,
										id.id,
										id.ServiceOrderDetailId,
										id.ThirdPartySalesPrice,
										CASE 
											WHEN id.RecoveryFeeType = 1 OR id.SubTotalPatientSalesPrice =0 then '05' --No aplica
											WHEN i.DocumentType = 5 AND cg.MethodFixedAmountCollectionReport = 2 then '05' --No aplica para registros de servicio con cg parametrizado en 2 - Descuento pactado sobre Monto Fijo
											WHEN id.RecoveryFeeType = 2 then '02' -- Cuota moderadora
											WHEN id.RecoveryFeeType = 3 then '01' -- Copago
											WHEN id.RecoveryFeeType = 4 then '03' -- Bono  
											ELSE '05' 
										END RecoveryFeeType,
										sod.ServiceDate,
										sod.IPSServiceId,
										sod.CUPSEntityId,
										sod.ProductId,
										sod.PerformsHealthProfessionalThirdPartyId,
										sod.AuthorizationNumber,
										sod.IsPackage
								from Billing.Invoice i WITH (NOLOCK) 
								JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.InvoiceId = I.id 
								JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = SOD.id
								JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = i.CareGroupId
								WHERE i.Status = 1
									AND id.InvoicedQuantity > 0
									AND sod.SettlementType <> 3
									AND sod.IsDelete = 0
									AND (sod.IncludeServiceOrderDetailId IS NULL or ( sod.IncludeServiceOrderDetailId IS not null and  sod.recoveryratio is not null ))),

		Cte_IsAllPackage AS (SELECT cg.InvoiceId,MIN(CAST(cg.IsPackage AS TINYINT)) IsAllPackage
							from Cte_GeneralData cg
							GROUP by cg.InvoiceId)

		SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				RTRIM(gd.AuthorizationNumber) numAutorizacion,
				hcmipres.CodCUPSMipres idMIPRES,
				FORMAT(isnull(gd.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,--falta
				CASE CUPS.RIPSConcept 
						WHEN '06' THEN '03' 
						WHEN '07' THEN '05' 
						WHEN '08' THEN '03'
						WHEN '09' THEN '01' 
						WHEN '11' THEN '01' 
						WHEN '14' THEN '02'
						WHEN '15' THEN '04'
						ELSE '01' 
				END tipoOS,
				dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) codTecnologiaSalud,
				SUBSTRING(cups.Description,1,60) nomTecnologiaSalud,
				gd.InvoicedQuantity cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				CAST(ROUND((gd.GrandTotalSalesPrice) / gd.InvoicedQuantity, 0) AS INT) vrUnitOS,
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END	 valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				cast(gd.DocumentType  AS TINYINT) as DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK) 
			JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.id = gd.CUPSEntityId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp WITH (NOLOCK) on gd.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id 
			LEFT JOIN HCJUSTECA hcmipres WITH (NOLOCK) on admission.NUMINGRES = hcmipres.NUMINGRES and hcmipres.CODSERIPS = CUPS.Code  AND (hcmipres.CodCUPSMipres IS NOT NULL)
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice>0 and gd.RecoveryFeeType<>'05'
			WHERE cups.RIPSConcept in ('06','08','09','11','14')

		UNION ALL

		SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				RTRIM(sodPack.AuthorizationNumber) numAutorizacion,
				hcmipres.CodCUPSMipres idMIPRES,
				FORMAT(isnull(sodPack.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,--falta
				CASE CUPS.RIPSConcept 
						WHEN '06' THEN '03' 
						WHEN '07' THEN '05' 
						WHEN '08' THEN '03'
						WHEN '09' THEN '01' 
						WHEN '11' THEN '01' 
						WHEN '14' THEN '02'
						WHEN '15' THEN '04'
						ELSE '01' 
				END tipoOS,
				dbo.fn_RemoveNonAlphaNumDash(RTRIM(cups.RIPSCode)) codTecnologiaSalud,
				SUBSTRING(cups.Description,1,60) nomTecnologiaSalud,
				sodPack.InvoicedQuantity cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				0 vrUnitOS,
				0 vrServicio,
				'05' conceptoRecaudo,
				0 valorPagoModerador,
				NULL numFEVPagoModerador,
				null consecutivo,
				cast(gd.DocumentType  AS TINYINT) as DocumentType,
				NULL InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK)
			JOIN Cte_IsAllPackage pac WITH(NOLOCK) on gd.InvoiceId = pac.InvoiceId and pac.IsAllPackage=1
			JOIN Billing.ServiceOrderDetail sodPack WITH(NOLOCK) on sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
			JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.id = sodPack.CUPSEntityId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp WITH (NOLOCK) on sodPack.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id 
			LEFT JOIN HCJUSTECA hcmipres WITH (NOLOCK) on admission.NUMINGRES = hcmipres.NUMINGRES and hcmipres.CODSERIPS = CUPS.Code  AND (hcmipres.CodCUPSMipres IS NOT NULL)
			WHERE cups.RIPSConcept in ('06','08','09','11','14')

	----- SEGMENTO NUEVO PARA CORREGIR CANTIDADES EN HONORARIOS
		UNION ALL
			SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				RTRIM(gd.AuthorizationNumber) numAutorizacion,
				hcmipres.CodCUPSMipres idMIPRES,
				FORMAT(isnull(gd.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,--falta
				'05' AS tipoOS,
				dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) codTecnologiaSalud,
				SUBSTRING(cups.Description,1,60) nomTecnologiaSalud,
				1 AS cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrUnitOS,
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END	 valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				cast(gd.DocumentType  AS TINYINT) as DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK) 
			JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.id = gd.CUPSEntityId 
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI 
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp WITH (NOLOCK) on gd.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id 
			LEFT JOIN HCJUSTECA hcmipres WITH (NOLOCK) on admission.NUMINGRES = hcmipres.NUMINGRES and hcmipres.CODSERIPS = CUPS.Code  AND (hcmipres.CodCUPSMipres IS NOT NULL)
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice>0 and gd.RecoveryFeeType<>'05'
			WHERE cups.RIPSConcept in ('07')

----- HASTA AQUI

		UNION ALL 

			SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				RTRIM(isnull(gd.AuthorizationNumber,iif(LEN(admission.IAUTORIZA) > 0 ,admission.IAUTORIZA, null))) numAutorizacion,
				null idMIPRES,
				FORMAT(isnull(gd.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,--falta
				'01' tipoOS, --revisar
				IP.Code codTecnologiaSalud,
				SUBSTRING(ip.Name,1,60) nomTecnologiaSalud,
				gd.InvoicedQuantity cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoldentificacion,
					CAST(ROUND((gd.GrandTotalSalesPrice) / gd.InvoicedQuantity, 0) AS INT) vrUnitOS,
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END	 valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				cast(gd.DocumentType  AS TINYINT) as DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK)
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON gd.ProductId = ip.id 
			JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id and PT.Class=3
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp on gd.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice>0 and gd.RecoveryFeeType<>'05'
	
	
	UNION ALL

	SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				RTRIM(isnull(sodPack.AuthorizationNumber,iif(LEN(admission.IAUTORIZA) > 0 ,admission.IAUTORIZA, null))) numAutorizacion,
				null idMIPRES,
				FORMAT(isnull(sodPack.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,--falta
				'01' tipoOS, --revisar
				IP.Code codTecnologiaSalud,
				SUBSTRING(ip.Name,1,60) nomTecnologiaSalud,
				sodPack.InvoicedQuantity cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoldentificacion,
				0 vrUnitOS,
				0 vrServicio,
				'05' conceptoRecaudo,
				0 valorPagoModerador,
				NULL numFEVPagoModerador,
				null consecutivo,
				cast(gd.DocumentType  AS TINYINT) as DocumentType,
				NULL InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK)
			JOIN Cte_IsAllPackage pac WITH(NOLOCK) on gd.InvoiceId = pac.InvoiceId and pac.IsAllPackage=1
			JOIN Billing.ServiceOrderDetail sodPack WITH(NOLOCK) on sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sodPack.ProductId = ip.id 
			JOIN Inventory.ProductType as PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id and PT.Class=3
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES 
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp on sodPack.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id