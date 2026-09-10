

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra contribuyentes o terceros omisos en el módulo de impuestos, es decir, aquellas personas naturales o jurídicas que no han cumplido con sus obligaciones tributarias en un período determinado. Actualmente el cuerpo real de la consulta está comentado (inactivo), por lo que retorna únicamente un valor estático de Id=1 sin datos reales. Cuando esté activa, integrará la tabla de omisos temporales (TempOmissive), los archivos de impuestos (TempFile) y los terceros (ThirdParty) para exponer el NIT, tipo de persona (natural o jurídica), dirección, teléfono y año fiscal del incumplimiento. Sirve de base para reportes de seguimiento y control de declarantes omisos ante obligaciones fiscales.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewOmissives';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'VIEW', @level1name = N'ViewOmissives';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista placeholder que retorna un valor constante; la lógica real para listar terceros omisos tributarios está comentada y deshabilitada.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewOmissives';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La vista siempre retorna una única fila con valor constante 1, sin consultar tablas reales (lógica original comentada).', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewOmissives';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Siempre devuelve una sola fila con Id=1, sin acceder a ninguna tabla.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewOmissives';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'VIEW', @level1name=N'ViewOmissives';
GO
