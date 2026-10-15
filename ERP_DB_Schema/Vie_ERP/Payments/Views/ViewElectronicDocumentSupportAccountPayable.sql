

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