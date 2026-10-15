
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	Listar DashBoard Areas Otros Procedimientos
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesOtrosProcedimientos]
@CentroAtencion varchar(MAX),
@AreaOtrosProcedimientos Varchar(4)
with recompile AS 
BEGIN

	SET NOCOUNT ON;
	--NO QX
select  I.CODTIPPAC as TipoPoblacion , 
			FECORDMED as FechaOrden,
			CA.CODICAMAS As CodCama,
			rtrim(CA.DESCCAMAS)  AS Cama,
			rtrim(F.UFUCODIGO) as CodigoUnidad,
			rtrim(F.UFUDESCRI)  as UnidadFuncionalActual,
			P.IPCODPACI as Identificacion,
			RTRIM(P.IPNOMCOMP) AS Paciente,
			RTRIM(P.IPCODPACI) + ' - '+ RTRIM(P.IPNOMCOMP) AS NombreCompletoPaciente,
			P.IPFECNACI AS FechaNacimiento ,[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(s.CODSERIPS ) as CodigoServicio,
			rtrim(s.CODSERIPS ) + ' - ' +rtrim(s.DESSERIPS) as Servicio,
			rtrim(CDD.Id) As IdDescRelacionada,
			isnull(rtrim(CD.name),'') as DescripcionRelacionada,
			dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 RTRIM(D.CODDIAGNO) + ' - ' + RTRIM(D.NOMDIAGNO) as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 Q.NUMEFOLIO as Folio,
			 CASE WHEN Q.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.AUTO EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion
			,rtrim(Q.OBSSERIPS) as Observacion
			,'NOQX' As Origen, Q.CANSERIPS
from HCORDPRON Q with(nolock) 
		inner join  INPACIENT P with(nolock) on P.IPCODPACI = Q.IPCODPACI 
		INNER JOIN ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		INNER JOIN ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES  AND I.IESTADOIN IN ('','P') 
		LEFT JOIN   CHCAMASHO CA with(nolock) on CA.CODICAMAS = I.CODCAMACT 
		INNER JOIN ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN  INUNIFUNC F with(nolock) on F.UFUCODIGO = I.UFUACTPAC 
		LEFT JOIN INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		LEFT JOIN  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS 
		LEFT JOIN  INDIAGNOS D with(nolock) ON D.CODDIAGNO = Q.CODDIAGNO 
		LEFT JOIN  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		LEFT JOIN  INESPECIA C with(nolock) on C.CODESPECI =I.CODESPTRA  
		INNER JOIN  HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS and  isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		INNER JOIN HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC  
		LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
WHERE Q.ESTSERIPS IN ('1') AND Q.SOLICITASALA = 0 AND S.SERIPSDASH = 12 AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) AND AC.ID = @AreaOtrosProcedimientos 
--Q.ESTSERIPS IN ('1','2') -> Listar los 2 no tiene sentido, cuando llega a estado 2:Confirmado, es cuando el CUPS no se hace en el centro de atención por ende desde la historia clinica los pasamos a este estado 2 directamente
-- cuando el CUPS si lo hacen en el centro de atención este cups desde la orden lo ponemos en estado 1:solicitado y cuando le hacen la HC lo pasamos a estado 2:Confirmado, asi es el deber ser
--Normalmente tienen mala parametrización y por eso hacen listar el estado 2 pero no es el deber ser, el deber ser es que hagan la parametrización correcta, comentario realizado 09-01-2024 juan patiño con davithson garavito
UNION  --QX
select  I.CODTIPPAC as TipoPoblacion ,
			FECORDMED as FechaOrden,
			CA.CODICAMAS As CodCama,
			rtrim(CA.DESCCAMAS)  AS Cama,
			rtrim(F.UFUCODIGO) as CodigoUnidad,
			rtrim(F.UFUDESCRI)  as UnidadFuncionalActual,
			P.IPCODPACI as Identificacion,
			RTRIM(P.IPNOMCOMP) AS Paciente,
			RTRIM(P.IPCODPACI) + ' - '+ RTRIM(P.IPNOMCOMP) AS NombreCompletoPaciente,
			P.IPFECNACI AS FechaNacimiento ,[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(s.CODSERIPS ) as CodigoServicio,
			rtrim(s.CODSERIPS ) + ' - ' +rtrim(s.DESSERIPS) as Servicio,
			rtrim(CDD.Id) As IdDescRelacionada,
			isnull(rtrim(CD.name),'') as DescripcionRelacionada,
			dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 RTRIM(D.CODDIAGNO) + ' - ' + RTRIM(D.NOMDIAGNO) as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 Q.NUMEFOLIO as Folio,
			 CASE WHEN Q.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.AUTO EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion 
			,rtrim(Q.OBSSERIPS) as Observacion
			,'QX' As Origen, Q.CANSERIPS
from HCORDPROQ  Q with(nolock) 
		inner join INPACIENT P with(nolock) on P.IPCODPACI = Q.IPCODPACI 
		INNER JOIN ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		INNER JOIN ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES  AND I.IESTADOIN IN ('','P') 
		LEFT JOIN   CHCAMASHO CA with(nolock) on CA.CODICAMAS = I.CODCAMACT 
		INNER JOIN ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN  INUNIFUNC F with(nolock)  on F.UFUCODIGO = I.UFUACTPAC 
		LEFT JOIN INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		LEFT JOIN  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS 
		LEFT JOIN  INDIAGNOS D with(nolock) ON D.CODDIAGNO = Q.CODDIAGNO 
		LEFT JOIN  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		LEFT JOIN INESPECIA C with(nolock) on C.CODESPECI =I.CODESPTRA  
		INNER JOIN  HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS  and isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		INNER JOIN HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC  
		LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN  contract.ContractDescriptions  CD with(nolock)  on CD.Id = CDD.ContractDescriptionId
WHERE Q.ESTSERIPS = '1' AND Q.MANEXTPRO = 0 AND ISNULL(Q.SOLICITASALA,0) = 0 AND S.SERIPSDASH = 12 AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) 
AND AC.ID = @AreaOtrosProcedimientos 
ORDER BY FECORDMED desc
END