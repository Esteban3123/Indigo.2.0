

CREATE view [Payments].[ViewElectronicDocumentSupportAccountPayable]
as
	SELECT
        apc.Id,
        esd.Code,
        esd.DocumentNumber,
        tc.[Name] as NameCustomer,
        tc.Nit as NitCustomer,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = tc.PersonId and [State] = 1) as AddresssCustomer,
        (select top 1 Phone from Common.Phone where IdPerson =  tc.PersonId) as PhoneCustomer,
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
		ap.EntityId as AccountPayableEntityId,
		ap.EntityCode as AccountPayableEntityCode,
		ap.EntityName as AccountPayableEntityName
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
        ts.Nit as NitSupplier,
        ts.[Name] as NameSupplier,
        (select top 1 [Addresss] from Common.[Address] where IdPerson = ts.PersonId and [State] = 1) as AddresssSupplier,
        (select top 1 Phone from Common.Phone where IdPerson =  ts.PersonId and [State] = 1) as PhoneSuppler,
        (select top 1 c.[Name] from Common.[Address] a inner join Common.City c on c.Id = a.CityId where a.IdPerson = ts.PersonId and a.[State] = 1) as CitySupplier,
        vt.id as IdParentDocument,
        vt.DocumentDate,
        0 Term,
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
        vt.Code as ParentDocumentCode,
        iif(esd.StatusElectronic = 3, esd.ShippingDate, null) as ValidationDate,
        esd.CUDS,
        esd.QR,
        vt.Detail as Observations,
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
		0 as AccountPayableEntityId,
		'' as AccountPayableEntityCode,
		'' as AccountPayableEntityName
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los documentos soporte electrónicos (equivalentes a facturas de compra) asociados a cuentas por pagar y comprobantes de egreso, integrando datos del proveedor (NIT, nombre, dirección, ciudad, teléfono) y del cliente receptor, junto con el detalle contable de cada línea (concepto, cuenta contable, tercero, centro de costo, valor débito y crédito). Combina mediante UNION los documentos soporte ligados a cuentas por pagar (AccountPayable) y los ligados a transacciones de tesorería (VoucherTransaction), siempre que manejen documento soporte electrónico. Expone además los datos de la resolución DIAN (número, prefijo, rango de facturas, fechas de vigencia), el estado de transmisión electrónica (Pendiente, Registrada, Enviada, Válida, Validación Fallida), el CUDS, el QR y la fecha de validación. Sirve como base para la impresión, consulta y trazabilidad de documentos soporte electrónicos en el módulo de cuentas por pagar y tesorería.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentSupportAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewElectronicDocumentSupportAccountPayable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de documentos soporte electrónicos asociados a cuentas por pagar y a transacciones de comprobante de tesorería, exponiendo datos del cliente, proveedor, autorización DIAN, conceptos contables y movimientos débito/crédito.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento soporte electrónico debe estar vinculado a una autorización de facturación (BillingAuthorization) válida.; Deben existir terceros cliente y proveedor referenciados en el documento soporte.; Para la primera rama: EntityName del documento soporte debe ser ''AccountPayable'' y la cuenta por pagar debe tener HandlesDocumentSupport = 1.; Para la segunda rama: EntityName debe ser ''VoucherTransaction'' y la transacción debe tener HandlesDocumentSupport = 1.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La forma de pago siempre se reporta como ''Crédito'' y el método de pago como ''Acuerdo Mutuo''.; Solo se incluyen documentos cuya entidad padre tiene HandlesDocumentSupport = 1.; La dirección y teléfono del cliente/proveedor se toman únicamente del primer registro activo (State = 1) salvo el teléfono del cliente que no filtra por estado.; Para transacciones de tesorería el Term se fija en 0 y los datos de entidad de cuenta por pagar quedan en blanco/cero.; Las fechas de resolución usan formato 105 (dd-mm-yyyy) y las fechas inicial/final formato 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento soporte electrónico; Cuenta por pagar; Autorización de facturación (resolución DIAN); Proveedor; Cliente; Concepto contable; Centro de costo; Retención; Comprobante de tesorería; Naturaleza débito/crédito; CUDS; Estado de validación electrónica', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas combinadas (UNION ALL) de documentos soporte electrónicos de cuentas por pagar y de comprobantes de tesorería que manejan documento soporte.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si esd.EntityName = ''AccountPayable'' AND ap.HandlesDocumentSupport = 1 → Se obtiene el detalle desde Payments.AccountPayable y AccountPayableDetailConcept, usando AccountPayableConcepts como concepto.; si esd.EntityName = ''VoucherTransaction'' AND vt.HandlesDocumentSupport = 1 → Se obtiene el detalle desde Treasury.VoucherTransaction y VoucherTransactionDetails, usando Treasury.ExpenseConcepts como concepto, y se devuelve AccountPayableEntityId=0 y códigos vacíos.; si esd.StatusElectronic IN (0,1,2,3,4) → Se traduce a etiquetas: 0=Invalida, 1=Registrada, 2=Enviada, 3=Valida, 4=Validacion Fallida. else Se etiqueta como ''Pendiente''.; si esd.StatusElectronic = 3 → Se expone esd.ShippingDate como ValidationDate. else ValidationDate es NULL.; si Nature = 1 (del detalle) → El valor se expone como DebitValue y CreditValue=0.; si Nature = 2 (del detalle) → El valor se expone como CreditValue y DebitValue=0.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Address; Common.Phone; Common.City; Billing.ElectronicSupportDocument; Billing.BillingAuthorization; Common.ThirdParty; Payments.AccountPayable; Payments.AccountPayableDetailConcept; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts; Payroll.CostCenter; Treasury.VoucherTransaction; Treasury.VoucherTransactionDetails; Treasury.ExpenseConcepts', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewElectronicDocumentSupportAccountPayable';
GO
