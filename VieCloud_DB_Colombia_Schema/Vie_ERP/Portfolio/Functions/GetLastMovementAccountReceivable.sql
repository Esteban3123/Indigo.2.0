
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que devuelve la fecha del último movimiento registrado sobre una cuenta por cobrar específica, identificada por su ID. Consolida en una sola fecha máxima todos los eventos posibles que pueden afectar una cuenta por cobrar: anticipos y notas de cartera, traslados de cartera (transferencias), cruces de cuentas de tesorería, recibos de caja y notas de reversión de recibos. Es útil para determinar cuándo fue la última gestión o transacción realizada sobre una deuda pendiente, apoyando el seguimiento de cartera, la antigüedad de saldos y la gestión de cobro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetLastMovementAccountReceivable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetLastMovementAccountReceivable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la fecha del último movimiento registrado sobre una cuenta por cobrar, considerando notas de cartera, transferencias, cruces de tesorería, recibos de caja y notas de reversión.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la cuenta por cobrar identificada para que las uniones devuelvan datos; de lo contrario el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha retornada siempre corresponde al MAX agregado entre seis fuentes de movimientos relacionados con la cuenta por cobrar.; Las notas de tesorería sólo se consideran cuando el detalle de recibo de caja tiene naturaleza = 2 (reversión).; El emparejamiento por cuenta contable principal (AccountReceivableAccounting.MainAccountId) se aplica como filtro en el ramo de anticipos y como left join en los demás ramos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Cartera; Notas de cartera; Anticipos; Transferencia de cartera; Cruce de cuentas (CxC); Recibo de caja; Nota de tesorería (reversión); Cuenta contable principal; Tercero; Factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna MAX(MovesDate) sobre la unión de las fechas máximas de: notas de cartera con anticipo (PortfolioNote.NoteDate), transferencias de cartera (PortfolioTransfer.DocumentDate), notas asociadas a transferencias (PortfolioNote.NoteDate vía PortfolioTransferId), cruces CxC de tesorería (CrossingAccount.DocumentDate), recibos de caja aplicados (CashReceipts.DocumentDate) y notas de tesorería de reversión de recibos con CRD.nature=2 (TreasuryNote.NoteDate).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioNote; Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.AccountReceivableAccounting; Billing.Invoice; Contract.CareGroup; GeneralLedger.MainAccounts; Common.Currency; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioTransfer; Treasury.CrossingAccountDetailCxC; Treasury.CrossingAccount; Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Treasury.TreasuryNote', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetLastMovementAccountReceivable';
GO
