


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