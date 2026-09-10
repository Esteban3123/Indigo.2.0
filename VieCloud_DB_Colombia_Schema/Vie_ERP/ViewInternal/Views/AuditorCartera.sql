

CREATE view [ViewInternal].[AuditorCartera]
as (
select
data.Documento,cast(data.Fecha as date) as Fecha, data.Codigo, data.Cuenta, sum(data.Debito) as Debito, sum(data.Credito) as Credito, isnull(cont.Debito,0) as DebitoContabilidad, isnull(cont.Credito,0) as CreditoContabilidad
from (
	select 
	'Radicacion' as Documento
	,rc.ConfirmDate as Fecha
	,RadicatedConsecutive as Codigo
	,masr.Id as IdCuenta
	,masr.Number as Cuenta
	,0 as Debito
	,rd.BalanceInvoice as Credito
	from Portfolio.RadicateInvoiceC rc
	inner join Portfolio.RadicateInvoiceD rd on rc.Id = rd.RadicateInvoiceCId
	inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = rd.InvoiceNumber and ar.AccountReceivableType = 2
	inner join GeneralLedger.MainAccounts masr on masr.Id = ar.AccountWithoutRadicateId
	where rc.State = 2
	union all
	select 
	'Radicacion' as Documento
	,rc.ConfirmDate as Fecha
	,RadicatedConsecutive as Codigo
	,mare.Id as IdCuenta
	,mare.Number as Cuenta
	,rd.BalanceInvoice as Debito
	,0 as Credito
	from Portfolio.RadicateInvoiceC rc
	inner join Portfolio.RadicateInvoiceD rd on rc.Id = rd.RadicateInvoiceCId
	inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = rd.InvoiceNumber and ar.AccountReceivableType = 2
	inner join GeneralLedger.MainAccounts mare on mare.Id = ar.AccountRadicateId
	where rc.State = 2
) as data
left join (
	select jv.EntityCode as Codigo,jvd.IdMainAccount as IdCuenta,sum(jvd.DebitValue) as Debito, sum(jvd.CreditValue) as Credito
	from GeneralLedger.JournalVouchers jv
	inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
	where jv.EntityName = 'RadicateInvoiceC' and jv.Status = 2
	group by jv.EntityCode,jvd.IdMainAccount
) as cont on data.IdCuenta = cont.IdCuenta and data.Codigo = cont.Codigo
 group by data.Documento,data.Fecha, data.Codigo, data.Cuenta, cont.Debito, cont.Credito

union all

select dataNota.Documento,cast(dataNota.Fecha as date) Fecha,dataNota.Codigo,dataNota.Cuenta,sum(dataNota.Debito) as Debito, sum(dataNota.Credito) as Credito, isnull(contNota.Debito,0) as DebitoContabilidad, isnull(contNota.Credito,0) as CreditoContabilidad
from (
select 
	'Notas' as Documento
	,n.NoteDate as Fecha
	,n.Code as Codigo
	,ma.Id as IdCuenta
	,ma.Number as Cuenta
	,case n.Nature when 1 then nara.AdjusmentValue else 0 end as Debito
	,case n.Nature when 1 then 0 else nara.AdjusmentValue end as Credito
	from Portfolio.PortfolioNote n
	inner join Portfolio.PortfolioNoteAccountReceivableAdvance nara on n.Id = nara.PortfolioNoteId
	inner join GeneralLedger.MainAccounts ma on ma.Id = nara.MainAccountId
	where n.Status = 2
union all
	select 
	'Notas' as Documento
	,n.NoteDate as Fecha
	,n.Code as Codigo
	,ma.Id as IdCuenta
	,ma.Number as Cuenta
	,case n.Nature when 1 then nara.AdjusmentValue else 0 end as Debito
	,case n.Nature when 1 then 0 else nara.AdjusmentValue end as Credito
	from Portfolio.PortfolioNote n
	inner join Portfolio.PortfolioNoteAccountReceivableAdvance nara on n.Id = nara.PortfolioNoteId
	inner join Portfolio.PortfolioAdvance pa on pa.Id = nara.PortfolioAdvanceId
	inner join GeneralLedger.MainAccounts ma on ma.Id = pa.MainAccountId
	where n.Status = 2
union all
select 
	'Notas' as Documento
	,n.NoteDate as Fecha
	,n.Code as Codigo
	,ma.Id as IdCuenta
	,ma.Number as Cuenta
	,case nd.Nature when 1 then nd.Value else 0 end as Debito
	,case nd.Nature when 1 then 0 else nd.Value end as Credito
	from Portfolio.PortfolioNote n
	inner join Portfolio.PortfolioNoteDetail nd on n.Id = nd.PortfolioNoteId
	inner join GeneralLedger.MainAccounts ma on ma.Id = nd.MainAccountId
	where n.Status = 2
union all
select 
	'Notas' as Documento
	,n.NoteDate as Fecha
	,n.Code as Codigo
	,ma.Id as IdCuenta
	,ma.Number as Cuenta
	,nd.Value as Debito
	,0 as Credito
	from Portfolio.PortfolioNote n
	inner join Portfolio.PortfolioNoteDistribution nd on n.Id = nd.PortfolioNoteId
	inner join GeneralLedger.MainAccounts ma on ma.Id = nd.MainAccountId
	where n.Status = 2
union all
select 
	'Notas' as Documento
	,n.NoteDate as Fecha
	,n.Code as Codigo
	,ma.Id as IdCuenta
	,ma.Number as Cuenta
	,0 as Debito
	,(select sum(Value) from Portfolio.PortfolioNoteDistribution where PortfolioNoteId = n.Id) as Credito
	from Portfolio.PortfolioNote n
	inner join Portfolio.PortfolioAdvance pa on pa.Id = n.PortfolioAdvanceId
	inner join GeneralLedger.MainAccounts ma on ma.Id = pa.MainAccountId
	where n.Status = 2 and n.NoteType = 4
) as dataNota
left join (
	select jv.EntityCode as Codigo,jvd.IdMainAccount as IdCuenta,sum(jvd.DebitValue) as Debito, sum(jvd.CreditValue) as Credito
	from GeneralLedger.JournalVouchers jv
	inner join GeneralLedger.JournalVoucherDetails jvd on jv.Id = jvd.IdAccounting
	where jv.EntityName = 'PortfolioNote' and jv.Status = 2
	group by jv.EntityCode,jvd.IdMainAccount
) as contNota on contNota.IdCuenta = dataNota.IdCuenta and contNota.Codigo = dataNota.Codigo
group by dataNota.Documento,dataNota.Fecha,dataNota.Codigo,dataNota.IdCuenta,dataNota.Cuenta,contNota.Debito,contNota.Credito

)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de auditoría de cartera que consolida y compara los movimientos contables esperados versus los registrados en el libro mayor para dos tipos de documentos: radicaciones de facturas confirmadas y notas de cartera aprobadas. Por cada documento, presenta débitos y créditos calculados desde el módulo de cartera junto a los débitos y créditos efectivamente contabilizados en los comprobantes de diario, permitiendo identificar diferencias o descuadres entre la cartera y la contabilidad.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de auditoría que confronta los movimientos esperados de cartera (radicaciones y notas) contra los registros contables efectivamente generados en los comprobantes de diario, para detectar diferencias por cuenta y documento.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las radicaciones a auditar deben estar en estado 2 (confirmadas); Las notas de cartera a auditar deben estar en estado 2 (confirmadas); Los comprobantes contables comparados deben estar en estado 2; Las cuentas por cobrar deben ser de tipo 2 para vincularse a la radicación; Los anticipos vinculados a notas requieren que la nota sea de tipo 4 (NoteType=4) para tomar el total distribuido como crédito', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se auditan documentos confirmados (State/Status = 2) tanto en cartera como en contabilidad; La conciliación se hace por par (Código de documento, Cuenta contable); Si no existe contrapartida contable, los valores DebitoContabilidad/CreditoContabilidad se muestran como 0 (ISNULL); La naturaleza de la nota determina si el valor afecta débito o crédito, manteniendo la dualidad contable; Las radicaciones siempre generan un movimiento espejo: crédito en cuenta sin radicar y débito en cuenta radicada por igual valor; Las fechas se truncan a fecha (sin hora) para la conciliación', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cartera; Cuentas por cobrar; Notas de cartera (débito/crédito); Anticipos de cartera; Comprobantes contables (Journal Vouchers); Plan de cuentas (MainAccounts); Conciliación contable / auditoría; Naturaleza débito-crédito; Distribución de notas', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve por cada Documento (Radicacion/Notas), Fecha, Código, Cuenta: los débitos y créditos esperados desde cartera y los efectivamente registrados en contabilidad (DebitoContabilidad/CreditoContabilidad)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rc.State = 2 en RadicateInvoiceC → Se incluye la radicación: genera crédito en la cuenta sin radicar (AccountWithoutRadicateId) y débito en la cuenta de radicado (AccountRadicateId) por el BalanceInvoice; si ar.AccountReceivableType = 2 → Solo cuentas por cobrar de este tipo se cruzan con el detalle de radicación; si n.Nature = 1 en PortfolioNote / PortfolioNoteDetail → El valor del ajuste o detalle se contabiliza como Débito; en otro caso como Crédito; si n.Status = 2 → Se incluyen los movimientos de la nota (anticipos asociados, detalle, distribución); si n.NoteType = 4 → Se agrega un crédito sobre la cuenta del PortfolioAdvance vinculado a la nota por la suma de PortfolioNoteDistribution.Value; si jv.EntityName = ''RadicateInvoiceC'' and jv.Status = 2 → Los detalles del comprobante se agregan como contrapartida contable de la radicación; si jv.EntityName = ''PortfolioNote'' and jv.Status = 2 → Los detalles del comprobante se agregan como contrapartida contable de la nota', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Portfolio.AccountReceivable; GeneralLedger.MainAccounts; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.PortfolioAdvance; Portfolio.PortfolioNoteDetail; Portfolio.PortfolioNoteDistribution', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'AuditorCartera';
GO
