







CREATE VIEW [Taxes].[ViewOmissives]
AS

SELECT 1 AS Id
/*
select tom.Id,
tf.Nit as Nit, 
case tp.PersonType when 1 then 'Natural' else 'Jurídico' end as ReasonSocial,
(select top 1 Addresss from Common.[Address] where IdPerson = tp.PersonId) as Addresss,
(select top 1 Phone from Common.[Phone] where IdPerson = tp.PersonId) as Phone,
tf.[Year] 
from Taxes.TempOmissive tom
inner join Taxes.TempFile as tf on tf.Nit = tom.Nit 
inner join Common.ThirdParty as tp on tf.Nit = tp.Nit 

*/