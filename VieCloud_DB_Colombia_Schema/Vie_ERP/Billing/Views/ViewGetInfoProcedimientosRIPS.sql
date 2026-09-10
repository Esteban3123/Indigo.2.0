

-- =============================================
-- Author:		Cristian Camilo Bahamon Castaño
-- Create date: 2023-12-29
-- Description:	Vista encargada de obtener la informacion de los procedimientos para RIPS electronicos
-- LastModification: Anthony Ocampo
-- ModificationDate: 2026-05-08
-- =============================================

CREATE VIEW [Billing].[ViewGetInfoProcedimientosRIPS]
AS

with Cte_InvoiceCopay as (	SELECT ic.InvoiceId InvoiceIdEntity, i.InvoiceNumber InvoiceNumberCopay
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
									i.PatientCode,
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
				NULLIF(NULLIF(RTRIM(gd.AuthorizationNumber), ''), '0') numAutorizacion, 
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
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice) , 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE
					WHEN gd.RecoveryFeeType='05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				null consecutivo,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(20)) codComplicacionCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodComplicacionCIE11,
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
			 LEFT JOIN dbo.INDIAGNOP Diag ON Diag.NUMINGRES = admision.NUMINGRES AND Diag.IPCODPACI = admision.IPCODPACI AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1
			 OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = admision.NUMINGRES
					AND D.IPCODPACI = admision.IPCODPACI
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			 ) DiagPrin
			 OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = admision.NUMINGRES
					AND D.IPCODPACI = admision.IPCODPACI
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(Diag.CODDIAGNO, DiagPrin.DiagnosticCode, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			 ) DiagRel
			 WHERE (cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14'))
			 AND NOT EXISTS (SELECT 1 FROM Cte_IsAllPackage iap WHERE iap.InvoiceId = gd.InvoiceId AND iap.IsAllPackage = 1)

			 UNION ALL

			 SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				FORMAT(sodPack.ServiceDate, 'yyyy-MM-dd HH:mm') fechaInicioAtencion,
				NULLIF(mi.IdMipres, '') idMIPRES,  
				NULLIF(NULLIF(RTRIM(sodPack.AuthorizationNumber), ''), '0') numAutorizacion, 
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoRelacionadoCIE11,
				CAST(NULL AS VARCHAR(20)) codComplicacionCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodComplicacionCIE11,
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
			 LEFT JOIN dbo.INDIAGNOP Diag ON Diag.NUMINGRES = admision.NUMINGRES AND Diag.IPCODPACI = admision.IPCODPACI AND DIAG.CODDIAGNO = admision.CODDIAEGR AND DIAG.CODDIAPRI = 1
			 OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = admision.NUMINGRES
					AND D.IPCODPACI = admision.IPCODPACI
					AND D.CODDIAPRI = 1
				ORDER BY CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END,
					D.FECDIAGNO DESC,
					D.CODDIAGNO
			 ) DiagPrin
			 OUTER APPLY (
				SELECT TOP (1) D.CODDIAGNO AS DiagnosticCode
				FROM dbo.INDIAGNOP D WITH (NOLOCK)
				WHERE D.NUMINGRES = admision.NUMINGRES
					AND D.IPCODPACI = admision.IPCODPACI
					AND D.CODDIAPRI = 0
					AND D.CODDIAGNO <> COALESCE(Diag.CODDIAGNO, DiagPrin.DiagnosticCode, admision.CODDIAEGR, gd.OutputDiagnosis, 'Z000')
				GROUP BY D.CODDIAGNO
				ORDER BY MIN(CASE D.DIAINGEGR WHEN 'E' THEN 1 WHEN 'A' THEN 2 ELSE 3 END),
					MAX(D.FECDIAGNO) DESC,
					D.CODDIAGNO
			 ) DiagRel
			 WHERE (cups.RIPSConcept not in ('01','06','07','08','09','11', '12', '13','14'))
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de procedimientos requerida para la generación de RIPS electrónicos (Registros Individuales de Prestación de Servicios de Salud). Integra datos de facturas activas, detalle de líneas facturadas, órdenes de servicio, diagnósticos principal y relacionado (CIE-10), identificación del profesional que ejecuta el procedimiento, código CUPS/RIPS, modalidad de atención, grupo de servicios, tipo y valor del concepto de recaudo (copago, cuota moderadora, bono o no aplica), número de factura de copago vinculada y código MIPRES cuando corresponde. Cruza información de admisiones y centros de atención con los contratos y grupos de atención para determinar correctamente el tipo de recaudo reportable. Es utilizada para la generación y reporte de RIPS ante entidades pagadoras (EPS, aseguradoras), habilitación y auditoría de facturación electrónica en salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoProcedimientosRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoProcedimientosRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de procedimientos facturados en el formato requerido para los RIPS electrónicos, incluyendo datos del prestador, profesional, diagnósticos, autorización, conceptos de recaudo y servicios empaquetados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura (Billing.Invoice) debe tener Status = 1 (activa); El detalle de orden de servicio (ServiceOrderDetail) no debe estar marcado como eliminado (IsDelete = 0); El valor facturado (ThirdPartySalesPrice + SubTotalPatientSalesPrice) redondeado a entero debe ser mayor a 0; El detalle no debe corresponder a un servicio incluido en otro (IncludeServiceOrderDetailId IS NULL) o, si lo es, debe tener recoveryratio definido; El concepto RIPS del CUPS no debe estar en (''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14'') — se excluyen conceptos no procedimentales (medicamentos, insumos, estancias, etc.)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El diagnóstico principal nunca es nulo: si no hay datos se asigna ''Z000'' como valor por defecto; Solo se reportan conceptos RIPS de tipo procedimiento (excluye explícitamente ''01'',''06'',''07'',''08'',''09'',''11'',''12'',''13'',''14''); Cuando la factura completa es un paquete (IsAllPackage=1), el servicio principal conserva su valor real (ThirdPartySalesPrice + SubTotalPatientSalesPrice) y los componentes del paquete se reportan con vrServicio=0; El tipo de documento del profesional se normaliza siempre a ''CC'' o ''CE'' (cualquier otro valor se mapea a ''CC''); Solo se procesan facturas activas (Status=1) y detalles de orden vigentes (IsDelete=0); El número de copago FEV (numFEVPagoModerador) solo se vincula cuando hay valor de paciente positivo y conceptoRecaudo distinto de ''05''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS electrónicos; Procedimientos CUPS; Diagnóstico principal y relacionado (CIE-10); Autorización de servicios; MIPRES; Cuota moderadora; Copago; Bono; Concepto de recaudo; Modalidad de atención; Vía de ingreso al servicio de salud; Finalidad de la tecnología en salud; Servicios empaquetados (paquetes de atención); Prestador (IPS); Profesional de salud; Tipo de documento de identificación; Centro de atención', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve una fila por cada InvoiceDetail facturado como procedimiento RIPS; si la factura es totalmente paquete (IsAllPackage=1) además expande filas adicionales por cada componente del paquete (vía PackageServiceOrderDetailId) con vrServicio=0 y conceptoRecaudo=''05''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si id.RecoveryFeeType = 1 OR SubTotalPatientSalesPrice = 0 → conceptoRecaudo = ''05'' (No aplica); si DocumentType = 5 AND CareGroup.MethodFixedAmountCollectionReport = 2 (registro de servicio con descuento pactado sobre monto fijo) → conceptoRecaudo = ''05'' (No aplica); si RecoveryFeeType = 2 → conceptoRecaudo = ''02'' (Cuota moderadora); si RecoveryFeeType = 3 → conceptoRecaudo = ''01'' (Copago); si RecoveryFeeType = 4 → conceptoRecaudo = ''03'' (Bono) else conceptoRecaudo = ''05'' por defecto; si Cte_IsAllPackage.IsAllPackage = 1 (todos los detalles de la factura son paquete) → el servicio principal conserva vrServicio = ROUND(ThirdPartySalesPrice + SubTotalPatientSalesPrice, 0) y se expanden los componentes del paquete vía la segunda rama del UNION ALL con vrServicio = 0; si IsAllPackage = 0 → vrServicio = ROUND(ThirdPartySalesPrice + SubTotalPatientSalesPrice, 0); si tip.SIGLA NOT IN (''CE'',''CC'') OR tip.SIGLA IS NULL → tipoDocumentoIdentificacion = ''CC'' (default) else Se usa la sigla original; si conceptoRecaudo = ''05'' → valorPagoModerador = 0 else valorPagoModerador = ROUND(SubTotalPatientSalesPrice, 0); si Diagnóstico no encontrado en INDIAGNOP ni en CTEs → codDiagnosticoPrincipal toma COALESCE en orden: Diag.CODDIAGNO → DiagnósticoPrincipal CTE → admision.CODDIAEGR → invoice.OutputDiagnosis → ''Z000'' (valor por defecto); el LEFT JOIN a INDIAGNOP incluye AND Diag.IPCODPACI = admision.IPCODPACI para evitar fan-out cuando INDIAGNOP tiene múltiples registros para el mismo ingreso y diagnóstico con diferente código de paciente — sin esta condición cada fila de InvoiceDetail se duplica, inflando vrServicio en el JSON RIPS y generando error RVG08', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.InvoiceCopay; Billing.BasicBilling; Billing.MipresCode; Contract.CareGroup; Contract.CUPSEntity; Contract.RIPSServices; Contract.RIPSServiceGroups; dbo.ADINGRESO; dbo.INDIAGNOP; dbo.ADCENATEN; dbo.ADTIPOIDENTIFICA; Common.ThirdParty; Common.Person; EntryRoutesHealthServices; Admissions.AdmissionModalities; Admissions.HealthPurposes; dbo.fn_RemoveNonAlphaNumDash', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoProcedimientosRIPS';
GO
