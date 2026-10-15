
CREATE VIEW [Common].[ViewThirdPartyDocuments]
AS
select convert( varchar(20), IdThirdParty) + Code+'635'  as [Key], IdThirdParty as ThirdPartyId, 'Recibos de Caja' as Process, Code,DocumentDate as DocumentDate,Status as Status,'635' as TagForm  from Treasury.CashReceipts 
union all
select convert( varchar(20), IdThirdParty)+Code+'636' as [Key],IdThirdParty as ThirdPartyId, 'Comprobantes de Egreso' as Process, Code,DocumentDate as DocumentDate,Status as Status,'636' as TagForm  from Treasury.VoucherTransaction where IdThirdParty is not null
union all 
select convert( varchar(20), ThirdPartyId)+Code+'682' as [Key],ThirdPartyId  as ThirdPartyId, 'Cuentas por Cobrar' as Process, Code,AccountReceivableDate  as DocumentDate,Status as Status,'682' as TagForm  from Portfolio.AccountReceivable 
union all
select convert( varchar(20), ThirdPartyId)+Code+'1507' as [Key],ThirdPartyId  as ThirdPartyId, 'Anticipos de Cartera' as Process, Code,DocumentDate as DocumentDate,Status as Status,'' as TagForm  from Portfolio.PortfolioAdvance
union all
select convert( varchar(20), IdThirdParty)+Code+'730' as [Key],IdThirdParty as ThirdPartyId, 'Cuentas por Pagar' as Process, Code,DocumentDate as DocumentDate,Status as Status,'730' as TagForm  from Payments.AccountPayable where IdThirdParty is not null
union all 
select convert( varchar(20), IdThirdParty)+Code+'999' as [Key],IdThirdParty as ThirdPartyId, 'Anticipos de Pagos' as Process, Code,DocumentDate as DocumentDate,Status as Status,'' as TagForm  from Payments.AdvancePayments  where IdThirdParty is not null