
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2024-02-07
-- Description:	Vista encargada de obtener la informacion de los medicamentos para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoMedicamentosRIPS]
AS
	WITH Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay
							FROM Billing.InvoiceCopay ic WITH(NOLOCK)
							JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.id
							JOIN Billing.Invoice i WITH(NOLOCK) on bb.InvoiceId =i.id
							GROUP by ic.InvoiceId, i.InvoiceNumber),

	Cte_InventoryMeasurementUnit as (	select id,StandardCode
										from Inventory.InventoryMeasurementUnit WITH(NOLOCK)),

	Cte_HCHOJAMED as (	SELECT hc.NUMINGRES, hc.CODPRODUC,MAX(hc.FECAPLMED) MaxFECAPLMED, MIN(hc.FECAPLMED) MinFECAPLMED, hc.CODPROSAL
						from HCHOJAMED hc WITH(NOLOCK)
						GROUP by hc.NUMINGRES, hc.CODPRODUC, hc.CODPROSAL),

	Cte_HCJUNOPOM AS (	SELECT hcp.NUMINGRES,hcp.CODPROSAL,hcp.CODPRODUC,max(hcp.CODMINSALUD) CODMINSALUD
						FROM HCJUNOPOM hcp WITH(NOLOCK)
						GROUP by hcp.NUMINGRES,hcp.CODPROSAL,hcp.CODPRODUC
						),
	Cte_GeneralData  as (	select	i.id InvoiceId, 
									i.InvoiceNumber,
									i.PatientCode,
									i.OutputDiagnosis,
									i.HealthAdministratorId,
									i.RevenueControlDetailId,
									id.id,
									id.InvoicedQuantity,
									id.ServiceOrderDetailId,
									id.GrandTotalSalesPrice,
									id.SubTotalPatientSalesPrice,
									id.ThirdPartySalesPrice,
									RTRIM(sod.AuthorizationNumber) AS AuthorizationNumber ,
									sod.ServiceDate,
									sod.PerformsHealthProfessionalCode,
									sod.ProductId,
									sod.CUPSEntityId,
									sod.IPSServiceId,
									admission.NUMINGRES,
									admission.CODDIAEGR,
									trim(ca.CODIPSSEC) AS CODIPSSEC,
									RTRIM(tp.Nit) AS Nit,
									COALESCE(tip.SIGLA,'CC') SIGLA,
									i.[Status] InvoiceStatus,
									CASE 
										WHEN id.RecoveryFeeType = 1 OR id.SubTotalPatientSalesPrice =0 then '05' --No aplica
										WHEN i.DocumentType = 5 AND cg.MethodFixedAmountCollectionReport = 2 then '05' --No aplica para registros de servicio con cg parametrizado en 2 - Descuento pactado sobre Monto Fijo
										WHEN id.RecoveryFeeType = 2 then '02' -- Cuota moderadora
										WHEN id.RecoveryFeeType = 3 then '01' -- Copago
										WHEN id.RecoveryFeeType = 4 then '03' -- Bono  
										ELSE '05' 
									END RecoveryFeeType,
									i.DocumentType,
									sod.IsPackage
							FROM Billing.Invoice i WITH (NOLOCK) 
							JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.InvoiceId = I.id 
							JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.id AND sod.IsDelete =0
							JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.id
							JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON so.AdmissionNumber = admission.NUMINGRES 
							JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON admission.CODCENATE = ca.CODCENATE 
							join Common.ThirdParty tp WITH (NOLOCK) on sod.PerformsHealthProfessionalThirdPartyId = tp.id
							join Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
							JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = i.CareGroupId
							LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
							where id.InvoicedQuantity > 0 and i.[Status] = 1 AND sod.SettlementType <> 3 AND sod.IncludeServiceOrderDetailId IS NULL ),

		Cte_IsAllPackage AS (SELECT cg.InvoiceId,MIN(CAST(cg.IsPackage AS TINYINT)) IsAllPackage
							from Cte_GeneralData cg
							GROUP by cg.InvoiceId),

		Cte_MedicalFormula AS(	SELECT	mfd.DiagnosticCode,
										mfid.InvoiceDetailId,
										mfd.TreatmentDays
								FROM [Inventory].[MedicalFormulaInvoiceDetail] mfid WITH(NOLOCK)
								JOIN Inventory.MedicalFormulaDetail mfd WITH(NOLOCK) on mfid.MedicalFormulaDetailId = mfd.Id
								JOIN Cte_GeneralData cteg WITH(NOLOCK) on cteg.Id = mfid.InvoiceDetailId
								JOIN(	SELECT mfdd.MedicalFormulaDetailId, mfdd.ProductId 
										FROM Inventory.MedicalFormulaDetailProducts mfdd WITH(NOLOCK)
										GROUP by mfdd.MedicalFormulaDetailId, mfdd.ProductId
										)mffdq on mffdq.MedicalFormulaDetailId= mfd.Id and mffdq.ProductId=cteg.ProductId
								LEFT JOIN INDIAGNOS idg WITH(NOLOCK) on idg.CODDIAGNO = mfd.DiagnosticCode
								),
		Cte_MipresInvoice as (	SELECT *
								 FROM (
									SELECT
										cteg.Id AS InvoiceDetailId,
										cteg.ServiceOrderDetailId,
										mc.Code,
										mc.IdMipres,
										mc.CreationDate,
										ROW_NUMBER() OVER (
											PARTITION BY cteg.ServiceOrderDetailId
											ORDER BY mc.CreationDate DESC, mc.Id DESC
										) AS rn
									FROM Billing.MipresCode mc WITH (NOLOCK)
									JOIN Cte_GeneralData cteg WITH (NOLOCK)
									  ON mc.ServiceOrderDetailId = cteg.ServiceOrderDetailId
								) x
								WHERE x.rn = 1 )

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) codPrestador,
				mi.IdMipres idMIPRES,
				FORMAT(gd.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(cteMedical.DiagnosticCode, Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado,
				CASE
					WHEN mt.Code = '03' THEN '03'
					WHEN ATC.UNIRS = 1 THEN '04'
				ELSE mt.Code END as tipoMedicamento,
				CASE
					WHEN mt.Code IN ('01', '02', '03') THEN COALESCE(NULLIF(LTRIM(RTRIM(IP.IUM)),''), IP.CodeCUM) --Para medicamentos con uso con registro, vital no disponible o preparación magistral
					WHEN IP.IUM <> ' ' THEN IP.IUM
				ELSE IP.CodeCUM 
				END as codTecnologiaSalud,
				NULL nomTecnologiaSalud,
				CASE
					WHEN mt.Code <> '03' THEN 0
					WHEN ATC.FormulationType = 1 THEN CAST(ATC.Weight AS INT)
					WHEN ATC.FormulationType = 2 THEN CAST(ATC.Volume AS INT)
					WHEN ATC.FormulationType = 3 THEN CAST(ATC.Weight AS INT)
					WHEN ATC.FormulationType = 4 THEN CAST(ATC.ConcentrationQuantity AS INT)
					ELSE 0
				END AS concentracionMedicamento,
				IIF(mt.Code = '03',
				CASE ATC.FormulationType
					WHEN 1 THEN CAST(IMUW.StandardCode AS INT)
					WHEN 2 THEN CAST(IMUV.StandardCode AS INT)
					WHEN 3 THEN CAST(IMUW.StandardCode AS INT)
					WHEN 4 THEN CAST(IMUA.StandardCode AS INT)
				ELSE 0 END, 0) as unidadMedida,	
				CASE mt.Code
					WHEN '03' then pfg.Code
					ELSE NULL
				END	 formaFarmaceutica, 
				cast(upr.Code as INT) unidadMinDispensa,
				gd.InvoicedQuantity cantidadMedicamento, 
				COALESCE( cteMedical.TreatmentDays, DATEDIFF(day, HOJAMEDICAMENTOS.MinFECAPLMED, HOJAMEDICAMENTOS.MaxFECAPLMED + 1),1) diasTratamiento,
				gd.SIGLA TipoDocumentoIdentificacion, 
				gd.Nit numDocumentoIdentificacion,
				CAST(ROUND((gd.GrandTotalSalesPrice) / gd.InvoicedQuantity, 0) AS INT) vrUnitMedicamento, 
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT) 
				END valorPagoModerador, 
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(0 AS INT) vrDispensacion,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				gd.DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH(NOLOCK)
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON gd.ProductId = ip.id
			JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id
			LEFT JOIN Inventory.MedicationType mt ON ip.MedicationTypeId = mt.id
			LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.id 
			LEFT JOIN Inventory.UPRUnits upr on atc.UPRUnitsId = upr.id
			LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg on pf.PharmaceuticalFormGroupingId = pfg.id
			LEFT JOIN Cte_HCHOJAMED HOJAMEDICAMENTOS WITH (NOLOCK) on gd.NUMINGRES = HOJAMEDICAMENTOS.NUMINGRES 
																		and atc.Code = HOJAMEDICAMENTOS.CODPRODUC  
																		and gd.PerformsHealthProfessionalCode = HOJAMEDICAMENTOS.CODPROSAL
			LEFT JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.id 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND Diag.IPCODPACI = gd.PatientCode AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) 
					ON gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			LEFT JOIN Cte_MedicalFormula cteMedical WITH(NOLOCK) on cteMedical.InvoiceDetailId= gd.Id
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			) DiagPrin
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(cteMedical.DiagnosticCode, Diag.CODDIAGNO, DiagPrin.DiagnosticCode, gd.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			) DiagRel
			WHERE NOT EXISTS (
				SELECT 1
				FROM Inventory.ProductType PTA WITH (NOLOCK)
				WHERE PTA.Id = IP.ProductTypeId
					AND PTA.Class = 2
					AND PTA.Name = 'ALIMENTO'
			)

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) codPrestador,
				mi.IdMipres idMIPRES,
				FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado,
				CASE
					WHEN mt.Code = '03' THEN '03'
					WHEN ATC.UNIRS = 1 THEN '04'
				ELSE mt.Code END as tipoMedicamento,
				CASE
					WHEN mt.Code IN ('01', '02', '03') THEN COALESCE(NULLIF(LTRIM(RTRIM(IP.IUM)),''), IP.CodeCUM) --Para medicamentos con uso con registro, vital no disponible o preparación magistral
					WHEN IP.IUM <> ' ' THEN IP.IUM
				ELSE IP.CodeCUM  
				END as codTecnologiaSalud,
				NULL nomTecnologiaSalud,
				CASE
					WHEN mt.Code <> '03' THEN 0
					WHEN ATC.FormulationType = 1 THEN CAST(ATC.Weight AS INT)
					WHEN ATC.FormulationType = 2 THEN CAST(ATC.Volume AS INT)
					WHEN ATC.FormulationType = 3 THEN CAST(ATC.Weight AS INT)
					WHEN ATC.FormulationType = 4 THEN CAST(ATC.ConcentrationQuantity AS INT)
					ELSE 0
				END AS concentracionMedicamento,
				IIF(mt.Code = '03',
				CASE ATC.FormulationType
					WHEN 1 THEN CAST(IMUW.StandardCode AS INT)
					WHEN 2 THEN CAST(IMUV.StandardCode AS INT)
					WHEN 3 THEN CAST(IMUW.StandardCode AS INT)
					WHEN 4 THEN CAST(IMUA.StandardCode AS INT)
				ELSE 0 END, 0) as unidadMedida,	
				CASE mt.Code
					WHEN '03' then pfg.Code
					ELSE NULL
				END	 formaFarmaceutica, 
				cast(upr.Code as INT) unidadMinDispensa,
				sodPack.InvoicedQuantity cantidadMedicamento, 
				COALESCE(DATEDIFF(day, HOJAMEDICAMENTOS.MinFECAPLMED, HOJAMEDICAMENTOS.MaxFECAPLMED + 1),1) diasTratamiento,
				COALESCE(tip.SIGLA,'CC') TipoDocumentoIdentificacion, 
				tp.Nit numDocumentoIdentificacion,
				0 vrUnitMedicamento, 
				0 vrServicio,
				'05' conceptoRecaudo,
				0 valorPagoModerador, 
				NULL numFEVPagoModerador,
				null consecutivo,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(0 AS INT) vrDispensacion,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				gd.DocumentType,
				NULL InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH(NOLOCK)
			JOIN Cte_IsAllPackage pac on gd.InvoiceId = pac.InvoiceId and pac.IsAllPackage =1
			JOIN Billing.ServiceOrderDetail sodPack WITH(NOLOCK) on  sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
			join Common.ThirdParty tp WITH (NOLOCK) on sodPack.PerformsHealthProfessionalThirdPartyId = tp.id
			join Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON sodPack.ProductId = ip.id
			JOIN Inventory.ATC as ATC WITH (NOLOCK) ON IP.ATCId = ATC.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			LEFT JOIN Inventory.MedicationType mt ON ip.MedicationTypeId = mt.id
			LEFT JOIN Inventory.PharmaceuticalForm AS PF WITH (NOLOCK) ON ATC.PharmaceuticalFormId = PF.id 
			LEFT JOIN Inventory.UPRUnits upr on atc.UPRUnitsId = upr.id
			LEFT JOIN Inventory.PharmaceuticalFormGrouping pfg on pf.PharmaceuticalFormGroupingId = pfg.id
			left JOIN Cte_HCHOJAMED HOJAMEDICAMENTOS WITH (NOLOCK) on gd.NUMINGRES = HOJAMEDICAMENTOS.NUMINGRES and atc.Code = HOJAMEDICAMENTOS.CODPRODUC  and sodPack.PerformsHealthProfessionalCode = HOJAMEDICAMENTOS.CODPROSAL
			LEFT JOIN Cte_InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.id 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND Diag.IPCODPACI = gd.PatientCode AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			) DiagPrin
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(Diag.CODDIAGNO, DiagPrin.DiagnosticCode, gd.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			) DiagRel
			WHERE NOT EXISTS (
				SELECT 1
				FROM Inventory.ProductType PTA WITH (NOLOCK)
				WHERE PTA.Id = IP.ProductTypeId
					AND PTA.Class = 2
					AND PTA.Name = 'ALIMENTO'
			)

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) AS codPrestador,
				mi.IdMipres idMIPRES,
				FORMAT(gd.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado,
				CASE cups.RIPSConcept 
					WHEN '12' THEN '01' 
					WHEN '13' THEN '01' 
					ELSE '02' 
				END tipoMedicamento,
				cups.RIPSCode codTecnologiaSalud,
				SUBSTRING(cups.Description,1,30) nomTecnologiaSalud,
				CAST(0 as INT) concentracionMedicamento,
				0 unidadMedida,
				NULL formaFarmaceutica, 
				1 unidadMinDispensa,
				gd.InvoicedQuantity cantidadMedicamento, 
				1 diasTratamiento,
				gd.SIGLA TipoDocumentoIdentificacion, 
				gd.Nit numDocumentoIdentificacion,
				CAST(ROUND((gd.GrandTotalSalesPrice) / gd.InvoicedQuantity, 0) AS INT) vrUnitMedicamento, 
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType AS conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT) 
				END valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(0 AS INT) vrDispensacion,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				gd.DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH(NOLOCK)
			JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.id = gd.CUPSEntityId 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND Diag.IPCODPACI = gd.PatientCode AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			) DiagPrin
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(Diag.CODDIAGNO, DiagPrin.DiagnosticCode, gd.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			) DiagRel
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) 
				ON gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			WHERE cups.RIPSConcept in ('12','13')
				AND NOT EXISTS (
					SELECT 1
					FROM Inventory.ProductType PTA WITH (NOLOCK)
					JOIN Inventory.InventoryProduct IPA WITH (NOLOCK) ON IPA.ProductTypeId = PTA.Id
					WHERE IPA.Id = gd.ProductId
						AND PTA.Class = 2
						AND PTA.Name = 'ALIMENTO'
				)

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) AS codPrestador,
				mi.IdMipres idMIPRES,
				FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado,
				CASE cups.RIPSConcept 
					WHEN '12' THEN '01' 
					WHEN '13' THEN '01' 
					ELSE '02' 
				END tipoMedicamento,
				cups.RIPSCode codTecnologiaSalud,
				SUBSTRING(cups.Description,1,30) nomTecnologiaSalud,
				CAST(0 as INT) concentracionMedicamento,
				0 unidadMedida,
				NULL formaFarmaceutica, 
				1 unidadMinDispensa,
				sodPack.InvoicedQuantity cantidadMedicamento, 
				1 diasTratamiento,
				COALESCE(tip.SIGLA,'CC') TipoDocumentoIdentificacion, 
				tp.Nit numDocumentoIdentificacion,
				0 vrUnitMedicamento, 
				0 vrServicio,
				'05' AS conceptoRecaudo,
				0 valorPagoModerador,
				NULL numFEVPagoModerador,
				null consecutivo,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(0 AS INT) vrDispensacion,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				gd.DocumentType,
				NULL InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH(NOLOCK)
			JOIN Cte_IsAllPackage pac on gd.InvoiceId = pac.InvoiceId and pac.IsAllPackage =1
			JOIN Billing.ServiceOrderDetail sodPack WITH(NOLOCK) ON sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
			JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.id = sodPack.CUPSEntityId 
			JOIN Common.ThirdParty tp WITH (NOLOCK) on sodPack.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND Diag.IPCODPACI = gd.PatientCode AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			) DiagPrin
			OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = gd.NUMINGRES
					AND D.IPCODPACI = gd.PatientCode
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(Diag.CODDIAGNO, DiagPrin.DiagnosticCode, gd.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			) DiagRel
			WHERE cups.RIPSConcept in ('12','13')
				AND NOT EXISTS (
					SELECT 1
					FROM Inventory.ProductType PTA WITH (NOLOCK)
					JOIN Inventory.InventoryProduct IPA WITH (NOLOCK) ON IPA.ProductTypeId = PTA.Id
					WHERE IPA.Id = sodPack.ProductId
						AND PTA.Class = 2
						AND PTA.Name = 'ALIMENTO'
				)
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de medicamentos dispensados para la generación de RIPS electrónicos. Integra datos de facturas activas (Billing.Invoice), detalles de órdenes de servicio con productos farmacéuticos, hojas de medicamentos aplicados (HCHOJAMED), fórmulas médicas, diagnósticos CIE-10 del ingreso (INDIAGNOP/ADINGRESO), copagos, códigos MIPRES y unidades de medida de inventario. Permite reportar a los entes de control (Ministerio de Salud / ADRES) el consumo de medicamentos por paciente, factura, ingreso y profesional de salud, con los campos requeridos por el estándar RIPS: código prestador, número de autorización, identificador MIPRES, tipo de cuota de recuperación, diagnóstico principal y fechas de aplicación del medicamento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoMedicamentosRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoMedicamentosRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye, para los RIPS electrónicos, el detalle de medicamentos e insumos asimilables a medicamentos por factura, resolviendo diagnósticos, codificación CUM/IUM, MIPRES, copagos, paquetes y unidades RIPS.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas a reportar deben tener Billing.Invoice.Status = 1.; Cada detalle facturado debe tener InvoicedQuantity > 0, ServiceOrderDetail.IsDelete = 0, SettlementType <> 3 y IncludeServiceOrderDetailId nulo.; El producto facturado debe existir en Inventory.InventoryProduct y tener clasificación ATC asociada.; El profesional que ejecuta debe estar asociado a un ThirdParty/Person válido para obtener tipo y número de documento.; Para la rama de paquetes se requiere que TODOS los detalles de la factura sean paquete (IsAllPackage=1) y que existan ServiceOrderDetail con PackageServiceOrderDetailId apuntando al ítem cabecera.; Para la rama de insumos como medicamentos, el CUPSEntity asociado debe tener RIPSConcept en (''12'',''13'').', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con Status=1 (activas) y detalles con InvoicedQuantity>0.; Se excluyen detalles de orden de servicio con SettlementType=3 o IncludeServiceOrderDetailId no nulo.; El diagnóstico principal nunca queda vacío: si no se resuelve por fórmula médica, INDIAGNOP, diagnóstico de egreso ni de salida, se asigna ''Z000'' por defecto.; Cuando RecoveryFeeType resuelve a ''05'' (no aplica), valorPagoModerador siempre es 0 y no se vincula numFEVPagoModerador.; Para preparaciones magistrales (mt.Code=''03'') codTecnologiaSalud usa primero InventoryProduct.IUM y, si está vacío, InventoryProduct.CodeCUM; además se reportan concentración y unidad de medida; para los demás tipos concentración=0 y unidadMedida=0.; Para medicamentos con UNIRS=1 el tipoMedicamento se fuerza a ''04'' salvo que sea preparación magistral.; vrUnitMedicamento se calcula como GrandTotalSalesPrice/InvoicedQuantity redondeado a entero; vrServicio = ThirdPartySalesPrice + SubTotalPatientSalesPrice.; En las ramas de paquete (IsAllPackage=1) los valores monetarios (vrUnitMedicamento, vrServicio, valorPagoModerador) se reportan en 0 y conceptoRecaudo se fuerza a ''05''.; diasTratamiento por defecto es 1 cuando no hay fórmula médica ni registros en HCHOJAMED que permitan calcular el rango de fechas de aplicación.; Para CUPS con RIPSConcept ''12'' o ''13'' se mapea como medicamento RIPS con tipoMedicamento=''01'' y se toma el código y descripción (truncada a 30 caracteres) de CUPSEntity.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; medicamentos; MIPRES; diagnóstico principal y relacionado (CIE-10); autorización; copago / cuota moderadora / bono (concepto de recaudo); preparación magistral; medicamento vital no disponible; código CUM / IUM; clasificación ATC; forma farmacéutica; unidad mínima de dispensación; días de tratamiento; paquete de servicios (IsPackage); CUPS RIPSConcept (12/13 insumos como medicamentos); tipo de documento de identificación del prestador; factura electrónica de venta (FEV)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.InvoiceCopay; Billing.BasicBilling; Billing.MipresCode; dbo.ADINGRESO; dbo.INDIAGNOP; dbo.ADCENATEN; dbo.ADTIPOIDENTIFICA; Common.ThirdParty; Common.Person; Contract.CareGroup; Contract.CUPSEntity; Inventory.InventoryProduct; Inventory.ATC; Inventory.MedicationType; Inventory.PharmaceuticalForm; Inventory.PharmaceuticalFormGrouping; Inventory.UPRUnits; Inventory.InventoryMeasurementUnit; Inventory.MedicalFormulaInvoiceDetail; Inventory.MedicalFormulaDetail; Inventory.MedicalFormulaDetailProducts; Inventory.ProductType; dbo.HCHOJAMED; dbo.HCJUNOPOM', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoMedicamentosRIPS';
GO
