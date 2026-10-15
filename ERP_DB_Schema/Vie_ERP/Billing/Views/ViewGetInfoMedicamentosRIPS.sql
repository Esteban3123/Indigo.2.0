
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2024-02-07
-- Description:	Vista encargada de obtener la informacion de los medicamentos para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoMedicamentosRIPS]
AS
with DIAGNOSTICO as  (
				SELECT	T.InvoiceId,
						T.AdmissionNumber,
						T.DiagnosticCode,
						T.CODDIAPRI
				FROM
				(
					SELECT 
						i.id InvoiceId,
						I.AdmissionNumber,
						DIAG.CODDIAPRI,
						ROW_NUMBER() OVER (PARTITION BY i.id, I.AdmissionNumber ORDER BY
							CASE Diag.DIAINGEGR 
								WHEN 'E' THEN 1 
								WHEN 'A' THEN 2 
								ELSE 3 
							END ASC,
							Diag.FECDIAGNO DESC) AS IdPartition,
						Diag.CODDIAGNO AS DiagnosticCode
					FROM Billing.Invoice AS I WITH(NOLOCK)
					JOIN dbo.ADINGRESO admision WITH(NOLOCK) ON admision.NUMINGRES = I.AdmissionNumber 
					JOIN dbo.INDIAGNOP Diag WITH(NOLOCK)  ON Diag.NUMINGRES = admision.NUMINGRES
					WHERE I.[Status] = 1 
				) T            
				WHERE T.IdPartition = 1
			),
	Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay 
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
				gd.AuthorizationNumber numAutorizacion,
				mi.IdMipres idMIPRES,
				FORMAT(gd.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(cteMedical.DiagnosticCode, Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado, 
				CASE
					WHEN mt.Code = '03' THEN '03'
					WHEN ATC.UNIRS = 1 THEN '04'
				ELSE mt.Code END as tipoMedicamento,
				CASE
					WHEN mt.Code = '01' OR mt.Code = '02' THEN COALESCE(IP.IUM, IP.CodeCUM) --Para medicamento con uso con registro o uso como vital no disponible
					WHEN mt.Code = '03' THEN DCI.AlternativeCode --Para preparación magistral
					WHEN IP.IUM <> ' ' THEN IP.IUM
				ELSE IP.CodeCUM 
				END as codTecnologiaSalud,
				NULL nomTecnologiaSalud,
				IIF(mt.Code = '03',cast(ATC.ConcentrationQuantity as INT), 0) AS concentracionMedicamento,
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
			LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.id 
			LEFT JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.id 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			LEFT JOIN DIAGNOSTICO DiagPrin WITH (NOLOCK) ON DiagPrin.AdmissionNumber = gd.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 1
			LEFT JOIN DIAGNOSTICO DiagRel WITH (NOLOCK) ON DiagRel.AdmissionNumber = gd.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 0
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) 
					ON gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			LEFT JOIN Cte_MedicalFormula cteMedical WITH(NOLOCK) on cteMedical.InvoiceDetailId= gd.Id
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) codPrestador,
				sodPack.AuthorizationNumber numAutorizacion,
				mi.IdMipres idMIPRES,
				FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm') fechaDispensAdmon ,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,gd.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado, 
				CASE
					WHEN mt.Code = '03' THEN '03'
					WHEN ATC.UNIRS = 1 THEN '04'
				ELSE mt.Code END as tipoMedicamento,
				CASE
					WHEN mt.Code = '01' OR mt.Code = '02' THEN COALESCE(IP.IUM, IP.CodeCUM) --Para medicamento con uso con registro o uso como vital no disponible
					WHEN mt.Code = '03' THEN DCI.AlternativeCode --Para preparación magistral
					WHEN IP.IUM <> ' ' THEN IP.IUM
				ELSE IP.CodeCUM  
				END as codTecnologiaSalud,
				NULL nomTecnologiaSalud,
				IIF(mt.Code = '03',cast(ATC.ConcentrationQuantity as INT), 0) AS concentracionMedicamento,
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
			LEFT JOIN Inventory.DCI as DCI WITH (NOLOCK) ON atc.DCIId = dci.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUW WITH (NOLOCK) ON ATC.WeightMeasureUnit = IMUW.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUV WITH (NOLOCK) ON ATC.VolumeMeasureUnit = IMUV.id 
			LEFT JOIN Cte_InventoryMeasurementUnit IMUA WITH (NOLOCK) ON ATC.AdministrationUnitId = IMUA.id 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			LEFT JOIN DIAGNOSTICO DiagPrin WITH (NOLOCK) ON DiagPrin.AdmissionNumber = gd.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 1
			LEFT JOIN DIAGNOSTICO DiagRel WITH (NOLOCK) ON DiagRel.AdmissionNumber = gd.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 0
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) AS codPrestador,
				gd.AuthorizationNumber AS numAutorizacion,
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
				gd.DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH(NOLOCK)
			JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.id = gd.CUPSEntityId 
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			LEFT JOIN DIAGNOSTICO DiagPrin WITH (NOLOCK) ON DiagPrin.AdmissionNumber = gd.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 1
			LEFT JOIN DIAGNOSTICO DiagRel WITH (NOLOCK) ON DiagRel.AdmissionNumber = gd.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 0
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) 
				ON gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			WHERE cups.RIPSConcept in ('12','13')

			UNION ALL

			SELECT	
				gd.InvoiceNumber,
				LTRIM(RTRIM(gd.CODIPSSEC)) AS codPrestador,
				sodPack.AuthorizationNumber AS numAutorizacion,
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
			LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = gd.NUMINGRES AND DIAG.CODDIAGNO = gd.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			LEFT JOIN DIAGNOSTICO DiagPrin WITH (NOLOCK) ON DiagPrin.AdmissionNumber = gd.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 1
			LEFT JOIN DIAGNOSTICO DiagRel WITH (NOLOCK) ON DiagRel.AdmissionNumber = gd.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId and DiagPrin.CODDIAPRI = 0
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			WHERE cups.RIPSConcept in ('12','13')
GO