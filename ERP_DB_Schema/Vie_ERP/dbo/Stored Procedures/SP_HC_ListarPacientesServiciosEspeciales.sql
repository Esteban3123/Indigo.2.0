

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesServiciosEspeciales]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

select distinct P.IPCODPACI as Identificacion, rtrim(ltrim(P.IPNOMCOMP)) as NombrePaciente, rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as Entidad,
f.CODCENATE,f.UFUCODIGO,
(select top 1 FECHINGRES from ..HCONCOFICH  where IPCODPACI = F.IPCODPACI order by FECHINGRES asc ) as FechaIngresoPrograma
from ..INPACIENT p inner join  ..HCONCOFICH  F on P.IPCODPACI = F.IPCODPACI inner join 
..INENTIDAD ENT on ENT.CODENTIDA = P.CODENTIDA 
 where f.CODCENATE = @CentroAtencion and UFUCODIGO = @UnidadFuncional
end