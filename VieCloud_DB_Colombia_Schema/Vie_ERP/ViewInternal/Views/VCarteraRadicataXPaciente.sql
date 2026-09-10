

CREATE view [ViewInternal].[VCarteraRadicataXPaciente]
as
(
select 
rc.RadicatedConsecutive as Radicado
, tp.Nit as Nit
, tp.Name as Entidad
,ar.InvoiceNumber as Factura
,case paci.IPTIPODOC when '1' then 'Cédula de Ciudadanía' when '2' then 'Cédula de Extranjería' when '3' then 'Tarjeta de Identidad' when '4' then 'Registro Civil' when '5' then 'Pasaporte' when '6' then 'Adulto Sin Identificación' when '7' then 'Menor Sin Identificación' else '' end as TipoIdentificacionPaciente
,rd.PatientCode as IdentificacionPaciente
,rd.PatientName as NombrePaciente
,rd.IngressDate as FechaIngreso
,cast(rd.InvoiceDate as date) as FechaFactura
,rc.ConfirmDateSystem as FechaRadicado
,isnull(fur.numsoa,'') as NumeroPoliza
,cast(rd.InvoiceValueEntity + rd.InvoiceValuePacient as decimal(18,0)) as ValorBrutoFactura
,cast(rd.InvoiceValuePacient as decimal(18,0)) as ValorCuotaRecuperacion
,cast(isnull(rd.CreditNoteValue,0)as decimal(18,0)) as ValorNotaCredito
,cast(isnull(rd.DebitNoteValue,0)as decimal(18,0)) as ValorNotaDebito
,cast(rd.BalanceInvoice as decimal(18,0)) as ValorNetoFactura
,isnull(notas.Value,0) as TotalNotas
,isnull(pagos.Valor,0) as TotalPagos
,ar.Balance as SaldoActual
from Portfolio.AccountReceivable ar
inner join Common.ThirdParty as tp on ar.ThirdPartyId = tp.Id
inner join Portfolio.RadicateInvoiceD rd on rd.InvoiceNumber = ar.InvoiceNumber and rd.State = 2
inner join Portfolio.RadicateInvoiceC rc on rc.Id = rd.RadicateInvoiceCId and rc.State = 2
left join (select ar.InvoiceNumber, sum(ptd.Value) as Valor from Portfolio.PortfolioTransfer pt
inner join Portfolio.PortfolioTransferDetail ptd on pt.Id = ptd.PortfolioTrasferId
inner join Portfolio.AccountReceivable ar on ar.Id = ptd.AccountReceivableId
where pt.Status = 2 and ar.AccountReceivableType = 2
group by ar.InvoiceNumber) as pagos on pagos.InvoiceNumber = ar.InvoiceNumber
left join (select ar.InvoiceNumber, sum(pnd.AdjusmentValue) as Value from Portfolio.PortfolioNote pn
inner join Portfolio.PortfolioNoteAccountReceivableAdvance pnd on pnd.PortfolioNoteId = pn.Id
inner join Portfolio.AccountReceivable ar on ar.Id = pnd.AccountReceivableId
where ar.AccountReceivableType = 2
group by ar.InvoiceNumber) as notas on notas.InvoiceNumber = ar.InvoiceNumber
left join dbo.INPACIENT paci on paci.IPCODPACI = rd.PatientCode
left join dbo.ADFURIPSU AS FUR on fur.NUMINGRES = rd.ingressnumber
where ar.AccountReceivableType = 2
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting de cartera radicada a nivel de paciente. Consolida facturas radicadas ante entidades pagadoras (EPS, aseguradoras) en estado confirmado, mostrando datos del paciente, tipo de identificación, valores brutos, notas crédito/débito, cuota de recuperación, pagos y saldo actual por cuenta por cobrar. Incluye el número de póliza FURIPS para casos de accidentes de tránsito. Filtra exclusivamente cuentas por cobrar de tipo paciente (`AccountReceivableType = 2`).', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la cartera radicada por paciente y factura, mostrando datos del radicado, entidad pagadora, paciente, valores brutos/netos, notas crédito/débito, pagos y saldo actual.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas y radicados deben estar en estado 2 (confirmados/activos) para ser incluidos.; Solo se consideran cuentas por cobrar de tipo 2 (AccountReceivableType = 2).; Las transferencias de cartera deben tener Status = 2 para sumar como pagos.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor bruto de la factura se calcula como InvoiceValueEntity + InvoiceValuePacient.; Las notas crédito/débito y los totales de notas/pagos por defecto son 0 cuando no existen registros relacionados (ISNULL).; Solo se incluyen radicados de facturas cuyo encabezado y detalle estén ambos en State = 2.; Los pagos provienen exclusivamente de transferencias de cartera confirmadas (Status = 2) sobre cuentas por cobrar tipo 2.; El número de póliza se toma del FURIPS asociado al número de ingreso del paciente; si no existe se devuelve cadena vacía.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera radicada; Factura; Paciente; Tipo de identificación; Entidad pagadora (NIT); Cuota de recuperación; Nota crédito; Nota débito; Saldo de factura; Pagos / transferencias de cartera; Póliza SOAT (FURIPS); Radicado de cobro', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto de resultados con la cartera radicada por paciente filtrando ar.AccountReceivableType = 2.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si paci.IPTIPODOC = ''1'' → Etiqueta el tipo de identificación como ''Cédula de Ciudadanía'' else Otros valores mapean a CE (2), TI (3), RC (4), Pasaporte (5), Adulto Sin Identificación (6), Menor Sin Identificación (7) o cadena vacía si no coincide.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.RadicateInvoiceD; Portfolio.RadicateInvoiceC; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; dbo.INPACIENT; dbo.ADFURIPSU', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VCarteraRadicataXPaciente';
GO
