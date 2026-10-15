Create PROCEDURE [dbo].[ESE_SP_Referencia_HC_Odontologia]
@FechaIni DateTime,
@FechaFin DateTime
AS

SELECT CASE C.IPTIPODOC  WHEN 1 THEN 'Cédula de Ciudadanía' When 2 Then 'Cédula de Extranjería' When 3 Then 'Tarjeta de Identidad' When 4 Then 'Registro Civil' When 5 Then 'Pasaporte' When 6 Then 'Adulto Sin Identificación' When 7 Then 'Menor Sin Identificación' When 8 Then 'Número único de identificación personal' When 9 Then 'Certificado Nacido Vivo' When 10 Then 'Carnet Diplomático' When 11 Then 'Salvoconducto' When 12 Then 'Permiso especial de Permanencia' END AS 'Tipo de ID del paciente',
			C.IPCODPACI AS 'Numero de id del paciente',C.IPPRIAPEL AS 'Apellido 1',C.IPSEGAPEL AS 'Apellido 2',C.IPPRINOMB AS 'Nombre 1',C.IPSEGNOMB AS 'Nombre 2',C.IPFECNACI AS 'Fecha de nacieminto', (cast(datediff(dd,IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as 'Edad en años',
			CASE IPSEXOPAC WHEN 1 THEN 'Masculino' when 2 then 'Femenino' end as 'Sexo', D.NUMINGRES AS'Ingreso',
			E.Code AS 'Código Grupo de atención',E.Name AS 'Nombre Grupo de atención',F.CODENTIDA as 'Código Entidad',F.NOMENTIDA as 'Nombre entidad',
			case EntityType when 1 then 'EPS Contributivo' when 2 then 'EPS Subsidiado' when 3 then 'ET Vinculados Municipios' when 4 then 'ET Vinculados Departamentos' when 5 then 'ARL Riesgos Laborales' when 6 then 'MP Medicina Prepagada' when 7 then 'IPS Privada' when 8 then 'IPS Publica' when 9 then 'Regimen Especial' when 10 then 'Accidentes de transito' when 11 then 'Fosyga' when 12 then 'Otros' when 13 then 'Aseguradoras' when 99 then 'Particulares' end as 'Tipo de entidad',
			G.CODPROSAL AS 'Código del profesional',G.NOMMEDICO AS 'Nombre del profesional',
			I.CODDIAGNO AS 'Codigo Diagnostico principal',
			I.NOMDIAGNO AS 'Diagnostico principal'
From dbo.HCHISPACA  A
	INNER JOIN dbo.INPACIENT C WITH (NOLOCK) ON A.IPCODPACI = C.IPCODPACI 
	INNER JOIN dbo.HCREFCONT z WITH (NOLOCK) ON A.IPCODPACI = z.IPCODPACI AND a.NUMINGRES = z.NUMINGRES AND z.NUMEFOLIO = a.NUMEFOLIO
	INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES = D.NUMINGRES 
	INNER JOIN Contract.CareGroup E WITH (NOLOCK) ON E.Id = D.GENCAREGROUP 
	INNER JOIN dbo.INENTIDAD F WITH (NOLOCK) ON F.CODENTIDA = D.CODENTIDA 
	INNER JOIN dbo.INPROFSAL G WITH (NOLOCK) ON G.CODPROSAL = A.CODPROSAL 
	INNER JOIN dbo.INDIAGNOS  I WITH (NOLOCK) ON I.CODDIAGNO  = A.CODDIAGNO
	INNER  JOIN  dbo.HCRIESGOSP J  WITH (NOLOCK) ON J.NUMINGRCES = A.NUMINGRES 
Where  a.IDMODELOHC IN (33,28,42) AND  A.FECHISPAC  BETWEEN @FechaIni and @FechaFin