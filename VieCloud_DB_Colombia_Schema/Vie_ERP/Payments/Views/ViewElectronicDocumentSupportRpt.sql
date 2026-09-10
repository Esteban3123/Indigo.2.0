

CREATE view [Payments].[ViewElectronicDocumentSupportRpt]
as
	SELECT
        apc.Id,
        esd.Code,
        esd.DocumentNumber,
        tc.[Name] as NameCustomer,
        tc.Nit as NitCustomer,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = tc.PersonId and [State] = 1) as AddresssCustomer,
        (select top 1 Phone from Common.Phone where IdPerson =  tc.PersonId) as PhoneCustomer,
		(select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = tc.PersonId and a.[State] = 1) as CityCustomer,
        ts.Nit as NitSupplier,
        ts.[Name] as NameSupplier,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = ts.PersonId and [State] = 1) as AddresssSupplier,
        (select top 1 Phone from Common.Phone where IdPerson =  ts.PersonId and [State] = 1) as PhoneSuppler,
        (select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = ts.PersonId and a.[State] = 1) as CitySupplier,
        ap.id as IdParentDocument,
        ap.DocumentDate,
        ap.Term,
        'Crédito' as PaymentWay,
        'Acuerdo Mutuo' as PaymentMethod,
        case esd.StatusElectronic
            when 0 then 'Invalida'
            when 1 then 'Registrada'
            when 2 then 'Enviada'
            when 3 then 'Valida'
            when 4 then 'Validacion Fallida'
            else 'Pendiente'
        end as [State],
        ap.Code as ParentDocumentCode,
        iif(esd.StatusElectronic = 3, esd.ShippingDate, null) as ValidationDate,
        esd.CUDS,
        esd.QR,
        ap.Coments as Observations,
		esd.SubTotalValue,
		esd.TaxValue,
        ap.[Value] as Total,
        ba.ResolutionNumber,
        convert(varchar, ba.ResolutionDate, 105) as ResolutionDate,
        ba.InvoicePrefix,
        ba.InitialInvoice,
        ba.FinalInvoice,
        convert(varchar, ba.InitialDate, 103) as InitialDate,
        convert(varchar, ba.FinalDate, 103) as FinalDate,
		concat(c.Code, ' - ', c.[Name]) as Concept,
        concat(ma.Number, ' - ' ,ma.[Name]) as Account,
        t.[Name] as ThirdParty,
        t.Nit,
        concat(cc.Code, '  ', cc.[Name]) as CostCenter,
        IIF(apc.Nature = 1, apc.[Value], 0) as DebitValue,
        IIF(apc.Nature = 2, apc.[Value], 0) as CreditValue,
        ma.RetencionType,
		esd.EntityName,
		ap.EntityId as SourceEntityId,
		ap.EntityCode as SourceEntityCode,
		ap.EntityName as SourceEntityName
    from Billing.ElectronicSupportDocument esd
    inner join Billing.BillingAuthorization ba on ba.Id = esd.BillingAuthorizationId
    inner join Common.ThirdParty ts on ts.Id = esd.SupplierThirdPartyId
    inner join Common.ThirdParty tc on tc.Id = esd.CustomerThirdPartyId
    inner join Payments.AccountPayable ap on ap.Id = esd.EntityId and esd.EntityName = 'AccountPayable'	
	inner join Payments.AccountPayableDetailConcept apc (nolock) on apc.IdAccountPayable = ap.Id
    inner join Payments.AccountPayableConcepts c on c.Id = apc.IdConceptAccountPayable
    inner join GeneralLedger.MainAccounts ma on ma.Id = apc.IdAccount
    left join Common.ThirdParty t on t.Id = apc.IdThirdParty
    left join Payroll.CostCenter cc on cc.Id = apc.IdCostCenter
    where ap.HandlesDocumentSupport = 1

	UNION ALL

	SELECT
        vtd.Id,
        esd.Code,
        esd.DocumentNumber,
        tc.[Name] as NameCustomer,
        tc.Nit as NitCustomer,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = tc.PersonId and [State] = 1) as AddresssCustomer,
        (select top 1 Phone from Common.Phone where IdPerson =  tc.PersonId) as PhoneCustomer,
		(select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = tc.PersonId and a.[State] = 1) as CityCustomer,
        ts.Nit as NitSupplier,
        ts.[Name] as NameSupplier,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = ts.PersonId and [State] = 1) as AddresssSupplier,
        (select top 1 Phone from Common.Phone where IdPerson =  ts.PersonId and [State] = 1) as PhoneSuppler,
        (select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = ts.PersonId and a.[State] = 1) as CitySupplier,
        vt.id as IdParentDocument,
        vt.DocumentDate,
        0 Term,
        'Contado' as PaymentWay,
        'Acuerdo Mutuo' as PaymentMethod,
        case esd.StatusElectronic
            when 0 then 'Invalida'
            when 1 then 'Registrada'
            when 2 then 'Enviada'
            when 3 then 'Valida'
            when 4 then 'Validacion Fallida'
            else 'Pendiente'
        end as [State],
        vt.Code as ParentDocumentCode,
        iif(esd.StatusElectronic = 3, esd.ShippingDate, null) as ValidationDate,
        esd.CUDS,
        esd.QR,
        vt.Detail as Observations,
		esd.SubTotalValue,
		esd.TaxValue,
        vt.Value as Total,
        ba.ResolutionNumber,
        convert(varchar, ba.ResolutionDate, 105) as ResolutionDate,
        ba.InvoicePrefix,
        ba.InitialInvoice,
        ba.FinalInvoice,
        convert(varchar, ba.InitialDate, 103) as InitialDate,
        convert(varchar, ba.FinalDate, 103) as FinalDate,
		concat(c.Code, ' - ', c.Description) as Concept,
        concat(ma.Number, ' - ' ,ma.[Name]) as Account,
        t.[Name] as ThirdParty,
        t.Nit,
        concat(cc.Code, '  ', cc.[Name]) as CostCenter,
        IIF(vtd.Nature = 1, vtd.[Value], 0) as DebitValue,
        IIF(vtd.Nature = 2, vtd.[Value], 0) as CreditValue,
        ma.RetencionType,
		esd.EntityName,
		0 as SourceEntityId,
		'' as SourceEntityCode,
		'' as SourceEntityName
    from Billing.ElectronicSupportDocument esd
    inner join Billing.BillingAuthorization ba on ba.Id = esd.BillingAuthorizationId
    inner join Common.ThirdParty ts WITH(NOLOCK) on ts.Id = esd.SupplierThirdPartyId
    inner join Common.ThirdParty tc WITH(NOLOCK) on tc.Id = esd.CustomerThirdPartyId
	INNER join Treasury.VoucherTransaction vt WITH(NOLOCK) on vt.Id = esd.EntityId and esd.EntityName ='VoucherTransaction'
	INNER join Treasury.VoucherTransactionDetails vtd WITH(NOLOCK) on vt.Id = vtd.IdVoucherTransaction
	INNER join Treasury.ExpenseConcepts c WITH(NOLOCK) on vtd.IdExpenseConcept=c.Id
	inner join GeneralLedger.MainAccounts ma on ma.Id = vtd.IdMainAccount
	left join Common.ThirdParty t on t.Id = vtd.IdThirdParty
    left join Payroll.CostCenter cc on cc.Id = vtd.IdCostCenter
	where vt.HandlesDocumentSupport =1

	UNION ALL 

	SELECT 
		esdan.Id, 
		esdan.Code,
		esd.DocumentNumber,
		tpc.Name NameCustomer,
		tpc.Nit NitCustomer,
		(select top 1 [Addresss] from Common.[Address] where IdPerson = tpc.PersonId and [State] = 1) as AddresssCustomer,
        (select top 1 Phone from Common.Phone where IdPerson =  tpc.PersonId) as PhoneCustomer,
		(select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = tpc.PersonId and a.[State] = 1) as CityCustomer,
		tps.Nit NitSupplier,
		tps.Name NameSupplier,
		(select top 1 [Addresss] from [Common].[Address] a where a.IdPerson = tps.PersonId AND a.[State] = 1) AddresssSupplier,
		(select top 1 Phone from Common.Phone where IdPerson =  tps.PersonId) PhoneSuppler,
		(select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = tps.PersonId and a.[State] = 1) CitySupplier,
		esd.EntityId IdParentDocument,
		esdan.DocumentDate,
		0 Term,
		NULL PaymentWay,
		NULL PaymentMethod,
		case esdan.StatusElectronic
            when 0 then 'Invalida'
            when 1 then 'Registrada'
            when 2 then 'Enviada'
            when 3 then 'Valida'
            when 4 then 'Validacion Fallida'
            else 'Pendiente'
        end as [State],
		pn.Code ParentDocumentCode,
		iif(esdan.StatusElectronic = 3, esd.ShippingDate, null) as ValidationDate,
		esdan.CUDS,
		esdan.QR,
		pn.Comment Observations,
		esdan.SubTotalValue,
		esdan.TaxValue,
		esdan.TotalValue Total,
		NULL ResolutionNumber,
		NULL ResolutionDate,
		NULL InvoicePrefix,
		NULL InitialInvoice,
		NULL FinalInvoice,
        NULL InitialDate,
        NULL FinalDate,
		CASE esdan.NoteType
			WHEN 1 THEN 'Devolución parcial de los bienes y/o no aceptación parcial del servicio'
			WHEN 2 THEN 'Anulación del documento soporte'
			WHEN 3 THEN 'Rebaja o descuento parcial o total'
			WHEN 4 THEN 'Ajuste de precios'
			WHEN 5 THEN 'Otros'
			ELSE ''
		END Concept,
        NULL as Account,
        NULL as ThirdParty,
        NULL Nit,
        NULL CostCenter,
        NULL DebitValue,
        NULL CreditValue,
        NULL RetencionType,
		esdan.EntityName,
		0 as SourceEntityId,
		'' as SourceEntityCode,
		'' as SourceEntityName
	FROM Billing.ElectronicSupportDocumentAdjustmentNote esdan 
	INNER JOIN Billing.ElectronicSupportDocument esd WITH (NOLOCK) ON esd.Id = esdan.ElectronicSupportDocumentId
	INNER JOIN Common.ThirdParty tps WITH (NOLOCK) ON tps.Id = esd.SupplierThirdPartyId
	INNER JOIN Common.ThirdParty tpc WITH (NOLOCK) ON tpc.Id = esd.CustomerThirdPartyId
	LEFT JOIN Payments.PaymentNotes pn WITH (NOLOCK) ON pn.Id = esdan.EntityId AND 'PaymentNotes' = esdan.EntityName
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para reportería de documentos soporte electrónicos emitidos ante la DIAN, que consolida en un único resultado dos tipos de documentos: cuentas por pagar a proveedores (pago a crédito) y comprobantes de egreso o vales de tesorería (pago de contado). Para cada documento soporte integra los datos del proveedor (NIT, nombre, dirección, teléfono, ciudad), los datos del adquirente o cliente, la resolución de autorización DIAN (prefijo, rango de numeración, fechas de vigencia), el estado de transmisión electrónica (pendiente, registrada, enviada, válida, validación fallida), el CUDS y QR del documento digital, los valores subtotal e impuestos, y el detalle contable por concepto de cuenta por pagar con su cuenta contable, tercero, centro de costo y naturaleza débito/crédito. Sirve para generar informes y auditorías de documentos soporte electrónicos en el proceso de compras y pagos a proveedores.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentSupportRpt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentSupportRpt';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información necesaria para reportes de documentos soporte electrónicos (compras a crédito, pagos de contado y notas de ajuste), unificando datos del cliente, proveedor, autorización DIAN, conceptos contables y estado electrónico.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento soporte electrónico debe estar asociado a una autorización de facturación (BillingAuthorization); Para la rama de cuentas por pagar: AccountPayable.HandlesDocumentSupport = 1 y EntityName = ''AccountPayable''; Para la rama de tesorería: VoucherTransaction.HandlesDocumentSupport = 1 y EntityName = ''VoucherTransaction''; Para notas de ajuste: la nota debe estar vinculada a un ElectronicSupportDocument existente; opcionalmente referenciada en PaymentNotes con EntityName = ''PaymentNotes''; Las direcciones y ciudades se obtienen únicamente de registros con State = 1 (activos)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La forma de pago siempre es ''Crédito'' para cuentas por pagar y ''Contado'' para transacciones de tesorería; el método de pago es siempre ''Acuerdo Mutuo'' en ambos casos; Las notas de ajuste no incluyen información de autorización DIAN ni desglose contable (resolución, cuenta, tercero, centro de costo, débito/crédito quedan en NULL); Solo se consideran direcciones, teléfonos y ciudades activas (State = 1) al traer datos de contacto del cliente y proveedor; El estado electrónico nunca queda numérico: siempre se materializa como etiqueta textual; Las fechas de resolución se formatean como dd-MM-yyyy (estilo 105) y las de vigencia como dd/MM/yyyy (estilo 103); Para notas de ajuste, los campos SourceEntityId/Code/Name se reportan vacíos (0/'''')', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Autorización de facturación DIAN (resolución, prefijo, rango de numeración); Cuenta por pagar a proveedor; Transacción de comprobante de tesorería (egreso); Nota de ajuste a documento soporte (devolución, anulación, descuento, ajuste de precio); Concepto contable / cuenta del PUC; Centro de costo; Tercero proveedor / cliente; Naturaleza débito/crédito; Tipo de retención; CUDS y QR de facturación electrónica; Forma de pago (Crédito/Contado) y método de pago (Acuerdo Mutuo)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve la unión (UNION ALL) de tres orígenes: documentos soporte ligados a AccountPayable (forma de pago ''Crédito''), a VoucherTransaction (forma de pago ''Contado'') y a notas de ajuste de documento soporte (NoteType traducido a descripción).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si esd.StatusElectronic IN (0,1,2,3,4) → Traduce el estado electrónico a etiqueta: 0=Invalida, 1=Registrada, 2=Enviada, 3=Valida, 4=Validacion Fallida else Cualquier otro valor se rotula como ''Pendiente''; si esd.StatusElectronic = 3 → Expone ShippingDate como ValidationDate else ValidationDate queda en NULL; si ap.HandlesDocumentSupport = 1 AND esd.EntityName = ''AccountPayable'' → Incluye el documento como compra a crédito con detalle de AccountPayableDetailConcept; si vt.HandlesDocumentSupport = 1 AND esd.EntityName = ''VoucherTransaction'' → Incluye el documento como pago de contado con detalle de VoucherTransactionDetails; si apc.Nature = 1 (o vtd.Nature = 1) → El valor se reporta como DebitValue else Se reporta como 0 en débito; si apc.Nature = 2 (o vtd.Nature = 2) → El valor se reporta como CreditValue else Se reporta como 0 en crédito; si esdan.NoteType IN (1..5) → Traduce el tipo de nota: 1=Devolución parcial, 2=Anulación, 3=Rebaja/descuento, 4=Ajuste de precios, 5=Otros else Concepto vacío ('''')', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ElectronicSupportDocument; Billing.BillingAuthorization; Billing.ElectronicSupportDocumentAdjustmentNote; Common.ThirdParty; Common.Address; Common.Phone; Common.City; Payments.AccountPayable; Payments.AccountPayableDetailConcept; Payments.AccountPayableConcepts; Payments.PaymentNotes; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.ExpenseConcepts; GeneralLedger.MainAccounts; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportRpt';
GO
