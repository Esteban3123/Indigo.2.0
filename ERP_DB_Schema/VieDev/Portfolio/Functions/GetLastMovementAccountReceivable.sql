
create FUNCTION [Portfolio].[GetLastMovementAccountReceivable]
(
    @AccountReceivableId int
)
RETURNS date
AS
BEGIN
	
return (select max(MovesDate)
from (
SELECT 
	max(PN.NoteDate) as MovesDate
	FROM [Portfolio].[PortfolioNoteAccountReceivableAdvance] PARA with (nolock) 
	inner join Portfolio.PortfolioNote PN with (nolock) on PN.Id = PARA.PortfolioNoteId 
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PARA.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
	inner join Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = PARA.MainAccountId
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId 
	left join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PARA.MainAccountId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where ar.Id = @AccountReceivableId
union all
	SELECT 
		max(PT.DocumentDate) as MovesDate
	FROM [Portfolio].[PortfolioTransferDetail] PTD with (nolock) 
	inner join Portfolio.PortfolioTransfer PT with (nolock) on PT.Id = PTD.PortfolioTrasferId
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = PTD.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = AR.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = PTD.MainAccountId 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where ar.Id = @AccountReceivableId
union all
	SELECT 
		max(PN.NoteDate) as MovesDate		
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
	where ar.Id = @AccountReceivableId
union all
	SELECT 
		max(CA.DocumentDate) as MovesDate
	FROM [Treasury].[CrossingAccountDetailCxC] CADCXC with (nolock) 
	inner join Treasury.CrossingAccount CA  with (nolock) on CA.Id = CADCXC.CrossingAccountId 
	inner join Portfolio.AccountReceivable AR with (nolock) on AR.Id = CADCXC.AccountReceivableId 
	inner join Common.ThirdParty TP with (nolock) on TP.Id= AR.ThirdPartyId 
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = CADCXC.MainAccountId 
	LEFT JOIN Portfolio.AccountReceivableAccounting ARA with (nolock) on ARA.AccountReceivableId = AR.Id AND ARA.MainAccountId = MA.Id
	left join Billing.Invoice IV with (nolock) on IV.Id = AR.InvoiceId 
	left join Contract.CareGroup CG with (nolock) on CG.Id = IV.CareGroupId
	left join Common.Currency c on c.Id = ar.CurrencyId
	where ar.Id = @AccountReceivableId
union all
	SELECT 
		max(CR.DocumentDate) as MovesDate
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
	where ar.Id = @AccountReceivableId
union all 
	--- Listo las notas de reversion de recibos de caja que cruzaron con facturas
	SELECT distinct
		max(TN.NoteDate) as MovesDate
	FROM Treasury.TreasuryNote TN
	inner join [Treasury].[CashReceipts] CR with (nolock) on TN.CashReceiptId = CR.Id
	inner join Treasury.CashReceiptDetails CRD with (nolock) on CRD.IdCashReceipt = CR.Id 
	inner join Common.ThirdParty TP with (nolock) on TP.Id = CRD.IdThirdParty 
	inner join treasury.CashReceiptAccountReceivable as crar with (nolock) on crar.CashReceiptDetailId = crd.id
	inner join GeneralLedger.MainAccounts MA with (nolock) on MA.Id = crd.IdMainAccount and crd.nature = 2
	left join Common.Currency c on c.Id = crd.CurrencyId
	where crar.AccountReceivableId = @AccountReceivableId

) as dat)
END