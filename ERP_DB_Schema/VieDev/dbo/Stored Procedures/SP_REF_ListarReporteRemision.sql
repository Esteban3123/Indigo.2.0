
-- =============================================
-- Author:		<Yezid Garcia Medina, Desarrollador Junior>
-- Create date: <18 Noviembre de 2019>
-- Description:	<Listar Reporte Remision>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarReporteRemision]
(
	@Centro varchar(20),
	@FechaInicio Datetime ,
	@FechaFin Datetime 
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here	
	SELECT
	
		[Fecha solicitud] ,
		[Ingreso] ,
		[Codigo centro de atencion] ,
		[Centro de atencion] ,
		[Codigo unidad remision] ,
		[Unidad remision] ,
		[Manejo extramural]	,
		[Tipo de identificacion] ,
		[Numero de identificacion] ,
		[Nombres y apellidos] ,
		[Primer nombre] , 
		[Segundo nombre] , 
		[Primer apellido] , 
		[Segundo apellido] ,
		[Fecha nacimiento] ,
		[Edad en años] ,
		[Edad completa] ,
		[Sexo] ,
		[Codigo grupo de atencion] ,
		[Grupo de atencion] ,
		[Codigo entidad] ,
		[Entidad] ,
		[Codigo regimen] ,
		[Regimen],		
		[Codigo motivo remision] ,
		[Motivo remision] ,
		[Codigo dx] , 
		[Diagnostico] , 
		[Codigo servicio al que se remite] ,
		[Servicio al que se remite] ,
		[Codigo especialidad a la que se remite] ,
		[Especialidad a la que se remite] ,	
		[Codigo entidad a la que se remite] ,
		[Entidad a la que se remite] ,	
		[Codigo profesional que remite] ,
		[Profesional que remite] ,
		MIN([Fecha ingreso solicitado sin definir pertinencia] ) AS [Fecha ingreso solicitado sin definir pertinencia],
		MIN([Tiempo en solicitado sin definir pertinencia] ) AS [Tiempo en solicitado sin definir pertinencia],
		MIN([Fecha ingreso solicitado con pertinencia medica] ) AS [Fecha ingreso solicitado con pertinencia medica],
		MIN([Tiempo en solicitado con pertinencia medica] ) AS [Tiempo en solicitado con pertinencia medica],
		MIN([Fecha ingreso gestionando] ) AS [Fecha ingreso gestionando]  , 
		MIN ([Tiempo en gestionando] ) AS [Tiempo en gestionando] ,
		MIN([Fecha ingreso aceptado con pendiente de salida] ) AS [Fecha ingreso aceptado con pendiente de salida],
		MIN([Tiempo en aceptado con pendiente de salida]  ) AS [Tiempo en aceptado con pendiente de salida],
		[Fecha de salida - orden medica] ,	
		MAX([Fecha aceptacion]) AS [Fecha aceptacion], 			
		[Fecha del egreso] ,			
		[Tiempo total espera] ,
		[Codigo estado solicitud] ,
		[Estado solicitud] ,
		[Fecha suspension],
		[Codigo motivo suspension] ,
		[Motivo suspension] ,
		[Justificacion]

	 FROM
	 
	(
		SELECT
			A.FECSOLICIT AS [Fecha solicitud] ,
			RTRIM(F.NUMINGRES) AS [Ingreso] ,
			RTRIM(D.CODCENATE) AS [Codigo centro de atencion] ,
			RTRIM(D.NOMCENATE) AS [Centro de atencion] ,
			RTRIM(E.UFUCODIGO) AS [Codigo unidad remision] ,
			RTRIM(E.UFUDESCRI) AS [Unidad remision] ,
			CASE A.EXTRAMURAL
				WHEN 1 THEN 'Si'
				ELSE 'No'
			END AS [Manejo extramural] ,
			CASE
				WHEN G.IPTIPODOC = 1 THEN 'C.C.'
				WHEN G.IPTIPODOC = 2 THEN 'C.E.'
				WHEN G.IPTIPODOC = 3 THEN 'T.I.'
				WHEN G.IPTIPODOC = 4 THEN 'Registro Civil'
				WHEN G.IPTIPODOC = 5 THEN 'Pasaporte'
				WHEN G.IPTIPODOC = 6 THEN 'Adulto Sin Identificación'
				WHEN G.IPTIPODOC = 7 THEN 'Menor Sin Identificación'
				WHEN G.IPTIPODOC = 8 THEN 'Número único de identificación personal'
			END AS [Tipo de identificacion] ,
			RTRIM(G.IPCODPACI) AS [Numero de identificacion] ,
			RTRIM(G.IPNOMCOMP) AS [Nombres y apellidos] ,
			RTRIM(G.IPPRINOMB) AS [Primer nombre] , 
			RTRIM(G.IPSEGNOMB) AS [Segundo nombre] , 
			RTRIM(G.IPPRIAPEL)AS [Primer apellido] , 
			RTRIM(G.IPSEGAPEL) AS [Segundo apellido] ,
			G.IPFECNACI AS [Fecha nacimiento] ,
			cast ( datediff ( dd, G.IPFECNACI ,GETDATE( ) ) / 365.25 as int ) AS [Edad en años] ,
			dbo.EDAD( G.IPFECNACI, [Common].[GETDATE]() ) AS [Edad completa] ,
			CASE G.IPSEXOPAC
				WHEN 1 THEN 'Masculino'
				WHEN 2 THEN 'Femenino'
			END AS [Sexo] ,
			RTRIM(K.Code) AS [Codigo grupo de atencion] ,
			RTRIM(K.Name) AS [Grupo de atencion] ,
			RTRIM(L.Code) AS [Codigo entidad] ,
			RTRIM(L.Name) AS [Entidad] ,
			L.EntityType AS [Codigo regimen] ,
			CASE L.EntityType
				WHEN 1 THEN 'EPS Contributivo'
				WHEN 2 THEN 'EPS Subsidiado'
				WHEN 3 THEN 'ET Vinculados Municipios'
				WHEN 4 THEN 'ET Vinculados Departamentos'
				WHEN 5 THEN 'ARL Riesgos Laborales'
				WHEN 6 THEN 'MP Medicina Prepagada'
				WHEN 7 THEN 'IPS Privada'
				WHEN 8 THEN 'IPS Publica'
				WHEN 9 THEN 'Regimen Especial'
				WHEN 10 THEN 'Accidentes de transito'
				WHEN 11 THEN 'Fosyga'
				WHEN 12 THEN 'Otros'
				WHEN 13 THEN 'Aseguradoras'
				WHEN 99 THEN 'Particulares'
			END	AS [Regimen],			
			RTRIM(M.Codigo) AS [Codigo motivo remision] ,
			RTRIM(M.Nombre) AS [Motivo remision] ,
			RTRIM(H.CODDIAGNO) AS [Codigo dx] , 
			RTRIM(H.NOMDIAGNO) AS [Diagnostico] , 
			RTRIM(N.Codigo) AS [Codigo servicio al que se remite] ,
			RTRIM(N.Nombre) AS [Servicio al que se remite] ,			
			RTRIM(I.CODESPECI) AS [Codigo especialidad a la que se remite] ,
			RTRIM(I.DESESPECI) AS [Especialidad a la que se remite] ,			
			CASE
				WHEN temp1.FECHCONFIR IS NOT NULL AND B.ESTADO = 3 OR  B.ESTADO = 5 THEN  RTRIM(R.CODENTIDA)
				ELSE null
			END AS [Codigo entidad a la que se remite] ,
			CASE
				WHEN temp1.FECHCONFIR IS NOT NULL AND B.ESTADO = 3 OR  B.ESTADO = 5 THEN RTRIM(R.NOMENTIDA)
				ELSE null
			END AS [Entidad a la que se remite] ,			
			RTRIM(J.CODPROSAL) AS [Codigo profesional que remite] ,
			RTRIM(J.NOMMEDICO) AS [Profesional que remite] ,
			A.FECSOLICIT AS [Fecha ingreso solicitado sin definir pertinencia] ,	
			dbo.DiferenciaTXTDiasHoras( A.FECSOLICIT , iif( B.FECHSIPERTINEN IS NOT NULL, B.FECHSIPERTINEN ,
										iif( B.FECHASUSPEN IS NOT NULL, B.FECHASUSPEN , [Common].[GETDATE]() ) )) AS [Tiempo en solicitado sin definir pertinencia] ,
			B.FECHSIPERTINEN AS [Fecha ingreso solicitado con pertinencia medica] ,
			dbo.DiferenciaTXTDiasHoras( B.FECHSIPERTINEN , iif( temp2.FECSEGUIMIENTO IS NOT NULL, temp2.FECSEGUIMIENTO , 
										iif( B.FECHASUSPEN IS NOT NULL, B.FECHASUSPEN , [Common].[GETDATE]() ) )) AS [Tiempo en solicitado con pertinencia medica] ,
			temp2.FECSEGUIMIENTO AS [Fecha ingreso gestionando] ,
			dbo.DiferenciaTXTDiasHoras( temp2.FECSEGUIMIENTO , iif( temp1.FECHCONFIR IS NOT NULL, temp1.FECHCONFIR ,  
										iif( B.FECHASUSPEN IS NOT NULL, B.FECHASUSPEN , [Common].[GETDATE]() ) )) AS [Tiempo en gestionando] ,
			temp1.FECHCONFIR AS [Fecha ingreso aceptado con pendiente de salida] ,
			dbo.DiferenciaTXTDiasHoras( temp1.FECHCONFIR , iif( CASE A.EXTRAMURAL WHEN 1 THEN Q.FECALTPAC	ELSE F.FECHEGRESO END  IS NOT NULL, 
										CASE A.EXTRAMURAL WHEN 1 THEN Q.FECALTPAC	ELSE F.FECHEGRESO END  ,  
										iif( B.FECHASUSPEN IS NOT NULL, B.FECHASUSPEN , [Common].[GETDATE]() ) )) AS [Tiempo en aceptado con pendiente de salida] ,
			Q.FECALTPAC AS [Fecha de salida - orden medica] ,			
			temp1.FECHCONFIR AS [Fecha aceptacion] , 			
			CASE A.EXTRAMURAL
				WHEN 1 THEN Q.FECALTPAC
				ELSE F.FECHEGRESO
			END AS [Fecha del egreso] ,			
			dbo.DuracionTotalRemision( A.FECSOLICIT , B.FECHSIPERTINEN, temp2.FECSEGUIMIENTO, temp1.FECHCONFIR, CASE A.EXTRAMURAL WHEN 1 THEN Q.FECALTPAC	ELSE F.FECHEGRESO END , B.FECHASUSPEN ) AS [Tiempo total espera] ,
			B.ESTADO AS [Codigo estado solicitud] ,
			CASE B.ESTADO
				WHEN 1 THEN '1. Solicitado sin definir pertinencia'
				WHEN 2 THEN '3. Gestionando'
				WHEN 3 THEN '4. Aceptado con pendiente de salida'
				WHEN 6 THEN '2. Solicitado con pertinencia médica'
				WHEN 4 THEN 'Suspendido'
				WHEN 5 THEN 'Ya salio'
			END AS [Estado solicitud] ,
			B.FECHASUSPEN AS [Fecha suspension],
			RTRIM(O.Codigo) AS [Codigo motivo suspension] ,
			RTRIM(O.Nombre) AS [Motivo suspension] ,
			RTRIM(B.JUSTSUSPEN) AS [Justificacion]

		FROM
			HCREFCONT AS A with (nolock)
			INNER JOIN HCREFCONP AS B with (nolock)
			ON A.IDHCREFCONP = B.AUTO
			INNER JOIN HCREFCONTDET AS C with (nolock)
			ON A.AUTO = C.HCREFCONTAUTO
			INNER JOIN ADCENATEN AS D with (nolock)
			ON A.CODCENATE = D.CODCENATE
			INNER JOIN INUNIFUNC AS E with (nolock)
			ON A.UFUCODIGO = E.UFUCODIGO
			INNER JOIN ADINGRESO AS F with (nolock)
			ON A.NUMINGRES = F.NUMINGRES
			INNER JOIN INPACIENT AS G with (nolock)
			ON A.IPCODPACI = G.IPCODPACI
			INNER JOIN INDIAGNOS AS H with (nolock)
			ON C.CODDIAGNO = H.CODDIAGNO						
			INNER JOIN INESPECIA AS I with (nolock)
			ON A.CODESPECI = I.CODESPECI
			INNER JOIN INPROFSAL AS J with (nolock)
			ON A.CODPROSAL = J.CODPROSAL			
			INNER JOIN Contract.CareGroup AS K with (nolock)
			ON F.GENCAREGROUP = K.Id
			INNER JOIN Contract.HealthAdministrator AS L with (nolock)
			ON F.GENCONENTITY = L.Id	
			INNER JOIN RCMOTREF AS M with (nolock)
			ON A.MOTREMISI = M.Id	
			INNER JOIN RCSERVICIOS AS N with (nolock)
			ON A.SERVIDOREM = N.Codigo
			LEFT JOIN RCMOTNOREF AS O with (nolock)
			ON B.RCMOTNOREFID = O.Id			
			LEFT JOIN (SELECT row_number() over(partition by P1.HCREFCONPID order by P1.FECHCONFIR  desc) idpart, b1.auto, P1.id, p1.CODENTIDA, P1.FECHCONFIR FROM HCREFCONT AS A1 with (nolock)
				INNER JOIN HCREFCONP AS B1 with (nolock)
				ON A1.IDHCREFCONP = B1.AUTO 
				INNER JOIN HCREFCONTD AS P1 
				ON B1.AUTO = P1.HCREFCONPID
				WHERE 
				A1.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				A1.FECREGSIS BETWEEN  @FechaInicio  AND @FechaFin and
				P1.FECHCONFIR IS NOT NULL ) temp1
			ON temp1.auto = b.auto	AND temp1.idpart = 1
			LEFT JOIN (SELECT row_number() over(partition by P2.HCREFCONPID order by P2.FECSEGUIMIENTO  asc) idpart, b2.auto, P2.id, P2.FECSEGUIMIENTO FROM HCREFCONT AS A2 with (nolock)
				INNER JOIN HCREFCONP AS B2 with (nolock)
				ON A2.IDHCREFCONP = B2.AUTO 
				INNER JOIN HCREFCONTD AS P2 
				ON B2.AUTO = P2.HCREFCONPID  
				WHERE 
				A2.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro))
				AND
				A2.FECREGSIS BETWEEN  @FechaInicio  AND @FechaFin) temp2
			ON temp2.auto = b.auto	AND temp2.idpart = 1
			LEFT JOIN HCREGEGRE AS Q with (nolock)
			ON A.NUMINGRES = Q.NUMINGRES
			LEFT JOIN INENTIDAD AS R with (nolock)
			ON temp1.CODENTIDA = R.CODENTIDA
			

		WHERE	

			A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@Centro))
			AND
			A.FECREGSIS BETWEEN  @FechaInicio  AND @FechaFin				
		
		) AS T1

		GROUP BY
		[Fecha solicitud] ,
		[Ingreso] ,
		[Codigo centro de atencion] ,
		[Centro de atencion] ,
		[Codigo unidad remision] ,
		[Unidad remision] ,
		[Manejo extramural]	,
		[Tipo de identificacion] ,
		[Numero de identificacion] ,
		[Nombres y apellidos] ,
		[Primer nombre] , 
		[Segundo nombre] , 
		[Primer apellido] , 
		[Segundo apellido] ,
		[Fecha nacimiento] ,
		[Edad en años] ,
		[Edad completa] ,
		[Sexo] ,
		[Codigo grupo de atencion] ,
		[Grupo de atencion] ,
		[Codigo entidad] ,
		[Entidad] ,
		[Codigo regimen] ,
		[Regimen],		
		[Codigo motivo remision] ,
		[Motivo remision] ,
		[Codigo dx] , 
		[Diagnostico] , 
		[Codigo servicio al que se remite] ,
		[Servicio al que se remite] ,
		[Codigo especialidad a la que se remite] ,
		[Especialidad a la que se remite] ,	
		[Codigo entidad a la que se remite] ,
		[Entidad a la que se remite] ,	
		[Codigo profesional que remite] ,
		[Profesional que remite] ,		
		[Fecha de salida - orden medica] ,
		[Fecha del egreso] ,			
		[Tiempo total espera] ,
		[Codigo estado solicitud] ,
		[Estado solicitud] ,
		[Fecha suspension],
		[Codigo motivo suspension] ,
		[Motivo suspension] ,
		[Justificacion]

-- fin SP
END