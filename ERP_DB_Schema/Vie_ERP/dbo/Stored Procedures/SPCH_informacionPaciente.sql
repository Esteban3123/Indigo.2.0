
CREATE PROCEDURE [dbo].[SPCH_informacionPaciente]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

 SELECT
 d.NUMINGRES,
 d.CODDIAPRI,
 d.FECDIAGNO,
--Consulta que tenga un diagnostico inicial (d - INDIAGNOS)
case WHEN D.NOMDIAGNO IS NULL THEN 'No Tiene Diagnostico Principal' else RTRIM(d.NOMDIAGNO) end as DaignostcoPrincipal,
--Nombre Persona (p - INPACIENT)
RTRIM(p.IPNOMCOMP) AS  NombreCompleto, 
-- Edad (b - INPACIENT)
p.IPFECNACI as Edad,
-- Grupo Sanguineo (b - INPACIENT)
CASE WHEN p.IPGRUPSAN IS NULL THEN 'No Tiene Grupo Sanguineo' else p.IPGRUPSAN end  as GrupoSanguineo,
-- Datos Personales (p - INPACIENT)
p.IPCODPACI as Identificacion,
-- Direccion (p - INPACIENT)
p.IPDIRECCI as Direccion,
-- Telefono (p - INPACIENT)
p.IPTELMOVI as Telefono,
-- Genero (p - INPACIENT)
case p.IPSEXOPAC when 1 then 'Masculino' when 2 then 'Femenino' end as Sexo,
-- Fecha Nacimiento (b - INPACIENT)
convert(varchar, p.IPFECNACI, 105) as FechaNacimiento
FROM INPACIENT p
left join (SELECT [NUMINGRES] ,[IPCODPACI] ,[NOMDIAGNO]  ,[CODDIAPRI], FECDIAGNO FROM [INDIAGNOH] rel inner join [INDIAGNOS] diag on rel.CODDIAGNO = diag.CODDIAGNO) as d on d.IPCODPACI = p.IPCODPACI
WHERE p.IPCODPACI= @Paciente
End