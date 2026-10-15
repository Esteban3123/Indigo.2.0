






CREATE VIEW [Portfolio].[ViewListPortfolioTransferDetail]
AS

	select 
			ptd.Id as PortfolioTransferDetailId, 
			pt.Id as PortfolioTransferId, 
			ar.Id as AccountReceivableId, 
			ma.Id as MainAccountId, 
			ptd.Value, 
			ara.Value as ValueBill, 
			ara.Balance,
			ar.InvoiceNumber, 
			CONCAT(ma.Number,' - ',ma.Name) as CodeNameMainAccount,
			ptd.CostCenterId,
			CASE ma.Id
				WHEN  ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
				WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
				WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
				WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
				WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
				WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
				ELSE 'N/A'
			END PortfolioStatusName,
			c.Id CurrencyInvoiceId,
			c.Abbreviation CurrencyAbbreviation,
			CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
	from	[Portfolio].PortfolioTransferDetail ptd with (nolock)
			inner join [Portfolio].PortfolioTransfer pt with (nolock) on pt.Id = ptd.PortfolioTrasferId
			inner join [Portfolio].AccountReceivable ar  with (nolock) on ar.Id = ptd.AccountReceivableId
			left join Billing.Invoice i  with (nolock) on ar.InvoiceId = i.Id
			left join INPACIENT pa with (nolock) on i.PatientCode=pa.IPCODPACI
			inner join [GeneralLedger].MainAccounts ma with (nolock) on ma.Id = ptd.MainAccountId
			inner join GeneralLedger.LegalBook lb on lb.Id = ma.LegalBookId and lb.OfficialBook = 1
			inner join [Portfolio].AccountReceivableAccounting ara with (nolock) on ara.AccountReceivableId = ar.Id and ara.MainAccountId = ma.Id
			JOIN Common.Currency c WITH(NOLOCK) on isnull(ar.CurrencyId,(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings))=c.Id

			UNION ALL

			select 
					ptd.Id as PortfolioTransferDetailId, 
					pt.Id as PortfolioTransferId, 
					ar.Id as AccountReceivableId, 
					ma.Id as MainAccountId, 
					ptd.Value, 
					iar.Value as ValueBill, 
					iar.Balance,
					ar.InvoiceNumber, 
					CONCAT(ma.Number,' - ',ma.Name) as CodeNameMainAccount,
					ptd.CostCenterId,
					CASE ma.Id
						WHEN  ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
						WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
						WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
						WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
						WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
						WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
						ELSE 'N/A'
					END PortfolioStatusName,
					c.Id CurrencyInvoiceId,
					c.Abbreviation CurrencyAbbreviation,
					CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
			from [Portfolio].PortfolioTransferDetail ptd with (nolock)
			join [Portfolio].PortfolioTransfer pt with (nolock) on pt.Id = ptd.PortfolioTrasferId
			join [Portfolio].AccountReceivable ar  with (nolock) on ar.Id = ptd.AccountReceivableId
			left JOIN Billing.Invoice i  with (nolock) on ar.InvoiceId = i.Id
			left join INPACIENT pa with (nolock) on i.PatientCode=pa.IPCODPACI
			join [GeneralLedger].MainAccounts ma with (nolock) on ma.Id = ptd.MainAccountId
			join GeneralLedger.LegalBook lb on lb.Id = ma.LegalBookId and lb.OfficialBook = 1
			left join [Portfolio].AccountReceivableAccounting ara with (nolock) on ara.AccountReceivableId = ar.Id and ara.MainAccountId = ma.Id
			JOIN Portfolio.PortfolioInitialBalanceAccountReceivable iar on iar.InvoiceNumber  = ar.InvoiceNumber
			join Portfolio.PortfolioInitialBalanceAccountReceivableAccounting iara on iar.Id = iara.PortfolioInitialBalanceAccountReceivableId
			JOIN Common.Currency c WITH(NOLOCK) on isnull(ar.CurrencyId,(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings))=c.Id
			where ara.id is null