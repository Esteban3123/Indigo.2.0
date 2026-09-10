

CREATE VIEW [Billing].[ViewReportRadicateInvoice]
AS
 
WITH CTE_Officialcurrency AS (
	SELECT TOP 1 cs.OfficialCurrencyId 
	FROM GeneralLedger.CompanySettings cs
),
Cte_TRM AS (
	SELECT * 
	FROM Billing.RevenueControlDetail 
	WHERE IsMasterAccount = 2 AND RevenueControlDetailMasterId IS NOT NULL
)

SELECT 
	CONCAT(I.Id, '-', I.DocumentType) AS Id,  
	I.DocumentType, 
	I.InvoiceDate, 
	I.InvoiceNumber, 
	HA.Id AS HealthAdministratorId, 
	I.PatientCode, 
	pcp.FirstName + ' ' + pcp.SecondName + ' ' + pcp.FirstLastName + ' ' + pcp.SecondLastName as PatientFullName, 
	CG.Id AS CareGroupId, 
	IC.Id AS InvoiceCategoryId, 
	IC.Name AS InvoiceCategory,
	I.TotalInvoice AS TotalInvoice, 
	I.ThirdPartyDiscountValue AS Discount,
	TP.Id AS ThirdPartyId, 
	TP.Nit + '-' + TP.DigitVerification as Nit, 
	TP.Name AS ThirdParty, I.AdmissionNumber, 
	I.InvoicedUser, 
	I.Status,
	PRC.Id AS RadicateInvoiceIdC, 
	PU.Fullname As InvoicedUserName, 
	CENTRO.NOMCENATE,
	dips.BranchOfficeId,
	cu.id CurrencyId,
	cu.Abbreviation CurrencyAbbreviation
FROM Billing.Invoice AS I WITH (NOLOCK) 
INNER JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON I.ThirdPartyId = TP.Id
INNER JOIN Common.Person as cp  WITH (NOLOCK) on cp.Id = tp.PersonId 
INNER JOIN CTE_Officialcurrency cs WITH(NOLOCK) on 1=1
INNER JOIN Common.Currency as cu WITH (NOLOCK) on cu.id = ISNULL(i.CurrencyId,CS.OfficialCurrencyId)
LEFT JOIN Common.Person as pcp  WITH (NOLOCK) on pcp.IdentificationNumber = I.PatientCode 
LEFT JOIN Contract.CareGroup AS CG  WITH (NOLOCK) ON I.CareGroupId = CG.Id 
LEFT JOIN Contract.HealthAdministrator AS HA  WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.Contract AS C  WITH (NOLOCK) ON cg.ContractId = C.Id 
LEFT JOIN Billing.InvoiceCategories as IC WITH (NOLOCK)  ON I.InvoiceCategoryId = IC.Id						  
LEFT JOIN dbo.INPACIENT AS INP  WITH (NOLOCK) ON I.PatientCode = INP.IPCODPACI 
LEFT JOIN dbo.ADINGRESO AS AD WITH (NOLOCK)  ON AD.NUMINGRES = I.AdmissionNumber
LEFT JOIN Security.[UserInt] AS U ON U.UserCode = I.InvoicedUser 
LEFT JOIN Security.PersonInt AS PU ON U.IdPerson = PU.Id 
LEFT JOIN DBO.ADNIVELES AS AN WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO 
LEFT JOIN Portfolio.RadicateInvoiceD AS PRD WITH (NOLOCK) ON PRD.InvoiceNumber = I.InvoiceNumber AND PRD.state <> 4 
LEFT JOIN Portfolio.RadicateInvoiceC AS PRC WITH (NOLOCK) ON PRC.Id = PRD.RadicateInvoiceCId
LEFT JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON I.DocumentType = 7 AND I.Id = dips.InvoiceId
LEFT JOIN dbo.ADCENATEN AS CENTRO WITH (NOLOCK)  ON AD.CODCENATE = CENTRO.CODCENATE
 
UNION ALL
 
SELECT 
	CONCAT(rcd.Id, '-', 8) AS Id,
	8 DocumentType, 
	COALESCE(cte.CreationDate,i.InvoiceDate, rcd.CreationDate)  InvoiceDate,
	NULL InvoiceNumber, 
	HA.Id AS HealthAdministratorId, 
	rc.PatientCode, 
	pcp.FirstName + ' ' + pcp.SecondName + ' ' + pcp.FirstLastName + ' ' + pcp.SecondLastName as PatientFullName, 
	CG.Id AS CareGroupId, 
	NULL InvoiceCategoryId, 
	NULL InvoiceCategory,
	rcd.TotalFolio TotalInvoice, 
	rcd.PatientDiscount AS Discount,
	TP.Id AS ThirdPartyId, 
	TP.Nit + '-' + TP.DigitVerification as Nit, 
	TP.Name AS ThirdParty, 
	rc.AdmissionNumber, 
	i.InvoicedUser InvoicedUser, 
	rcd.Status,
	NULL RadicateInvoiceIdC, 
	pu.Fullname InvoicedUserName, 
	CENTRO.NOMCENATE,
	NULL BranchOfficeId,
	cu.id CurrencyId,
	cu.Abbreviation CurrencyAbbreviation
FROM Billing.RevenueControlDetail rcd
JOIN Billing.RevenueControl rc ON rc.Id = rcd.RevenueControlId
INNER JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON rcd.ThirdPartyId = TP.Id
INNER JOIN Common.Person as cp  WITH (NOLOCK) on cp.Id = tp.PersonId 
INNER JOIN CTE_Officialcurrency cs WITH(NOLOCK) on 1=1
INNER JOIN Common.Currency as cu WITH (NOLOCK) on cu.id = CS.OfficialCurrencyId
LEFT JOIN Common.Person as pcp  WITH (NOLOCK) on pcp.IdentificationNumber = rc.PatientCode
LEFT JOIN Contract.CareGroup AS CG  WITH (NOLOCK) ON rcd.CareGroupId = CG.Id 
LEFT JOIN Contract.HealthAdministrator AS HA  WITH (NOLOCK) ON rcd.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.Contract AS C  WITH (NOLOCK) ON cg.ContractId = C.Id 
LEFT JOIN dbo.INPACIENT AS INP  WITH (NOLOCK) ON rc.PatientCode = INP.IPCODPACI 
LEFT JOIN dbo.ADINGRESO AS AD WITH (NOLOCK)  ON AD.NUMINGRES = rc.AdmissionNumber
LEFT JOIN DBO.ADNIVELES AS AN WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO 
LEFT JOIN dbo.ADCENATEN AS CENTRO WITH (NOLOCK)  ON AD.CODCENATE = CENTRO.CODCENATE
LEFT JOIN Cte_TRM cte ON cte.RevenueControlDetailMasterId = rcd.Id
LEFT JOIN Billing.Invoice i ON i.RevenueControlDetailId = rcd.Id				  
LEFT JOIN Security.[UserInt] AS U ON U.UserCode = i.InvoicedUser
LEFT JOIN Security.PersonInt AS PU ON U.IdPerson = PU.Id 
WHERE (rcd.IsMasterAccount IN (2,4) AND rcd.RevenueControlDetailMasterId IS NOT NULL) OR rcd.IsMasterAccount IN (1,3)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería para el reporte de radicación de facturas. Consolida en un único resultado dos fuentes: las facturas de venta emitidas (Billing.Invoice) y los folios de control de ingresos (Billing.RevenueControlDetail), permitiendo ver en un solo listado tanto facturas convencionales como cuentas de cobro o folios de liquidación que aún no tienen número de factura formal. Para cada registro expone datos clave de negocio como número y fecha de factura, tipo de documento, nombre completo y código del paciente (cédula/identificación), tercero pagador con NIT, grupo de atención, contrato, EPS o administradora de salud, categoría de factura, total facturado, descuentos al paciente, número de ingreso, centro de atención, número de radicación en cartera (RadicateInvoiceC), usuario que facturó y moneda utilizada (aplicando la moneda oficial de la empresa si la factura no tiene moneda propia). Sirve como fuente principal para informes de seguimiento de radicación de facturas ante entidades pagadoras, cuadres de cartera y conciliación de cobros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportRadicateInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportRadicateInvoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en un solo reporte las facturas emitidas y los folios de control de ingresos pendientes/cobrables, enriqueciéndolos con datos del tercero pagador, paciente, admisión, centro de atención, moneda y radicación de cartera.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido (la CTE usa TOP 1 sin filtro y se aplica como INNER JOIN, por lo que sin esta fila la vista no devuelve datos); Toda factura debe tener un ThirdPartyId válido en Common.ThirdParty con su PersonId en Common.Person (INNER JOIN); Todo RevenueControlDetail incluido debe tener su RevenueControl asociado y su ThirdPartyId válido', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa o bien una factura (DocumentType original de Billing.Invoice) o bien un folio de control de ingresos reportado con DocumentType=8; El identificador expuesto siempre se compone como Id-DocumentType para diferenciar facturas y folios en el resultado unificado; La moneda nunca queda nula: si la factura no tiene CurrencyId, se sustituye por la moneda oficial de la empresa (GeneralLedger.CompanySettings.OfficialCurrencyId); Solo se incluyen folios cuya configuración de cuenta maestra sea válida: IsMasterAccount IN (2,4) con maestro asociado o IsMasterAccount IN (1,3); El nombre completo del paciente se construye concatenando los cuatro componentes de Common.Person obtenidos por IdentificationNumber = PatientCode; Las facturas siempre tienen tercero (INNER JOIN obligatorio con Common.ThirdParty); los folios también lo requieren; Las radicaciones solo se reflejan para registros de Billing.Invoice; los folios (DocumentType=8) nunca exponen RadicateInvoiceIdC', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Radicación de factura; Cartera; Folio de facturación; Control de ingresos; Tercero pagador; Administradora de salud (EPS); Grupo de atención; Categoría de factura; Paciente; Admisión/Ingreso; Centro de atención; Moneda oficial; Descuento al paciente; Cuenta maestra (IsMasterAccount); TRM/Tasa de cambio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewReportRadicateInvoice: Devuelve la unión (UNION ALL) de las facturas de Billing.Invoice y de los folios de Billing.RevenueControlDetail filtrados por configuración de cuenta maestra, etiquetando estos últimos con DocumentType=8', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Primera consulta: registros de Billing.Invoice → Se reporta como factura con su DocumentType original y se enlaza a radicación de cartera (Portfolio.RadicateInvoiceD/C) y, si DocumentType=7, a la sede de venta de productos (Inventory.DocumentInvoiceProductSales); si Segunda consulta (UNION ALL): folios de Billing.RevenueControlDetail con (IsMasterAccount IN (2,4) AND RevenueControlDetailMasterId IS NOT NULL) OR IsMasterAccount IN (1,3) → Se reporta como documento tipo 8 (folio/cuenta de cobro), tomando totales del folio (TotalFolio, PatientDiscount) y fecha desde TRM relacionada, factura asociada o el propio folio; si PRD.state <> 4 al unir Portfolio.RadicateInvoiceD → Solo se considera la radicación de cartera cuya línea de detalle no esté en estado 4 (anulada/excluida); si I.DocumentType = 7 al unir Inventory.DocumentInvoiceProductSales → Solo para facturas de venta de productos se obtiene la sede (BranchOfficeId); en otros tipos queda NULL; si ISNULL(I.CurrencyId, CS.OfficialCurrencyId) en la primera consulta → Si la factura no tiene moneda asignada, se usa la moneda oficial de la compañía; si CTE Cte_TRM filtra IsMasterAccount=2 AND RevenueControlDetailMasterId IS NOT NULL → La fecha de creación TRM solo se toma de detalles maestros de tasa con maestro definido (COALESCE prioriza cte.CreationDate, luego i.InvoiceDate, luego rcd.CreationDate)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Billing.RevenueControlDetail; Billing.Invoice; Common.ThirdParty; Common.Person; Common.Currency; Contract.CareGroup; Contract.HealthAdministrator; Contract.Contract; Billing.InvoiceCategories; dbo.INPACIENT; dbo.ADINGRESO; Security.UserInt; Security.PersonInt; dbo.ADNIVELES; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Inventory.DocumentInvoiceProductSales; dbo.ADCENATEN; Billing.RevenueControl', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportRadicateInvoice';
GO
