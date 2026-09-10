
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
					left join dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de atención en consulta externa para un profesional de salud, centro de atención, unidad funcional y rango de fechas determinados. Combina información de consultas externas registradas (ADCONCOEX), citas agendadas (AGASICITA), datos del paciente (INPACIENT), entidad aseguradora (INENTIDAD), ingreso hospitalario (ADINGRESO), especialidad médica (INESPECIA), actividad médica (AGACTIMED) y código de servicio CUPS (INCUPSIPS) para construir una lista de trabajo del médico en su sala de espera. Distingue dos grupos de pacientes: los que ya tienen ingreso registrado y están en espera o siendo atendidos, y los que solo tienen cita asignada aún sin ingreso. Incluye información clínica y administrativa como tipo de cita, modalidad de atención (presencial o teleconsulta), estado del ingreso, llamados al paciente, población especial, acompañantes, recomendaciones, descripción del contrato (ContractDescriptions / CUPSEntityContractDescriptions) y tipo de servicio; soporta además un modo de consulta asistida controlado por el parámetro EsConsultaAsistida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado consolidado de pacientes pendientes y citas asignadas de consulta externa para un profesional, centro y rango de fechas, diferenciando el modo de consulta asistida con filtro adicional por consultorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse @CentroAtencion para filtrar todas las consultas.; Debe proveerse un rango válido de fechas (@FechaInicial, @FechaFinal).; Si @EsConsultaAsistida = 0, se usa @Profesional como código del profesional.; Si @EsConsultaAsistida = 1, se requieren @ProfesionalConAsistida y @Consultorio para filtrar la agenda y consultas.; Las tablas catálogo (INESPECIA, INCUPSIPS, contract.CUPSEntityContractDescriptions, contract.ContractDescriptions) deben existir aunque su match es opcional (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna citas/consultas del centro de atención indicado (@CentroAtencion).; Los registros provenientes de ADCONCOEX se restringen a CONESTADO IN (''1'',''4'',''5'',''6'') (en espera o atendidos sin conducta).; Las citas asignadas (AGASICITA) se filtran a CODESTCIT=''0'' y TIPSOLICITU=1 (citas de consulta externa pendientes).; El rango de fechas (@FechaInicial, @FechaFinal) se aplica sobre FECHORAIN/FECHORAFI o IPFECHCIT en cada subconsulta.; En modo asistida (@EsConsultaAsistida=1) siempre se exige coincidencia con @Consultorio (CODIGOCON) y se usa @ProfesionalConAsistida en lugar de @Profesional.; Recomendacion solo es verdadera cuando hay un registro activo (Status=1) en RecommendPatient para el paciente e ingreso.; Los tres bloques UNION cubren: consultas externas con cita asignada, citas asignadas aún no consumidas y consultas registradas sin agenda previa.; Riesgo, escalas (DOWNT, RASS, VAS, APACHE, NORTON) y sus puntajes se devuelven siempre en cero (no se calculan en este SP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Consulta externa; Cita médica; Agendamiento; Profesional de la salud; Especialidad; Centro de atención; Consultorio; Consulta asistida; Modalidad presencial/teleconsulta; Tipo de cita (Primera vez, Control, Posoperatorio); Entidad/aseguradora; Tipo de servicio CUPS/IPS; Población especial; Acompañantes; Recomendación de paciente; RIAS (IDRIASCUPS); Ingreso/admisión; Tipo de documento (ASMS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EsConsultaAsistida = 0 → Consulta el listado de pacientes pendientes filtrando solo por profesional, sin restringir por consultorio. else Consulta el listado filtrando por profesional con asistida y restringiendo además por el consultorio (CODIGOCON = @Consultorio).; si CONESTADO = ''6'' en ADCONCOEX → Etiqueta el egreso como ''3 - Atendidos, sin definir conducta''. else Etiqueta el egreso como ''1 - En Espera'' (para estados ''1'',''4'',''5'').; si CODTIPCIT/CODTIPCON según valores ''0''/''1''/''2''/''3'' → Clasifica TipoCita como Primera Vez, Control o Pos Operatorio; cualquier otro valor se asume ''Primera Vez''.; si MODALIDAD = 0 / 1 → Modalidad = ''Presencial'' o ''Teleconsulta''; en otro caso vacío.; si Existe registro en RecommendPatient con IPCODPACI, NUMINGRES y Status = 1 → Marca Recomendacion = 1 (true).; si IPTIPODOC IN (6,7) en INPACIENT → Marca ASMS = 1 (paciente identificado por tipos de documento 6 o 7).; si Existe ADPOBESPEPAC con TIPOPOESPERIES = 1 para el paciente → Cuenta como población especial (POBESPECIAL).; si TIPSERIPS de INCUPSIPS entre 1 y 9 → Traduce a etiqueta de tipo de servicio (Laboratorios, Patologías, Imágenes diagnósticas, Procedimientos no quirúrgicos, Procedimientos quirúrgicos, Interconsultas, Ninguno, Consulta externa, Hemocomponentes).; si Tercera consulta UNION: NUMCONCIT IS NULL AND CODTIPCON IS NOT NULL en ADCONCOEX → Incluye consultas de control/registradas sin asignación previa en agenda (citas creadas directamente en consulta externa).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INESPECIA; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna';
-- GO
