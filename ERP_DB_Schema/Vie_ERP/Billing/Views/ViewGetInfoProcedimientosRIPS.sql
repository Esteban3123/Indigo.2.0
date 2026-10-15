

-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-12-29
-- Description:	Vista encargada de obtener la informacion de los procedimientos para RIPS electronicos
-- LastModification: Anthony Ocampo	
-- ModificationDate: 2025-08-13
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoProcedimientosRIPS]
AS

with DIAGNOSTICO_PRINCIPAL as  (
			SELECT TOP 1
						i.id InvoiceId,
						I.AdmissionNumber,
						Diag.CODDIAGNO AS DiagnosticCode
					FROM Billing.Invoice AS I WITH (NOLOCK)
					JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = I.AdmissionNumber 
					JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES
					WHERE I.[Status] = 1 AND DIAG.CODDIAPRI = 1 
					ORDER BY FECDIAGNO DESC),

			DIAGNOSTICO_RELACIONADO as  (
				SELECT TOP 1
						i.id InvoiceId,
						I.AdmissionNumber,
						Diag.CODDIAGNO AS DiagnosticCode
					FROM Billing.Invoice AS I WITH (NOLOCK)
					JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = I.AdmissionNumber 
					JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES
					WHERE I.[Status] = 1 AND DIAG.CODDIAPRI = 0 
					ORDER BY FECDIAGNO DESC),

			Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay 
									FROM Billing.InvoiceCopay ic WITH(NOLOCK)
									JOIN Billing.BasicBilling bb WITH(NOLOCK) on ic.BasicBillingId = bb.id
									JOIN Billing.Invoice i WITH(NOLOCK) on bb.InvoiceId =i.id
									GROUP by ic.InvoiceId, i.InvoiceNumber),
			Cte_GeneralData as (	SELECT
									i.id InvoiceId, 
									i.InvoiceNumber,
									i.OutputDiagnosis,
									i.HealthAdministratorId,
									i.RevenueControlDetailId,
									i.AdmissionNumber,
									id.id,
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
									sod.IsPackage,
									sod.CUPSEntityId,
									sod.IPSServiceId,
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
									JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.InvoiceId = i.id 
									JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.id = id.ServiceOrderDetailId
									JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = i.CareGroupId
									WHERE i.Status = 1 
									AND sod.IsDelete = 0
									AND CAST(ROUND((id.ThirdPartySalesPrice + ID.SubTotalPatientSalesPrice) , 0) AS INT) >0
									AND (sod.IncludeServiceOrderDetailId IS NULL or ( sod.IncludeServiceOrderDetailId IS not null and  sod.recoveryratio is not null ))),

			Cte_IsAllPackage AS (SELECT cg.InvoiceId,MIN(CAST(cg.IsPackage AS TINYINT)) IsAllPackage
								from Cte_GeneralData cg
								GROUP by cg.InvoiceId)
								,
			Cte_MipresInvoice as (	SELECT	mc.ServiceOrderDetailId,
											mc.Code,
											mc.IdMipres,cteg.Id InvoiceDetailId
									FROM Billing.MipresCode mc WITH(NOLOCK)
									JOIN Cte_GeneralData cteg WITH(NOLOCK) on mc.ServiceOrderDetailId = cteg.ServiceOrderDetailId )


			SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				FORMAT(gd.ServiceDate, 'yyyy-MM-dd HH:mm') fechaInicioAtencion,
				NULLIF(mi.IdMipres, '') idMIPRES,  
				RTRIM(gd.AuthorizationNumber) numAutorizacion, 
				dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codProcedimiento,
				erhs.RIPSCode viaIngresoServicioSalud,
				am.CodeRIPS modalidadGrupoServicioTecSal,
				rsg.Code grupoServicios,
				CAST(rs.Code AS INT) codServicio, 
				hp.Code finalidadTecnologiaSalud,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion, 
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,admision.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado, 
				NULL codComplicacion,
				CASE WHEN pac.IsAllPackage = 1 THEN 0 ELSE  CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) END vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE
					WHEN gd.RecoveryFeeType='05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END valorPagoModerador, 
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				gd.DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			 FROM Cte_GeneralData gd WITH (NOLOCK)
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.id = gd.CUPSEntityId 
			 left join Contract.RIPSServices rs on CUPS.RIPSServiceId = rs.id --
			 left join Contract.RIPSServiceGroups rsg on rs.ServiceGroup = rsg.id --
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = gd.AdmissionNumber 
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON CA.CODCENATE = admision.CODCENATE 
			 JOIN Common.ThirdParty tp on gd.PerformsHealthProfessionalThirdPartyId = tp.id
			 JOIN Common.Person p on tp.PersonId = p.id
			 LEFT join EntryRoutesHealthServices erhs on erhs.id = admision.IdEntryRoutesHealthServices
			 LEFT join Admissions.AdmissionModalities am on admision.IdAdmissionModalities = am.id
			 LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			 LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on cic.InvoiceIdEntity = gd.InvoiceId
															and gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			 LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			 left JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON admision.IdHealthPurposes = hp.id--
			 LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			 LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.AdmissionNumber = admision.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId
			 LEFT JOIN DIAGNOSTICO_RELACIONADO DiagRel ON DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId
			 LEFT JOIN Cte_IsAllPackage pac on gd.InvoiceId=pac.InvoiceId
			 WHERE (cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14'))

			 UNION ALL

			 SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm') fechaInicioAtencion,
				NULLIF(mi.IdMipres, '') idMIPRES,  
				RTRIM(sodPack.AuthorizationNumber) numAutorizacion, 
				dbo.fn_RemoveNonAlphaNumDash(RTRIM(CUPS.RIPSCode)) as codProcedimiento,
				erhs.RIPSCode viaIngresoServicioSalud,
				am.CodeRIPS modalidadGrupoServicioTecSal,
				rsg.Code grupoServicios,
				CAST(rs.Code AS INT) codServicio, 
				hp.Code finalidadTecnologiaSalud,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion, 
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				COALESCE(Diag.CODDIAGNO,DiagPrin.DiagnosticCode,admision.CODDIAEGR,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal ,
				DiagRel.DiagnosticCode codDiagnosticoRelacionado, 
				NULL codComplicacion,
				0 vrServicio,
				'05' conceptoRecaudo,
				0 valorPagoModerador, 
				NULL numFEVPagoModerador,
				null consecutivo,
				gd.DocumentType,
				NULL InvoiceDetailId,
				gd.InvoiceId
			 FROM Cte_GeneralData gd WITH (NOLOCK)
			 JOIN Cte_IsAllPackage pac on gd.InvoiceId=pac.InvoiceId and pac.IsAllPackage =1
			 JOIN Billing.ServiceOrderDetail sodPack WITH (NOLOCK) ON sodPack.PackageServiceOrderDetailId = gd.ServiceOrderDetailId
			 JOIN [Contract].CUPSEntity CUPS WITH (NOLOCK) ON CUPS.id = sodPack.CUPSEntityId 
			 left join Contract.RIPSServices rs on CUPS.RIPSServiceId = rs.id --
			 left join Contract.RIPSServiceGroups rsg on rs.ServiceGroup = rsg.id --
			 JOIN dbo.ADINGRESO admision WITH (NOLOCK) ON admision.NUMINGRES = gd.AdmissionNumber
			 JOIN dbo.ADCENATEN CA WITH (NOLOCK) ON CA.CODCENATE = admision.CODCENATE 
			 join Common.ThirdParty tp on sodPack.PerformsHealthProfessionalThirdPartyId = tp.id
			 join Common.Person p on tp.PersonId = p.id
			 LEFT join EntryRoutesHealthServices erhs on erhs.id = admision.IdEntryRoutesHealthServices
			 LEFT join Admissions.AdmissionModalities am on admision.IdAdmissionModalities = am.id
			 LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			 LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			 left JOIN Admissions.HealthPurposes hp WITH(NOLOCK) ON admision.IdHealthPurposes = hp.id--
			 LEFT JOIN dbo.INDIAGNOP Diag WITH (NOLOCK) ON Diag.NUMINGRES = admision.NUMINGRES AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1 
			 LEFT JOIN DIAGNOSTICO_PRINCIPAL DiagPrin ON DiagPrin.AdmissionNumber = admision.NUMINGRES AND DiagPrin.InvoiceId = gd.InvoiceId
			 LEFT JOIN DIAGNOSTICO_RELACIONADO DiagRel ON DiagRel.AdmissionNumber = admision.NUMINGRES AND DiagRel.InvoiceId = gd.InvoiceId
			 WHERE (cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14'))
GO