
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAtendidosConsultaExterna]
(
@CentroAtencion Char(10),
@Profesional Char(20),
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT g.CODAUTONU, RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(E.CODESPECI) AS CodEspecialidad ,'1 - En Espera'    AS Egreso,D.IESTADOIN AS EstadoIngreso, A.IPFECHACO,CASE g.CODTIPCIT WHEN '0' then 'Primera Vez' when '1' then 'Control' WHEN '2' THEN 'PosOperatorio' END AS TipoCita,RTRIM(C.NOMENTIDA) AS NOMENTIDA,A.IPCODPACI AS Identificacion,RTRIM(B.IPNOMCOMP) AS Paciente, A.NUMINGRES AS Ingreso ,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta,A.CODCONCEC AS ConsecutivoCita,G.FECHORAIN as IPFECHCIT,
	A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, '0' as ESCADOWNT, '0' as ESCARASS, '0' as ESCVASPAC, '0' as ESCAPAPAC, '0' as ESCNORPAC, 0 as PUNTAJEDOWN, 0 as PUNTAJERASS, 0 as PUNTAJEVAS,  0 as PUNTAJEAPACHE, 0 as PUNTAJENORTON, g.CODACTMED, D.CODTIPPAC as TipoPaciente, IPFECNACI AS 'Fecha Nacimiento' ,[dbo].[Edad](B.IPFECNACI,getdate()) as Edad, 
	CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, B.ZONAPARTADA, (SELECT count(*) FROM dbo.ADPOBESPEPAC Z INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = B.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL, D.VIVESOLO, (SELECT count(*) FROM ADACOMPAN AS AD WHERE AD.NUMINGRES = D.NUMINGRES) AS ACOMPANANTES, CONVERT(BIT,0) AS Riesgo, ISNULL(G.IDRIASCUPS,0) as IDRIASCUPS,Rtrim(Z.DESACTMED) as 'Actividad'
	FROM dbo.ADCONCOEX A  WITH (NOLOCK) 
	inner join dbo.AGASICITA g  WITH (NOLOCK)  on A.NUMCONCIT=g.CODAUTONU 
	inner join dbo.AGACTIMED z  WITH (NOLOCK)  on z.CODACTMED = g.CODACTMED 
	inner join dbo.INPACIENT B  WITH (NOLOCK)  ON A.IPCODPACI=B.IPCODPACI 
	inner join dbo.INENTIDAD C  WITH (NOLOCK)  ON A.CODENTIDA=C.CODENTIDA 
	inner join dbo.ADINGRESO D  WITH (NOLOCK)  ON A.NUMINGRES =D.NUMINGRES
	left outer join DBO.INESPECIA E  WITH (NOLOCK)  ON G.CODESPECI=E.CODESPECI
	WHERE A.CODCENATE=@CentroAtencion and g.CODESTCIT  ='1' AND A.CODPROSAL=@Profesional AND (G.FECHORAIN  >= @FechaInicial AND G.FECHORAFI  <=@FechaFinal)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes atendidos en consulta externa que se encuentran en estado ''En Espera'' para un profesional de salud, centro de atención y rango de fechas determinados. Combina los registros de consulta externa (ADCONCOEX) con las citas agendadas (AGASICITA), los datos del paciente (INPACIENT), la entidad aseguradora o pagador (INENTIDAD), el ingreso hospitalario o de atención (ADINGRESO), la especialidad médica (INESPECIA) y la actividad médica programada (AGACTIMED), consolidando en un único resultado toda la información clínica y administrativa necesaria para la sala de espera del consultorio. Incluye datos como identificación y nombre del paciente, número de ingreso, tipo de cita (primera vez, control, posoperatorio), especialidad, entidad, llamados al paciente, edad, tipo de paciente, población especial, acompañantes y la actividad médica asignada, sirviendo como fuente principal para la lista de trabajo del médico en consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con citas de consulta externa programadas y vigentes para un profesional en un centro de atención y rango de fechas, enriqueciendo con datos demográficos, entidad pagadora, ingreso, escalas y poblaciones especiales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención, profesional y rango de fechas deben ser provistos para filtrar las citas; Las citas deben existir en AGASICITA enlazadas con la consulta externa por NUMCONCIT/CODAUTONU; El paciente, entidad e ingreso de la consulta deben existir en sus maestros (INPACIENT, INENTIDAD, ADINGRESO)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas en estado ''1'' (en espera/activa); El egreso reportado siempre es ''1 - En Espera'' y la alerta inicial siempre ''Normal'' con MuestraAlerta=0; Las escalas clínicas (Down, RASS, VAS, Apache, Norton) se inicializan en ''0''/0; este SP no calcula puntajes reales; El campo Riesgo siempre se devuelve como BIT 0; La edad se calcula con la función dbo.Edad sobre la fecha de nacimiento del paciente; Solo cuenta poblaciones especiales con TIPOPOESPERIES=1 (riesgo); Las consultas se hacen con NOLOCK, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta externa; Cita médica; Tipo de cita (Primera vez/Control/PosOperatorio); Especialidad médica; Actividad médica; Paciente; Entidad pagadora; Ingreso/Admisión; Llamados de turno; Escalas clínicas (Down, RASS, VAS, Apache, Norton); Población especial / riesgo; Acompañantes; Zona apartada; Vive solo; Tipo de documento (ASMS); RIAS CUPS; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo citas con CODESTCIT=''1'' (estado de cita activo/en espera) cuyo rango FECHORAIN..FECHORAFI esté dentro de las fechas indicadas, para el centro y profesional dados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si g.CODTIPCIT = ''0'' → Clasifica la cita como ''Primera Vez'' else Si ''1'' => ''Control''; si ''2'' => ''PosOperatorio''; si B.IPTIPODOC IN (6,7) → Marca al paciente como ASMS=1 (presumiblemente menor/extranjero según tipo de documento) else ASMS=0; si Existe registro en ADPOBESPEPAC para el paciente con TIPOPOESPERIES=1 → Reporta el paciente como perteneciente a población especial (POBESPECIAL>0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INESPECIA; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtendidosConsultaExterna';
-- GO
