
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-12-10
-- Description:	Vista encargada de obetner la informacion de la seccion consultas para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoConsultasRIPS]
AS

with DIAGNOSTICO_PRINCIPAL as  (
				SELECT	Diag.NUMINGRES,
						ROW_NUMBER() OVER (PARTITION BY Diag.NUMINGRES order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END ASC, Diag.FECDIAGNO DESC) IdPartition,
						Diag.CODDIAGNO,
						Diag.TIPDIAGNO 
				FROM dbo.INDIAGNOP Diag 
				WHERE DIAG.CODDIAPRI = 1
			),

	 DIAGNOSTICO_SEGUNDARIO as  (
		SELECT	Id,
					AdmissionNumber,
					DiagnosticCodeRel1 = MAX(CASE WHEN IdPartition = 1 THEN CODDIAGNO ELSE NULL END),--egreso
					DiagnosticCodeRel2 = MAX(CASE WHEN IdPartition = 2 THEN CODDIAGNO ELSE NULL END),--ambos
					DiagnosticCodeRel3 = MAX(CASE WHEN IdPartition = 3 THEN CODDIAGNO ELSE NULL END)--ingreso
		FROM
		(
			SELECT	I.Id, 
				I.AdmissionNumber,
				ROW_NUMBER() OVER (PARTITION BY i.Id, I.AdmissionNumber order by CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END) IdPartition,
				Diag.CODDIAGNO
			FROM Billing.Invoice AS I 
			INNER JOIN dbo.ADINGRESO admision ON admision.NUMINGRES = I.AdmissionNumber 
			INNER JOIN dbo.INDIAGNOP Diag ON Diag.NUMINGRES = admision.NUMINGRES
			WHERE I.[Status] = 1 AND DIAG.CODDIAPRI = 0
		) T
		GROUP BY T.Id, AdmissionNumber
	),
	Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay 
							FROM Billing.InvoiceCopay ic WITH(NOLOCK)
							JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.Id
							JOIN Billing.Invoice i WITH(NOLOCK) on bb.InvoiceId =i.Id
							GROUP by ic.InvoiceId, i.InvoiceNumber),
	Cte_GeneralData as (	SELECT 
							i.Id InvoiceId, 
							i.InvoiceNumber,
							i.OutputDiagnosis,
							i.HealthAdministratorId,
							i.RevenueControlDetailId,
							i.AdmissionNumber,
							id.Id,
							id.InvoicedQuantity,
							id.ServiceOrderDetailId,
							id.GrandTotalSalesPrice,
							id.SubTotalPatientSalesPrice,
							id.ThirdPartySalesPrice,
							RTRIM(sod.AuthorizationNumber) AS AuthorizationNumber ,
							sod.ServiceDate,
							sod.ServiceOrderId,
							sod.PerformsHealthProfessionalCode,
							sod.PerformsHealthProfessionalThirdPartyId,
							sod.ProductId,
							sod.CUPSEntityId,
							sod.IPSServiceId,
							sod.IsPackage,
							i.[Status] InvoiceStatus,
							CASE
								WHEN id.RecoveryFeeType = 1 OR id.SubTotalPatientSalesPrice =0 then '05' --No aplica
								WHEN i.DocumentType = 5 AND cg.MethodFixedAmountCollectionReport = 2 then '05' --No aplica para registros de servicio con cg parametrizado en 2 - Descuento pactado sobre Monto Fijo
								WHEN id.RecoveryFeeType = 2 then '02' -- Cuota moderadora
								WHEN id.RecoveryFeeType = 3 then '01' -- Copago
								WHEN id.RecoveryFeeType = 4 then '03' -- Bono  
							ELSE '05' 
							END RecoveryFeeType,
							i.DocumentType
							FROM Billing.Invoice i WITH (NOLOCK)
							JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.InvoiceId = i.Id 
							JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId
							JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = i.CareGroupId
							WHERE i.Status = 1 
									AND sod.IsDelete = 0
									AND sod.SettlementType <> 3
									AND (sod.IncludeServiceOrderDetailId IS NULL or ( sod.IncludeServiceOrderDetailId IS not null and  sod.recoveryratio is not null ))),

			Cte_IsAllPackage AS (SELECT cg.InvoiceId,MIN(CAST(cg.IsPackage AS TINYINT)) IsAllPackage
					from Cte_GeneralData cg
					GROUP by cg.InvoiceId)

	select 
			gd.InvoiceNumber,
			LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
			FORMAT(gd.ServiceDate, 'yyyy-MM-dd HH:mm')  fechaInicioAtencion, 
			RTRIM(gd.AuthorizationNumber) numAutorizacion,
			dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codConsulta,
			am.CodeRIPS modalidadGrupoServicioTecSal,
			rsg.Code grupoServicios,
			CAST(rs.Code AS INT) codServicio, 
			CASE WHEN hp.Code IN ('14','26','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42') THEN '44' ELSE hp.Code END AS   finalidadTecnologiaSalud, 
			cfa.RIPSCode causaMotivoAtencion,
			COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,
			DiagRel.DiagnosticCodeRel1 AS codDiagnosticoRelacionado1,
			DiagRel.DiagnosticCodeRel2 AS codDiagnosticoRelacionado2,
			DiagRel.DiagnosticCodeRel3 AS codDiagnosticoRelacionado3,
			CASE COALESCE(DiagPrin.TIPDIAGNO,'C')  
				WHEN 'I' then '01' --I: Impresion Diagnostica 
				WHEN 'C' then '02' --C: Confirmado Nuevo  
				WHEN 'r' then '03' --R: Confirmado Repetido
			END tipoDiagnosticoPrincipal,
			CASE 
				WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC'
				ELSE tip.SIGLA 
			END tipoDocumentoIdentificacion,
			RTRIM(tp.Nit) numDocumentoIdentificacion,
			CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice), 0) AS INT) vrServicio,
			gd.RecoveryFeeType conceptoRecaudo,			
			CASE 
				WHEN gd.RecoveryFeeType ='05' THEN 0
				ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT) 
			END valorPagoModerador,
			cic.InvoiceNumberCopay numFEVPagoModerador,
			null consecutivo,
			gd.DocumentType,
			gd.Id InvoiceDetailId,
			gd.InvoiceId
	FROM Cte_GeneralData gd WITH (NOLOCK)
	JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = gd.CUPSEntityId	
	JOIN Contract.RIPSServices rs WITH (NOLOCK) on cups.RIPSServiceId = rs.Id
	JOIN Contract.RIPSServiceGroups rsg WITH (NOLOCK) on rs.ServiceGroup = rsg.Id
	JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = gd.AdmissionNumber
	JOIN Common.ThirdParty tp WITH (NOLOCK) on gd.PerformsHealthProfessionalThirdPartyId = tp.Id
	JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.Id
	JOIN Admissions.AdmissionModalities am WITH (NOLOCK) on admision.IdAdmissionModalities = am.Id
	JOIN INUNIFUNC uf WITH (NOLOCK) on admision.UFUCODIGO = uf.UFUCODIGO
	JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
	LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.ID
	LEFT JOIN Causesofattention cfa WITH (NOLOCK) on admision.ICAUSAING = cfa.Code
	LEFT JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON admision.IdHealthPurposes = hp.Id--
	LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK)
			on gd.InvoiceId = cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
	LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin WITH (NOLOCK) ON DiagPrin.IdPartition = 1 AND DiagPrin.NUMINGRES = admision.NUMINGRES 		
	LEFT JOIN DIAGNOSTICO_SEGUNDARIO DiagRel WITH (NOLOCK) on DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.Id = gd.InvoiceId
	WHERE cups.RIPSConcept = '01'

	UNION ALL

	select 
			gd.InvoiceNumber,
			LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
			FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm')  fechaInicioAtencion, 
			RTRIM(sodPack.AuthorizationNumber) numAutorizacion,
			dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codConsulta,
			am.CodeRIPS modalidadGrupoServicioTecSal,
			rsg.Code grupoServicios,
			CAST(rs.Code AS INT) codServicio, 
			CASE WHEN hp.Code IN ('14','26','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42') THEN '44' ELSE hp.Code END AS   finalidadTecnologiaSalud, 
			cfa.RIPSCode causaMotivoAtencion,
			COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,
			DiagRel.DiagnosticCodeRel1 AS codDiagnosticoRelacionado1,
			DiagRel.DiagnosticCodeRel2 AS codDiagnosticoRelacionado2,
			DiagRel.DiagnosticCodeRel3 AS codDiagnosticoRelacionado3,
			CASE COALESCE(DiagPrin.TIPDIAGNO,'C')  
				WHEN 'I' then '01' --I: Impresion Diagnostica 
				WHEN 'C' then '02' --C: Confirmado Nuevo  
				WHEN 'r' then '03' --R: Confirmado Repetido
			END tipoDiagnosticoPrincipal,
			CASE 
				WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC'
				ELSE tip.SIGLA 
			END tipoDocumentoIdentificacion, 
			RTRIM(tp.Nit) numDocumentoIdentificacion,
			0 vrServicio,
			'05' conceptoRecaudo,
			0 valorPagoModerador,
			NULL numFEVPagoModerador,
			null consecutivo,
			gd.DocumentType,
			NULL InvoiceDetailId,
			gd.InvoiceId
	FROM Cte_GeneralData gd WITH (NOLOCK)
	JOIN Cte_IsAllPackage pac on pac.InvoiceId =gd.InvoiceId and pac.IsAllPackage =1
	JOIN Billing.ServiceOrderDetail sodPack WITH (NOLOCK) ON sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
	JOIN [Contract].CUPSEntity cups WITH (NOLOCK) ON cups.Id = sodPack.CUPSEntityId
	JOIN Contract.RIPSServices rs WITH (NOLOCK) on cups.RIPSServiceId = rs.Id
	JOIN Contract.RIPSServiceGroups rsg WITH (NOLOCK) on rs.ServiceGroup = rsg.Id
	JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = gd.AdmissionNumber
	JOIN Common.ThirdParty tp WITH (NOLOCK) on sodPack.PerformsHealthProfessionalThirdPartyId = tp.Id
	JOIN Common.Person p WITH (NOLOCK) on tp.PersonId = p.Id
	JOIN Admissions.AdmissionModalities am WITH (NOLOCK) on admision.IdAdmissionModalities = am.Id
	JOIN INUNIFUNC uf WITH (NOLOCK) on admision.UFUCODIGO = uf.UFUCODIGO
	JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON admision.CODCENATE = CA.CODCENATE
	LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.ID
	LEFT JOIN Causesofattention cfa WITH (NOLOCK) on admision.ICAUSAING = cfa.Code
	LEFT JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON admision.IdHealthPurposes = hp.Id
	LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin WITH (NOLOCK) ON DiagPrin.IdPartition = 1 AND DiagPrin.NUMINGRES = admision.NUMINGRES 		
	LEFT JOIN DIAGNOSTICO_SEGUNDARIO DiagRel WITH (NOLOCK) on DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.Id = gd.InvoiceId
	WHERE cups.RIPSConcept = '01'