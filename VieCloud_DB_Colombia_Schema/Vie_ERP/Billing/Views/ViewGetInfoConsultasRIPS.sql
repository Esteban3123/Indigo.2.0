
-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-12-10
-- Description:	Vista encargada de obetner la informacion de la seccion consultas para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoConsultasRIPS]
AS

	with Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay
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
							i.PatientCode,
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
			NULLIF(NULLIF(RTRIM(gd.AuthorizationNumber), ''), '0') numAutorizacion,
			dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codConsulta,
			am.CodeRIPS modalidadGrupoServicioTecSal,
			rsg.Code grupoServicios,
			CAST(rs.Code AS INT) codServicio, 
			CASE WHEN hp.Code IN ('14','26','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42') THEN '44' ELSE hp.Code END AS   finalidadTecnologiaSalud, 
			cfa.RIPSCode causaMotivoAtencion,
			COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,
			DiagRel1.DiagnosticCode AS codDiagnosticoRelacionado1,
			DiagRel2.DiagnosticCode AS codDiagnosticoRelacionado2,
			DiagRel3.DiagnosticCode AS codDiagnosticoRelacionado3,
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
			[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado1CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado1CIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado2CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado2CIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado3CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado3CIE11,
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
	OUTER APPLY (
		SELECT TOP (1)
			Diag.CODDIAGNO,
			Diag.TIPDIAGNO
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 1
		ORDER BY CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
			Diag.FECDIAGNO DESC,
			Diag.CODDIAGNO
	) DiagPrin
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel1
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
			AND (DiagRel1.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel1.DiagnosticCode)
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel2
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
			AND (DiagRel1.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel1.DiagnosticCode)
			AND (DiagRel2.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel2.DiagnosticCode)
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel3
	WHERE cups.RIPSConcept = '01'

	UNION ALL

	select 
			gd.InvoiceNumber,
			LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
			FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm')  fechaInicioAtencion, 
			NULLIF(NULLIF(RTRIM(sodPack.AuthorizationNumber), ''), '0') numAutorizacion,
			dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codConsulta,
			am.CodeRIPS modalidadGrupoServicioTecSal,
			rsg.Code grupoServicios,
			CAST(rs.Code AS INT) codServicio, 
			CASE WHEN hp.Code IN ('14','26','28','29','30','31','32','33','34','35','36','37','38','39','40','41','42') THEN '44' ELSE hp.Code END AS   finalidadTecnologiaSalud, 
			cfa.RIPSCode causaMotivoAtencion,
			COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,
			DiagRel1.DiagnosticCode AS codDiagnosticoRelacionado1,
			DiagRel2.DiagnosticCode AS codDiagnosticoRelacionado2,
			DiagRel3.DiagnosticCode AS codDiagnosticoRelacionado3,
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
			[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado1CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado1CIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado2CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado2CIE11,
			CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionado3CIE11,
			CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionado3CIE11,
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
	OUTER APPLY (
		SELECT TOP (1)
			Diag.CODDIAGNO,
			Diag.TIPDIAGNO
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 1
		ORDER BY CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
			Diag.FECDIAGNO DESC,
			Diag.CODDIAGNO
	) DiagPrin
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel1
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
			AND (DiagRel1.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel1.DiagnosticCode)
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel2
	OUTER APPLY (
		SELECT TOP (1) Diag.CODDIAGNO AS DiagnosticCode
		FROM dbo.INDIAGNOP Diag WITH (NOLOCK)
		WHERE Diag.NUMINGRES = admision.NUMINGRES
			AND Diag.IPCODPACI = admision.IPCODPACI
			AND Diag.CODDIAPRI = 0
			AND Diag.CODDIAGNO <> COALESCE(DiagPrin.CODDIAGNO, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
			AND (DiagRel1.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel1.DiagnosticCode)
			AND (DiagRel2.DiagnosticCode IS NULL OR Diag.CODDIAGNO <> DiagRel2.DiagnosticCode)
		GROUP BY Diag.CODDIAGNO
		ORDER BY MIN(CASE Diag.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
			MAX(Diag.FECDIAGNO) DESC,
			Diag.CODDIAGNO
	) DiagRel3
	WHERE cups.RIPSConcept = '01'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que reúne toda la información necesaria para generar la sección de Consultas dentro de los RIPS electrónicos (Registros Individuales de Prestación de Servicios de Salud). Integra datos de facturación (facturas activas, detalles de factura, copagos y cuotas moderadoras), admisiones de pacientes, diagnósticos CIE-10 (principal y hasta tres relacionados, diferenciando ingreso, egreso y ambos), servicios CUPS, modalidad de atención, finalidad de la tecnología en salud y tipo de documento del prestador. Consolida los campos requeridos por la normativa de RIPS electrónicos como: código del prestador, fecha de atención, número de autorización, diagnóstico principal y tipo, concepto de recaudo, valor del servicio, valor del pago moderador y número de la factura electrónica de venta del copago. Se utiliza como fuente directa para la generación y validación de archivos RIPS de consultas ambulatorias y de urgencias.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoConsultasRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoConsultasRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el detalle de la sección "consultas" para los RIPS electrónicos, consolidando datos de factura, admisión, diagnósticos, profesional y CUPS clasificados como concepto RIPS ''01'' (consultas), incluyendo servicios empacados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe estar activa (Billing.Invoice.Status = 1).; El detalle de orden de servicio no debe estar eliminado (ServiceOrderDetail.IsDelete = 0) ni tener SettlementType = 3.; Si IncludeServiceOrderDetailId no es nulo, debe existir RecoveryRatio definido.; El CUPS asociado debe tener RIPSConcept = ''01'' (consulta).; Para la rama de paquetes, todas las líneas de la factura deben ser paquete (IsAllPackage = 1) y existir un ServiceOrderDetail con PackageServiceOrderDetailId apuntando al detalle padre.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen facturas con Status = 1.; Solo se reportan CUPS cuyo RIPSConcept = ''01'' (consultas).; El diagnóstico principal se prioriza en el orden: egreso (''E''), ambos (''A''), ingreso (otros), tomando el más reciente por FECDIAGNO.; Si no se encuentra diagnóstico principal en INDIAGNOP, se usa CODDIAEGR de la admisión, luego OutputDiagnosis, y como último fallback el código ''Z000''.; Los diagnósticos relacionados se materializan en hasta 3 columnas (DiagnosticCodeRel1..3) según prioridad E/A/otros y solo aplican a diagnósticos con CODDIAPRI = 0.; tipoDocumentoIdentificacion siempre se reporta como ''CC'' o ''CE''; cualquier otro valor se normaliza a ''CC''.; Las líneas generadas por explosión de paquete reportan vrServicio = 0, conceptoRecaudo = ''05'' y valorPagoModerador = 0.; numFEVPagoModerador solo se asigna cuando hay aporte del paciente (SubTotalPatientSalesPrice > 0) y conceptoRecaudo distinto de ''05''.; Se excluyen detalles con SettlementType = 3 y los marcados como eliminados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; Consulta; Diagnóstico principal; Diagnóstico relacionado (CIE-10); Cuota moderadora; Copago; Bono; Autorización; CUPS; Modalidad de admisión; Finalidad de la atención en salud; Causa de atención; Profesional de la salud; Centro de atención (IPS); Tipo de documento de identificación; Servicio empacado (paquete); Concepto de recaudo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewGetInfoConsultasRIPS: Devuelve una fila por línea facturada de consulta (RIPSConcept=''01'') más, vía UNION ALL, una fila por cada subdetalle de paquete cuando todas las líneas de la factura son paquete (IsAllPackage=1), con vrServicio=0 y conceptoRecaudo=''05''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si id.RecoveryFeeType = 1 OR SubTotalPatientSalesPrice = 0 → conceptoRecaudo = ''05'' (No aplica); si i.DocumentType = 5 AND CareGroup.MethodFixedAmountCollectionReport = 2 → conceptoRecaudo = ''05'' (No aplica para registro de servicio con descuento pactado sobre Monto Fijo); si id.RecoveryFeeType = 2 → conceptoRecaudo = ''02'' (Cuota moderadora); si id.RecoveryFeeType = 3 → conceptoRecaudo = ''01'' (Copago); si id.RecoveryFeeType = 4 → conceptoRecaudo = ''03'' (Bono) else conceptoRecaudo = ''05'' por defecto; si HealthPurposes.Code IN (''14'',''26'',''28..42'') → finalidadTecnologiaSalud = ''44'' else Se usa hp.Code original; si ADTIPOIDENTIFICA.SIGLA NOT IN (''CE'',''CC'') OR es NULL → tipoDocumentoIdentificacion = ''CC'' por defecto else Se usa la sigla original; si DiagPrin.TIPDIAGNO = ''I'' / ''C'' / ''r'' → tipoDiagnosticoPrincipal = ''01'' / ''02'' / ''03'' respectivamente; si es NULL se asume ''C'' → ''02''; si conceptoRecaudo = ''05'' → valorPagoModerador = 0 else valorPagoModerador = ROUND(SubTotalPatientSalesPrice,0)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fn_RemoveNonAlphaNumDash', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDIAGNOP; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.InvoiceCopay; Billing.BasicBilling; Contract.CareGroup; Contract.CUPSEntity; Contract.RIPSServices; Contract.RIPSServiceGroups; dbo.ADINGRESO; Common.ThirdParty; Common.Person; Admissions.AdmissionModalities; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.ADTIPOIDENTIFICA; dbo.Causesofattention; Admissions.HealthPurposes', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoConsultasRIPS';
GO
