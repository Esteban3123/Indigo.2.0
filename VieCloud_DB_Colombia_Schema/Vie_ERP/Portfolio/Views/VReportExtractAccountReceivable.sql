
CREATE VIEW [Portfolio].[VReportExtractAccountReceivable]
AS
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,PN.Code as MovesCode
		,PN.NoteDate as MovesDate
		,SUM(case when PN.Nature = 1 then PARA.AdjusmentValue else 0 end) as MovesDebit
		,SUM(case when PN.Nature = 2 then PARA.AdjusmentValue else 0 end) as MovesCredit
		,case when PN.Nature = 1 then 'Nota Débito De CxC' else 'Nota Crédito De CxC' end as VoucherName
		,CG.Id as IdCareGroup
		,PN.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioNoteAccountReceivableAdvance] PARA with (nolock) 
	inner join Portfolio.PortfolioNote PN with (nolock) on PN.Id = PARA.PortfolioNoteId 
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PARA.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
	inner join Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = PARA.MainAccountId
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId 
	left join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PARA.MainAccountId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where PN.Status = 2
	group by TP.Nit,TP.Name,Ma.Id,MA.Number,MA.Name, AR.InvoiceNumber, AR.OpeningBalance, AR.Value, ARA.Balance, PN.Code, PN.NoteDate, PN.Nature, CG.Id, PN.Status, CG.Code, AR.AccountReceivableType,AR.Id, c.Abbreviation
union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,PT.Code as MovesCode
		,PT.DocumentDate as MovesDate
		,0 as MovesDebit
		,sum(PTD.ValueInCurrencyInvoice) as MovesCredit
		,'Cruce Ant Vs CxC' as VoucherName
		,CG.Id as IdCareGroup
		,PT.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioTransferDetail] PTD with (nolock) 
	inner join Portfolio.PortfolioTransfer PT with (nolock) on PT.Id = PTD.PortfolioTrasferId
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PTD.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PTD.MainAccountId 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where PT.Status IN (2, 4)
	group by TP.Nit,TP.Name,Ma.Id,MA.Number,MA.Name, AR.InvoiceNumber, AR.OpeningBalance, AR.Value, ARA.Balance, PT.Code, PT.DocumentDate, CG.Id, PT.Status, CG.Code, AR.AccountReceivableType,AR.Id, c.Abbreviation
union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,PN.Code as MovesCode
		,PN.NoteDate as MovesDate		
		,sum(PTD.ValueInCurrencyInvoice) as MovesDebit
		,0 as MovesCredit
		,'Reversión Cruce Ant Vs CxC' as VoucherName
		,CG.Id as IdCareGroup
		,PN.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioTransferDetail] PTD with (nolock) 
	inner join Portfolio.PortfolioTransfer PT with (nolock) on PT.Id = PTD.PortfolioTrasferId
	inner join Portfolio.PortfolioNote PN with (nolock) on PT.Id = PN.PortfolioTransferId
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PTD.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PTD.MainAccountId 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where PN.Status = 2 and pn.NoteType <> 1
	group by TP.Nit,TP.Name,MA.Id,MA.Number,MA.Name, AR.InvoiceNumber, AR.OpeningBalance, AR.Value, ARA.Balance, PN.Code, PN.NoteDate, CG.Id, PN.Status, CG.Code, AR.AccountReceivableType, AR.Id, c.Abbreviation

-- TODO: Added
	union all

    SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,cr.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,pn.Code as MovesCode
		,PN.NoteDate as MovesDate
		,0 as MovesDebit
		,(isnull(SUM(PTD.Value),0) - ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 1),0) + ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 2),0)) as MovesCredit
		,'Reversión Cruce Ant Vs CxC' as VoucherName
		,0 as IdCareGroup
		,PT.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioTransfer] PT with (nolock)
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PT.PortfolioAdvanceId
	join [Treasury].[CashReceipts] cr (nolock) on PA.CashReceiptId = cr.Id
    inner join Portfolio.PortfolioNote PN with (nolock) on PT.Id = PN.PortfolioTransferId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Portfolio.PortfolioTransferDetail PTD with (nolock) on PTD.PortfolioTrasferId = PT.Id 
	left join Common.Currency c on c.Id = pa.CurrencyId
	where PN.Status = 2 and pn.NoteType = 1
	group by TP.Nit,TP.Name,MA.Id, MA.Number, MA.Name, cr.Code, pn.Code, PA.OpeningBalance, PA.Value, PA.Balance, PT.Code, PN.NoteDate, PT.Status, PT.Id,c.Abbreviation

union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,CA.Code as MovesCode
		,CA.DocumentDate as MovesDate
		,0 as MovesDebit
		,CADCXC.CrossingValue as MovesCredit
		,'Cruce De Cuenta' as VoucherName
		,CG.Id as IdCareGroup
		,CA.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Treasury].[CrossingAccountDetailCxC] CADCXC with (nolock) 
	inner join Treasury.CrossingAccount CA  with (nolock) on CA.Id = CADCXC.CrossingAccountId 
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = CADCXC.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id= AR.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = CADCXC.MainAccountId 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where CA.Status = 2
union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,CR.Code as MovesCode
		,CR.DocumentDate as MovesDate
		,case when CRD.Nature = 1 then CRAR.Value else 0 end as MovesDebit
		,case when CRD.Nature = 2 then CRAR.Value else 0 end as MovesCredit
		,'Recibo De Caja' as VoucherName
		,CG.Id as IdCareGroup
		,CR.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Treasury].[CashReceipts] CR with (nolock)
	inner join Treasury.CashReceiptDetails CRD  with (nolock) on CRD.IdCashReceipt = CR.Id 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
	inner join Treasury.CashReceiptAccountReceivable CRAR with (nolock) on CRAR.CashReceiptDetailId = CRD.Id 
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = CRAR.AccountReceivableId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = CRD.IdMainAccount 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where CR.Status in (2,4)
union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,ISNULL(CR.Code, PA.Code) as MovesCode
		,ISNULL(CR.DocumentDate, PA.DocumentDate) as MovesDate
		,0 as MovesDebit
		,IPA.Value as MovesCredit
		,IIF(CR.Id IS NULL, 'Anticipo', 'Recibo De Caja') as VoucherName
		,CG.Id as IdCareGroup
		,ISNULL(CR.Status, PA.Status) as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM Portfolio.PortfolioAdvance PA  with (nolock)
	inner join Billing.InvoicePortfolioAdvance IPA with (nolock) ON PA.Id = IPA.PortfolioAdvanceId
	inner join Billing.Invoice IV with (nolock) on IPA.InvoiceId = IV.Id
	inner join Portfolio.AccountReceivable AR with (nolock) on IV.Id = AR.InvoiceId
	inner join Common.ThirdParty TP with (nolock) on AR.ThirdPartyId = TP.Id
	inner join GeneralLedger.MainAccounts MA with (nolock) on AR.AccountWithoutRadicateId = MA.Id	
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Contract.CareGroup CG with (nolock) on IV.CareGroupId = CG.Id
	left join [Treasury].[CashReceipts] CR with (nolock) on PA.CashReceiptId = CR.Id AND CR.Status in (2,4)
	left join Common.Currency c on c.Id = ar.CurrencyId
	where IV.DocumentType = 5 AND AR.AccountReceivableType = 6 AND IV.Status = 1
union all
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,ARA.Value as BillValueInitial
		,case AR.Status when 3 then 0 else ARA.Balance end as BillCurrentBalance
		,AR.Code as MovesCode
		,AR.AccountReceivableDate as MovesDate
		,IIF(AR.OpeningBalance = 0, AR.Value,ISNULL(ARA.Value,0)) as MovesDebit
		,0 as MovesCredit
		,'Cuenta Por Cobrar' as VoucherName
		,CG.Id as IdCareGroup
		,AR.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[AccountReceivable] AR with (nolock)
	inner join Common.ThirdParty TP with (nolock)  on TP.Id = AR.ThirdPartyId
	inner join Portfolio.AccountReceivableAccounting ARA with (nolock) on AR.Id = ARA.AccountReceivableId
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = ARA.MainAccountId 
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	--where AR.Status = 2 
union all 
	------ Movimientos de Facturas Anuladas
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,ARA.Value as BillValueInitial
		,case AR.Status when 3 then 0 else ARA.Balance end as BillCurrentBalance
		,(select cast(jvt.Code as varchar(10))+ '-' +cast(jv.Consecutive as varchar(10)) from GeneralLedger.JournalVouchers jv inner join GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.Id where jv.Id = (select MAX(Id) from GeneralLedger.JournalVouchers where EntityCode = AR.InvoiceNumber and EntityName = 'Invoice')) as MovesCode
		,i.AnnulmentDate as MovesDate
		,0 as MovesDebit
		,AR.Value as MovesCredit
		,'C. Contable Anulacion CxC' as VoucherName
		,CG.Id as IdCareGroup
		,AR.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[AccountReceivable] AR with (nolock)
	inner join Billing.Invoice i with (nolock) on i.Id = AR.InvoiceId
	inner join Common.ThirdParty TP with (nolock)  on TP.Id = AR.ThirdPartyId
	inner join Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = ARA.MainAccountId 
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where AR.Status = 3
union all 
	--- Movimientos Reclasificación de Cartera
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Factura' as TypeDocument
		,AR.InvoiceNumber as DocumentNumber
		,case when AR.OpeningBalance = 1 then 'Saldo CxC' when AR.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,AR.Value as BillValueInitial
		,ARA.Balance as BillCurrentBalance
		,PR.Code as MovesCode
		,PR.ConfirmationDate as MovesDate
		,SUM(IIF(PR.TargetAccountId = MA.Id, PR.Value, 0)) as MovesDebit
		,SUM(IIF(PR.SourceAccountId = MA.Id, PR.Value, 0)) as MovesCredit
		,'Reclasificación Documento' as VoucherName
		,CG.Id as IdCareGroup
		,AR.Status as Status
		,CG.Code as CareGroupCode
		,AR.AccountReceivableType as AccountReceivableType
		,AR.Id AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM Portfolio.PortfolioReclassification PR WITH (NOLOCK)
	JOIN Portfolio.AccountReceivable AR WITH (NOLOCK) ON pr.AccountReceivableId = ar.Id
	JOIN Common.ThirdParty TP WITH (NOLOCK) ON AR.ThirdPartyId = TP.Id
	JOIN GeneralLedger.MainAccounts MA WITH (NOLOCK) ON PR.SourceAccountId = MA.Id OR PR.TargetAccountId = MA.Id
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	LEFT JOIN Contract.CareGroup CG WITH (NOLOCK) ON AR.CareGroupId = CG.Id
	left join Common.Currency c on c.Id = ar.CurrencyId
	where PR.Status = 2
	group by TP.Nit,TP.Name,MA.Id,MA.Number,MA.Name, AR.InvoiceNumber, AR.OpeningBalance, AR.Value, ARA.Balance, PR.Code, PR.ConfirmationDate, CG.Id, AR.Status, CG.Code, AR.AccountReceivableType,AR.Id, c.Abbreviation
union all 
	--- Listo todos los anticipos por recibos de caja
	SELECT distinct
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,CR.Code as MovesCode
		,CR.DocumentDate as MovesDate
		,case when CRD.Nature = 1 then PA.Value else 0 end as MovesDebit
		,case when CRD.Nature = 2 then PA.Value else 0 end as MovesCredit
		,'Recibo De Caja' as VoucherName
		,0 as IdCareGroup
		,CR.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Treasury].[CashReceipts] CR with (nolock)
	inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.IdCashReceipt = CR.Id 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.CashReceiptId = CR.Id
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join [Treasury].[EntityBankAccounts] EBA on CR.IdBankAccount = EBA.Id 
	left join Treasury.CashRegisters crr on CR.IdCashRegister = crr.Id
	left join Common.Currency c on c.Id = ISNULL(EBA.CurrencyId ,crr.CurrencyId)
	where CR.Status in (2,4)
union all
	--- Listo las notas de reversion de recibos de caja
	SELECT distinct
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,TN.Code as MovesCode
		,TN.NoteDate as MovesDate
		,case when CRD.Nature = 1 then 0 else PA.Value end as MovesDebit
		,case when CRD.Nature = 2 then 0 else PA.Value end as MovesCredit
		,'Nota Reversion RC' as VoucherName
		,0 as IdCareGroup
		,CR.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM Treasury.TreasuryNote TN
	inner join [Treasury].[CashReceipts] CR with (nolock) on TN.CashReceiptId = CR.Id
	inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.IdCashReceipt = CR.Id 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.CashReceiptId = CR.Id
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = crd.CurrencyId
	where TN.Status in (2)
union all 
	--- Listo las notas de reversion de recibos de caja que cruzaron con facturas
	SELECT distinct
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Reversión RC' as TypeDocument
		,crar.invoicenumber as DocumentNumber
		,'Reversión RC' as EntityName
		, crar.Value as BillValueInitial
		,crar.value as BillCurrentBalance
		,TN.Code as MovesCode
		,TN.NoteDate as MovesDate
		,crar.Value as MovesDebit
		,0 as MovesCredit
		,'Nota Reversión RC' as VoucherName
		,0 as IdCareGroup
		,CR.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,crar.AccountReceivableId AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM Treasury.TreasuryNote TN
	inner join [Treasury].[CashReceipts] CR with (nolock) on TN.CashReceiptId = CR.Id
	inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.IdCashReceipt = CR.Id 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
	inner join treasury.CashReceiptAccountReceivable as crar with (nolock) on crar.CashReceiptDetailId = crd.id
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = crd.IdMainAccount and crd.nature = 2
	left join Common.Currency c on c.Id = crd.CurrencyId
	where TN.Status in (2) and tn.notetype = 4
union all
	--- Listo todos los anticipos por traslado
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,PT.Code as MovesCode
		,PT.DocumentDate as MovesDate
		,(isnull(SUM(PTD.Value),0) - ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 1),0) + ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 2),0)) as MovesDebit
		,0 as MovesCredit
		,'Cruce Ant Vs CxC' as VoucherName
		,0 as IdCareGroup
		,PT.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioTransfer] PT with (nolock)
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PT.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Portfolio.PortfolioTransferDetail PTD with (nolock) on PTD.PortfolioTrasferId = PT.Id 
	left join Common.Currency c on c.Id = pa.CurrencyId
	where PT.Status in (2,4)
	group by TP.Nit,TP.Name,MA.Id, MA.Number, MA.Name, PA.Code, PA.OpeningBalance, PA.Value, PA.Balance, PT.Code, PT.DocumentDate, PT.Status, PT.Id,c.Abbreviation
union all
	--- Listo las reversiones de los traslados del anticipo
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,'A-'+PT.Code as MovesCode
		,I.AnnulmentDate as MovesDate
		,0 as MovesDebit
		,(isnull(SUM(PTD.Value),0) - ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 1),0) + ISNULL((select sum(Value) from Portfolio.PortfolioTransferOtherConcept where PT.Id = PortfolioTransferId And Nature = 2),0)) as MovesCredit
		,'Reversion Cruce Ant Vs CxC' as VoucherName
		,0 as IdCareGroup
		,PT.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,PTD.AccountReceivableId as AccountReceivableType
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioTransfer] PT with (nolock)
	inner join Portfolio.PortfolioTransferDetail PTD with (nolock) on PT.Id = PTD.PortfolioTrasferId
	inner join Portfolio.AccountReceivable AC with (nolock) on AC.Id = PTD.AccountReceivableId
	inner join Billing.Invoice I with (nolock) on I.Id = AC.InvoiceId
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PT.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = ac.CurrencyId
	where PT.Status in (4)
	group by TP.Nit,TP.Name,MA.Id, MA.Number, MA.Name, PA.Code, PA.OpeningBalance, PA.Value, PA.Balance, PT.Code, I.AnnulmentDate, PT.Status, PT.Id,PTD.AccountReceivableId,c.Abbreviation
union all 
	--- Listo todos los anticipos por Nota
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,PN.Code as MovesCode
		,PN.NoteDate as MovesDate
		,case when PN.Nature = 1 then PNARA.AdjusmentValue else 0 end as MovesDebit
		,case when PN.Nature = 2 then PNARA.AdjusmentValue else 0 end as MovesCredit
		,case when PN.Nature = 1 then 'Nota Débito De CxC' else 'Nota Crédito De CxC' end as VoucherName
		,0 as IdCareGroup
		,PN.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,ISNULL(PNARA.AccountReceivableId, 0) AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioNote] PN with (nolock)
	inner join Portfolio.PortfolioNoteAccountReceivableAdvance PNARA with (nolock) on PNARA.PortfolioNoteId = PN.Id
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PNARA.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = pn.CurrencyId
	where PN.Status in (2)
union all 
	--- Listo todos los anticipos por Nota de Distribucción
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,PN.Code as MovesCode
		,PN.NoteDate as MovesDate
		,(Select SUM(Value) From Portfolio.PortfolioNoteDistribution where PortfolioNoteId = PN.Id) as MovesDebit
		,0 as MovesCredit
		,case when PN.Nature = 1 then 'Nota Débito De Distribucción' else 'Nota Crédito De Distribucción' end as VoucherName
		,0 as IdCareGroup
		,PN.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].[PortfolioNote] PN with (nolock)
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PN.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = pn.CurrencyId
	where PN.Status in (2)
Union All
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,PN.Code as MovesCode
		,PN.NoteDate as MovesDate
		, 0 as MovesDebit
		, PND.Value  MovesCredit
		,case when PN.Nature = 1 then 'Nota Débito De Distribucción' else 'Nota Crédito De Distribucción' end as VoucherName
		,0 as IdCareGroup
		,PN.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Portfolio].PortfolioNoteDistribution PND with (nolock)
	inner join Portfolio.PortfolioNote PN with (nolock) on PND.PortfolioNoteId = PN.Id 
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = PND.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = pn.CurrencyId
	where PN.Status in (2)
union all 
	--- Listo todos los anticipos por Comprobante Contable
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,VT.Code as MovesCode
		,VT.DocumentDate as MovesDate
		,sum(case when VTD.Nature = 1 then VTA.Value else 0 end) as MovesDebit
		,sum(case when VTD.Nature = 2 then VTA.Value else 0 end) as MovesCredit
		,'Reintegro de Anticipo' as VoucherName
		,0 as IdCareGroup
		,VT.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM [Treasury].[VoucherTransaction] VT with (nolock) 
	inner join Treasury.VoucherTransactionDetails VTD with (nolock) on VTD.IdVoucherTransaction = VT.Id 
	inner join Treasury.VoucherTransactionAdvance VTA with (nolock) on VTA.IdVoucherTransactionD = VTD.Id
	inner join Portfolio.PortfolioAdvance PA with (nolock) on PA.Id = VTA.PortfolioAdvanceId
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = pa.CurrencyId
	where VT.Status in (2)
	group by TP.Nit,TP.Name,MA.Id,MA.Number,MA.Name,PA.Code,PA.OpeningBalance,PA.Value,PA.Balance,VT.Code,VT.DocumentDate,VTD.Nature,VT.Status,c.Abbreviation
union all
	--- listo los anticipos de saldo inicial
	SELECT 
		TP.Nit as ThirdPartyNit
		,TP.Name as ThirdPartyName
		,MA.Id as AccountNumberId
		,MA.Number as AccountNumber
		,MA.Name as AccountName
		,'Anticipo' as TypeDocument
		,PA.Code as DocumentNumber
		,case when PA.OpeningBalance = 1 then 'Saldo CxC' when PA.OpeningBalance = 0 then 'Saldo Inicial' end as EntityName
		,PA.Value as BillValueInitial
		,PA.Balance as BillCurrentBalance
		,PA.Code as MovesCode
		,PA.DocumentDate as MovesDate
		,0 as MovesDebit
		,PA.Value as MovesCredit
		,'Saldo Inicial' as VoucherName
		,0 as IdCareGroup
		,PA.Status as Status
		,'' as CareGroupCode
		,0 as AccountReceivableType
		,0 AccountReceivableId
		,c.Abbreviation CurrencyName
	FROM Portfolio.PortfolioAdvance PA with (nolock) 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = PA.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PA.MainAccountId
	left join Common.Currency c on c.Id = pa.CurrencyId
	where PA.Status in (2) AND PA.OpeningBalance = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de extracción y reporte de movimientos de cuentas por cobrar (CxC) de cartera. Consolida en un único conjunto de datos todas las transacciones que afectan el saldo de cada documento de cobro (factura o anticipo): notas débito y crédito de cartera, cruces de anticipos contra CxC y sus reversiones. Integra información del tercero o entidad pagadora (NIT, nombre), la cuenta contable del plan de cuentas, el grupo de atención del contrato, el saldo inicial y saldo actual de la CxC, y el comprobante o movimiento que la afecta (código, fecha, valor débito, valor crédito y tipo de voucher). Se utiliza para reportería y auditoría de cartera, auxiliar contable de CxC, seguimiento de recaudo, conciliación de anticipos y extracción de datos para informes de gestión de cobro por entidad pagadora, EPS o aseguradora.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportExtractAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VReportExtractAccountReceivable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida en un único extracto contable todos los movimientos (débitos y créditos) que afectan las cuentas por cobrar y los anticipos de cartera, unificando facturas, notas, cruces, recibos de caja, traslados, reclasificaciones, anulaciones y reversiones para reportería.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los documentos de origen (notas, traslados, recibos de caja, cruces, reclasificaciones, comprobantes) deben estar en estados confirmados/aprobados (Status=2) o anulados (Status=4) para ser incluidos.; Las cuentas por cobrar deben tener su registro contable en Portfolio.AccountReceivableAccounting con MainAccountId asociado para resolver la cuenta contable del movimiento.; Cada documento debe tener tercero (Common.ThirdParty) y cuenta contable principal (GeneralLedger.MainAccounts) referenciados.; Los anticipos deben estar vinculados a un PortfolioAdvance con MainAccountId definido.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Devuelve un UNION ALL de ~21 bloques con movimientos de cartera; cada bloque expone TypeDocument=''Factura'' o ''Anticipo'' o ''Reversión RC'' y un VoucherName que identifica la naturaleza del movimiento (Recibo de Caja, Cruce, Nota Débito/Crédito, Reclasificación, Anulación, etc.).; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: EntityName se deriva de OpeningBalance: 1 → ''Saldo CxC'', 0 → ''Saldo Inicial'' tanto para AccountReceivable como para PortfolioAdvance.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para notas de cartera (PortfolioNote): si Nature=1 el valor se registra como MovesDebit y VoucherName=''Nota Débito De CxC''; si Nature=2 se registra como MovesCredit y VoucherName=''Nota Crédito De CxC''.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para facturas anuladas (AR.Status=3) BillCurrentBalance se fuerza a 0 y se genera un movimiento crédito por AR.Value con VoucherName=''C. Contable Anulacion CxC'', tomando MovesDate de Invoice.AnnulmentDate y MovesCode del último JournalVoucher cuya EntityCode=InvoiceNumber y EntityName=''Invoice''.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para reclasificaciones (PortfolioReclassification Status=2): si la cuenta MA coincide con TargetAccountId se acumula como MovesDebit, si coincide con SourceAccountId se acumula como MovesCredit, con VoucherName=''Reclasificación Documento''.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para traslados (PortfolioTransfer): el valor neto del cruce se calcula como SUM(PTD.Value) - SUM(OtherConcept Nature=1) + SUM(OtherConcept Nature=2); con Status=2 ó 4 figura como ''Cruce Ant Vs CxC'' y con Status=4 adicionalmente como ''Reversion Cruce Ant Vs CxC'' usando Invoice.AnnulmentDate.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para recibos de caja (CashReceipts Status IN (2,4)): CRD.Nature=1 → MovesDebit, Nature=2 → MovesCredit; VoucherName=''Recibo De Caja''.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para notas de tesorería (TreasuryNote Status=2): se generan reversiones de anticipo invirtiendo el signo (Nature=1 → MovesCredit, Nature=2 → MovesDebit) con VoucherName=''Nota Reversion RC''; cuando NoteType=4 se reporta adicionalmente como TypeDocument=''Reversión RC'' afectando la CxC cruzada.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para anticipos cruzados con factura vía InvoicePortfolioAdvance: solo se incluyen cuando IV.DocumentType=5, AR.AccountReceivableType=6 e IV.Status=1; VoucherName se determina con IIF(CR.Id IS NULL,''Anticipo'',''Recibo De Caja'').; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Para la línea base de cuentas por cobrar: MovesDebit = AR.Value cuando OpeningBalance=0, en otro caso ARA.Value (vía IIF), con VoucherName=''Cuenta Por Cobrar''.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Anticipos de saldo inicial: se incluyen únicamente cuando PA.Status=2 AND PA.OpeningBalance=1 con VoucherName=''Saldo Inicial'' como crédito por PA.Value.; [RETURN_RESULT] Portfolio.VReportExtractAccountReceivable: Reintegros de anticipo se obtienen de VoucherTransaction Status=2 vía VoucherTransactionAdvance, separando Debit/Credit por VTD.Nature, con VoucherName=''Reintegro de Anticipo''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VReportExtractAccountReceivable';
GO
