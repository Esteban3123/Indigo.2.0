

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesAgendados]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@Profesional Char(20),
@FechaInicial date,
@FechaFinal date
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT g.CODAUTONU, '1 - En Espera'    AS Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT,
A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status =1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.ADCONCOEX A inner join
dbo.ADCONCOED f on a.CODCONCEC=f.CODCONCEC inner join
dbo.AGASICITA g on f.NUMCONCIT=g.CODAUTONU inner join
dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
inner join dbo.INENTIDAD C with(nolock) ON A.CODENTIDA=C.CODENTIDA 
inner join dbo.ADINGRESO D with(nolock) ON A.NUMINGRES =D.NUMINGRES
WHERE A.CODCENATE=@CentroAtencion and A.CONESTADO='1' AND (G.FECHORAIN  >= @FechaInicial AND G.FECHORAFI  <=@FechaFinal)  AND g.TIPTRATAMIENTO = 2 --AND A.CODPROSAL=@Profesional

UNION

SELECT A.CODAUTONU, '2 - Asignadas'  AS Egreso,'' as EstadoIngreso,A.FECHORAIN AS IPFECHACO ,CASE CODTIPCIT WHEN '0' then 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
RTRIM(B.IPNOMCOMP) AS Paciente,'' AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,CAST('' AS int) AS ConsecutivoCita,A.FECHORAIN AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, A.CODACTMED, '' as TipoPaciente , IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad,
CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, '' AS VIVESOLO, '' AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo,  CONVERT(BIT,0) as Recomendacion
FROM dbo.AGASICITA A
INNER JOIN dbo.INPACIENT B with(nolock) ON A.IPCODPACI=B.IPCODPACI 
INNER JOIN dbo.INENTIDAD C with(nolock) ON B.CODENTIDA=C.CODENTIDA
WHERE A.CODCENATE=@CentroAtencion AND A.CODESTCIT='0'  AND (A.FECHORAIN >= @FechaInicial AND A.FECHORAIN<=@FechaFinal) AND A.TIPTRATAMIENTO = 2 --AND A.CODPROSAL=@Profesional

UNION

SELECT  0 AS CODAUTONU,'1 - En Espera'  AS Egreso,D.IESTADOIN as EstadoIngreso,A.IPFECHACO AS IPFECHACO ,CASE CODTIPCON WHEN '1' then 'Primera Vez' WHEN '2' THEN 'Control' WHEN '3' THEN 'Pos Operatorio' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,
RTRIM(B.IPNOMCOMP) AS Paciente,d.NUMINGRES AS Ingreso,CAST('' as bit) AS MuestraAlerta,'Normal' AS Alerta,A.CODCONCEC AS ConsecutivoCita,A.IPFECHCIT AS IPFECHCIT,'' AS LlamadoUno,'' AS LlamadoDos,'' AS LlamadoTres , '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, '' as CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,CAST('' AS CHAR(50)) AS Edad, 
CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status =1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion

FROM dbo.ADCONCOEX A INNER JOIN 
dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
INNER JOIN dbo.INENTIDAD C ON A.CODENTIDA=C.CODENTIDA 
INNER JOIN dbo.ADINGRESO D ON A.NUMINGRES =D.NUMINGRES
INNER JOIN dbo.INPROFSAL F ON A.CODPROSAL = F.CODPROSAL
WHERE A.CODCENATE=@CentroAtencion and A.CONESTADO='1'  AND (A.IPFECHCIT>= @FechaInicial AND A.IPFECHCIT<=@FechaFinal) --AND A.CODPROSAL=@Profesional
AND A.CODCONCEC  IN (SELECT CODCONCEC FROM ADCONCOED WHERE CODCONCEC=A.CODCONCEC AND (A.NUMCONCIT = 0) )
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes agendados en el módulo de oncología para un centro de atención y rango de fechas determinados, combinando tres estados posibles: pacientes en espera con conciliación vinculada a una cita, citas asignadas directamente desde el agendador, y conciliaciones sin cita formal asociada. Integra datos de conciliación (ADCONCOEX, ADCONCOED), citas médicas (AGASICITA), información del paciente (INPACIENT) como nombre, documento, fecha de nacimiento y zona apartada, entidad aseguradora (INENTIDAD), e ingreso hospitalario (ADINGRESO) para mostrar estado del ingreso, tipo de paciente, acompañantes y si vive solo. Devuelve también indicadores de escalas clínicas, población especial, si el paciente es menor de edad o ASMS, alertas y recomendaciones, siendo la pantalla principal de gestión de sala de espera oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con citas oncológicas (en espera, asignadas y con consulta externa sin cita conciliada) en un centro de atención y rango de fechas, enriqueciendo con datos de paciente, entidad, ingreso y banderas de riesgo/recomendación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y el rango de fechas (inicial y final) deben ser provistos; Deben existir registros de citas/consultas con TIPTRATAMIENTO = 2 (oncología) para los dos primeros bloques; Las relaciones entre ADCONCOEX, ADCONCOED, AGASICITA, INPACIENT, INENTIDAD y ADINGRESO deben estar consistentes para que el JOIN retorne filas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan registros del centro de atención solicitado; Solo se listan citas/consultas dentro del rango de fechas indicado; Los bloques 1 y 2 filtran exclusivamente tratamientos oncológicos (TIPTRATAMIENTO = 2); Las consultas externas incluidas siempre tienen CONESTADO = ''1'' (confirmadas); Las citas de agenda incluidas siempre tienen CODESTCIT = ''0''; Riesgo siempre se devuelve en 0 (no calculado en este SP); Las escalas y puntajes (DOWNTON, RASS, VAS, APACHE, NORTON) siempre se devuelven en ''0''/0 (placeholders); Alerta siempre se entrega como ''Normal'' y MuestraAlerta como bit vacío; El parámetro Profesional no se aplica como filtro (línea comentada); El parámetro UnidadFuncional no se utiliza', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Consulta externa; Agendamiento; Tratamiento oncológico; Tipo de cita (Primera vez/Control/Posoperatorio); Entidad aseguradora; Ingreso/Admisión; Estado de ingreso; Población especial; Acompañantes; Paciente que vive solo; Zona apartada; Tipo de documento ASMS; Recomendación de interconsulta; Escalas de valoración (Downton, RASS, VAS, Apache, Norton); Llamados de turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión de tres consultas: (1) consultas externas confirmadas con cita conciliada oncológica en estado ''1'' En Espera; (2) citas en agenda con CODESTCIT=''0'' marcadas como ''2 - Asignadas''; (3) consultas externas confirmadas sin número de cita conciliada (NUMCONCIT=0) marcadas como ''1 - En Espera''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODTIPCIT = ''0'' / ''1'' / ''2'' (bloques 1 y 2) → Etiqueta TipoCita como ''Primera Vez'', ''Control'' o ''PosOperatorio'' respectivamente; si CODTIPCON = ''1'' / ''2'' / ''3'' (bloque 3) → Etiqueta TipoCita como ''Primera Vez'', ''Control'' o ''Pos Operatorio'' respectivamente; si IPTIPODOC IN (6,7) → Marca el indicador ASMS = 1, en caso contrario 0 else ASMS = 0; si Existe al menos un registro en RecommendPatient para el paciente/ingreso con Status = 1 → Recomendacion = 1 (true) else Recomendacion = 0 (false); si A.CONESTADO = ''1'' AND TIPTRATAMIENTO = 2 (bloque 1) → Incluye la consulta externa como ''En Espera'' con datos de la cita asignada vinculada; si A.CODESTCIT = ''0'' AND TIPTRATAMIENTO = 2 (bloque 2) → Incluye la cita de la agenda como ''Asignadas''; si A.CONESTADO = ''1'' AND NUMCONCIT = 0 (bloque 3) → Incluye consultas externas sin cita conciliada como ''En Espera''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.ADCONCOED; dbo.AGASICITA; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INPROFSAL; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesAgendados';
-- GO
