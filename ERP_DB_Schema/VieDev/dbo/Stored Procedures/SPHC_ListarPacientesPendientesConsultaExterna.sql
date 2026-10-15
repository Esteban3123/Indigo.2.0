
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesPendientesConsultaExterna]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime,
@ProfesionalConAsistida Char(10),
@Consultorio Char(10),
@EsConsultaAsistida bit

)
AS
BEGIN
	SET NOCOUNT ON;
	IF @EsConsultaAsistida = 0
		BEGIN
			SELECT g.CODAUTONU, RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT,
				A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				,Modalidad = CASE WHEN G.MODALIDAD = 0 THEN 'Presencial' WHEN G.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
				,case inc.TIPSERIPS
						when 1 then 'Laboratorios'
						when 2 then 'Patologías'
						when 3 then 'Imágenes diagnósticas'
						when 4 then 'Procedimientos no quirúrgicos'
						when 5 then 'Procedimientos quirúrgicos'
						when 6 then 'Interconsultas'
						when 7 then 'Ninguno'
						when 8 then 'Consulta externa'
						when 9 then 'Hemocomponentes'
				END TipoServicio, 
				inc.TIPSERIPS as  CodigoTipoServicio,
				Rtrim(inc.CODSERIPS) as CodigoServicio,
				Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) as ServicioCUPS,
				isnull(rtrim(CDD.Id),0) As IdDescRelacionada,
				Isnull(Rtrim(CD.name),'') as NombreDescripcionRelacionada
				FROM dbo.ADCONCOEX A  WITH (NOLOCK) 
					inner join dbo.AGASICITA g  WITH (NOLOCK)  on A.NUMCONCIT=g.CODAUTONU 
					inner join dbo.AGACTIMED z  WITH (NOLOCK)  on z.CODACTMED = g.CODACTMED 
					inner join dbo.INPACIENT B  WITH (NOLOCK)  ON A.IPCODPACI=B.IPCODPACI 
					inner join dbo.INENTIDAD C  WITH (NOLOCK)  ON A.CODENTIDA=C.CODENTIDA 
					inner join dbo.ADINGRESO D  WITH (NOLOCK)  ON A.NUMINGRES =D.NUMINGRES
					left outer join DBO.INESPECIA E  WITH (NOLOCK)  ON G.CODESPECI=E.CODESPECI
					left join INCUPSIPS inc WITH (NOLOCK)  ON G.CODSERIPS = inc.CODSERIPS 
					LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = G.IDDESCRIPCIONRELACIONADA 
					LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
				WHERE 
				A.CODCENATE=@CentroAtencion 
				and A.CONESTADO IN ('1','4','5','6') 
				AND g.TIPSOLICITU = 1 
				AND A.CODPROSAL=@Profesional 
				AND (G.FECHORAIN  >= @FechaInicial AND G.FECHORAFI  <=@FechaFinal) 
			UNION
				SELECT A.CODAUTONU,RTRIM(D.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.CODESPECI) AS CodEspecialidad ,'2 - Asignadas'  AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
				RTRIM(B.IPNOMCOMP) AS Paciente,'' AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,0 AS ConsecutivoCita,A.FECHORAIN AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, A.CODACTMED, '' as TipoPaciente , IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, '' AS VIVESOLO, '' AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, isnull(A.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, Convert(Bit,0) as Recomendacion
				,case inc.TIPSERIPS
						when 1 then 'Laboratorios'
						when 2 then 'Patologías'
						when 3 then 'Imágenes diagnósticas'
						when 4 then 'Procedimientos no quirúrgicos'
						when 5 then 'Procedimientos quirúrgicos'
						when 6 then 'Interconsultas'
						when 7 then 'Ninguno'
						when 8 then 'Consulta externa'
						when 9 then 'Hemocomponentes' 
				END TipoServicio,
				inc.TIPSERIPS as  CodigoTipoServicio,
				Rtrim(inc.CODSERIPS) as CodigoServicio,
				Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) as ServicioCUPS,
				isnull(rtrim(CDD.Id),0) As IdDescRelacionada,
				Isnull(Rtrim(CD.name),'') as NombreDescripcionRelacionada
				FROM dbo.AGASICITA A WITH (NOLOCK)
					inner join dbo.AGACTIMED z WITH (NOLOCK) on z.CODACTMED = A.CODACTMED
					INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI 
					INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA=C.CODENTIDA
					inner join dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
					left join dbo.ADCONCOEX ad WITH(NOLOCK) on ad.NUMCONCIT = a.CODAUTONU and ad.CODCONCEC is null
					left join INCUPSIPS inc WITH (NOLOCK)  ON A.CODSERIPS = inc.CODSERIPS 
					LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
					LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
				WHERE
					A.CODCENATE=@CentroAtencion 
					AND A.CODESTCIT='0' 
					AND A.TIPSOLICITU = 1 
					AND A.CODPROSAL=@Profesional 
					AND (A.FECHORAIN >= @FechaInicial AND A.FECHORAIN<=@FechaFinal) 
			UNION
				SELECT  0 AS CODAUTONU, RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end  AS Egreso,D.IESTADOIN as EstadoIngreso,A.IPFECHACO AS IPFECHACO ,CASE CODTIPCON WHEN '1' then 'Primera Vez' WHEN '2' THEN 'Control' WHEN '3' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
				RTRIM(B.IPNOMCOMP) AS Paciente,d.NUMINGRES AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,A.CODCONCEC AS ConsecutivoCita,A.IPFECHCIT AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, '' as CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo,  0 as IDRIASCUPS,'' as 'Actividad', '' AS 'TipoActividad'
				, Modalidad = '', iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
				, '' as TipoServicio
				, '0' CodigoTipoServicio
				, '' as CodigoServicio
				, '' as ServicioCUPS				
				, 0  As IdDescRelacionada
				, '' as NombreDescripcionRelacionada
				FROM dbo.ADCONCOEX A 
					INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI 
					INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA 
					INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
					INNER JOIN dbo.INPROFSAL F WITH (NOLOCK) ON A.CODPROSAL = F.CODPROSAL
					LEFT OUTER JOIN dbo.INESPECIA E WITH (NOLOCK) ON A.CODESPECI=E.CODESPECI
				WHERE 
				A.CODCENATE=@CentroAtencion 
					and A.CONESTADO IN ('1','4','5','6') 
					AND A.CODPROSAL=@Profesional 
					AND (A.IPFECHCIT>= @FechaInicial AND A.IPFECHCIT<=@FechaFinal)
					AND A.NUMCONCIT IS NULL 
					AND A.CODTIPCON IS NOT NULL
		END

	ELSE

		BEGIN
			SELECT g.CODAUTONU, RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS  Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT,
				A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				,Modalidad = CASE WHEN G.MODALIDAD = 0 THEN 'Presencial' WHEN G.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
				,case inc.TIPSERIPS
						when 1 then 'Laboratorios'
						when 2 then 'Patologías'
						when 3 then 'Imágenes diagnósticas'
						when 4 then 'Procedimientos no quirúrgicos'
						when 5 then 'Procedimientos quirúrgicos'
						when 6 then 'Interconsultas'
						when 7 then 'Ninguno'
						when 8 then 'Consulta externa'
						when 9 then 'Hemocomponentes'
				END TipoServicio,
				inc.TIPSERIPS as  CodigoTipoServicio,
				Rtrim(inc.CODSERIPS) as CodigoServicio,
				Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) as ServicioCUPS,
				isnull(rtrim(CDD.Id),0) As IdDescRelacionada,
				Isnull(Rtrim(CD.name),'') as NombreDescripcionRelacionada
				FROM dbo.ADCONCOEX A  WITH (NOLOCK) 
					inner join dbo.AGASICITA g  WITH (NOLOCK)  on A.NUMCONCIT=g.CODAUTONU AND g.CODIGOCON = @Consultorio
					inner join dbo.AGACTIMED z  WITH (NOLOCK)  on z.CODACTMED = g.CODACTMED 
					inner join dbo.INPACIENT B  WITH (NOLOCK)  ON A.IPCODPACI=B.IPCODPACI 
					inner join dbo.INENTIDAD C  WITH (NOLOCK)  ON A.CODENTIDA=C.CODENTIDA 
					inner join dbo.ADINGRESO D  WITH (NOLOCK)  ON A.NUMINGRES =D.NUMINGRES
					left outer join DBO.INESPECIA E  WITH (NOLOCK)  ON G.CODESPECI=E.CODESPECI
					left join INCUPSIPS inc WITH (NOLOCK)  ON G.CODSERIPS = inc.CODSERIPS 
					LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = G.IDDESCRIPCIONRELACIONADA 
					LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
				WHERE 
					A.CODCENATE=@CentroAtencion 
					and A.CONESTADO IN ('1','4','5','6') 
					AND g.TIPSOLICITU = 1 
					AND A.CODPROSAL=@ProfesionalConAsistida 
					AND (G.FECHORAIN  >= @FechaInicial AND G.FECHORAFI  <=@FechaFinal) 
			UNION
				SELECT A.CODAUTONU,RTRIM(D.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.CODESPECI) AS CodEspecialidad ,'2 - Asignadas'  AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
				RTRIM(B.IPNOMCOMP) AS Paciente,'' AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,0 AS ConsecutivoCita,A.FECHORAIN AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, A.CODACTMED, '' as TipoPaciente , IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, '' AS VIVESOLO, '' AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, isnull(A.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, Convert(Bit,0) as Recomendacion
				,case inc.TIPSERIPS
						when 1 then 'Laboratorios'
						when 2 then 'Patologías'
						when 3 then 'Imágenes diagnósticas'
						when 4 then 'Procedimientos no quirúrgicos'
						when 5 then 'Procedimientos quirúrgicos'
						when 6 then 'Interconsultas'
						when 7 then 'Ninguno'
						when 8 then 'Consulta externa'
						when 9 then 'Hemocomponentes'
				END TipoServicio,
				inc.TIPSERIPS as  CodigoTipoServicio,
				Rtrim(inc.CODSERIPS) as CodigoServicio,
				Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) as ServicioCUPS,
				isnull(rtrim(CDD.Id),0) As IdDescRelacionada,
				Isnull(Rtrim(CD.name),'') as NombreDescripcionRelacionada
				FROM dbo.AGASICITA A WITH (NOLOCK)
						inner join dbo.AGACTIMED z WITH (NOLOCK) on z.CODACTMED = A.CODACTMED
						INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI 
						INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA=C.CODENTIDA
						inner join dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
						left join dbo.ADCONCOEX ad WITH(NOLOCK) on ad.NUMCONCIT = a.CODAUTONU and ad.CODCONCEC is null
						left join INCUPSIPS inc WITH (NOLOCK)  ON A.CODSERIPS = inc.CODSERIPS 
						LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
						LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
				WHERE 
						A.CODCENATE=@CentroAtencion 
						AND A.CODESTCIT='0' 
						AND A.TIPSOLICITU = 1 
						AND A.CODPROSAL=@ProfesionalConAsistida 
						AND (A.FECHORAIN >= @FechaInicial AND A.FECHORAIN<=@FechaFinal) 
						AND A.CODIGOCON = @Consultorio
			UNION
				SELECT  0 AS CODAUTONU, RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS  Egreso,D.IESTADOIN as EstadoIngreso,A.IPFECHACO AS IPFECHACO ,CASE CODTIPCON WHEN '1' then 'Primera Vez' WHEN '2' THEN 'Control' WHEN '3' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
				RTRIM(B.IPNOMCOMP) AS Paciente,d.NUMINGRES AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,A.CODCONCEC AS ConsecutivoCita,A.IPFECHCIT AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, '' as CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo,  0 as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				,Modalidad = '', iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
				,case inc.TIPSERIPS
						when 1 then 'Laboratorios'
						when 2 then 'Patologías'
						when 3 then 'Imágenes diagnósticas'
						when 4 then 'Procedimientos no quirúrgicos'
						when 5 then 'Procedimientos quirúrgicos'
						when 6 then 'Interconsultas'
						when 7 then 'Ninguno'
						when 8 then 'Consulta externa'
						when 9 then 'Hemocomponentes'
				END TipoServicio,
				inc.TIPSERIPS as  CodigoTipoServicio,
				Rtrim(inc.CODSERIPS) as CodigoServicio,
				Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) as ServicioCUPS,
				isnull(rtrim(CDD.Id),0) As IdDescRelacionada,
				Isnull(Rtrim(CD.name),'') as NombreDescripcionRelacionada
				FROM dbo.ADCONCOEX A 
						INNER JOIN  dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI 
						INNER JOIN dbo.INENTIDAD C  WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA 
						INNER JOIN dbo.ADINGRESO D  WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
						INNER JOIN dbo.INPROFSAL F  WITH (NOLOCK) ON A.CODPROSAL = F.CODPROSAL
						INNER JOIN dbo.AGASICITA g  WITH (NOLOCK) ON A.NUMCONCIT = g.CODAUTONU AND g.CODIGOCON = @Consultorio
						inner join dbo.AGACTIMED z WITH (NOLOCK) on z.CODACTMED = g.CODACTMED
						LEFT OUTER JOIN dbo.INESPECIA E WITH (NOLOCK) ON A.CODESPECI=E.CODESPECI
						left join INCUPSIPS inc WITH (NOLOCK)  ON G.CODSERIPS = inc.CODSERIPS
						LEFT JOIN  contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = G.IDDESCRIPCIONRELACIONADA 
						LEFT JOIN  contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
				WHERE 
						A.CODCENATE=@CentroAtencion 
						and A.CONESTADO IN ('1','4','5','6') 
						AND A.CODPROSAL=@ProfesionalConAsistida 
						AND (A.IPFECHCIT>= @FechaInicial AND A.IPFECHCIT<=@FechaFinal)
						AND A.NUMCONCIT IS NULL 
						AND A.CODTIPCON IS NOT NULL
		END
END