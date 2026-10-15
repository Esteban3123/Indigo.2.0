



CREATE VIEW [Taxes].[VMassivePersuasivePayment]
AS
select ar.Id, ar.AccountReceivableType as Tipo ,case ar.AccountReceivableType when 3 then 'Industria y Comercio' when 8 then 'Impuesto Predial' end TipoImpuesto, ar.InvoiceNumber as Factura,
tp.nit as NitTercero,  tp.Name as NombreTercero,  ExpiredDate as FechaVencimiento, ar.Value as Valor,  Balance as Saldo, '2017' as Vigencia
from Portfolio.AccountReceivable as ar left outer join Common.ThirdParty as tp on ar.ThirdPartyId = tp.id  where AccountReceivableType in (3,8)