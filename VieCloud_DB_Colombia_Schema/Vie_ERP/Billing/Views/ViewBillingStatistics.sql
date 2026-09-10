

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadísticas de facturación de la IPS que consolida en una sola consulta toda la información necesaria para análisis y reportería de facturas emitidas. Integra datos de la factura (número, fecha, valor total, tipo de documento, estado facturado o anulado, copago y valor entidad), con el ingreso o admisión del paciente (número de ingreso, fecha de admisión, causa, tipo ambulatorio u hospitalario, diagnóstico de egreso, fecha de alta médica), los datos demográficos del paciente (nombre completo, cédula, tipo de documento, fecha de nacimiento, edad calculada, sexo), el centro de atención, la unidad funcional o servicio, la administradora de salud o EAPB (EPS/ARS) con su NIT y código, el grupo de atención o contrato, el usuario facturador y el usuario que anuló la factura con su motivo de reversa. Es la vista principal para tableros de control, estadísticas de producción y auditoría de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingStatistics';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewBillingStatistics';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada para análisis estadístico de facturación que enriquece cada factura con datos de admisión, paciente, administradora de salud, unidad funcional, egreso y usuarios facturador/anulador, traduciendo códigos a descripciones legibles.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener un usuario facturador (InvoicedUser) que exista en Security.User, dado el INNER JOIN con users_cte.; La factura debe tener un ThirdParty asociado (INNER JOIN Common.ThirdParty).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se calcula como DATEDIFF(dd, IPFECNACI, GETDATE())/365.25 truncado a entero.; Solo se muestran facturas cuyo usuario facturador y tercero existen (INNER JOIN), garantizando integridad referencial básica.; Los datos de admisión, grupo de cuidado, administradora de salud, motivo de reversión y egreso son opcionales (LEFT JOIN), por lo que pueden ser NULL si la factura no los tiene asociados.; Los campos descriptivos compuestos siguen el patrón ''Código - Nombre'' (CareCenter, ThirdParty, HealthAdministrator, CareGroup, User, Patient, ReversalReason).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Anulación/Reversión de factura; Admisión/Ingreso del paciente; Causa de ingreso; Tipo de ingreso (Ambulatorio/Hospitalario); Centro de atención; Unidad funcional; Administradora de salud (EAPB/EPS); Grupo de atención; Tipo de documento de factura (Capitada, Particular, EAPB); Copago (TotalPatientSalesPrice); Corte de cuenta; Egreso/Alta médica; Diagnóstico de egreso; Tipo de identificación del paciente; Edad del paciente; Tercero pagador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewBillingStatistics: Devuelve una fila por factura en Billing.Invoice con campos descriptivos derivados de catálogos y maestros relacionados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si f.Status = 1 → StatusDescription = ''Facturado'' else StatusDescription = ''Anulado''; si f.DocumentType (1..7) → Mapea a tipo de factura: 1=EAPB con Contrato, 2=EAPB sin Contrato, 3=Particular, 4=Capitada, 5=Control de Capitación, 6=Básica, 7=Venta de Productos; si ing.ICAUSAING (1..11) → Traduce causa de ingreso: Heridos en Combate, Enfermedad Profesional, Enfermedad General Adulto/Pediatría, Odontología, Accidente Tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada; si ing.TIPOINGRE = 1 → AdmissionTypeDescription = ''Ambulatorio'' else ''Hospitalario'' si TIPOINGRE = 2; si ing.IPSEXOPAC = 1 → SexDescription = ''Hombre'' else SexDescription = ''Mujer''; si F.IsCutAccount = ''True'' → IsCutAccountDescription = ''Si'' else ''No''; si F.AnnulmentUser tiene Fullname asociado en users_cte → AnnulmentUser concatena código y nombre completo del anulador else Solo se muestra el código (ISNULL devuelve cadena vacía)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Security.Person; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; Contract.HealthAdministrator; Common.ThirdParty; Billing.Invoice; Contract.CareGroup; Billing.BillingReversalReason; dbo.HCREGEGRE; dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewBillingStatistics';
GO
