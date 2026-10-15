
CREATE Procedure [dbo].[SPHC_ListarPacientesAtendidosOtrosProcedimientos]
(
@CentroAtencion varchar(MAX),
@AreaOtrosProcedimientos Varchar(4),
@CodSala varchar(6),
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;

select I.CODTIPPAC as TipoPoblacion , 
			ISNULL(cita.FECHORAIN, Q.FECHAREG) as FechaOrden,
			'' As CodCama,
			''  AS Cama,
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
			'' AS Aislamiento,
			CONVERT(BIT,0) AS Riesgo,
			I.NUMINGRES as Ingreso,
			 RTRIM(E.CODENTIDA) + ' - '+ RTRIM(E.NOMENTIDA) as EntidadPaciente,
			 '' as Diagnostico,
			RTRIM(PRO.CODPROSAL) + ' - ' + RTRIM(PRO.NOMMEDICO) as Profesional,
			RTRIM(C.CODESPECI) + ' - ' + RTRIM(C.DESESPECI) as Especialidad,
			 CASE WHEN P.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS,
			 P.ZONAPARTADA,
			 L.RIESGOAGRE,
			 (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = P.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
			 I.VIVESOLO,
			 (SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = I.NUMINGRES) AS ACOMPANANTES,
			 '' as Folio,
			 '' AS Prioridad
			 ,'Autorización' AS Autorizacion
			,Q.ID EntityId
			,rtrim(Cent.CODCENATE) as CodigoCentro
			,rtrim(Cent.NOMCENATE) as CentroAtencion
			,rtrim(Q.OBSERVACION) as Observacion
			,Q.IDCITA 
			,Q.IDCITA 
			,Rtrim(DESCRIPSAL) as 'Sala', F.UFUTIPUNI
			,Rtrim(isnull(Cita.CODACTMED,'')) as CodigoActividad, q.CANTIDAD as CANSERIPS
			,iif(Q.CODESPECI is null,'',Q.CODESPECI) as CodigoEspecialidad			
from AMBORDOTROSPRO Q 
		inner join INPACIENT P with(nolock)  on P.IPCODPACI = Q.IPCODPACI 
		inner join ADCENATEN Cent with(nolock) on Cent.CODCENATE = Q.CODCENATE 
		inner join ADINGRESO I with(nolock) on I.NUMINGRES = Q.NUMINGRES and IESTADOIN in ('','P','B')
		inner join ADACTIVID L with(nolock) ON P.CODACTIVI = L.codactivi 
		INNER JOIN INUNIFUNC F with(nolock) on F.UFUCODIGO = I.UFUCODIGO 
		left join  INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA 
		left join  INCUPSIPS S with(nolock) on s.CODSERIPS = Q.CODSERIPS  
		left join  INPROFSAL PRO with(nolock) ON PRO.CODPROSAL = Q.CODPROSAL 
		left join  INESPECIA C with(nolock) on C.CODESPECI =Q.CODESPECI 
		inner join HCAREASD AD with(nolock) on AD.CODSERIPS = Q.CODSERIPS and  isnull(AD.IDDESCRIPCIONRELACIONADA,0) = isnull(Q.IDDESCRIPCIONRELACIONADA,0) 
		inner join HCAREASC AC with(nolock) on AC.ID = AD.IDAREASC AND AC.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))  LEFT JOIN 
		contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = Q.IDDESCRIPCIONRELACIONADA LEFT JOIN 
		contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId 
		left join AGASICITA Cita with(nolock) on cita.CODAUTONU = Q.IDCITA 
		left JOIN AGENSALAC Sala with(nolock) on  sala.CODCONCEC = Cita.IDSALA		
		OUTER APPLY (
						SELECT TOP 1 h.INDICAPAC
						FROM HCHISPACA h
						WHERE h.IPCODPACI = Q.IPCODPACI 
						AND h.NUMINGRES = Q.NUMINGRES
						ORDER BY h.FECHISPAC DESC
					) AS UltimoDestino
WHERE (Q.ESTADO = '2' AND UltimoDestino.INDICAPAC <> '13')
AND Q.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))
AND AC.ID  = @AreaOtrosProcedimientos 
AND Convert(Date,Q.FECHAREG) = @FechaInicial

END