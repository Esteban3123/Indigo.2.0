CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesInterconsulta]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@CodeProfesional char(20),
@Usuario char(20)
)
AS
BEGIN
SET NOCOUNT ON;

		DECLARE @PerformAssistedInterconsultation as bit = 0
		DECLARE @CodigoEspecialidadUno as varchar(3)   
		DECLARE @CodigoEspecialidadDos as varchar(3)
		DECLARE @CodigoEspecialidadTres as varchar(3)
	
		Select @PerformAssistedInterconsultation = isnull(PerformAssistedInterconsultation,0), 
			   @CodigoEspecialidadUno = CODESPEC1,
		       @CodigoEspecialidadDos = CODESPEC2, 
		       @CodigoEspecialidadTres = CODESPEC3 
		FROM INPROFSAL where CODPROSAL = @CodeProfesional
		
		

IF @PerformAssistedInterconsultation = 1 

					SELECT RTRIM(A.IPCODPACI) AS Identificacion,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso,CASE WHEN AD.UFUAACTHOS IS NULL THEN AD.UFUCODIGO ELSE AD.UFUAACTHOS END AS CodigoUnidadFuncional, 
						CASE WHEN RTRIM(D.UFUDESCRI) IS NULL THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO=AD.UFUCODIGO) ELSE RTRIM(D.UFUDESCRI) END AS DescripcionUnidadFuncional,
						RTRIM(DESCCAMAS) AS Cama,
						dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento
						,'2 - Solicitudes valoración de interconsultas'  AS Tipo, H.DESESPECI AS Especialidad,TRIANUMER AS Consecutivo 
						, CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
						, B.ZONAPARTADA,
						L.RIESGOAGRE,
						(SELECT count(*) FROM dbo.ADPOBESPEPAC WHERE IPCODPACI = B.IPCODPACI) AS POBESPECIAL,
						AD.VIVESOLO,
						(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = AD.NUMINGRES) AS ACOMPANANTES,
						CONVERT(BIT,0) AS Riesgo
						,B.IPSEXOPAC AS Sexo 
						,AD.CODTIPPAC as TipoPaciente
						, IPFECNACI AS 'Fecha Nacimiento',CAST('' AS CHAR(50)) AS Edad
						, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
						, RTRIM(x.CODENTIDA) +'-'+ RTRIM(x.NOMENTIDA) as 'EntidadPaciente'
						, A.FECORDMED AS 'Fecha Solicitud'
						, RTRIM(z.NOMMEDICO) as 'Profesional',
						A.ESTSERIPS,
						ISNULL(A.NUMFOLIORESIDENTE,0) AS NUMFOLIORESIDENTE
						,A.IDETIPHIS 
						,A.AUTO
						,A.CODESPECI AS CODESPECI,
						iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
					FROM dbo.HCORDINTE  A 
						Inner Join  dbo.ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES and  (AD.IESTADOIN IN ('','P') OR (AD.IESTADOIN IN ('C') AND ad.CREADOAUTOMA = 1 )) --CREADOAUTOMA: Cuando crean el niño desde el Binomio madre hijo es decir de la HC del recien nacido Y filtramos por Ingreso en C para incluir estancia conjunta con la madre que hace ingreso cerrado
						Left OUTER JOIN dbo.INUNIFUNC D ON AD.UFUAACTHOS=D.UFUCODIGO 
						Inner Join dbo.INPacient B ON A.IPCODPACI=B.IPCODPACI 
						Left OUTER JOIN dbo.CHCAMASHO CA ON AD.CODCAMACT=CA.CODICAMAS 
						Inner Join  dbo.INESPECIA H ON A.CODESPECI=H.CODESPECI 
						Left OUTER JOIN dbo.ADTRIAGEU E ON A.IPCODPACI= E.IPCODPACI AND A.NUMINGRES=E.NUMINGRES 
						Left OUTER JOIN dbo.ADACTIVID L ON B.CODACTIVI = L.codactivi
						Left OUTER Join dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1)  
						Inner Join INENTIDAD x on AD.CODENTIDA = x.CODENTIDA 
						Inner Join INPROFSAL Z on A.CODPROSAL = Z.CODPROSAL  
						Inner join PerformAssistedInterconsultation tm on tm.CODESPECI = a.CODESPECI AND TM.CODPROSAL = @CodeProfesional
					WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS='1' 
		
ELSE 
			
					SELECT RTRIM(A.IPCODPACI) AS Identificacion,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso,CASE WHEN AD.UFUAACTHOS IS NULL THEN AD.UFUCODIGO ELSE AD.UFUAACTHOS END AS CodigoUnidadFuncional, 
						CASE WHEN RTRIM(D.UFUDESCRI) IS NULL THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO=AD.UFUCODIGO) ELSE RTRIM(D.UFUDESCRI) END AS DescripcionUnidadFuncional,
						RTRIM(DESCCAMAS) AS Cama,
						dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
						case ESTSERIPS when '6' then '4 - Pendientes por validar' else '2 - Solicitudes valoración de interconsultas' END AS Tipo, H.DESESPECI AS Especialidad,TRIANUMER AS Consecutivo 
						, CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
						, B.ZONAPARTADA,
						L.RIESGOAGRE,
						(SELECT count(*) FROM dbo.ADPOBESPEPAC WHERE IPCODPACI = B.IPCODPACI) AS POBESPECIAL,
						AD.VIVESOLO,
						(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = AD.NUMINGRES) AS ACOMPANANTES,
						CONVERT(BIT,0) AS Riesgo
						,B.IPSEXOPAC AS Sexo 
						,AD.CODTIPPAC as TipoPaciente
						, IPFECNACI AS 'Fecha Nacimiento',CAST('' AS CHAR(50)) AS Edad
						, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
						, RTRIM(x.CODENTIDA) +'-'+ RTRIM(x.NOMENTIDA) as 'EntidadPaciente'
						, A.FECORDMED AS 'Fecha Solicitud'
						, RTRIM(z.NOMMEDICO) as 'Profesional'
						,A.ESTSERIPS
						,ISNULL(A.NUMFOLIORESIDENTE,0) AS NUMFOLIORESIDENTE
						,A.AUTO
						,A.CODESPECI  AS CODESPECI,
						iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
					FROM dbo.HCORDINTE  A 
						 Inner Join  dbo.ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES and  (AD.IESTADOIN IN ('','P') OR (AD.IESTADOIN IN ('C') AND ad.CREADOAUTOMA = 1 )) --CREADOAUTOMA: Cuando crean el niño desde el Binomio madre hijo es decir de la HC del recien nacido Y filtramos por Ingreso en C para incluir estancia conjunta con la madre que hace ingreso cerrado
						 Left OUTER JOIN dbo.INUNIFUNC D ON AD.UFUAACTHOS=D.UFUCODIGO 
						 Inner Join dbo.INPacient B ON A.IPCODPACI=B.IPCODPACI 
						 Left OUTER JOIN dbo.CHCAMASHO CA ON AD.CODCAMACT=CA.CODICAMAS 
						 Inner Join  dbo.INESPECIA H ON A.CODESPECI=H.CODESPECI 
						 Left OUTER JOIN dbo.ADTRIAGEU E ON A.IPCODPACI= E.IPCODPACI AND A.NUMINGRES=E.NUMINGRES 
						 Left OUTER JOIN dbo.ADACTIVID L ON B.CODACTIVI = L.codactivi
						 Left OUTER Join dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1)  
						 Inner Join INENTIDAD x on AD.CODENTIDA = x.CODENTIDA 
						 Inner Join INPROFSAL Z on A.CODPROSAL = Z.CODPROSAL  
					 WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS IN ('1','6') AND A.CODESPECI in (@CodigoEspecialidadUno,@CodigoEspecialidadDos,@CodigoEspecialidadTres) 
			  UNION
					SELECT RTRIM(B.IPCODPACI) AS Identificacion,RTRIM(F.IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso,b.ufucodigo as  CodigoUnidadFuncional,
						UF.UFUDESCRI as  DescripcionUnidadFuncional,CAST('' as CHAR) AS  Cama,cast('' AS CHAR) AS  Aislamiento, '1 - Remisión de urgencias' AS Tipo,
						H.DESESPECI AS Especialidad ,TRIANUMER AS Consecutivo 
						, CASE WHEN F.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
						, F.ZONAPARTADA,
						L.RIESGOAGRE,
						(SELECT count(*) FROM dbo.ADPOBESPEPAC WHERE IPCODPACI = B.IPCODPACI) AS POBESPECIAL,
						AD.VIVESOLO,
						(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = AD.NUMINGRES) AS ACOMPANANTES,
						CONVERT(BIT,0) AS Riesgo
						,F.IPSEXOPAC AS Sexo 
						,AD.CODTIPPAC as TipoPaciente 
						, IPFECNACI AS 'Fecha Nacimiento',CAST('' AS CHAR(50)) AS Edad 
						, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
						, RTRIM(x.CODENTIDA) +'-'+ RTRIM(x.NOMENTIDA) as 'EntidadPaciente'
						, A.TRIAFECHA  AS 'Fecha Solicitud'
						, RTRIM(z.NOMMEDICO) as 'Profesional'
						,0 AS ESTSERIPS
						,0 AS NUMFOLIORESIDENTE
						,0 as AUTO
						,A.CODESPECI AS CODESPECI,
						iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
					FROM dbo.ADTRIAGEU A 
						Inner Join  dbo.ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES  and AD.IESTADOIN IN ('','P')
						Inner Join dbo.ADCONTURG B ON A.CODCONCEC=B.CODCONCEC 
						Inner Join  INUNIFUNC UF on b.UFUCODIGO= UF.UFUCODIGO 
						Inner Join dbo.ADCATTRIU E ON E.TRIACATEG=A.TRIACATEG 
						Left OUTER Join  dbo.INPACIENT F ON B.IPCODPACI=F.IPCODPACI 
						Inner JOIN  dbo.INESPECIA H ON A.CODESPECI=H.CODESPECI 
						Inner Join dbo.ADACTIVID L ON F.CODACTIVI = L.codactivi
						Left OUTER Join dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1)  
						Inner Join INENTIDAD x on AD.CODENTIDA = x.CODENTIDA  
						Inner Join INPROFSAL Z on A.CODPROSAL = Z.CODPROSAL 
					WHERE TRIORIGEN='2' AND TRREMITID = '1' AND A.CODESPECI in (@CodigoEspecialidadUno,@CodigoEspecialidadDos,@CodigoEspecialidadTres)  AND CONESTADO = '4' AND A.CODCENATE = @CentroAtencion  AND A.NUMINGRES IS NOT NULL AND A.NUMINGRES NOT IN (SELECT NUMDOCUME 
					FROM dbo.INDOCUMEN 
					WHERE CODDOCUME='3' AND CODUSUARI<>@Usuario)
			  UNION 
					SELECT A.IPCODPACI AS Identificacion,RTRIM(IPNOMCOMP) AS Paciente,A.NUMINGRES AS Ingreso, CASE WHEN A.UFUAACTHOS IS NULL THEN A.UFUCODIGO ELSE A.UFUAACTHOS end AS CodigoUnidadFuncional,RTRIM(D.UFUDESCRI) AS DescripcionUnidadFuncional,
						Rtrim(DESCCAMAS)AS  Cama,cast('' AS CHAR) AS  Aislamiento, '3 - Mis pacientes' AS Tipo, H.DESESPECI AS Especialidad,
						TRIANUMER AS Consecutivo 
						, CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
						, C.ZONAPARTADA,
						L.RIESGOAGRE,
						(SELECT count(*) FROM dbo.ADPOBESPEPAC WHERE IPCODPACI = C.IPCODPACI) AS POBESPECIAL,
						A.VIVESOLO,
						(SELECT count(*) FROM ADACOMPAN WHERE NUMINGRES = A.NUMINGRES) AS ACOMPANANTES,
						CONVERT(BIT,0) AS Riesgo
						,C.IPSEXOPAC AS Sexo  
						,A.CODTIPPAC as TipoPaciente
						, IPFECNACI AS 'Fecha Nacimiento',CAST('' AS CHAR(50)) AS Edad 
						, RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
						, RTRIM(x.CODENTIDA) +'-'+ RTRIM(x.NOMENTIDA) as 'EntidadPaciente'
						, NULL AS 'Fecha Solicitud'
						, NULL as 'Profesional'
						,0 AS ESTSERIPS
						,0 AS NUMFOLIORESIDENTE
						,0 as AUTO
						,A.CODESPTRA AS CODESPECI,
						iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
					FROM ADINGRESO A 
						Inner Join  INPACIENT C ON A.IPCODPACI=C.IPCODPACI 
						Inner Join  ADACTIVID L ON C.CODACTIVI = L.codactivi
						left Join INUNIFUNC D ON A.UFUAACTHOS=D.UFUCODIGO 
						Left OUTER JOIN dbo.CHCAMASHO CA ON A.CODCAMACT=CA.CODICAMAS 
						Inner Join  dbo.INESPECIA H ON A.CODESPTRA=H.CODESPECI 
						Inner Join  CHREGESTA N ON A.IPCODPACI= N.IPCODPACI  AND REGESTADO = 1 
						Left OUTER JOIN dbo.ADTRIAGEU E ON A.IPCODPACI= E.IPCODPACI AND A.NUMINGRES=E.NUMINGRES
						Left OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODDIAPRI = 1) 
						Inner Join INENTIDAD x on A.CODENTIDA = x.CODENTIDA  WHERE (A.CODCENATE=@CentroAtencion AND A.CODESPTRA IN (@CodigoEspecialidadUno,@CodigoEspecialidadDos,@CodigoEspecialidadTres) AND IESTADOIN='' AND UFUEGRHOS IS NULL)
						AND D.UFUTIPUNI NOT IN('15','24') and a.IESTADOIN IN ('')
END