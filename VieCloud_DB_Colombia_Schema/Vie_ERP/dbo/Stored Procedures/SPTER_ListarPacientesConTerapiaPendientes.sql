CREATE PROCEDURE [dbo].[SPTER_ListarPacientesConTerapiaPendientes]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	--Si no llega unidad funcional solo se traen los que tengan terapias, si un hijo de tiene terapia se trae con el union ya que la rejilla de recién nacido solo se crea si está seleccionada la unidad funcional
IF (@UnidadFuncional = '') 
BEGIN
	SELECT 
	  CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso, 
	  'Normal' as Alerta, 
	  A.CODICAMAS AS 'Codigo Cama', 
	  RTRIM(DESCCAMAS) AS Cama, 
	  dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion, 
	  dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', 
	  C.IPCODPACI AS Identificacion, 
	  C.NUMINGRES AS Ingreso, 
	  dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento, 
	  RTRIM(DESTIPEST) AS 'Tipo Estancia', 
	  RTRIM(IPNOMCOMP) AS Paciente, 
	  CAST(0 AS BIT) AS Resultado, 
	  A.CAMTRACIR AS TrasladoCirugia, 
	  A.CAMTRAMED AS TrasladoMedicamentos, 
	  A.CODCONCEC AS Consecutivo, 
	  CAST('' as bit) AS MuestraAlerta, 
	  K.CODESPECI AS CodigoEspecialidad, 
	  RTRIM(K.DESESPECI) AS DescripcionEspecialidad, 
	  IFECHAING, 
	  J.ESCADOWNT, 
	  J.ESCARASS, 
	  J.ESCNORPAC, 
	  J.ESCVASPAC, 
	  J.ESCAPAPAC, 
	  dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, 
	  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, 
	  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, 
	  dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, 
	  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, 
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92)) AS 'ESCALACAIDA',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)) AS 'ESCALADOLOR',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESCAIDA',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESDOLOR',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESCAIDA',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESDOLOR',
	  CONVERT(BIT, 0) AS Riesgo, 
	  CASE WHEN H.IPTIPODOC IN(6, 7) THEN 1 ELSE 0 END AS ASMS, 
	  H.ZONAPARTADA, 
	  L.RIESGOAGRE, 
	  IPFECNACI AS 'Fecha Nacimiento', 
	  CAST(
	    '' AS CHAR(50)
	  ) AS Edad, 
	  RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, 
	  Z.Color, 
			(SELECT CASE WHEN EXISTS 
				(SELECT * 
					FROM .dbo.HCORDPRON  PROCEDIMIENTOS
						INNER JOIN .dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS=CUPS.CODSERIPS 
						WHERE 
						PROCEDIMIENTOS.CODCENATE = @CentroAtencion			
						AND CUPS.TIPSERTER= '1'
						AND CUPS.TIPSERIPS = '4'
						AND PROCEDIMIENTOS.ESTSERIPS = '1'
						AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI 
						AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
				 )
						THEN CAST (1 AS BIT) ELSE CAST(0 AS BIT) END
			) AS Terapia,
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      dbo.ADPOBESPEPAC Z with(nolock) 
	      INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND TIPOPOESPERIES = 1
	  ) AS POBESPECIAL, 
	  J.VIVESOLO, 
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      ADACOMPAN with(nolock) 
	    WHERE 
	      NUMINGRES = J.NUMINGRES
	  ) AS ACOMPANANTES, 
	  Prof.CODPROSAL AS CodigoMedicoTratante, 
	  RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante, 
	  RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) as EntidadPaciente,
	  iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
	  rtrim(E.UFUCODIGO) AS UFUCODIGO,
	  rtrim(E.UFUDESCRI) AS 'UnidadFuncionalActual',
	  IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI, H.IPSEXOPAC AS Sexo, J.CODTIPPAC as TipoPaciente
	FROM 
	  dbo.CHCAMASHO A with(nolock) 
	  INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.CODCENATE 
	  INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO = E.UFUCODIGO 
	  LEFT OUTER JOIN dbo.CHREGESTA C with(nolock) ON A.CODICAMAS = C.CODICAMAS  AND C.REGESTADO = 1 
	  LEFT OUTER JOIN dbo.CHTIPESTA G with(nolock) ON G.CODTIPEST = C.CODTIPEST 
	  INNER JOIN dbo.INPacient H with(nolock) ON C.IPCODPACI = H.IPCODPACI 
	  LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON C.NUMINGRES = I.NUMINGRES 
	  INNER JOIN dbo.ADINGRESO J with(nolock) ON C.NUMINGRES = J.NUMINGRES 
	  INNER JOIN dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = J.CODENTIDA Outer apply (
	    select 
	      TOP 1 INDICAPAC, 
	      IPCODPACI, 
	      NUMINGRES 
	    from 
	      HCHISPACA 
	    where 
	      IPCODPACI = J.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	    order by 
	      FECHISPAC desc
	  ) as X 
	  LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON C.CODESPECI = K.CODESPECI 
	  INNER JOIN dbo.ADACTIVID L with(nolock) ON H.CODACTIVI = L.codactivi 
	  LEFT OUTER JOIN dbo.INPROFSAL Prof with (nolock) ON C.CODPROSAL = Prof.CODPROSAL 
	  LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (
	    SELECT 
	      TOP 1 CODDIAGNO 
	    FROM 
	      INDIAGNOP 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	      AND CODDIAPRI = 1
	  ) 
	  LEFT OUTER JOIN CHTIPOSAISLAMIENTOS Z with (nolock) ON A.CODAISLAM = Z.Id 
	WHERE 
	  A.CODCENATE = @CentroAtencion 
	 AND A.UFUCODIGO = ISNULL(NULLIF(@UnidadFuncional, ''), A.UFUCODIGO)
	 AND ESTADCAMA = '2' 
	 AND (
	            @UnidadFuncional <> ''
	            OR EXISTS (
	                SELECT 1 
	                FROM dbo.HCORDPRON PROCEDIMIENTOS
	                INNER JOIN dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS = CUPS.CODSERIPS 
	                WHERE PROCEDIMIENTOS.CODCENATE = @CentroAtencion
	                  AND CUPS.TIPSERTER = '1'
	                  AND CUPS.TIPSERIPS = '4'
	                  AND PROCEDIMIENTOS.ESTSERIPS = '1'
	                  AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI
	                  AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
	            )
	        )
UNION
	SELECT DISTINCT
	  CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso, 
	  'Normal' as Alerta, 
	  CAMA.CODICAMAS AS 'Codigo Cama', 
	  RTRIM(DESCCAMAS) AS Cama, 
	  dbo.ClaseHabitacion(CAMA.CODCLAHAB) AS ClaseHabitacion, 
	  dbo.ClaseCama(CAMA.CODCLACAM) AS 'Clase de Cama', 
	  A.IPCODPACI AS Identificacion, 
	  A.NUMINGRES AS Ingreso, 
	  '' AS Aislamiento, 
	  '' AS 'Tipo Estancia', 
	  RTRIM(IPNOMCOMP) AS Paciente, 
	  CAST(0 AS BIT) AS Resultado, 
	  '' AS TrasladoCirugia, 
	  '' AS TrasladoMedicamentos, 
	  0 AS Consecutivo, 
	  CAST('' as bit) AS MuestraAlerta, 
	  K.CODESPECI AS CodigoEspecialidad, 
	  RTRIM(K.DESESPECI) AS DescripcionEspecialidad, 
	  IFECHAING, 
	  J.ESCADOWNT, 
	  J.ESCARASS, 
	  J.ESCNORPAC, 
	  J.ESCVASPAC, 
	  J.ESCAPAPAC, 
	  dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, 
	  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, 
	  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, 
	  dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, 
	  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, 
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92)) AS 'ESCALACAIDA',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)) AS 'ESCALADOLOR',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESCAIDA',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESDOLOR',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESCAIDA',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESDOLOR',
	  CONVERT(BIT, 0) AS Riesgo, 
	  CASE WHEN H.IPTIPODOC IN(6, 7) THEN 1 ELSE 0 END AS ASMS, 
	  H.ZONAPARTADA, 
	  L.RIESGOAGRE, 
	  IPFECNACI AS 'Fecha Nacimiento', 
	  CAST(
	    '' AS CHAR(50)
	  ) AS Edad, 
	  RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, 
	  '' as Color, 
			(SELECT CASE WHEN EXISTS 
				(SELECT * 
					FROM .dbo.HCORDPRON  PROCEDIMIENTOS
						INNER JOIN .dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS=CUPS.CODSERIPS 
						WHERE 
						PROCEDIMIENTOS.CODCENATE = @CentroAtencion			
						AND CUPS.TIPSERTER= '1'
						AND CUPS.TIPSERIPS = '4'
						AND PROCEDIMIENTOS.ESTSERIPS = '1'
						AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI 
						AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
				 )
						THEN CAST (1 AS BIT) ELSE CAST(0 AS BIT) END
			) AS Terapia,
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      dbo.ADPOBESPEPAC Z with(nolock) 
	      INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND TIPOPOESPERIES = 1
	  ) AS POBESPECIAL, 
	  J.VIVESOLO, 
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      ADACOMPAN with(nolock) 
	    WHERE 
	      NUMINGRES = J.NUMINGRES
	  ) AS ACOMPANANTES, 
	  Prof.CODPROSAL AS CodigoMedicoTratante, 
	  RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante, 
	  RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) as EntidadPaciente,
	  iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
	  rtrim(E.UFUCODIGO) AS UFUCODIGO,
	  rtrim(E.UFUDESCRI) AS 'UnidadFuncionalActual',
	  IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI, H.IPSEXOPAC AS Sexo, J.CODTIPPAC as TipoPaciente
	FROM 
	  dbo.HCORDPRON A with(nolock) 
	  INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.CODCENATE 
	  INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO = E.UFUCODIGO 
	  INNER JOIN dbo.INPacient H with(nolock) ON A.IPCODPACI = H.IPCODPACI 
	  LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON A.NUMINGRES = I.NUMINGRES 
	  INNER JOIN dbo.ADINGRESO J with(nolock) ON A.NUMINGRES = J.NUMINGRES 
	  INNER JOIN HCRECINAC REC ON A.NUMINGRES = REC.NUMINGRESHIJO
	  INNER JOIN CHREGESTA CHR ON REC.NUMINGRES = CHR.NUMINGRES
	  INNER JOIN CHCAMASHO CAMA ON CHR.CODICAMAS = CAMA.CODICAMAS AND CAMA.ESTADCAMA = 2
	  INNER JOIN dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = J.CODENTIDA Outer apply (
	    select 
	      TOP 1 INDICAPAC, 
	      IPCODPACI, 
	      NUMINGRES 
	    from 
	      HCHISPACA 
	    where 
	      IPCODPACI = J.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	    order by 
	      FECHISPAC desc
	  ) as X 
	  INNER JOIN dbo.ADACTIVID L with(nolock) ON H.CODACTIVI = L.codactivi 
	  LEFT OUTER JOIN dbo.INPROFSAL Prof with (nolock) ON A.CODPROSAL = Prof.CODPROSAL 
	  LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON Prof.CODESPEC1 = K.CODESPECI 
	  LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (
	    SELECT 
	      TOP 1 CODDIAGNO 
	    FROM 
	      INDIAGNOP 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	      AND CODDIAPRI = 1
	  ) 
	WHERE 
	  A.CODCENATE = @CentroAtencion 
	 AND A.UFUCODIGO = ISNULL(NULLIF(@UnidadFuncional, ''), A.UFUCODIGO)
	 AND (
	            @UnidadFuncional <> ''
	            OR EXISTS (
	                SELECT 1 
	                FROM dbo.HCORDPRON PROCEDIMIENTOS
	                INNER JOIN dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS = CUPS.CODSERIPS 
	                WHERE PROCEDIMIENTOS.CODCENATE = @CentroAtencion
	                  AND CUPS.TIPSERTER = '1'
	                  AND CUPS.TIPSERIPS = '4'
	                  AND PROCEDIMIENTOS.ESTSERIPS = '1'
	                  AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI
	                  AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
	            )
	        )
UNION
SELECT DISTINCT
	  CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso, 
	  'Normal' as Alerta, 
	  '' AS 'Codigo Cama', 
	  '' AS Cama, 
	  '' AS ClaseHabitacion, 
	  '' AS 'Clase de Cama', 
	  A.IPCODPACI AS Identificacion, 
	  A.NUMINGRES AS Ingreso, 
	  '' AS Aislamiento, 
	  '' AS 'Tipo Estancia', 
	  RTRIM(IPNOMCOMP) AS Paciente, 
	  CAST(0 AS BIT) AS Resultado, 
	  '' AS TrasladoCirugia, 
	  '' AS TrasladoMedicamentos, 
	  0 AS Consecutivo, 
	  CAST('' as bit) AS MuestraAlerta, 
	  K.CODESPECI AS CodigoEspecialidad, 
	  RTRIM(K.DESESPECI) AS DescripcionEspecialidad, 
	  IFECHAING, 
	  J.ESCADOWNT, 
	  J.ESCARASS, 
	  J.ESCNORPAC, 
	  J.ESCVASPAC, 
	  J.ESCAPAPAC, 
	  dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, 
	  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, 
	  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, 
	  dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, 
	  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, 
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92)) AS 'ESCALACAIDA',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)) AS 'ESCALADOLOR',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESCAIDA',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESDOLOR',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESCAIDA',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESDOLOR',
	  CONVERT(BIT, 0) AS Riesgo, 
	  CASE WHEN H.IPTIPODOC IN(6, 7) THEN 1 ELSE 0 END AS ASMS, 
	  H.ZONAPARTADA, 
	  L.RIESGOAGRE, 
	  IPFECNACI AS 'Fecha Nacimiento', 
	  CAST(
	    '' AS CHAR(50)
	  ) AS Edad, 
	  RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, 
	  '' as Color, 
			(SELECT CASE WHEN EXISTS 
				(SELECT * 
					FROM .dbo.HCORDPRON  PROCEDIMIENTOS
						INNER JOIN .dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS=CUPS.CODSERIPS 
						WHERE 
						PROCEDIMIENTOS.CODCENATE = @CentroAtencion			
						AND CUPS.TIPSERTER= '1'
						AND CUPS.TIPSERIPS = '4'
						AND PROCEDIMIENTOS.ESTSERIPS = '1'
						AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI 
						AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
				 )
						THEN CAST (1 AS BIT) ELSE CAST(0 AS BIT) END
			) AS Terapia,
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      dbo.ADPOBESPEPAC Z with(nolock) 
	      INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND TIPOPOESPERIES = 1
	  ) AS POBESPECIAL, 
	  J.VIVESOLO, 
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      ADACOMPAN with(nolock) 
	    WHERE 
	      NUMINGRES = J.NUMINGRES
	  ) AS ACOMPANANTES, 
	  Prof.CODPROSAL AS CodigoMedicoTratante, 
	  RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante, 
	  RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) as EntidadPaciente,
	  iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
	  rtrim(E.UFUCODIGO) AS UFUCODIGO,
	  rtrim(E.UFUDESCRI) AS 'UnidadFuncionalActual',
	  IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI, H.IPSEXOPAC AS Sexo, J.CODTIPPAC as TipoPaciente
	FROM 
	  dbo.HCORDPRON A with(nolock) 
	  INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.CODCENATE 
	  INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO = E.UFUCODIGO 
	  INNER JOIN dbo.INPacient H with(nolock) ON A.IPCODPACI = H.IPCODPACI 
	  LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON A.NUMINGRES = I.NUMINGRES 
	  INNER JOIN dbo.ADINGRESO J with(nolock) ON A.NUMINGRES = J.NUMINGRES 
	  INNER JOIN dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = J.CODENTIDA Outer apply (
	    select 
	      TOP 1 INDICAPAC, 
	      IPCODPACI, 
	      NUMINGRES 
	    from 
	      HCHISPACA 
	    where 
	      IPCODPACI = J.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	    order by 
	      FECHISPAC desc
	  ) as X 
	  INNER JOIN dbo.ADACTIVID L with(nolock) ON H.CODACTIVI = L.codactivi 
	  LEFT OUTER JOIN dbo.INPROFSAL Prof with (nolock) ON A.CODPROSAL = Prof.CODPROSAL 
	  LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON Prof.CODESPEC1 = K.CODESPECI 
	  LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (
	    SELECT 
	      TOP 1 CODDIAGNO 
	    FROM 
	      INDIAGNOP 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	      AND CODDIAPRI = 1
	  ) 
	WHERE 
	  A.CODCENATE = @CentroAtencion 
	 AND A.UFUCODIGO = ISNULL(NULLIF(@UnidadFuncional, ''), A.UFUCODIGO)
	 AND NOT EXISTS (SELECT  NUMINGRES FROM CHREGESTA CHREG WHERE CHREG.NUMINGRES = A.NUMINGRES AND CHREG.REGESTADO = 1)
	 AND NOT EXISTS (SELECT  NUMINGRES FROM HCRECINAC HCREC WHERE HCREC.NUMINGRESHIJO = A.NUMINGRES)
	 AND (
	            @UnidadFuncional <> ''
	            OR EXISTS (
	                SELECT 1 
	                FROM dbo.HCORDPRON PROCEDIMIENTOS
	                INNER JOIN dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS = CUPS.CODSERIPS 
	                WHERE PROCEDIMIENTOS.CODCENATE = @CentroAtencion
	                  AND CUPS.TIPSERTER = '1'
	                  AND CUPS.TIPSERIPS = '4'
	                  AND PROCEDIMIENTOS.ESTSERIPS = '1'
	                  AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI
	                  AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
	            )
	        )
	ORDER BY 
	A.CODICAMAS
END
ELSE
BEGIN
	SELECT 
	  CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso, 
	  'Normal' as Alerta, 
	  A.CODICAMAS AS 'Codigo Cama', 
	  RTRIM(DESCCAMAS) AS Cama, 
	  dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion, 
	  dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', 
	  C.IPCODPACI AS Identificacion, 
	  C.NUMINGRES AS Ingreso, 
	  dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento, 
	  RTRIM(DESTIPEST) AS 'Tipo Estancia', 
	  RTRIM(IPNOMCOMP) AS Paciente, 
	  CAST(0 AS BIT) AS Resultado, 
	  A.CAMTRACIR AS TrasladoCirugia, 
	  A.CAMTRAMED AS TrasladoMedicamentos, 
	  A.CODCONCEC AS Consecutivo, 
	  CAST('' as bit) AS MuestraAlerta, 
	  K.CODESPECI AS CodigoEspecialidad, 
	  RTRIM(K.DESESPECI) AS DescripcionEspecialidad, 
	  IFECHAING, 
	  J.ESCADOWNT, 
	  J.ESCARASS, 
	  J.ESCNORPAC, 
	  J.ESCVASPAC, 
	  J.ESCAPAPAC, 
	  dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, 
	  dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, 
	  dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, 
	  dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, 
	  dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, 
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92)) AS 'ESCALACAIDA',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)) AS 'ESCALADOLOR',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESCAIDA',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESDOLOR',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESCAIDA',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESDOLOR',
	  CONVERT(BIT, 0) AS Riesgo, 
	  CASE WHEN H.IPTIPODOC IN(6, 7) THEN 1 ELSE 0 END AS ASMS, 
	  H.ZONAPARTADA, 
	  L.RIESGOAGRE, 
	  IPFECNACI AS 'Fecha Nacimiento', 
	  CAST(
	    '' AS CHAR(50)
	  ) AS Edad, 
	  RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, 
	  Z.Color, 
			(SELECT CASE WHEN EXISTS 
				(SELECT * 
					FROM .dbo.HCORDPRON  PROCEDIMIENTOS
						INNER JOIN .dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS=CUPS.CODSERIPS 
						WHERE 
						PROCEDIMIENTOS.CODCENATE = @CentroAtencion			
						AND CUPS.TIPSERTER= '1'
						AND CUPS.TIPSERIPS = '4'
						AND PROCEDIMIENTOS.ESTSERIPS = '1'
						AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI 
						AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
				 )
						THEN CAST (1 AS BIT) ELSE CAST(0 AS BIT) END
			) AS Terapia,
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      dbo.ADPOBESPEPAC Z with(nolock) 
	      INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND TIPOPOESPERIES = 1
	  ) AS POBESPECIAL, 
	  J.VIVESOLO, 
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      ADACOMPAN with(nolock) 
	    WHERE 
	      NUMINGRES = J.NUMINGRES
	  ) AS ACOMPANANTES, 
	  Prof.CODPROSAL AS CodigoMedicoTratante, 
	  RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante, 
	  RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) as EntidadPaciente,
	  iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
	  rtrim(E.UFUCODIGO) AS UFUCODIGO,
	  rtrim(E.UFUDESCRI) AS 'UnidadFuncionalActual',
	  IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI, H.IPSEXOPAC AS Sexo, J.CODTIPPAC as TipoPaciente
	FROM 
	  dbo.CHCAMASHO A with(nolock) 
	  INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.CODCENATE 
	  INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO = E.UFUCODIGO 
	  LEFT OUTER JOIN dbo.CHREGESTA C with(nolock) ON A.CODICAMAS = C.CODICAMAS  AND C.REGESTADO = 1 
	  LEFT OUTER JOIN dbo.CHTIPESTA G with(nolock) ON G.CODTIPEST = C.CODTIPEST 
	  INNER JOIN dbo.INPacient H with(nolock) ON C.IPCODPACI = H.IPCODPACI 
	  LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON C.NUMINGRES = I.NUMINGRES 
	  INNER JOIN dbo.ADINGRESO J with(nolock) ON C.NUMINGRES = J.NUMINGRES 
	  INNER JOIN dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = J.CODENTIDA Outer apply (
	    select 
	      TOP 1 INDICAPAC, 
	      IPCODPACI, 
	      NUMINGRES 
	    from 
	      HCHISPACA 
	    where 
	      IPCODPACI = J.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	    order by 
	      FECHISPAC desc
	  ) as X 
	  LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON C.CODESPECI = K.CODESPECI 
	  INNER JOIN dbo.ADACTIVID L with(nolock) ON H.CODACTIVI = L.codactivi 
	  LEFT OUTER JOIN dbo.INPROFSAL Prof with (nolock) ON C.CODPROSAL = Prof.CODPROSAL 
	  LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (
	    SELECT 
	      TOP 1 CODDIAGNO 
	    FROM 
	      INDIAGNOP 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND NUMINGRES = J.NUMINGRES 
	      AND CODDIAPRI = 1
	  ) 
	  LEFT OUTER JOIN CHTIPOSAISLAMIENTOS Z with (nolock) ON A.CODAISLAM = Z.Id 
	WHERE 
	  A.CODCENATE = @CentroAtencion 
	 AND A.UFUCODIGO = ISNULL(NULLIF(@UnidadFuncional, ''), A.UFUCODIGO)
	 AND ESTADCAMA = '2' 
	 AND (
	            @UnidadFuncional <> ''
	            OR EXISTS (
	                SELECT 1 
	                FROM dbo.HCORDPRON PROCEDIMIENTOS
	                INNER JOIN dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS = CUPS.CODSERIPS 
	                WHERE PROCEDIMIENTOS.CODCENATE = @CentroAtencion
	                  AND CUPS.TIPSERTER = '1'
	                  AND CUPS.TIPSERIPS = '4'
	                  AND PROCEDIMIENTOS.ESTSERIPS = '1'
	                  AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI
	                  AND PROCEDIMIENTOS.NUMINGRES = J.NUMINGRES
	            )
	        )
UNION
SELECT DISTINCT
	  CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso, 
	  'Normal' as Alerta, 
	  '' AS 'Codigo Cama', 
	  '' AS Cama, 
	  '' AS ClaseHabitacion, 
	  '' AS 'Clase de Cama', 
	  A.IPCODPACI AS Identificacion, 
	  A.NUMINGRES AS Ingreso, 
	  '' AS Aislamiento, 
	  '' AS 'Tipo Estancia', 
	  RTRIM(H.IPNOMCOMP) AS Paciente, 
	  CAST(0 AS BIT) AS Resultado, 
	  '' AS TrasladoCirugia, 
	  '' AS TrasladoMedicamentos, 
	  0 AS Consecutivo, 
	  CAST('' as bit) AS MuestraAlerta, 
	  K.CODESPECI AS CodigoEspecialidad, 
	  RTRIM(K.DESESPECI) AS DescripcionEspecialidad, 
	  IFECHAING, 
	  A.ESCADOWNT, 
	  A.ESCARASS, 
	  A.ESCNORPAC, 
	  A.ESCVASPAC, 
	  A.ESCAPAPAC, 
	  dbo.PuntajeEscalaDownTon(A.NUMINGRES, A.IPCODPACI) as PUNTAJEDOWN, 
	  dbo.PuntajeEscalaRass(A.NUMINGRES, A.IPCODPACI) as PUNTAJERASS, 
	  dbo.PuntajeEscalaNorton(A.NUMINGRES, A.IPCODPACI) as PUNTAJENORTON, 
	  dbo.PuntajeEscalaVas(A.NUMINGRES, A.IPCODPACI) as PUNTAJEVAS, 
	  dbo.PuntajeEscalaApache(A.NUMINGRES, A.IPCODPACI) as PUNTAJEAPACHE, 
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (47,92)) AS 'ESCALACAIDA',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (49,90,91,93)) AS 'ESCALADOLOR',
	  (Select CAST(CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESCAIDA',
	  (Select TOP 1 HE.RESULTADO from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'RESULTESDOLOR',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESCAIDA',
	  (Select TOP 1 HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = A.IPCODPACI AND HE.NUMINGRES = A.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC) AS 'TIPOESDOLOR',
	  CONVERT(BIT, 0) AS Riesgo, 
	  CASE WHEN H.IPTIPODOC IN(6, 7) THEN 1 ELSE 0 END AS ASMS, 
	  H.ZONAPARTADA, 
	  L.RIESGOAGRE, 
	  IPFECNACI AS 'Fecha Nacimiento', 
	  CAST(
	    '' AS CHAR(50)
	  ) AS Edad, 
	  RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico, 
	  '' as Color, 
			(SELECT CASE WHEN EXISTS 
				(SELECT * 
					FROM .dbo.HCORDPRON  PROCEDIMIENTOS
						INNER JOIN .dbo.INCUPSIPS CUPS ON PROCEDIMIENTOS.CODSERIPS=CUPS.CODSERIPS 
						WHERE 
						PROCEDIMIENTOS.CODCENATE = @CentroAtencion			
						AND CUPS.TIPSERTER= '1'
						AND CUPS.TIPSERIPS = '4'
						AND PROCEDIMIENTOS.ESTSERIPS = '1'
						AND PROCEDIMIENTOS.IPCODPACI = H.IPCODPACI 
						AND PROCEDIMIENTOS.NUMINGRES = A.NUMINGRES
				 )
						THEN CAST (1 AS BIT) ELSE CAST(0 AS BIT) END
			) AS Terapia,
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      dbo.ADPOBESPEPAC Z with(nolock) 
	      INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND TIPOPOESPERIES = 1
	  ) AS POBESPECIAL, 
	  A.VIVESOLO, 
	  (
	    SELECT 
	      count(*) 
	    FROM 
	      ADACOMPAN with(nolock) 
	    WHERE 
	      NUMINGRES = A.NUMINGRES
	  ) AS ACOMPANANTES, 
	  '' AS CodigoMedicoTratante, 
	  '' AS NombreMedicoTratante, 
	  RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) as EntidadPaciente,
	  iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion,
	  rtrim(E.UFUCODIGO) AS UFUCODIGO,
	  rtrim(E.UFUDESCRI) AS 'UnidadFuncionalActual',
	  IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI, H.IPSEXOPAC AS Sexo, A.CODTIPPAC as TipoPaciente
	FROM 
	  dbo.ADINGRESO A with(nolock) 
	  INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE = D.CODCENATE 
	  INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO = E.UFUCODIGO 
	  INNER JOIN dbo.INPacient H with(nolock) ON A.IPCODPACI = H.IPCODPACI 
	  LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON A.NUMINGRES = I.NUMINGRES 
	  --INNER JOIN dbo.ADINGRESO J with(nolock) ON A.NUMINGRES = J.NUMINGRES 
	  INNER JOIN dbo.INENTIDAD P with (nolock) ON P.CODENTIDA = A.CODENTIDA Outer apply (
	    select 
	      TOP 1 INDICAPAC, 
	      IPCODPACI, 
	      NUMINGRES 
	    from 
	      HCHISPACA 
	    where 
	      IPCODPACI = A.IPCODPACI 
	      AND NUMINGRES = A.NUMINGRES 
	    order by 
	      FECHISPAC desc
	  ) as X 
	  INNER JOIN dbo.ADACTIVID L with(nolock) ON H.CODACTIVI = L.codactivi 
	  LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON A.CODESPTRA = K.CODESPECI 
	  LEFT OUTER JOIN dbo.INDIAGNOS Q with (nolock) ON Q.CODDIAGNO = (
	    SELECT 
	      TOP 1 CODDIAGNO 
	    FROM 
	      INDIAGNOP 
	    WHERE 
	      IPCODPACI = H.IPCODPACI 
	      AND NUMINGRES = A.NUMINGRES 
	      AND CODDIAPRI = 1
	  ) 
	WHERE 
	  
	 --A.UFUCODIGO = ISNULL(NULLIF(@UnidadFuncional, ''), A.UFUCODIGO)
	 IESTADOIN='' AND UFUINGMED=NULLIF(@UnidadFuncional, '') AND UFUINGHOS IS NULL AND (UFUEGRMED IS NULL or DESTINOPAC = 1 ) AND DESTINOPAC IS NOT NULL
	 AND NOT EXISTS (SELECT  NUMINGRES FROM CHREGESTA CHREG WHERE CHREG.NUMINGRES = A.NUMINGRES AND CHREG.REGESTADO = 1)
	 AND NOT EXISTS (SELECT  NUMINGRES FROM HCRECINAC HCREC WHERE HCREC.NUMINGRESHIJO = A.NUMINGRES)
  ORDER BY 
  A.CODICAMAS
END
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados que tienen terapias físicas, respiratorias u ocupacionales pendientes de atención, filtrados por centro de atención y opcionalmente por unidad funcional (sala o servicio). Integra información de camas, estancias activas, datos del paciente, ingreso, entidad aseguradora, diagnóstico principal, médico tratante, escalas clínicas de riesgo (caídas, dolor, Down-Ton, Rass, Norton, VAS, Apache) y estado de pre-alta o egreso. También identifica si el paciente pertenece a poblaciones especiales como PAPSIVI o recién nacidos, e incluye alertas de aislamiento, acompañantes y recomendaciones. Se usa principalmente en el módulo de terapias para que los terapeutas visualicen el censo de pacientes con órdenes de terapia pendientes en el piso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados (y recién nacidos asociados) de un centro de atención, opcionalmente filtrados por unidad funcional, enriquecidos con datos clínicos, escalas de valoración y un indicador de si tienen órdenes de terapia pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un código de centro de atención válido para filtrar registros (CODCENATE); Si no se envía unidad funcional (cadena vacía), solo se retornan pacientes que tengan terapias pendientes registradas en HCORDPRON; Las camas consideradas como ocupadas son las que tienen ESTADCAMA = ''2''; Los registros de estancia activos son aquellos con CHREGESTA.REGESTADO = 1; El diagnóstico mostrado se toma del diagnóstico principal (INDIAGNOP.CODDIAPRI = 1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado del paciente (egreso) se clasifica siempre en una de tres categorías: pre-alta (INDICAPAC=22), en la unidad (sin egreso) o con salida; Solo se incluyen camas con ESTADCAMA=''2'' en los bloques basados en CHCAMASHO; Las terapias pendientes se identifican por la combinación TIPSERTER=''1'' + TIPSERIPS=''4'' + ESTSERIPS=''1'' en HCORDPRON/INCUPSIPS; El diagnóstico mostrado siempre corresponde al diagnóstico principal (CODDIAPRI=1) más reciente del ingreso; La población especial cuenta solo registros con TIPOPOESPERIES = 1; Los recién nacidos se vinculan al ingreso de la madre vía HCRECINAC.NUMINGRESHIJO; Cuando hay unidad funcional, se excluyen ingresos que ya tienen estancia activa o que son recién nacidos hijos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @UnidadFuncional es vacío, retorna pacientes en cama ocupada + recién nacidos (vía HCRECINAC) + ingresos sin estancia ni recién nacido, todos restringidos a tener terapia pendiente (HCORDPRON con CUPS.TIPSERTER=''1'', CUPS.TIPSERIPS=''4'', ESTSERIPS=''1''); [RETURN_RESULT] resultset: Cuando @UnidadFuncional tiene valor, retorna pacientes de camas ocupadas en esa unidad + ingresos con UFUINGMED igual a la unidad, UFUINGHOS NULL, IESTADOIN vacío, sin egreso médico o con DESTINOPAC=1, y sin estancia ni recién nacido asociado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UnidadFuncional = '''' (no se especifica unidad funcional) → Ejecuta consulta UNION de tres bloques: camas ocupadas, recién nacidos (HCRECINAC) e ingresos sin estancia/sin recién nacido, exigiendo en todos que el paciente tenga terapia pendiente en HCORDPRON else Ejecuta consulta UNION de dos bloques filtrados por la unidad funcional indicada: camas ocupadas en esa unidad e ingresos con UFUINGMED igual a la unidad y reglas adicionales de estado/destino; si X.INDICAPAC = ''22'' → Clasifica al paciente como ''2 - Pre-alta hospitalaria'' else Si HCREGEGRE.NUMINGRES IS NULL clasifica como ''1 - Pacientes en la unidad'', en caso contrario ''3 - Pacientes con salida''; si H.IPTIPODOC IN (6,7) → Marca al paciente como ASMS = 1 else ASMS = 0; si HCESCALAS.TIPOESCALA IN (47,92) → Considera la escala como Escala de Caída else Si TIPOESCALA IN (49,90,91,93) la considera Escala de Dolor; cualquier otra es Escala General; si H.PoblacionPAPSIVI = 1 → Marca EsPoblacionPAPSIVI = 1 else EsPoblacionPAPSIVI = 0; si Existe HCORDPRON del paciente con CUPS.TIPSERTER=''1'', CUPS.TIPSERIPS=''4'' y ESTSERIPS=''1'' → Marca el indicador Terapia = 1 else Terapia = 0; si Existe RecommendPatient del paciente/ingreso con Status = 1 → Marca Recomendacion = 1 else Recomendacion = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INENTIDAD; dbo.HCHISPACA; dbo.INESPECIA; dbo.ADACTIVID; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.CHTIPOSAISLAMIENTOS; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.HCESCALAS; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPTER_ListarPacientesConTerapiaPendientes';
-- GO
