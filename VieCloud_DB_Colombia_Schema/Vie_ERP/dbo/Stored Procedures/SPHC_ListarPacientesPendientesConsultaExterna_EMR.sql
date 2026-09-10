
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesPendientesConsultaExterna_EMR]
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
        SELECT g.CODAUTONU,
            G.FECHORAFI AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            CASE A.CONESTADO
                WHEN '6' THEN '3 - Atendidos, sin definir conducta'
                ELSE '1 - En Espera'
            END AS Egreso,
            D.IESTADOIN AS EstadoIngreso,
            A.IPFECHACO,
            CASE g.CODTIPCIT
                WHEN '0' THEN 'Primera Vez'
                WHEN '1' THEN 'Control'
                WHEN '2' THEN 'PosOperatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            A.NUMINGRES AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            G.FECHORAIN AS IPFECHCIT,
            A.PRIMERLLA AS LlamadoUno,
            A.SEGUNDLLA AS LlamadoDos,
            A.TERCERLLA AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            g.CODACTMED,
            D.CODTIPPAC AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE
                WHEN B.IPTIPODOC IN(6, 7) THEN 1
                ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,
            (
                SELECT count(*)
                FROM dbo.ADPOBESPEPAC Z
                INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                WHERE IPCODPACI = B.IPCODPACI
                    AND TIPOPOESPERIES = 1
            ) AS POBESPECIAL,
            D.VIVESOLO,
            (
                SELECT count(*)
                FROM ADACOMPAN AS AD
                WHERE AD.NUMINGRES = D.NUMINGRES
            ) AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            ISNULL(G.IDRIASCUPS, 0) AS IDRIASCUPS,
            Rtrim(Z.DESACTMED) AS 'Actividad',
            z.ACTIVICON AS 'TipoActividad',
            Modalidad = CASE
                WHEN G.MODALIDAD = 0 THEN 'Presencial'
                WHEN G.MODALIDAD = 1 THEN 'Teleconsulta'
                ELSE ''
            END,
            iif(
                (
                    SELECT COUNT(*)
                    FROM dbo.RecommendPatient
                    WHERE IPCODPACI = A.IPCODPACI
                        AND NUMINGRES = A.NUMINGRES
                        AND Status = 1
                ) > 0, Convert(Bit, 1), Convert(Bit,0)
            ) AS Recomendacion ,
            CASE inc.TIPSERIPS
                WHEN 1 THEN 'Laboratorios'
                WHEN 2 THEN 'Patologías'
                WHEN 3 THEN 'Imágenes diagnósticas'
                WHEN 4 THEN 'Procedimientos no quirúrgicos'
                WHEN 5 THEN 'Procedimientos quirúrgicos'
                WHEN 6 THEN 'Interconsultas'
                WHEN 7 THEN 'Ninguno'
                WHEN 8 THEN 'Consulta externa'
                WHEN 9 THEN 'Hemocomponentes'
            END TipoServicio,
            inc.TIPSERIPS AS CodigoTipoServicio,
            Rtrim(inc.CODSERIPS) AS CodigoServicio,
            Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) AS ServicioCUPS,
            isnull(rtrim(CDD.Id), 0) AS IdDescRelacionada,
            Isnull(Rtrim(CD.name), '') AS NombreDescripcionRelacionada
        FROM dbo.ADCONCOEX A WITH (NOLOCK)
        INNER JOIN dbo.AGASICITA g WITH (NOLOCK) ON A.NUMCONCIT=g.CODAUTONU
        INNER JOIN dbo.AGACTIMED z WITH (NOLOCK) ON z.CODACTMED = g.CODACTMED
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA
        INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
        LEFT OUTER JOIN DBO.INESPECIA E WITH (NOLOCK) ON G.CODESPECI=E.CODESPECI
        LEFT JOIN INCUPSIPS inc WITH (NOLOCK) ON G.CODSERIPS = inc.CODSERIPS
        LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = G.IDDESCRIPCIONRELACIONADA
        LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CONESTADO IN ('1', '4', '5', '6')
            AND g.TIPSOLICITU = 1
            AND A.CODPROSAL=@Profesional
            AND (G.FECHORAIN >= @FechaInicial AND G.FECHORAFI <=@FechaFinal)
        
        UNION
        
        SELECT A.CODAUTONU,
            A.FECHORAFI AS FechaFinCita,
            RTRIM(D.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(D.CODESPECI) AS CodEspecialidad,
            '2 - Asignadas' AS Egreso,
            '' AS EstadoIngreso,
            A.FECHORAIN AS IPFECHACO,
            CASE CODTIPCIT
                WHEN '0' THEN 'Primera Vez'
                WHEN '1' THEN 'Control'
                WHEN '2' THEN 'Pos Operatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            '' AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            0 AS ConsecutivoCita,
            A.FECHORAIN AS IPFECHCIT,
            '' AS LlamadoUno,
            '' AS LlamadoDos,
            '' AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            A.CODACTMED,
            '' AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE WHEN B.IPTIPODOC IN(6, 7) THEN 1
            ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,
            (
                SELECT count(*)
                FROM dbo.ADPOBESPEPAC Z
                INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1
            ) AS POBESPECIAL,
            '' AS VIVESOLO,
            '' AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            isnull(A.IDRIASCUPS, 0) AS IDRIASCUPS,
            Rtrim(Z.DESACTMED) AS 'Actividad',
            z.ACTIVICON AS 'TipoActividad' ,
            Modalidad = CASE
                WHEN A.MODALIDAD = 0 THEN 'Presencial'
                WHEN A.MODALIDAD = 1 THEN 'Teleconsulta'
                ELSE ''
            END,
            Convert(Bit,0) AS Recomendacion ,
            CASE inc.TIPSERIPS
                WHEN 1 THEN 'Laboratorios'
                WHEN 2 THEN 'Patologías'
                WHEN 3 THEN 'Imágenes diagnósticas'
                WHEN 4 THEN 'Procedimientos no quirúrgicos'
                WHEN 5 THEN 'Procedimientos quirúrgicos'
                WHEN 6 THEN 'Interconsultas'
                WHEN 7 THEN 'Ninguno'
                WHEN 8 THEN 'Consulta externa'
                WHEN 9 THEN 'Hemocomponentes'
            END TipoServicio,
            inc.TIPSERIPS AS CodigoTipoServicio,
            Rtrim(inc.CODSERIPS) AS CodigoServicio,
            Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) AS ServicioCUPS,
            isnull(rtrim(CDD.Id), 0) AS IdDescRelacionada,
            Isnull(Rtrim(CD.name), '') AS NombreDescripcionRelacionada
        FROM dbo.AGASICITA A WITH (NOLOCK)
        INNER JOIN dbo.AGACTIMED z WITH (NOLOCK) ON z.CODACTMED = A.CODACTMED
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA=C.CODENTIDA
        LEFT JOIN dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
        LEFT JOIN dbo.ADCONCOEX ad WITH(NOLOCK) ON ad.NUMCONCIT = a.CODAUTONU
        AND ad.CODCONCEC IS NULL
        LEFT JOIN INCUPSIPS inc WITH (NOLOCK) ON A.CODSERIPS = inc.CODSERIPS
        LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
        LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CODESTCIT='0'
            AND A.TIPSOLICITU = 1
            AND A.CODPROSAL=@Profesional
            AND (A.FECHORAIN >= @FechaInicial
            AND A.FECHORAIN<=@FechaFinal)
        
        UNION
        
        SELECT 0 AS CODAUTONU,
            A.FECAUSENT AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            CASE A.CONESTADO
                WHEN '6' THEN '3 - Atendidos, sin definir conducta'
                ELSE '1 - En Espera'
            END AS Egreso,
            D.IESTADOIN AS EstadoIngreso,
            A.IPFECHACO AS IPFECHACO,
            CASE CODTIPCON
                WHEN '1' THEN 'Primera Vez'
                WHEN '2' THEN 'Control'
                WHEN '3' THEN 'Pos Operatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            d.NUMINGRES AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            A.IPFECHCIT AS IPFECHCIT,
            '' AS LlamadoUno,
            '' AS LlamadoDos,
            '' AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            '' AS CODACTMED,
            D.CODTIPPAC AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE WHEN B.IPTIPODOC IN(6, 7) THEN 1
            ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,
            (
                SELECT count(*)
                FROM dbo.ADPOBESPEPAC Z
                INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1
            ) AS POBESPECIAL,
            D.VIVESOLO,
            (
                SELECT count(*)
                FROM ADACOMPAN AS AD
                WHERE AD.NUMINGRES = D.NUMINGRES
            ) AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            0 AS IDRIASCUPS,
            '' AS 'Actividad',
            '' AS 'TipoActividad' ,
            Modalidad = '',
            iif(
                (
                    SELECT COUNT(*)
                    FROM dbo.RecommendPatient
                    WHERE IPCODPACI = A.IPCODPACI
                        AND NUMINGRES = A.NUMINGRES
                        AND Status = 1
                ) > 0, Convert(Bit,1), Convert(Bit,0)
            ) AS Recomendacion ,
            '' AS TipoServicio ,
            '0' CodigoTipoServicio ,
            '' AS CodigoServicio ,
            '' AS ServicioCUPS ,
            0 AS IdDescRelacionada ,
            '' AS NombreDescripcionRelacionada
        FROM dbo.ADCONCOEX A
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA
        INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
        INNER JOIN dbo.INPROFSAL F WITH (NOLOCK) ON A.CODPROSAL = F.CODPROSAL
        LEFT OUTER JOIN dbo.INESPECIA E WITH (NOLOCK) ON A.CODESPECI=E.CODESPECI
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CONESTADO IN ('1','4','5','6')
            AND A.CODPROSAL=@Profesional
            AND (A.IPFECHCIT>= @FechaInicial AND A.IPFECHCIT<=@FechaFinal)
            AND A.NUMCONCIT IS NULL
            AND A.CODTIPCON IS NOT NULL

    END
	ELSE
    BEGIN
        
        SELECT g.CODAUTONU,
            G.FECHORAFI AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            CASE A.CONESTADO
                WHEN '6' THEN '3 - Atendidos, sin definir conducta'
                ELSE '1 - En Espera'
            END AS Egreso,
            D.IESTADOIN AS EstadoIngreso,
            A.IPFECHACO,
            CASE g.CODTIPCIT
                WHEN '0' THEN 'Primera Vez'
                WHEN '1' THEN 'Control'
                WHEN '2' THEN 'PosOperatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            A.NUMINGRES AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            G.FECHORAIN AS IPFECHCIT,
            A.PRIMERLLA AS LlamadoUno,
            A.SEGUNDLLA AS LlamadoDos,
            A.TERCERLLA AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            g.CODACTMED,
            D.CODTIPPAC AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE WHEN B.IPTIPODOC IN(6, 7) THEN 1
            ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,
            (
                SELECT count(*)
                FROM dbo.ADPOBESPEPAC Z
                INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                WHERE IPCODPACI = B.IPCODPACI
                    AND TIPOPOESPERIES = 1
            ) AS POBESPECIAL,
            D.VIVESOLO,
            (
                SELECT count(*)
                FROM ADACOMPAN AS AD
                WHERE AD.NUMINGRES = D.NUMINGRES
            ) AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            ISNULL(G.IDRIASCUPS, 0) AS IDRIASCUPS,
            Rtrim(Z.DESACTMED) AS 'Actividad',
            z.ACTIVICON AS 'TipoActividad' ,
            Modalidad = CASE
                WHEN G.MODALIDAD = 0 THEN 'Presencial'
                WHEN G.MODALIDAD = 1 THEN 'Teleconsulta'
                ELSE ''
            END,
            iif(
                (
                    SELECT COUNT(*)
                    FROM dbo.RecommendPatient
                    WHERE IPCODPACI = A.IPCODPACI
                        AND NUMINGRES = A.NUMINGRES
                        AND Status = 1
                ) > 0, Convert(Bit,1), Convert(Bit,0)
            ) AS Recomendacion ,
            CASE inc.TIPSERIPS
                WHEN 1 THEN 'Laboratorios'
                WHEN 2 THEN 'Patologías'
                WHEN 3 THEN 'Imágenes diagnósticas'
                WHEN 4 THEN 'Procedimientos no quirúrgicos'
                WHEN 5 THEN 'Procedimientos quirúrgicos'
                WHEN 6 THEN 'Interconsultas'
                WHEN 7 THEN 'Ninguno'
                WHEN 8 THEN 'Consulta externa'
                WHEN 9 THEN 'Hemocomponentes'
            END TipoServicio,
            inc.TIPSERIPS AS CodigoTipoServicio,
            Rtrim(inc.CODSERIPS) AS CodigoServicio,
            Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) AS ServicioCUPS,
            isnull(rtrim(CDD.Id), 0) AS IdDescRelacionada,
            Isnull(Rtrim(CD.name), '') AS NombreDescripcionRelacionada
        FROM dbo.ADCONCOEX A WITH (NOLOCK)
        INNER JOIN dbo.AGASICITA g WITH (NOLOCK) ON A.NUMCONCIT=g.CODAUTONU AND g.CODIGOCON = @Consultorio
        INNER JOIN dbo.AGACTIMED z WITH (NOLOCK) ON z.CODACTMED = g.CODACTMED
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA
        INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
        LEFT OUTER JOIN DBO.INESPECIA E WITH (NOLOCK) ON G.CODESPECI=E.CODESPECI
        LEFT JOIN INCUPSIPS inc WITH (NOLOCK) ON G.CODSERIPS = inc.CODSERIPS
        LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = G.IDDESCRIPCIONRELACIONADA
        LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CONESTADO IN ('1','4','5','6')
            AND g.TIPSOLICITU = 1
            AND A.CODPROSAL=@ProfesionalConAsistida
            AND (G.FECHORAIN >= @FechaInicial
                AND G.FECHORAFI <=@FechaFinal)
        
        UNION
        
        SELECT A.CODAUTONU,
            A.FECHORAFI AS FechaFinCita,
            RTRIM(D.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(D.CODESPECI) AS CodEspecialidad,
            '2 - Asignadas' AS Egreso,
            '' AS EstadoIngreso,
            A.FECHORAIN AS IPFECHACO,
            CASE CODTIPCIT
                WHEN '0' THEN 'Primera Vez'
                WHEN '1' THEN 'Control'
                WHEN '2' THEN 'Pos Operatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            '' AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            0 AS ConsecutivoCita,
            A.FECHORAIN AS IPFECHCIT,
            '' AS LlamadoUno,
            '' AS LlamadoDos,
            '' AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            A.CODACTMED,
            '' AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE WHEN B.IPTIPODOC IN(6,7) THEN 1
            ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,
            (
                SELECT count(*)
                FROM dbo.ADPOBESPEPAC Z
                INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                WHERE IPCODPACI = B.IPCODPACI
                    AND TIPOPOESPERIES = 1
            ) AS POBESPECIAL,
            '' AS VIVESOLO,
            '' AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            isnull(A.IDRIASCUPS, 0) AS IDRIASCUPS,
            Rtrim(Z.DESACTMED) AS 'Actividad',
            z.ACTIVICON AS 'TipoActividad' ,
            Modalidad = CASE
                WHEN A.MODALIDAD = 0 THEN 'Presencial'
                WHEN A.MODALIDAD = 1 THEN 'Teleconsulta'
                ELSE ''
            END,
            Convert(Bit,0) AS Recomendacion ,
            CASE inc.TIPSERIPS
                WHEN 1 THEN 'Laboratorios'
                WHEN 2 THEN 'Patologías'
                WHEN 3 THEN 'Imágenes diagnósticas'
                WHEN 4 THEN 'Procedimientos no quirúrgicos'
                WHEN 5 THEN 'Procedimientos quirúrgicos'
                WHEN 6 THEN 'Interconsultas'
                WHEN 7 THEN 'Ninguno'
                WHEN 8 THEN 'Consulta externa'
                WHEN 9 THEN 'Hemocomponentes'
            END TipoServicio,
            inc.TIPSERIPS AS CodigoTipoServicio,
            Rtrim(inc.CODSERIPS) AS CodigoServicio,
            Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) AS ServicioCUPS,
            isnull(rtrim(CDD.Id), 0) AS IdDescRelacionada,
            Isnull(Rtrim(CD.name), '') AS NombreDescripcionRelacionada
        FROM dbo.AGASICITA A WITH (NOLOCK)
        INNER JOIN dbo.AGACTIMED z WITH (NOLOCK) ON z.CODACTMED = A.CODACTMED
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA=C.CODENTIDA
        INNER JOIN dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
        LEFT JOIN dbo.ADCONCOEX ad WITH(NOLOCK) ON ad.NUMCONCIT = a.CODAUTONU
        AND ad.CODCONCEC IS NULL
        LEFT JOIN INCUPSIPS inc WITH (NOLOCK) ON A.CODSERIPS = inc.CODSERIPS
        LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
        LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CODESTCIT='0'
            AND A.TIPSOLICITU = 1
            AND A.CODPROSAL=@ProfesionalConAsistida
            AND (A.FECHORAIN >= @FechaInicial
                AND A.FECHORAIN<=@FechaFinal)
            AND A.CODIGOCON = @Consultorio

        UNION
        
        SELECT 0 AS CODAUTONU,
            G.FECHORAFI AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            CASE A.CONESTADO
                WHEN '6' THEN '3 - Atendidos, sin definir conducta'
                ELSE '1 - En Espera'
            END AS Egreso,
            D.IESTADOIN AS EstadoIngreso,
            A.IPFECHACO AS IPFECHACO,
            CASE CODTIPCON
                WHEN '1' THEN 'Primera Vez'
                WHEN '2' THEN 'Control'
                WHEN '3' THEN 'Pos Operatorio'
                ELSE 'Primera Vez'
            END AS TipoCita,
            RTRIM(C.NOMENTIDA) AS NOMENTIDA,
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            d.NUMINGRES AS Ingreso,
            CAST('' AS bit) AS MuestraAlerta,
            'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            A.IPFECHCIT AS IPFECHCIT,
            '' AS LlamadoUno,
            '' AS LlamadoDos,
            '' AS LlamadoTres,
            '0' AS ESCADOWNT,
            '0' AS ESCARASS,
            '0' AS ESCVASPAC,
            '0' AS ESCAPAPAC,
            '0' AS ESCNORPAC,
            0 AS PUNTAJEDOWN,
            0 AS PUNTAJERASS,
            0 AS PUNTAJEVAS,
            0 AS PUNTAJEAPACHE,
            0 AS PUNTAJENORTON,
            '' AS CODACTMED,
            D.CODTIPPAC AS TipoPaciente,
            IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,
            CASE
                WHEN B.IPTIPODOC IN(6,
                                    7) THEN 1
                ELSE 0
            END AS ASMS,
            B.ZONAPARTADA,

        (SELECT count(*)
        FROM dbo.ADPOBESPEPAC Z
        INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
        WHERE IPCODPACI = B.IPCODPACI
            AND TIPOPOESPERIES = 1) AS POBESPECIAL,
            D.VIVESOLO,

        (SELECT count(*)
        FROM ADACOMPAN AS AD
        WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES,
            CONVERT(BIT,0) AS Riesgo,
            0 AS IDRIASCUPS,
            Rtrim(Z.DESACTMED) AS 'Actividad',
            z.ACTIVICON AS 'TipoActividad' ,
            Modalidad = '',
            iif(
                    (SELECT COUNT(*)
                    FROM dbo.RecommendPatient
                    WHERE IPCODPACI = A.IPCODPACI
                        AND NUMINGRES = A.NUMINGRES
                        AND Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) AS Recomendacion ,
            CASE inc.TIPSERIPS
                WHEN 1 THEN 'Laboratorios'
                WHEN 2 THEN 'Patologías'
                WHEN 3 THEN 'Imágenes diagnósticas'
                WHEN 4 THEN 'Procedimientos no quirúrgicos'
                WHEN 5 THEN 'Procedimientos quirúrgicos'
                WHEN 6 THEN 'Interconsultas'
                WHEN 7 THEN 'Ninguno'
                WHEN 8 THEN 'Consulta externa'
                WHEN 9 THEN 'Hemocomponentes'
            END TipoServicio,
            inc.TIPSERIPS AS CodigoTipoServicio,
            Rtrim(inc.CODSERIPS) AS CodigoServicio,
            Concat(Rtrim(inc.CODSERIPS), ' - ', Rtrim(inc.DESSERIPS)) AS ServicioCUPS,
            isnull(rtrim(CDD.Id), 0) AS IdDescRelacionada,
            Isnull(Rtrim(CD.name), '') AS NombreDescripcionRelacionada
        FROM dbo.ADCONCOEX A
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA=C.CODENTIDA
        INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES =D.NUMINGRES
        INNER JOIN dbo.INPROFSAL F WITH (NOLOCK) ON A.CODPROSAL = F.CODPROSAL
        INNER JOIN dbo.AGASICITA g WITH (NOLOCK) ON A.NUMCONCIT = g.CODAUTONU
        AND g.CODIGOCON = @Consultorio
        INNER JOIN dbo.AGACTIMED z WITH (NOLOCK) ON z.CODACTMED = g.CODACTMED
        LEFT OUTER JOIN dbo.INESPECIA E WITH (NOLOCK) ON A.CODESPECI=E.CODESPECI
        LEFT JOIN INCUPSIPS inc WITH (NOLOCK) ON G.CODSERIPS = inc.CODSERIPS
        LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = G.IDDESCRIPCIONRELACIONADA
        LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
        WHERE A.CODCENATE=@CentroAtencion
            AND A.CONESTADO IN ('1','4','5','6')
            AND A.CODPROSAL=@ProfesionalConAsistida
            AND (A.IPFECHCIT>= @FechaInicial
                AND A.IPFECHCIT<=@FechaFinal)
            AND A.NUMCONCIT IS NULL
            AND A.CODTIPCON IS NOT NULL
        
    END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de atención en consulta externa para un profesional de salud, centro de atención, unidad funcional y rango de fechas dado. Combina datos de citas agendadas (AGASICITA), ingresos de pacientes (ADINGRESO), información del paciente (INPACIENT), entidades aseguradoras (INENTIDAD), especialidades médicas (INESPECIA) y servicios CUPS (INCUPSIPS) para construir la sala de espera digital del médico. Soporta dos modos: consulta normal (atendida directamente por el profesional) y consulta asistida (cuando otro profesional apoya la atención), diferenciados por el parámetro @EsConsultaAsistida. Devuelve para cada paciente en espera o asignado: identificación, nombre, número de ingreso, tipo de cita (primera vez, control, posoperatorio), estado en cola (en espera, atendido sin conducta, asignado), modalidad (presencial o teleconsulta), actividad médica, servicio CUPS, descripción del contrato relacionada y alertas clínicas como población especial, acompañantes y recomendaciones activas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes pendientes de atención en consulta externa para un profesional, combinando citas asignadas, consultas con confirmación y consultas sin agenda, con soporte para modalidad asistida por consultorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención, profesional y rango de fechas deben ser provistos; Para modo asistida (@EsConsultaAsistida=1) deben suministrarse @ProfesionalConAsistida y @Consultorio; Las citas en AGASICITA deben tener TIPSOLICITU=1 (solicitud de consulta externa) para ser consideradas en los tramos de citas; Las consultas en ADCONCOEX deben pertenecer al centro de atención y al profesional consultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera consultas externas con CONESTADO en (''1'',''4'',''5'',''6'') (estados activos/pendientes/atendidos sin conducta); Las citas asignadas (segundo tramo) solo se incluyen si CODESTCIT=''0'' (cita pendiente) y TIPSOLICITU=1; El tramo de consultas sin agenda exige NUMCONCIT IS NULL y CODTIPCON IS NOT NULL (consultas registradas directamente, sin cita previa); Siempre filtra por el centro de atención (@CentroAtencion) y por el rango de fechas suministrado; En modalidad asistida, todos los tramos restringen por el consultorio (@Consultorio); El conteo POBESPECIAL solo considera población especial con TIPOPOESPERIES=1; Los campos de escalas clínicas (Down, Rass, VAS, Apache, Norton) se devuelven siempre en cero/''0'' (no se calculan en este procedimiento); La columna Riesgo siempre se devuelve como 0 (false)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta externa; Cita médica; Paciente; Profesional de la salud; Especialidad; Consultorio; Modalidad presencial/Teleconsulta; Tipo de cita (Primera vez/Control/PosOperatorio); Población especial; Acompañantes; Recomendación de interconsulta; Servicios CUPS; Entidad/aseguradora; Ingreso/admisión; Consulta asistida; Tipo de documento ASMS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @EsConsultaAsistida=0 retorna la unión de: (a) consultas confirmadas en ADCONCOEX con cita en AGASICITA filtradas por @Profesional y rango de fechas, (b) citas asignadas en AGASICITA con CODESTCIT=''0'' del @Profesional, (c) consultas en ADCONCOEX sin cita asociada (NUMCONCIT IS NULL y CODTIPCON IS NOT NULL); [RETURN_RESULT] resultset: Cuando @EsConsultaAsistida=1 retorna las mismas tres uniones pero filtrando por @ProfesionalConAsistida y restringiendo por @Consultorio (g.CODIGOCON=@Consultorio o A.CODIGOCON=@Consultorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EsConsultaAsistida = 0 → Ejecuta el listado filtrando por @Profesional sin restringir consultorio else Ejecuta el listado filtrando por @ProfesionalConAsistida y exigiendo @Consultorio en la cita; si A.CONESTADO = ''6'' en ADCONCOEX → Marca el egreso como ''3 - Atendidos, sin definir conducta'' else Marca el egreso como ''1 - En Espera''; si CODTIPCIT en AGASICITA: ''0'',''1'',''2'' → Clasifica TipoCita como ''Primera Vez'',''Control'' o ''PosOperatorio'' respectivamente; cualquier otro valor se trata como ''Primera Vez''; si CODTIPCON en ADCONCOEX (consultas sin cita): ''1'',''2'',''3'' → Clasifica TipoCita como ''Primera Vez'',''Control'' o ''Pos Operatorio''; otros valores caen a ''Primera Vez''; si MODALIDAD = 0 / 1 en cita → Modalidad se reporta como ''Presencial'' / ''Teleconsulta''; en otro caso se deja vacío; si B.IPTIPODOC IN (6,7) → Marca ASMS = 1, en caso contrario ASMS = 0; si Existe registro en RecommendPatient para el paciente e ingreso con Status=1 → Marca Recomendacion = 1 (true), de lo contrario 0 (false); si TIPSERIPS de INCUPSIPS entre 1 y 9 → Traduce a la descripción correspondiente (Laboratorios, Patologías, Imágenes diagnósticas, Procedimientos no quirúrgicos, Procedimientos quirúrgicos, Interconsultas, Ninguno, Consulta externa, Hemocomponentes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INESPECIA; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR';
-- GO
