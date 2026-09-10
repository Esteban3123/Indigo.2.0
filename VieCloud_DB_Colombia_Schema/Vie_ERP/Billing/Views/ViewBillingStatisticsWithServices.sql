

CREATE VIEW [Billing].[ViewBillingStatisticsWithServices]
AS
SELECT 
	CAST(NEWID() AS VARCHAR(40)) Id,
	ing.CODCENATE CareCenterCode, 
	CEN.NOMCENATE AS CareCenterName, 
	ing.CODCENATE + ' - ' + CEN.NOMCENATE CareCenterDescription,
	t.Nit ThirdPartyNit, 
	t.Name ThirdPartyName, 
	t.Nit + ' - ' + t.Name ThirdPartyDescription,
	ea.Code HealthAdministratorCode, 
	ea.Name HealthAdministratorName,
	ea.Code + ' - ' + ea.Name HealthAdministratorDescription,
	ga.Code CareGroupCode, 
	ga.Name CareGroupName, 
	ga.Code + ' - ' + ga.Name CareGroupDescription,
	F.Status StatusInvoice, 
	IIF(f.Status = 1, 'Facturado', 'Anulado') StatusDescription,
	f.DocumentType, 
	CASE f.documentType 
		WHEN '1' THEN 'Factura EAPB con Contrato' 
		WHEN '2' THEN 'Factura EAPB Sin Contrato' 
		WHEN '3' THEN 'Factura Particular' 
		WHEN '4' THEN 'Factura Capitada ' 
		WHEN '5' THEN 'Control de Capitacion'
		WHEN '6' THEN 'Factura Basica' 
		WHEN '7' THEN 'Factura de Venta de Productos' 
	END AS DocumentTypeDescription, 
	F.InvoiceNumber,
	F.AdmissionNumber, 
	ing.IFECHAING AdmissionDate, 
	CASE ing.ICAUSAING 
		WHEN '1' THEN 'Heridos en Combate' 
		WHEN '2' THEN 'Enfermedad Profesional' 
		WHEN '3' THEN 'Enfermedad General Adulto'
		WHEN '4' THEN 'Enfermedad General Pediatria' 
		WHEN '5' THEN 'Odontología' 
		WHEN '6' THEN 'Accidente Transito' 
		WHEN '7' THEN 'Catastrofe/Fisalud' 
		WHEN '8' THEN 'Quemados' 
		WHEN '9' THEN 'Maternidad' 
		WHEN '10' THEN 'Accidente Laboral' 
		WHEN '11' THEN 'Cirugia Programada'
	END CauseIncomeDescription, 
	CASE ing.TIPOINGRE 
		WHEN '1' THEN 'Ambulatorio' 
		WHEN '2' THEN 'Hospitalario' 
	END AS AdmissionTypeDescription,
	F.PatientCode, 
	CASE P.IPSEXOPAC 
		WHEN '1' THEN 'Hombre'
		ELSE 'Mujer'
	END SexDescription, 
	F.TotalInvoice, 
	F.InvoiceDate, 
	ROUND([Common].[CurrencyConverterByModule](sod.GrandTotalSalesPrice,cs.OfficialCurrencyId,isnull(cu.Id,cs.OfficialCurrencyId),NULL,'Invoice',so.CreationDate),2) as TotalValue,
	UF.Code FunctionalUnitCode, 
	UF.Name FunctionalUnitName, 
	ing.CODDIAEGR DiagnosticCode,
	diag.NOMDIAGNO DiagnosticName,
	CASE F.IsCutAccount
		WHEN 'True' THEN 'Si' 
		ELSE 'No' 
	END IsCutAccountDescription,
	id.SubTotalPatientSalesPrice ValueCopay, 
	u.UserCode, 
	per.Fullname UserName, 
	u.UserCode + ' - ' + per.Fullname UserDescription,
	t.Id ThirdPartyId, 
	ea.Id HealthAdministratorId,
	ga.Id CareGroupId, 
	id.ThirdPartySalesPrice EntityValue, 
	P.IPCODPACI PatientIdentification,
	P.IPNOMCOMP PatientName,
	p.IPCODPACI + ' - ' + P.IPNOMCOMP PatientDescription,
	sod.PerformsHealthProfessionalCode,
	RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalName,
	LTRIM(RTRIM(prof.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription, 
	ips.Id IPSServiceId, 
	ips.Code IPSServiceCode,
	ips.Name IPSServiceName, 
	ip.Id ProductId,
	ip.Code ProductCode,
	ip.Name ProductName,
	iif(ce.Id is null, ip.Code + ' - ' + ip.Name, ce.Code + ' - ' + ce.Description) DescriptionIPSServiceOrProduct,
	sod.InvoicedQuantity, 
	so.Code ServiceOrderCode,
	so.OrderDate,
	case p.IPTIPODOC 
		when 1 then 'Cédula de Ciudadanía' 
		when 2 then 'Cédula de Extranjería' 
		when 3 then 'Tarjeta de Identidad' 
		when 4 then 'Registro Civil' 
		when 5 then 'Pasaporte' 
		when 6 then 'Adulto Sin Identificación'
		when 7 then 'Menor Sin Identificación'
		when 8 then 'Número único de identificación personal'
		when 9 then 'Certificado Nacido Vivo' 
		when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
		when 11 then 'Salvoconducto (Aplica para extranjeros)'
		when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)' 
		when 13 then 'Permiso por Protección Temporal (Aplica para extranjeros)' 
		when 14 then 'Documento extranjero' 
	end IdentificationTypeDescription,
	p.IPFECNACI PatientBirth, 
	p.IPDIRECCI PatientAdress,
	p.IPTELEFON PatientPhone,
	ce.Id CupsEntityId, 
	ce.Code CupsEntityCode,
	ce.Description CupsEntityName,
	case sod.Presentation 
		when 1 then 'No quirúrgico'
		when 2 then 'Quirúrgico' 
		when 3 then 'Paquete' 
	end PresentationDescription,
	ROUND([Common].[CurrencyConverterByModule](sod.TotalSalesPrice,cs.OfficialCurrencyId,isnull(cu.Id,cs.OfficialCurrencyId),NULL,'Invoice',so.CreationDate),2) as UnitValue, --sod.TotalSalesPrice UnitValue, 
	CASE sod.SettlementType 
		WHEN 3 THEN 'Si' 
		ELSE 'No' 
	END AS ApplyProcedure,
	ISNULL(bg.Code, bgProduct.Code) BillingGroupCode, 
	ISNULL(bg.Name, bgProduct.Name) BillingGroupName,
	case sod.ApplyRIAS 
		when 1 then 'Si' 
		else 'No' 
	end ApplyRIASDescription, 
	r.CODPRO RIASCode, 
	r.NOMBRE RIASName,
	CASE sod.RecordType
		WHEN 1 THEN 'Servicios'
		WHEN 2 THEN 'Medicamentos' 
	END AS ServiceOrProduct,
	ing.CODDIAEGR CIE10, (cast(datediff(dd, p.IPFECNACI, Common.Getdate()) / 365.25 as int)) PatientAge,
	case ga.EntityType
		when 1 then 'EPS Contributivo'
		when 2 then 'EPS Subsidiado' 
		when 3 then 'ET Vinculados Municipios' 
		when 4 then 'ET Vinculados Departamentos' 
		when 5 then 'ARL Riesgos Laborales' 
		when 6 then 'MP Medicina Prepagada' 
		when 7 then 'IPS Privada'
		when 8 then 'IPS Publica' 
		when 9 then 'Regimen Especial'
		when 10 then 'Accidentes de transito'
		when 11 then 'Fosyga' 
		when 12 then 'Otros' 
		when 13 then 'Aseguradoras' 
		when 99 then 'Particulares' 
	end EntityTypeDescription,
	cu.Id as CurrencyId,
	cu.Abbreviation as CurrencyAbbreviation,
	ISNULL(ic.Name, '') AS  InvoiceCategory
FROM Billing.Invoice AS F WITH (nolock) 
LEFT JOIN Billing.InvoiceCategories ic ON IC.ID = F.InvoiceCategoryId
inner join Billing.InvoiceDetail id WITH (nolock) on id.InvoiceId = f.Id
inner join Billing.ServiceOrderDetail sod WITH(NOLOCK) on sod.Id = id.ServiceOrderDetailId
inner join Billing.ServiceOrder so WITH(NOLOCK) on so.Id = sod.ServiceOrderId
inner join Payroll.FunctionalUnit AS UF WITH (nolock) ON UF.Id = sod.PerformsFunctionalUnitId
inner join Common.ThirdParty AS t WITH (nolock) ON t.Id = F.ThirdPartyId
inner join Security.[User] AS u  ON u.UserCode = F.InvoicedUser 
inner join Security.Person AS per ON per.Id = u.IdPerson 
JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
left outer join dbo.ADINGRESO AS ing WITH (nolock) ON ing.NUMINGRES = F.AdmissionNumber 
left outer join .INDIAGNOS diag WITH(NOLOCK) on diag.CODDIAGNO = ing.CODDIAEGR
left outer join DBO.ADCENATEN AS CEN WITH (nolock) ON CEN.CODCENATE =ing.CODCENATE  
left outer join Contract.CareGroup AS ga WITH (nolock) ON ga.Id = F.CareGroupId 
left outer join Contract.Contract c WITH(NOLOCK) on c.Id = ga.ContractId
left outer join Contract.HealthAdministrator AS ea WITH (nolock) ON ea.Id = F.HealthAdministratorId 
left outer join dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = f.PatientCode
left outer join Contract.IPSService ips WITH(NOLOCK) on ips.Id = sod.IPSServiceId
left outer join Inventory.InventoryProduct ip WITH(NOLOCK) on ip.Id = sod.ProductId
left outer join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = sod.PerformsHealthProfessionalCode
left outer join Contract.CUPSEntity ce WITH(NOLOCK) on ce.Id = sod.CUPSEntityId
left outer join Billing.BillingGroup bg WITH(NOLOCK) on bg.Id = ce.BillingGroupId
left outer join Billing.BillingGroup bgProduct WITH(NOLOCK) on bgProduct.Id = ip.BillingGroupId
left outer join .RIASCUPS rc WITH(NOLOCK) on rc.ID = sod.RIASCupsId
left outer join .RIAS r WITH(NOLOCK) on r.ID = rc.IDRIAS
left join Common.Currency cu on cu.Id = f.CurrencyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de estadísticas de facturación con detalle de servicios y productos: integra facturas emitidas, sus líneas de detalle, órdenes de servicio, datos del ingreso hospitalario y del paciente para generar un reporte analítico completo de cobros. Consolida información del tercero pagador (EPS, ARL, aseguradora, particular), la unidad funcional que realizó el servicio, el profesional de la salud ejecutante, el código CUPS o producto facturado, diagnóstico CIE-10 del egreso, copagos, valores por entidad y por paciente, tipo de documento de factura y categoría de factura. Sirve como fuente principal para reportes de estadísticas de facturación, seguimiento de ingresos por contrato, análisis de producción por unidad funcional o profesional, control de cuentas de cobro, generación de RIPS y auditoría de servicios facturados a pagadores y pacientes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingStatisticsWithServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingStatisticsWithServices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida estadísticas de facturación a nivel de línea de servicio/producto, enriqueciendo cada detalle facturado con información del paciente, ingreso, profesional, contrato, administradora, CUPS, RIAS y conversión de moneda.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada factura (Billing.Invoice) debe tener al menos un InvoiceDetail asociado a un ServiceOrderDetail y a una ServiceOrder (joins INNER).; Debe existir un Tercero (Common.ThirdParty) asociado a la factura y una Unidad Funcional (Payroll.FunctionalUnit) para el ServiceOrderDetail.; El usuario facturador (Invoice.InvoicedUser) debe existir en Security.User y tener Persona asociada en Security.Person.; Debe existir registro en GeneralLedger.CompanySettings para obtener la moneda oficial usada en la conversión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id de cada fila es un GUID generado en tiempo de consulta (no persistente).; Solo aparecen facturas con detalle, orden de servicio y unidad funcional válidos (joins INNER sobre InvoiceDetail, ServiceOrderDetail, ServiceOrder y FunctionalUnit).; Los valores monetarios (TotalValue, UnitValue) siempre se entregan convertidos a la moneda de la factura (o a la moneda oficial de la compañía si la factura no tiene moneda) usando la fecha de creación de la orden de servicio y el módulo ''Invoice''.; La edad del paciente se calcula como DATEDIFF(dd, fecha_nacimiento, Common.Getdate()) / 365.25 truncado a entero.; InvoiceCategory nunca es NULL en la salida: se reemplaza por cadena vacía con ISNULL.; BillingGroup proveniente del CUPS tiene precedencia sobre el del producto.; Se asume CompanySettings como configuración única (JOIN ... ON 1=1).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewBillingStatisticsWithServices: Devuelve una fila por cada línea de detalle de factura (InvoiceDetail × ServiceOrderDetail), generando un Id único por fila vía CAST(NEWID() AS VARCHAR(40)).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.Status = 1 → StatusDescription = ''Facturado'' else StatusDescription = ''Anulado''; si f.DocumentType IN (''1''..''7'') → Mapea a tipos: Factura EAPB con/Sin Contrato, Particular, Capitada, Control de Capitación, Básica, Venta de Productos.; si ing.ICAUSAING IN (''1''..''11'') → Mapea causa de ingreso: Heridos en Combate, Enfermedad Profesional, Enfermedad General Adulto/Pediatría, Odontología, Accidente Tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada.; si ing.TIPOINGRE = ''1'' o ''2'' → AdmissionTypeDescription = ''Ambulatorio'' o ''Hospitalario''; si P.IPSEXOPAC = ''1'' → SexDescription = ''Hombre'' else SexDescription = ''Mujer'' (cualquier otro valor incluido NULL se trata como Mujer); si F.IsCutAccount = ''True'' → IsCutAccountDescription = ''Si'' else IsCutAccountDescription = ''No''; si p.IPTIPODOC entre 1 y 14 → Traduce a tipo de documento colombiano (CC, CE, TI, RC, Pasaporte, etc.) incluyendo documentos para extranjeros.; si ce.Id IS NULL → DescriptionIPSServiceOrProduct = ip.Code + '' - '' + ip.Name (producto) else DescriptionIPSServiceOrProduct = ce.Code + '' - '' + ce.Description (CUPS); si sod.Presentation IN (1,2,3) → PresentationDescription = ''No quirúrgico'', ''Quirúrgico'' o ''Paquete''; si sod.SettlementType = 3 → ApplyProcedure = ''Si'' else ApplyProcedure = ''No''; si sod.ApplyRIAS = 1 → ApplyRIASDescription = ''Si'' else ApplyRIASDescription = ''No''; si sod.RecordType = 1 o 2 → ServiceOrProduct = ''Servicios'' o ''Medicamentos''; si ga.EntityType IN (1..13, 99) → Mapea tipo de entidad: EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Privada/Pública, Régimen Especial, Accidentes de tránsito, Fosyga, Otros, Aseguradoras, Particulares.; si BillingGroup desde CUPS o desde Producto → Usa ISNULL(bg.Code, bgProduct.Code) — prioriza el grupo de facturación del CUPSEntity sobre el del producto de inventario.; si cu.Id IS NULL (factura sin moneda asignada) → Usa cs.OfficialCurrencyId como moneda destino en la conversión vía Common.CurrencyConverterByModule.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.CurrencyConverterByModule; Common.Getdate', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatisticsWithServices';
GO
