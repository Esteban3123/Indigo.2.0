

CREATE view [ViewInternal].[VPortfolioAudit]
as
(
/***** Radicados v contabilidad ****/
select *
from (
--- Cuentas contables sin Radicar
select rc.RadicatedConsecutive, ma.Number + ' - ' + ma.Name as Cuenta, sum(rd.BalanceInvoice) as ValorRadicado, data.EntityCode, data.Value as ValorContabilidad
from Portfolio.RadicateInvoiceC rc
inner join Portfolio.RadicateInvoiceD rd on rd.RadicateInvoiceCId = rc.Id
inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = rd.InvoiceNumber and ar.AccountReceivableType = 2
inner join GeneralLedger.MainAccounts ma on ma.Id = ar.AccountWithoutRadicateId
left join (
select jv.EntityId, jv.EntityCode, jvd.IdMainAccount, sum(jvd.CreditValue) + sum(jvd.DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
where jv.EntityName = 'RadicateInvoiceC' and jv.LegalBookId = 1
group by jv.EntityId, jv.EntityCode, jvd.IdMainAccount
) as data on data.EntityId = rc.Id and data.IdMainAccount = ma.Id
where rc.State in (2) and cast(rc.RadicatedDate as date) > '07/01/2016'
group by rc.RadicatedConsecutive, data.EntityCode, data.Value, ma.Number, ma.Name
union all
--- Cuentas contables de Radicadas
select rc.RadicatedConsecutive, ma.Number + ' - ' + ma.Name as Cuenta, sum(rd.BalanceInvoice) as ValorRadicado, data.EntityCode, data.Value as ValorContabilidad
from Portfolio.RadicateInvoiceC rc
inner join Portfolio.RadicateInvoiceD rd on rd.RadicateInvoiceCId = rc.Id
inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = rd.InvoiceNumber and ar.AccountReceivableType = 2
inner join GeneralLedger.MainAccounts ma on ma.Id = ar.AccountRadicateId
left join (
select jv.EntityId, jv.EntityCode, jvd.IdMainAccount, sum(jvd.CreditValue) + sum(jvd.DebitValue) as Value
from GeneralLedger.JournalVouchers jv
inner join GeneralLedger.JournalVoucherDetails jvd on jvd.IdAccounting = jv.Id
where jv.EntityName = 'RadicateInvoiceC' and jv.LegalBookId = 1
group by jv.EntityId, jv.EntityCode, jvd.IdMainAccount
) as data on data.EntityId = rc.Id and data.IdMainAccount = ma.Id
where rc.State in (2) and cast(rc.RadicatedDate as date) > '07/01/2016'
group by rc.RadicatedConsecutive, data.EntityCode, data.Value, ma.Number, ma.Name
) as data2
where data2.ValorRadicado <> isnull(data2.ValorContabilidad,0)
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de auditoría que detecta inconsistencias entre los valores radicados en cartera y los registros contables del libro mayor. Compara el saldo de facturas radicadas (estado 2, desde julio 2016) contra los movimientos débito/crédito en comprobantes de diario asociados a la entidad `RadicateInvoiceC`, considerando tanto cuentas sin radicar como cuentas radicadas. Solo expone registros donde exista diferencia entre el valor radicado y el valor contabilizado.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Identifica inconsistencias entre el valor radicado de facturas de cartera y el valor efectivamente contabilizado en el libro mayor para cada cuenta contable (sin radicar y radicada) de los radicados confirmados.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de comprobantes contables (JournalVouchers) asociados con EntityName=''RadicateInvoiceC'' y LegalBookId=1 para poder cruzar contra el radicado.; Las cuentas por cobrar deben tener configuradas las cuentas contables ''sin radicar'' (AccountWithoutRadicateId) y ''radicada'' (AccountRadicateId) en el plan de cuentas.; Los radicados deben estar en estado 2 y con RadicatedDate > 01/07/2016 para entrar en la auditoría.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se auditan radicados en estado 2 (radicado/confirmado).; Solo se consideran radicados con fecha posterior al 01/07/2016.; Solo se cruza contabilidad de comprobantes cuya entidad origen es ''RadicateInvoiceC'' y pertenecen al libro legal 1.; Únicamente se cruzan cuentas por cobrar de tipo 2 (radicadas).; El valor contable se calcula como la suma de débitos más créditos por cuenta principal.; La vista solo expone registros donde el valor radicado difiere del valor registrado en contabilidad (incluyendo casos sin contabilización, tratados como 0).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cuentas por cobrar; Cuentas contables (PUC); Comprobantes de diario; Libro legal; Cartera; Auditoría contable de radicados', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewInternal.VPortfolioAudit: Cuando rc.State=2 y RadicatedDate > 01/07/2016, retorna por cada radicado y cuenta contable (sin radicar y radicada) el valor radicado vs el valor contabilizado, filtrando solo aquellos donde ValorRadicado <> ISNULL(ValorContabilidad,0).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VPortfolioAudit';
GO
