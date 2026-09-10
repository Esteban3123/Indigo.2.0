
CREATE VIEW [Billing].[ViewElectronicSupportDocumentHeader]
AS

WITH Cte_PickAddress AS (
    SELECT
        ad.IdPerson,
        MIN(ad.Id) AS AddressId
    FROM Common.[Address] ad WITH (NOLOCK)
    WHERE ad.[State] = 1
    GROUP BY ad.IdPerson
),
Cte_AdressSupplier AS (
    SELECT 
        a.IdPerson, 
        a.Addresss AS AddressSupplier,
        LEFT(RTRIM(ISNULL(d.Code, '')), 1) DepartmentCode,
        RIGHT('00' + LEFT(LTRIM(RTRIM(ci.Code)), 2), 2) AS CityCode
    FROM Cte_PickAddress pa
    JOIN Common.Address a WITH(NOLOCK) ON pa.AddressId = a.Id
    LEFT JOIN Common.City ci ON a.CityId = ci.Id
    LEFT JOIN Common.Department d ON a.DepartmentId = d.Id

    GROUP BY a.IdPerson, a.Addresss, d.Code, ci.Code
),
Cte_EconomicActivity AS(
    SELECT 
        tpea.ThirdPartyId, 
        tpea.EconomicActivityId, 
        MIN(ea.Code) EconomicActivityCode
    FROM Common.ThirdPartyEconomicActivities tpea WITH (NOLOCK)
    JOIN Common.EconomicActivity ea WITH (NOLOCK) ON tpea.EconomicActivityId = ea.Id
    GROUP BY ThirdPartyId, EconomicActivityId, ea.Code
),
Cte_MainDataSupplier AS (
    SELECT 
        p.Id AS PersonId,
        tp.Id AS ThirdPartyId,
        tp.Nit AS ThirdPartyIdentificationNumber,
        tp.[Name] AS ThirdPartyName,
        CASE it.SIGLA
            WHEN 'CF' THEN '01'
            WHEN 'CJ' THEN '02'
            WHEN 'DM' THEN '03'
            WHEN 'NI' THEN '04'
            WHEN 'PA' THEN '03'
            WHEN 'ND' THEN '05'
            WHEN 'NC' THEN '06'
            ELSE '00'
        END AS IdentificationType,
  CASE 
    WHEN tp.Class = 2 OR it.SIGLA = 'PA'THEN 1 ELSE 0
    END AS IsForeign,
        ISNULL(ea.EconomicActivityCode, '') AS EconomicActivityCode
    FROM Common.ThirdParty tp WITH (NOLOCK)
    JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
    JOIN ADTIPOIDENTIFICA it WITH (NOLOCK) ON p.IdentificationTypeId = it.Id
    LEFT JOIN Cte_EconomicActivity ea WITH (NOLOCK) ON tp.Id = ea.ThirdPartyId
),
Cte_EconomyActivitySupplier AS (
    SELECT 
        vt.Id AS VoucherTransactionId,
        MIN(ea.Code) AS EconomicActivityTransactionCode
    FROM Treasury.VoucherTransaction vt
    JOIN Treasury.VoucherTransactionDetails vtd ON vt.Id = vtd.IdVoucherTransaction
    LEFT JOIN Common.EconomicActivity ea ON vtd.EconomicActivityId = ea.Id
    GROUP BY vt.Id
),
Cte_CurrencyOfficial AS(
    SELECT co.Abbreviation
    FROM GeneralLedger.CompanySettings cs
    JOIN Common.Currency co WITH (NOLOCK) ON co.Id = cs.OfficialCurrencyId
)

--CUENTAS POR PAGAR
	SELECT
		  -- Datos generales del documento electrónico
		  esd.Id AS Id,
		  ap.Id AS EntityId,
		  ap.Code AS EntityCode,
		  'AccountPayable' AS EntityName,
		  ----------------------------------
		  ISNULL(ea.Code, '') AS EconomyActivityCode,
		  '' AS TaxRegistrationDrinks,
		  '08' AS DocumentType,	 
		  ap.Coments AS Comment,
		  '' AS AnotherContent,
		  '' AS AnotherComment,
		  IIF(esd.StatusElectronic IN (2,3), esd.DocumentNumber, '') AS DocumentNumber,
		  IIF(esd.StatusElectronic IN (2,3), esd.CUDS, '') AS DocumentKey,
		  0 AS BatchId,
		  ap.BillNumber AS SupplierInvoiceNumber,
		  ap.BillDate AS SupplierInvoiceDate,
		  -- Datos del receptor
		  s.ThirdPartyName AS ReceiverName,
		  s.IdentificationType AS ReceiverIdentificationType,
		  CAST(s.IsForeign AS BIT) AS SupplierIsForeign,
		  IIF(s.IsForeign = 0, s.ThirdPartyIdentificationNumber, '') AS ReceiverIdentificationNumber,
		  IIF(s.IsForeign = 1, s.ThirdPartyIdentificationNumber, '') AS ReceiverForeignIdentification,
		  s.ThirdPartyName AS ReceiverTradeName,
		  ISNULL(s.EconomicActivityCode, '') AS ReceiverEconomicActivity,
		  IIF(s.IsForeign = 0, ISNULL(ads.DepartmentCode, ''), '') AS Province,
		  IIF(s.IsForeign = 0, ISNULL(ads.CityCode, ''), '') AS Township,
		  IIF(s.IsForeign = 0, ISNULL(ads.CityCode, ''), '') AS District,
		  IIF(s.IsForeign = 0, 'Barrio', '') AS Neighborhood,
		  IIF(s.IsForeign = 0, ISNULL(ads.AddressSupplier, ''), '') AS ReceiverOtherAddresses,
		  IIF(s.IsForeign = 1, ISNULL(ads.AddressSupplier, ''), '') AS ReceiverForeignOtherAddresses,
		  0 AS ReceiverPhoneCountryCode,
		  0 AS ReceiverPhoneNumber,
		  '' AS ReceiverEmailAddress,
		  '' AS EmailCopyAddress,
		  '01' AS SaleCondition,
		  '' AS OtherSaleCondition,
		  CAST(ap.Term AS VARCHAR(20)) AS CreditDeadline,
		  -- Medios de pago
		  '04' AS PaymentMethods, --	Transferencia O 99 oTROS
		  ap.Value AS TotalPaymentMethods,
		  -- Moneda
		  ISNULL(c.Abbreviation, cu.Abbreviation) AS CurrencyAbreviation,
		  IIF(ap.CurrencyId = 2,  trm.ValueOfficialToCurrency,CAST(1 AS DECIMAL(18,2))) AS TRMValue,
		  -- Totales
		  CAST(0 AS DECIMAL(18,2)) AS TotalSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalDiscounts,
		  CAST(0 AS DECIMAL(18,2)) AS TotalNetSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalTax,
		  CAST(0 AS DECIMAL(18,2)) AS TotalReceipt --Total de comprobante
    FROM Billing.ElectronicSupportDocument esd WITH(NOLOCK)
    INNER JOIN Payments.AccountPayable ap WITH(NOLOCK) ON ap.Id = esd.EntityId and esd.EntityName = 'AccountPayable'	
	INNER JOIN Cte_MainDataSupplier s ON esd.SupplierThirdPartyId = s.ThirdPartyId
	LEFT JOIN Cte_AdressSupplier ads ON s.PersonId = ads.IdPerson
	JOIN Cte_CurrencyOfficial cu ON 1 = 1
	LEFT JOIN Common.EconomicActivity ea WITH(NOLOCK) ON ap.IdEconomicActivity = ea.Id
	LEFT JOIN Common.Currency c WITH(NOLOCK) ON ap.CurrencyId = c.Id
	LEFT JOIN Payments.AccountPayableExchangeRate aper WITH(NOLOCK) ON ap.Id = aper.AccountPayableId
	LEFT JOIN Common.TRM trm on trm.MeasurementDate = convert(date,esd.DocumentDate)
    WHERE ap.HandlesDocumentSupport = 1	

	UNION ALL

	--COMPROBANTE DE EGRESO
	SELECT
		  -- Datos generales del documento electrónico
		  esd.Id AS Id,
		  vt.Id AS EntityId,
		  vt.Code AS EntityCode,
		  'VoucherTransaction' AS EntityName,
		  ----------------------------------
		  --ISNULL(ea.Code, '') AS EconomyActivityCode,
		  ISNULL(eas.EconomicActivityTransactionCode, '') EconomyActivityCode,
		  '' AS TaxRegistrationDrinks,
		  '08' AS DocumentType,	 
		  vt.Detail AS Comment,
		  '' AS AnotherContent,
		  '' AS AnotherComment,
		  IIF(esd.StatusElectronic IN (2,3), esd.DocumentNumber, '') AS DocumentNumber,
		  IIF(esd.StatusElectronic IN (2,3), esd.CUDS, '') AS DocumentKey,
		  0 AS BatchId,
		  '' AS SupplierInvoiceNumber,
		  CAST(NULL AS DATETIME) AS SupplierInvoiceDate,
		  -- Datos del receptor
		  s.ThirdPartyName AS ReceiverName,
		  s.IdentificationType AS ReceiverIdentificationType,
		  CAST(s.IsForeign AS BIT) AS SupplierIsForeign,
		  IIF(s.IsForeign = 0, s.ThirdPartyIdentificationNumber, '') AS ReceiverIdentificationNumber,
		  IIF(s.IsForeign = 1, s.ThirdPartyIdentificationNumber, '') AS ReceiverForeignIdentification,
		  s.ThirdPartyName AS ReceiverTradeName,
		  ISNULL(s.EconomicActivityCode, '') AS ReceiverEconomicActivity,
		  IIF(s.IsForeign = 0, ads.DepartmentCode, '') AS Province,
		  IIF(s.IsForeign = 0, ads.CityCode, '') AS Township,
		  IIF(s.IsForeign = 0, ads.CityCode, '') AS District,
		  IIF(s.IsForeign = 0, 'Barrio', '') AS Neighborhood,
		  IIF(s.IsForeign = 0, ads.AddressSupplier, '') AS ReceiverOtherAddresses,
		  IIF(s.IsForeign = 1, ads.AddressSupplier, '')  AS ReceiverForeignOtherAddresses,
		  0 AS ReceiverPhoneCountryCode, 
		  0 AS ReceiverPhoneNumber,	
		  '' AS ReceiverEmailAddress,
		  '' AS EmailCopyAddress,
		  '01' AS SaleCondition,
		  '' AS OtherSaleCondition,
		  '0' AS CreditDeadline,
		  -- Medios de pago
		  CASE 
			WHEN vt.IdCashRegister IS NOT NULL AND vt.ExpenseType IN (2, 3) 
				THEN '01'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 1 
				THEN '03' -- Cheque = 1, Nota débito = 2, PSE = 3'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 2 
				THEN '04'
			WHEN vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 3
				THEN '03'
			ELSE '99'
		  END AS PaymentMethods,
		  vt.[Value] AS TotalPaymentMethods,
		  -- Moneda
		  ISNULL(c.Abbreviation, cu.Abbreviation) CurrencyAbreviation,
		  CAST(1 AS DECIMAL(18,2)) AS TRMValue,
		  -- Totales
		  CAST(0 AS DECIMAL(18,2)) AS TotalSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalDiscounts,
		  CAST(0 AS DECIMAL(18,2)) AS TotalNetSales,
		  CAST(0 AS DECIMAL(18,2)) AS TotalTax,
		  CAST(0 AS DECIMAL(18,2)) AS TotalReceipt --Total de comprobante
    from Billing.ElectronicSupportDocument esd
	INNER JOIN Treasury.VoucherTransaction vt WITH(NOLOCK) on vt.Id = esd.EntityId and esd.EntityName ='VoucherTransaction'
	INNER JOIN Cte_MainDataSupplier s ON esd.SupplierThirdPartyId = s.ThirdPartyId
	LEFT JOIN Cte_AdressSupplier ads ON s.PersonId = ads.IdPerson
	JOIN Cte_CurrencyOfficial cu ON 1 = 1
	LEFT JOIN Cte_EconomyActivitySupplier eas ON eas.VoucherTransactionId = vt.Id
	LEFT JOIN Common.Currency c WITH(NOLOCK) ON vt.CurrencyId = c.Id
	where vt.HandlesDocumentSupport = 1 AND vt.VoucherClass = 1 -- 1 - Pago; 2 - Reembolso; 3 - Traslado	
	
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el encabezado de los documentos de soporte electrónico emitidos por la organización, integrando tanto cuentas por pagar (AccountPayable) como comprobantes de egreso (VoucherTransaction). Para cada documento reúne los datos del receptor o proveedor (nombre, tipo y número de identificación, actividad económica, dirección con departamento y ciudad), la moneda oficial, condiciones de venta, medios de pago y totales del comprobante. Para cuentas por pagar expone además el número y la fecha de la factura del proveedor (SupplierInvoiceNumber/SupplierInvoiceDate, tomados de Payments.AccountPayable.BillNumber/BillDate), usados para poblar el nodo de referencia del documento electrónico; para comprobantes de egreso estos campos se devuelven vacíos/nulos por no existir un concepto de factura de proveedor equivalente. También expone SupplierIsForeign (derivado de Common.ThirdParty.Class = 2 o tipo de identificación pasaporte), usado por el consumidor para determinar el tipo de identificación (05 Extranjero No Domiciliado / 06 No Contribuyente) y el tipo de documento de referencia (16 / 14) del segmento correspondiente al proveedor en la Factura Electrónica de Compra. Compone la información cruzando el documento electrónico con el tercero proveedor, su persona natural asociada, el tipo de documento de identidad, la actividad económica y la dirección activa más antigua registrada. Sirve como fuente principal para la generación y transmisión de documentos de soporte electrónico ante la DIAN, en procesos de facturación electrónica y tesorería.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicSupportDocumentHeader';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewElectronicSupportDocumentHeader';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola estructura los encabezados de documentos soporte electrónicos originados tanto en cuentas por pagar como en comprobantes de egreso de tesorería, normalizando datos del receptor, dirección, actividad económica, moneda y medio de pago para su transmisión electrónica.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DocumentType siempre es ''08'' (documento soporte electrónico); SaleCondition siempre es ''01'' y TRMValue siempre 1; Totales TotalSales, TotalDiscounts, TotalNetSales, TotalTax y TotalReceipt siempre se devuelven en 0; Para cuentas por pagar el medio de pago siempre es ''04'' (transferencia); Sólo se incluyen comprobantes de egreso de clase 1 (Pago); se excluyen Reembolso (2) y Traslado (3); La identificación del receptor se segrega: nacional (IsForeign=0) en ReceiverIdentificationNumber, extranjera (IsForeign=1) en ReceiverForeignIdentification; DocumentNumber y CUDS sólo se exponen cuando StatusElectronic está en {2,3}; CityCode se normaliza a 2 caracteres con padding de ceros a la izquierda y DepartmentCode usa solo el primer carácter del código; Por persona se selecciona la dirección activa de menor Id como dirección oficial del proveedor; Neighborhood literal ''Barrio'' para receptores nacionales; Para cuentas por pagar, SupplierInvoiceNumber/SupplierInvoiceDate se toman de Payments.AccountPayable.BillNumber/BillDate; para comprobantes de egreso (VoucherTransaction) SupplierInvoiceNumber siempre es cadena vacía y SupplierInvoiceDate siempre es NULL, por no existir una factura de proveedor asociada', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Cuenta por pagar; Comprobante de egreso; Tercero proveedor; Tipo de identificación; Tercero extranjero; Actividad económica; Medio de pago (efectivo, cheque, transferencia, PSE); Condición de venta; Moneda oficial; CUDS; Plazo de crédito; Factura del proveedor (número y fecha) para el nodo de referencia del documento electrónico', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewElectronicSupportDocumentHeader: Devuelve un encabezado por documento soporte electrónico, uniendo cuentas por pagar (DocumentType ''08'', PaymentMethods fijo ''04'') y comprobantes de egreso de tesorería con medio de pago calculado según IdCashRegister/IdEntityBankAccount/PaymentMethod.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si it.SIGLA en (''CF'',''CJ'',''DM'',''NI'',''PA'') → Mapea a tipo de identificación electrónico ''01'',''02'',''03'',''04'',''03'' respectivamente else Tipo de identificación ''00''; si tp.Class = 2 (Extranjero) AND it.SIGLA = ''PA'' → Marca IsForeign = 1 y expone identificación/dirección en campos ''Foreign'' else IsForeign = 0; expone identificación y dirección en campos nacionales con Province/Township/District/Neighborhood; si esd.StatusElectronic IN (2,3) → Expone DocumentNumber y CUDS reales (DocumentKey) else DocumentNumber y DocumentKey se devuelven en blanco; si ap.CurrencyId = 2 (rama AccountPayable) → TRMValue = trm.ValueOfficialToCurrency según fecha del documento else TRMValue = 1; si vt.IdCashRegister IS NOT NULL AND vt.ExpenseType IN (2,3) → PaymentMethods = ''01'' (efectivo/caja) else Evalúa siguientes ramas según IdEntityBankAccount y PaymentMethod; si vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 1 (Cheque) → PaymentMethods = ''03''; si vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 2 (Nota débito) → PaymentMethods = ''04''; si vt.IdEntityBankAccount IS NOT NULL AND vt.PaymentMethod = 3 (PSE) → PaymentMethods = ''03'' else PaymentMethods = ''99'' (otros); si EntityName = ''AccountPayable'' → Toma EntityCode/Comment/Term/Value de Payments.AccountPayable y EconomyActivityCode de ap.IdEconomicActivity else Si EntityName = ''VoucherTransaction'', toma datos de Treasury.VoucherTransaction y EconomyActivityCode del MIN(Code) de las actividades económicas de sus detalles', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Address; Common.City; Common.Department; Common.ThirdPartyEconomicActivities; Common.EconomicActivity; Common.ThirdParty; Common.Person; ADTIPOIDENTIFICA; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; GeneralLedger.CompanySettings; Common.Currency; Billing.ElectronicSupportDocument; Payments.AccountPayable; Payments.AccountPayableExchangeRate; Common.TRM', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewElectronicSupportDocumentHeader';
GO
