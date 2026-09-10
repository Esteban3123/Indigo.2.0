
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
							GROUP by cg.InvoiceId),
							
		Cte_MipresInvoice as (	SELECT	mc.ServiceOrderDetailId,
										mc.Code,
										mc.IdMipres,cteg.Id InvoiceDetailId
								FROM Billing.MipresCode mc WITH(NOLOCK)
								JOIN Cte_GeneralData cteg WITH(NOLOCK) on mc.ServiceOrderDetailId = cteg.ServiceOrderDetailId )

		SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				NULLIF(NULLIF(RTRIM(gd.AuthorizationNumber), ''), '0') numAutorizacion,
				mi.IdMipres idMIPRES,
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
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
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice>0 and gd.RecoveryFeeType<>'05'
			WHERE cups.RIPSConcept in ('06','08','09','11','14')

		UNION ALL

		SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				NULLIF(NULLIF(RTRIM(sodPack.AuthorizationNumber), ''), '0') numAutorizacion,
				mi.IdMipres idMIPRES,
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
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
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			WHERE cups.RIPSConcept in ('06','08','09','11','14')

	----- SEGMENTO NUEVO PARA CORREGIR CANTIDADES EN HONORARIOS
		UNION ALL
			SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				NULLIF(NULLIF(RTRIM(gd.AuthorizationNumber), ''), '0') numAutorizacion,
				mi.IdMipres idMIPRES,
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
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
			LEFT JOIN Cte_MipresInvoice mi WITH(NOLOCK) on mi.InvoiceDetailId = gd.Id and mi.ServiceOrderDetailId=gd.ServiceOrderDetailId
			LEFT JOIN Cte_InvoiceCopay cic WITH(NOLOCK) on gd.InvoiceId=cic.InvoiceIdEntity and gd.SubTotalPatientSalesPrice>0 and gd.RecoveryFeeType<>'05'
			WHERE cups.RIPSConcept in ('07')

----- HASTA AQUI

		UNION ALL 

			SELECT 
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				NULLIF(NULLIF(RTRIM(ISNULL(gd.AuthorizationNumber, IIF(LEN(admission.IAUTORIZA) > 0, admission.IAUTORIZA, NULL))), ''), '0') numAutorizacion,
				NULL idMIPRES,
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
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
				NULLIF(NULLIF(RTRIM(ISNULL(sodPack.AuthorizationNumber, IIF(LEN(admission.IAUTORIZA) > 0, admission.IAUTORIZA, NULL))), ''), '0') numAutorizacion,
				NULL idMIPRES,
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
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
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

	UNION ALL

	SELECT
				gd.InvoiceNumber,
				LTRIM(RTRIM(CA.CODIPSSEC)) codPrestador,
				NULLIF(IIF(LEN(admission.IAUTORIZA) > 0, RTRIM(admission.IAUTORIZA), NULL), '0') numAutorizacion,
				NULL idMIPRES,
				FORMAT(isnull(gd.ServiceDate,gd.InvoiceDate), 'yyyy-MM-dd HH:mm') fechaSuministroTecnologia,
				'04' tipoOS,
				IP.IUM codTecnologiaSalud,
				SUBSTRING(ip.Name,1,60) nomTecnologiaSalud,
				gd.InvoicedQuantity cantidadOS,
				CASE WHEN (tip.SIGLA NOT IN ('CE','CC') OR tip.SIGLA IS NULL) THEN 'CC' ELSE tip.SIGLA END tipoDocumentoIdentificacion,
				RTRIM(tp.Nit) numDocumentoIdentificacion,
				CAST(ROUND((gd.GrandTotalSalesPrice) / gd.InvoicedQuantity, 0) AS INT) vrUnitOS,
				CAST(ROUND((gd.ThirdPartySalesPrice + gd.SubTotalPatientSalesPrice), 0) AS INT) vrServicio,
				gd.RecoveryFeeType conceptoRecaudo,
				CASE gd.RecoveryFeeType
					WHEN '05' THEN 0
					ELSE CAST(ROUND(gd.SubTotalPatientSalesPrice,0) AS INT)
				END valorPagoModerador,
				cic.InvoiceNumberCopay numFEVPagoModerador,
				NULL consecutivo,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.PatientCode) codigoVIDA,
				CAST(0 AS INT) vrDispensacion,
				CAST(gd.DocumentType AS TINYINT) AS DocumentType,
				gd.id InvoiceDetailId,
				gd.InvoiceId
			FROM Cte_GeneralData gd WITH (NOLOCK)
			JOIN Inventory.InventoryProduct IP WITH (NOLOCK) ON gd.ProductId = IP.id
			JOIN Inventory.ProductType PT WITH (NOLOCK) ON IP.ProductTypeId = PT.id
			JOIN [dbo].[INPACIENT] pacient WITH (NOLOCK) ON gd.PatientCode = pacient.IPCODPACI
			JOIN dbo.ADINGRESO admission WITH (NOLOCK) ON gd.AdmissionNumber = admission.NUMINGRES
			JOIN dbo.[ADCENATEN] AS CA WITH (NOLOCK) ON admission.CODCENATE = CA.CODCENATE
			JOIN Common.ThirdParty tp WITH (NOLOCK) ON gd.PerformsHealthProfessionalThirdPartyId = tp.id
			JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.id
			LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON p.IdentificationTypeId = tip.id
			LEFT JOIN Cte_InvoiceCopay cic WITH (NOLOCK) ON gd.InvoiceId = cic.InvoiceIdEntity AND gd.SubTotalPatientSalesPrice > 0 AND gd.RecoveryFeeType <> '05'
			WHERE gd.IsPackage = 0
				AND PT.Class = 2
				AND PT.Name = 'ALIMENTO'



GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que recopila la información de ''Otros Servicios'' requerida para la generación de RIPS electrónicos (Resolución 2275). Consolida datos de facturas activas, detalle de líneas facturadas, órdenes de servicio y códigos CUPS para clasificar procedimientos, medicamentos e insumos según los conceptos RIPS (tipo de tecnología en salud, código, nombre, cantidad y valores). Integra además el tipo y número de documento de identificación del profesional ejecutante, el código del prestador IPS, el concepto de recaudo (cuota moderadora, copago, bono o no aplica) con su valor, el número de la factura electrónica del pago moderador (copago), y el código MIPRES cuando aplica. Existe para alimentar el reporte electrónico de RIPS en la sección de otros servicios/tecnologías en salud, permitiendo a las EPS y entes de control validar lo facturado por la IPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoOtrosServiciosRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewGetInfoOtrosServiciosRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de "otros servicios" (procedimientos, honorarios y otros insumos/dispositivos) de facturas activas para la generación de RIPS electrónicos, mapeando códigos CUPS/productos, conceptos de recaudo y datos del prestador.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe estar en estado activo (Billing.Invoice.Status = 1).; El detalle de factura debe tener cantidad facturada mayor a cero (InvoiceDetail.InvoicedQuantity > 0).; El detalle de orden de servicio no debe estar marcado como eliminado (ServiceOrderDetail.IsDelete = 0) ni con SettlementType = 3.; Si el detalle de orden de servicio tiene IncludeServiceOrderDetailId no nulo, debe tener RecoveryRatio definido para ser considerado.; El paciente, ingreso, centro de atención, tercero (profesional) y persona deben existir y estar relacionados correctamente.; Para el segmento de productos (insumos/dispositivos), el ProductType debe ser de Class = 3.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas con Status=1 e ítems con cantidad facturada > 0.; Se excluyen detalles de orden de servicio eliminados (IsDelete=1) o con SettlementType=3.; Detalles incluidos en otro (IncludeServiceOrderDetailId no nulo) sólo se consideran si tienen recoveryratio definido.; El tipo de documento de identificación se normaliza siempre a ''CC'' cuando la sigla original no es ''CE'' ni ''CC'' o es nula.; El nombre de la tecnología en salud se trunca a 60 caracteres.; El código de tecnología en salud se sanitiza con dbo.fn_RemoveNonAlphaNumDash para CUPS.; vrUnitOS para procedimientos se calcula como GrandTotalSalesPrice / InvoicedQuantity redondeado a entero.; vrServicio se calcula como ThirdPartySalesPrice + SubTotalPatientSalesPrice redondeado a entero.; numFEVPagoModerador sólo se asocia cuando SubTotalPatientSalesPrice > 0 y el conceptoRecaudo no es ''05''.; Para filas generadas por expansión de paquete, vrUnitOS, vrServicio y valorPagoModerador siempre son 0 y conceptoRecaudo=''05''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewGetInfoOtrosServiciosRIPS: Devuelve filas para CUPS con RIPSConcept en (''06'',''08'',''09'',''11'',''14'') usando datos del propio detalle (procedimientos).; [RETURN_RESULT] Billing.ViewGetInfoOtrosServiciosRIPS: Cuando todos los detalles de la factura son paquete (IsAllPackage=1), agrega filas adicionales con los detalles internos del paquete (PackageServiceOrderDetailId), reportando vrUnitOS=0, vrServicio=0, conceptoRecaudo=''05'' y valorPagoModerador=0.; [RETURN_RESULT] Billing.ViewGetInfoOtrosServiciosRIPS: Para CUPS con RIPSConcept = ''07'' (honorarios) fuerza tipoOS=''05'', cantidadOS=1 y vrUnitOS = vrServicio = ThirdPartySalesPrice + SubTotalPatientSalesPrice redondeado.; [RETURN_RESULT] Billing.ViewGetInfoOtrosServiciosRIPS: Para productos de inventario con ProductType.Class = 3 emite filas con tipoOS=''01'' y código/nombre tomados de InventoryProduct (Code, Name).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si InvoiceDetail.RecoveryFeeType = 1 OR SubTotalPatientSalesPrice = 0 → conceptoRecaudo = ''05'' (No aplica); si Invoice.DocumentType = 5 AND CareGroup.MethodFixedAmountCollectionReport = 2 → conceptoRecaudo = ''05'' (No aplica) — descuento pactado sobre monto fijo en registros de servicio else Se evalúa el RecoveryFeeType del detalle; si RecoveryFeeType = 2 → conceptoRecaudo = ''02'' (Cuota moderadora); si RecoveryFeeType = 3 → conceptoRecaudo = ''01'' (Copago); si RecoveryFeeType = 4 → conceptoRecaudo = ''03'' (Bono) else conceptoRecaudo = ''05''; si CUPS.RIPSConcept IN (''06'',''08'') → tipoOS=''03''; si CUPS.RIPSConcept = ''07'' → tipoOS=''05'' (honorarios) con cantidadOS=1 y vrUnitOS=vrServicio; si CUPS.RIPSConcept IN (''09'',''11'') → tipoOS=''01''; si CUPS.RIPSConcept = ''14'' → tipoOS=''02''; si CUPS.RIPSConcept = ''15'' → tipoOS=''04'' else tipoOS=''01'' por defecto; si tip.SIGLA NOT IN (''CE'',''CC'') OR tip.SIGLA IS NULL → tipoDocumentoIdentificacion = ''CC'' else Se conserva la sigla original; si conceptoRecaudo = ''05'' → valorPagoModerador = 0 else valorPagoModerador = ROUND(SubTotalPatientSalesPrice,0); si Cte_IsAllPackage.IsAllPackage = 1 (todos los detalles son paquete) → Se incluyen filas adicionales por cada detalle interno del paquete (PackageServiceOrderDetailId) con valores en cero; si AuthorizationNumber del detalle es NULL en segmento de productos → Se usa admission.IAUTORIZA si LEN > 0, sino NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fn_RemoveNonAlphaNumDash', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewGetInfoOtrosServiciosRIPS';
GO
