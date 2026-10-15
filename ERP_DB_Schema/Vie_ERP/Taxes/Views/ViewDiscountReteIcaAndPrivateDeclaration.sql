








CREATE VIEW [Taxes].[ViewDiscountReteIcaAndPrivateDeclaration]
AS

select stri.Id,
stri.IdentificationNumber as Nit,
case tp.PersonType when 1 then 'Natural' else 'Jurídico' end as ReasonSocial,
(select top 1 Addresss from Common.[Address] where IdPerson = tp.PersonId) as Addresss,
(select top 1 Phone from Common.[Phone] where IdPerson = tp.PersonId) as Phone, 
TiebackTax as ValueOne, 
mag.TotalRetenido as ValueTwo,
(TiebackTax - mag.TotalRetenido) as DifferenceValue

from Taxes.SingleTaxReturnForInsdutry as stri
inner join (select IdentificationNumber, sum(TaxWithheld) as TotalRetenido from taxes.TaxesMagneticMediaDetail
 group by IdentificationNumber) as mag on stri.IdentificationNumber = mag.IdentificationNumber and TiebackTax <> mag.TotalRetenido
 inner join Common.ThirdParty tp on tp.Nit = stri.IdentificationNumber