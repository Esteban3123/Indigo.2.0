


CREATE VIEW [Billing].[ViewBillingStatistics]
AS

with users_cte as (
	
SELECT u.UserCode, per.Fullname
FROM Security.[User] AS u
inner join Security.Person AS per ON per.Id = u.IdPerson 

), 
admission_cte as (
	select ing.NUMINGRES, ing.CODCENATE, CEN.NOMCENATE, ing.IFECHAING, ing.ICAUSAING, ing.TIPOINGRE, ing.CODDIAEGR, 
	UF.UFUCODIGO, UF.UFUDESCRI, p.IPFECNACI, p.IPTIPODOC,p.IPPRINOMB, p.IPSEGNOMB,  p.IPPRIAPEL, p.IPSEGAPEL,p.IPSEXOPAC, p.IPNOMCOMP, ing.IPCODPACI
	from dbo.ADINGRESO AS ing 
	--WITH (nolock) ON CAST(ing.NUMINGRES AS int) = F.AdmissionNumber 
	inner join DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ing.CODCENATE
	inner join dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = ing.UFUCODIGO
	inner join dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = ing.IPCODPACI
),
health_cte as (
	select ea.Id, ea.Code, ea.Name, tHA.Nit, tHA.[Name] as NameThird, tHA.Id as IdThirdParty
	from Contract.HealthAdministrator AS ea
	inner join Common.ThirdParty tHA with(nolock) on tHA.Id = ea.ThirdPartyId
)


SELECT 
	F.Id,
	ing.CODCENATE CareCenterCode, ing.NOMCENATE AS CareCenterName, ing.CODCENATE + ' - ' + ing.NOMCENATE CareCenterDescription,
	hc.Nit ThirdPartyNit, hc.Name ThirdPartyName, hc.Nit + ' - ' + hc.Name ThirdPartyDescription,
	hc.Code HealthAdministratorCode, hc.Name HealthAdministratorName, hc.Code + ' - ' + hc.Name HealthAdministratorDescription,
	ga.Code CareGroupCode, ga.Name CareGroupName, ga.Code + ' - ' + ga.Name CareGroupDescription,
	F.Status StatusInvoice, IIF(f.Status = 1, 'Facturado', 'Anulado') StatusDescription,
	f.DocumentType, CASE f.documentType WHEN '1' THEN 'Factura EAPB con Contrato' WHEN '2' THEN 'Factura EAPB Sin Contrato' WHEN '3' THEN 'Factura Particular' WHEN '4' THEN 'Factura Capitada ' WHEN '5' THEN 'Control de Capitacion' WHEN '6' THEN 'Factura Basica' WHEN '7' THEN 'Factura de Venta de Productos' END AS DocumentTypeDescription, 
	F.InvoiceNumber, F.AdmissionNumber, ing.IFECHAING AdmissionDate, 
	CASE ing.ICAUSAING WHEN '1' THEN 'Heridos en Combate' WHEN '2' THEN 'Enfermedad Profesional' WHEN '3' THEN 'Enfermedad General Adulto' WHEN '4' THEN 'Enfermedad General Pediatria' WHEN '5' THEN 'Odontología' WHEN '6' THEN 'Accidente Transito' WHEN '7' THEN 'Catastrofe/Fisalud' WHEN '8' THEN 'Quemados' WHEN '9' THEN 'Maternidad' WHEN '10' THEN 'Accidente Laboral' WHEN '11' THEN 'Cirugia Programada' END CauseIncomeDescription, 
	CASE ing.TIPOINGRE WHEN '1' THEN 'Ambulatorio' WHEN '2' THEN 'Hospitalario' END AS AdmissionTypeDescription, F.PatientCode, CASE ing.IPSEXOPAC WHEN '1' THEN 'Hombre' ELSE 'Mujer' END SexDescription, 
	F.TotalInvoice, F.InvoiceDate, f.TotalInvoice TotalValue, 
	RTRIM(LTRIM(ing.UFUCODIGO)) FunctionalUnitCode, ing.UFUDESCRI FunctionalUnitName, 
	salida.FECALTPAC HighMedicalDate, ing.CODDIAEGR DiagnosticCode, CASE F.IsCutAccount WHEN 'True' THEN 'Si' ELSE 'No' END IsCutAccountDescription,
	f.TotalPatientSalesPrice ValueCopay, 
	us.UserCode, us.Fullname UserName, us.UserCode + ' - ' + us.Fullname UserDescription,
	hc.IdThirdParty ThirdPartyId, hc.Id HealthAdministratorId, ga.Id CareGroupId, f.ThirdPartySalesValue EntityValue, ing.IPNOMCOMP PatientName, ing.IPCODPACI + ' - ' + ing.IPNOMCOMP PatientDescription,
	ing.IPFECNACI BirthDate,
	ti.NOMBRE IdentificationTypeDescription,
	ing.IPPRINOMB FirstName, ing.IPSEGNOMB SecondName, ing.IPPRIAPEL FirstLastName, ing.IPSEGAPEL SecondLastName, (cast(datediff(dd, ing.IPFECNACI, GETDATE()) / 365.25 as int)) PatientAge,
	f.AnnulmentUser + ISNULL(' - ' + usAn.Fullname, '') AnnulmentUser, f.AnnulmentDate, brr.Code + ' - ' + brr.Name ReversalReasonDescription
FROM Billing.Invoice AS F WITH (nolock) 
inner join users_cte us on us.UserCode = F.InvoicedUser
inner join Common.ThirdParty AS t WITH (nolock) ON t.Id = F.ThirdPartyId 
left join admission_cte ing on ing.NUMINGRES = F.AdmissionNumber
left join Contract.CareGroup AS ga WITH (nolock) ON ga.Id = F.CareGroupId 
left join health_cte hc on hc.Id = f.HealthAdministratorId
left join Billing.BillingReversalReason brr with(nolock) on brr.Id = f.ReversalReasonId
LEFT JOIN dbo.HCREGEGRE AS salida WITH (nolock) ON salida.NUMINGRES = f.AdmissionNumber AND salida.IPCODPACI = f.PatientCode
left join users_cte usAn on usAn.UserCode = F.AnnulmentUser
LEFT JOIN ADTIPOIDENTIFICA ti WITH(NOLOCK) ON ti.CODIGO =ing.IPTIPODOC