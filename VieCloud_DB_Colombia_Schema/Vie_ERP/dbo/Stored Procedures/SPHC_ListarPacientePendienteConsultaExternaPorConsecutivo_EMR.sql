
CREATE PROCEDURE [dbo].[SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR]
(
    @CentroAtencion Char(10),
    @Profesional Char(20),
    @ProfesionalConAsistida Char(10),
    @Consultorio Char(10),
    @EsConsultaAsistida bit,
    @Consecutivo int
)
AS
BEGIN
	SET NOCOUNT ON;
	IF @EsConsultaAsistida = 0
		BEGIN
			SELECT g.CODAUTONU
                , G.FECHORAFI
                , G.CODESTCIT AS EstadoCita
                , RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad 
                , case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS Egreso,D.IESTADOIN AS EstadoIngreso
                , A.IPFECHACO,CASE g.CODTIPCIT when '1' then 'control' WHEN '2' THEN 'post_operation' else 'first_time' END AS TipoCita
                , RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso 
                , CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT 
                , A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS
                , '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE
                , 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' 
                ,CAST('' AS CHAR(50)) AS Edad
                , CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad', z.ACTIVICON AS 'TipoActividad'
				, Modalidad = CASE WHEN G.MODALIDAD = 0 THEN 'Presencial' WHEN G.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
				, case inc.TIPSERIPS
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
                    AND (@Consecutivo IS NULL OR @Consecutivo = A.CODCONCEC)
			UNION
				SELECT A.CODAUTONU
                , A.FECHORAFI
                , A.CODESTCIT AS EstadoCita
                ,RTRIM(D.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.CODESPECI) AS CodEspecialidad ,'2 - Asignadas'  AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO 
                ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita
                ,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
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
                    AND (@Consecutivo IS NULL OR @Consecutivo = A.CODAUTONU)
			UNION
				SELECT  0 AS CODAUTONU
                , A.FECAUSENT
                , A.CONESTADO AS EstadoCita
                , RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end  AS Egreso,D.IESTADOIN as EstadoIngreso,A.IPFECHACO AS IPFECHACO ,CASE CODTIPCON WHEN '1' then 'Primera Vez' WHEN '2' THEN 'Control' WHEN '3' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
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
					AND A.NUMCONCIT IS NULL 
					AND A.CODTIPCON IS NOT NULL
                    AND (@Consecutivo IS NULL OR @Consecutivo = A.CODCONCEC)
		END

	ELSE

		BEGIN
			SELECT g.CODAUTONU
                , G.FECHORAFI
                , G.CODESTCIT AS EstadoCita
                , RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS  Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT,
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
                    AND (@Consecutivo IS NULL OR @Consecutivo = A.CODCONCEC)
			UNION
				SELECT A.CODAUTONU
                , A.FECHORAFI
                , A.CODESTCIT AS EstadoCita
                ,RTRIM(D.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.CODESPECI) AS CodEspecialidad ,'2 - Asignadas'  AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
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
						AND A.CODIGOCON = @Consultorio
                        AND (@Consecutivo IS NULL OR @Consecutivo = A.CODAUTONU)
			UNION
				SELECT  0 AS CODAUTONU
                , G.FECHORAFI
                , G.CODESTCIT AS EstadoCita
                , RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,case A.CONESTADO when '6' then '3 - Atendidos, sin definir conducta' else '1 - En Espera' end AS  Egreso,D.IESTADOIN as EstadoIngreso,A.IPFECHACO AS IPFECHACO ,CASE CODTIPCON WHEN '1' then 'Primera Vez' WHEN '2' THEN 'Control' WHEN '3' THEN 'Pos Operatorio' else 'Primera Vez' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
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
						AND A.NUMCONCIT IS NULL 
						AND A.CODTIPCON IS NOT NULL
                        AND (@Consecutivo IS NULL OR @Consecutivo = A.CODCONCEC)
		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de atención en consulta externa para un profesional de salud, centro de atención y consultorio específicos, filtrando opcionalmente por un número de consecutivo de cita. Combina información de la consulta externa (ADCONCOEX), citas agendadas (AGASICITA), datos del paciente (INPACIENT), ingreso (ADINGRESO), entidad aseguradora (INENTIDAD), especialidad médica (INESPECIA), actividad médica (AGACTIMED) y servicios CUPS (INCUPSIPS), incluyendo también la descripción de contrato relacionada (CUPSEntityContractDescriptions, ContractDescriptions). Distingue dos escenarios según el parámetro @EsConsultaAsistida: cuando es falso, muestra pacientes con ingreso activo en sala de espera o en atención; cuando es verdadero, incluye además citas asignadas sin ingreso formal. Devuelve por cada paciente datos clínicos y operativos como estado de la cita, tipo (primera vez, control, posoperatorio), modalidad (presencial o teleconsulta), tipo de servicio CUPS, descripción del contrato, llamados realizados, población especial, acompañantes y recomendación activa, sirviendo como fuente principal de la lista de trabajo del médico en el módulo de consulta externa del EMR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la lista de pacientes pendientes de atención en consulta externa para un profesional (y opcionalmente un consultorio en consulta asistida), unificando consultas en curso, citas asignadas y consultas espontáneas, con datos clínicos, administrativos y de servicio CUPS para la bandeja del EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un centro de atención válido para filtrar las citas/consultas.; Si @EsConsultaAsistida = 0 debe enviarse @Profesional; si es 1 debe enviarse @ProfesionalConAsistida y @Consultorio.; Las tablas maestras de paciente, entidad, especialidad, actividad médica e ingreso deben tener los registros referenciados por las citas/consultas para que aparezcan en el resultado (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran registros del centro de atención recibido como parámetro.; En el bloque de consultas en ADCONCOEX siempre se exigen estados CONESTADO IN (''1'',''4'',''5'',''6'').; En el bloque de citas asignadas en AGASICITA siempre se exige CODESTCIT=''0'' y TIPSOLICITU=1.; Las consultas tomadas desde ADCONCOEX con NUMCONCIT NULL requieren además CODTIPCON IS NOT NULL (consultas espontáneas/sin cita previa).; El parámetro @Consecutivo actúa como filtro opcional: si es NULL no restringe.; En modo asistido todas las consultas y citas deben corresponder al consultorio @Consultorio.; El procedimiento es de solo lectura (SET NOCOUNT ON, sin DML).; Modalidad fuera de {0,1} se normaliza a cadena vacía.; La recomendación activa se determina exclusivamente con Status=1 en RecommendPatient.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta externa; EMR / Historia clínica electrónica; Cita médica (asignada, en espera, atendida); Tipo de cita: primera vez, control, posoperatorio; Modalidad: presencial / teleconsulta; Servicios CUPS (Laboratorios, Patologías, Imágenes, Procedimientos, Interconsultas, Hemocomponentes); Centro de atención; Profesional de la salud; Consultorio / consulta asistida; Especialidad médica; Entidad pagadora (EPS/ARS); Población especial / riesgo; Acompañantes del paciente; Llamados al paciente en sala; Recomendación / interconsulta activa; Descripción contractual CUPS; Paciente ASMS (tipos de documento 6 y 7)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EsConsultaAsistida = 0 (consulta no asistida) → Filtra citas y consultas del profesional (@Profesional) sin restringir por consultorio. else Filtra por @ProfesionalConAsistida y exige que la cita pertenezca al consultorio @Consultorio (g.CODIGOCON = @Consultorio).; si A.CONESTADO = ''6'' en ADCONCOEX → Egreso se reporta como ''3 - Atendidos, sin definir conducta''. else Egreso se reporta como ''1 - En Espera'' para estados ''1'',''4'',''5'',''6''.; si B.IPTIPODOC IN (6,7) → Marca al paciente como ASMS = 1 (sin documento o tipos especiales). else ASMS = 0.; si G.MODALIDAD = 0 / = 1 / otro → Modalidad se etiqueta como ''Presencial'', ''Teleconsulta'' o cadena vacía respectivamente.; si Existe registro en RecommendPatient con mismo IPCODPACI, NUMINGRES y Status=1 → Recomendacion = 1 (bit), de lo contrario 0.; si inc.TIPSERIPS entre 1 y 9 → Traduce el código a la categoría CUPS correspondiente (Laboratorios, Patologías, Imágenes diagnósticas, Procedimientos no quirúrgicos/quirúrgicos, Interconsultas, Ninguno, Consulta externa, Hemocomponentes).; si @Consecutivo IS NULL → No filtra por consecutivo (devuelve todos los registros que cumplan las demás condiciones). else Restringe a la cita/consulta cuyo CODCONCEC o CODAUTONU coincide con @Consecutivo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INESPECIA; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientePendienteConsultaExternaPorConsecutivo_EMR';
-- GO
