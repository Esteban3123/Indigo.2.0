-- =============================================
-- Author:      <Author, Edwin Reifer , Name>
-- Create Date: <Create Date,12/02/2021 >
-- Description: <Description, sp que Consulta las citas de consulta externa para el formulario que se encuentra en admsiones llamado control citas consulta externa>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_ListarControlCitasConsultaExterna]
(
	 @TipoConsultar as integer, --1:ControlCitas ; 2:PreConsulta
	 @CentroAtencion VARCHAR(10), --='11011',
	 @FechaInicial DATETIME, --= '2021/02/12 00:00:00',
	 @FechaFinal DATETIME --= '2021/02/12 23:59:59' 
)

AS
BEGIN

if @TipoConsultar = 1 begin
			SELECT a.CODCONCEC AS 'Codigo', RTRIM(E.DESESPECI) AS 'Especialidad',RTRIM(E.CODESPECI) AS CodEspecialidad ,'1 - En Espera' AS Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' END AS 'Tipo Cita',RTRIM(C.NOMENTIDA) AS 'Entidad',A.IPCODPACI AS 'Identificación',RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente', A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as 'Fecha Inicial Cita',
				A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, D.VIVESOLO, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad'
				,Modalidad = CASE WHEN G.MODALIDAD = 0 THEN 'Presencial' WHEN G.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, RTRIM(I.NOMMEDICO) AS 'Profesional',g.FECHORAFI AS 'Fecha Final Cita','Sin Especificar' AS 'Consultorio', 
				CASE A.CONESTADO WHEN 1 THEN 'Con Ingreso' WHEN 3 Then 'Cumplida' when 4 then 'En Sala' when 5 then 'En Consultorio'  END AS 'ESTADO',
				CASE A.CONESTADO WHEN 1 THEN '2. Con Ingreso' WHEN 3 Then '5. Cumplida' when 4 then '3. En Sala' when 5 then '4. En Consultorio'  END AS 'Orden_Agrupacion',	
				g.FECREGSIS as 'Fecha Asignacion Cita',
				A.FECREGSIS as 'Fecha Cita Con Ingreso',
				A.FECCITACUMPLIDA as 'Fecha Cita Cumplida',
				A.FECREGISTROSALA AS 'Fecha Paciente sala',
				A.FECENCONSULTORIO as 'Fecha Paciente Consultorio'
			FROM dbo.ADCONCOEX A  WITH (NOLOCK) 
				--inner join dbo.ADCONCOED f on a.CODCONCEC=f.CODCONCEC 
				inner join dbo.AGASICITA g  WITH (NOLOCK)  on A.NUMCONCIT=g.CODAUTONU 
				inner join dbo.AGACTIMED z  WITH (NOLOCK)  on z.CODACTMED = g.CODACTMED 
				inner join dbo.INPACIENT B  WITH (NOLOCK)  ON A.IPCODPACI=B.IPCODPACI 
				inner join dbo.INENTIDAD C  WITH (NOLOCK)  ON A.CODENTIDA=C.CODENTIDA 
				inner join dbo.ADINGRESO D  WITH (NOLOCK)  ON A.NUMINGRES =D.NUMINGRES
				inner JOIN dbo.INPROFSAL i  WITH (NOLOCK)  ON A.CODPROSAL = I.CODPROSAL
				left outer join DBO.INESPECIA E  WITH (NOLOCK)  ON G.CODESPECI=E.CODESPECI
				WHERE A.CODCENATE=@CentroAtencion and A.CONESTADO in(1,3,4,5)  AND (A.IPFECHCIT  >= @FechaInicial AND A.IPFECHCIT  <=@FechaFinal)
			UNION
				SELECT A.CODAUTONU  AS 'Codigo',RTRIM(D.DESESPECI) AS 'Especialidad',RTRIM(D.CODESPECI) AS CodEspecialidad ,'2 - Asignadas' AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' END AS 'Tipo Cita',RTRIM(C.NOMENTIDA) AS 'Entidad',A.IPCODPACI AS 'Identificación',
				RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente','' AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,0 AS ConsecutivoCita,A.FECHORAIN AS 'Fecha Inicial Cita','' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, A.CODACTMED, '' as TipoPaciente , IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, '' AS VIVESOLO, CONVERT(BIT,0) AS Riesgo, isnull(A.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad'
				,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, RTRIM(I.NOMMEDICO) AS 'Profesional',A.FECHORAFI AS 'Fecha Final Cita',RTRIM(F.DESCRICON) AS 'Consultorio',
				CASE A.CODESTCIT WHEN 0 THEN 'Asignada' END AS 'ESTADO', CASE A.CODESTCIT WHEN 0 THEN '1. Asignada' END AS 'Orden_Agrupacion',
				A.FECREGSIS as 'Fecha Asignacion Cita',
				NULL as 'Fecha Cita Con Ingreso',
				NULL as 'Fecha Cita Cumplida',
				NULL AS 'Fecha Paciente sala',
				NULL as 'Fecha Paciente Consultorio'
				FROM dbo.AGASICITA A WITH (NOLOCK)
				inner join dbo.AGACTIMED z WITH (NOLOCK) on z.CODACTMED = A.CODACTMED
				INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI=B.IPCODPACI 
				INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA=C.CODENTIDA
				inner join dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI=D.CODESPECI
				INNER JOIN dbo.INPROFSAL i WITH (NOLOCK) ON A.CODPROSAL = I.CODPROSAL
				inner JOIN dbo.AGCONSULT F ON A.CODIGOCON = F.CODIGOCON  AND A.CODCENATE = F.CODCENATE 
				left join dbo.ADCONCOEX ad WITH(NOLOCK) on ad.NUMCONCIT = a.CODAUTONU
			WHERE A.CODCENATE=@CentroAtencion AND A.CODESTCIT='0'AND (A.FECHORAIN >= @FechaInicial AND A.FECHORAIN<=@FechaFinal) and ad.CODCONCEC is NULL


END  else if @TipoConsultar = 2 begin

		SELECT a.CODCONCEC AS 'Codigo', RTRIM(E.DESESPECI) AS 'Especialidad',RTRIM(E.CODESPECI) AS CodEspecialidad ,'1 - En Espera' AS Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' END AS 'Tipo Cita',RTRIM(C.NOMENTIDA) AS 'Entidad',A.IPCODPACI AS 'Identificación',RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente', A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as 'Fecha Inicial Cita',
				A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
				CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, D.VIVESOLO, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad'
				,Modalidad = CASE WHEN G.MODALIDAD = 0 THEN 'Presencial' WHEN G.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END, RTRIM(I.NOMMEDICO) AS 'Profesional',g.FECHORAFI AS 'Fecha Final Cita','Sin Especificar' AS 'Consultorio', 
				CASE A.CONESTADO WHEN 1 THEN 'Con Ingreso' when 4 then 'En Sala' END AS 'ESTADO',
				CASE A.CONESTADO WHEN 1 THEN '2. Con Ingreso' when 4 then '3. En Sala'  END AS 'Orden_Agrupacion',	
				g.FECREGSIS as 'Fecha Asignacion Cita',
				A.FECREGSIS as 'Fecha Cita Con Ingreso',
				A.FECCITACUMPLIDA as 'Fecha Cita Cumplida',
				A.FECREGISTROSALA AS 'Fecha Paciente sala',
				A.FECENCONSULTORIO as 'Fecha Paciente Consultorio',
				A.UFUCODIGO as UnidadFuncionalFacturacion
			FROM dbo.ADCONCOEX A  WITH (NOLOCK) 
				--inner join dbo.ADCONCOED f on a.CODCONCEC=f.CODCONCEC 
				inner join dbo.AGASICITA g  WITH (NOLOCK)  on A.NUMCONCIT=g.CODAUTONU 
				inner join dbo.AGACTIMED z  WITH (NOLOCK)  on z.CODACTMED = g.CODACTMED 
				inner join dbo.INPACIENT B  WITH (NOLOCK)  ON A.IPCODPACI=B.IPCODPACI 
				inner join dbo.INENTIDAD C  WITH (NOLOCK)  ON A.CODENTIDA=C.CODENTIDA 
				inner join dbo.ADINGRESO D  WITH (NOLOCK)  ON A.NUMINGRES =D.NUMINGRES
				inner JOIN dbo.INPROFSAL i  WITH (NOLOCK)  ON A.CODPROSAL = I.CODPROSAL
				left outer join DBO.INESPECIA E  WITH (NOLOCK)  ON G.CODESPECI=E.CODESPECI
				WHERE A.CODCENATE=@CentroAtencion and A.CONESTADO in(1,4)  AND (A.IPFECHCIT  >= @FechaInicial AND A.IPFECHCIT <=@FechaFinal)
				AND not exists (select * from HCEXFISIC z WITH (NOLOCK)  where z.NUMINGRES = a.NUMINGRES and z.IPCODPACI = a.IPCODPACI)

end

end



/*
Estado del Control de Consultas:
1: Sin Atender/Con Ingreso
2: Ausente/Anulada
3: Atendido/Cita Cumplida
4: En sala
5: En consultorio
*/