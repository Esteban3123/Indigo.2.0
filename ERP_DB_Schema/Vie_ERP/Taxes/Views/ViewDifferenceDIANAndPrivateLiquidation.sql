







CREATE VIEW [Taxes].[ViewDifferenceDIANAndPrivateLiquidation]
AS

select tp.Id,
tp.Nit as Nit, 
case tp.PersonType when 1 then 'Natural' else 'Jurídico' end as ReasonSocial,
(select top 1 Addresss from Common.[Address] where IdPerson = tp.PersonId) as Addresss,
(select top 1 Phone from Common.[Phone] where IdPerson = tp.PersonId) as Phone, 
0 as ValueOne, 
stri.TotalTaxableOrdinaryAndExtraordinary as ValueTwo,
(stri.TotalTaxableOrdinaryAndExtraordinary) as DifferenceValue,
2022 as Year
from  Common.ThirdParty as tp
inner join Taxes.SingleTaxReturnForInsdutry as stri on stri.IdentificationNumber = tp.Nit and stri.TaxablePeriod = 2022