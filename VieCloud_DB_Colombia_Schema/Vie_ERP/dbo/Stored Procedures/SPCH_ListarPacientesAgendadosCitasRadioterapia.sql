

CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasRadioterapia]
(
@centroAtencion varchar(max),
@UnidadFuncional varchar(max),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT ORD.ID, A.CODCENATE, A.IPCODPACI, Rtrim(C.NOMCENATE) as CentroAtencion,[dbo].[EDAD] (D.IPFECNACI,[Common].[GETDATE]()) As 'Edad',
		A.CODAUTONU AS AUTO, A.TIPTRATAMIENTO,FECHORAIN, FECHORAFI,A.IDEQUIPOTRA AS IDEQUIPO,E.CODEQUIPO as CODIGOEQUIPO,rtrim(E.DESCREQUI) as EQUIPO,S.CODCONCEC AS IDSALA, S.CODIGSALA as CODIGOSALA,S.DESCRIPSAL,S.CODIGSALA +' - ' + S.DESCRIPSAL  AS SALA ,
		A.CODACTMED, F.DESACTMED, A.CODESPECI, null AS ID, RTRIM(A.IPCODPACI) AS IPCODPACI,RTRIM(D.IPNOMCOMP) AS IPNOMCOMP,RTRIM(D.IPDIRECCI) AS IPDIRECCI ,RTRIM(D.IPTELEFON) AS TELEFONO ,RTRIM(D.IPTELMOVI) AS CELULAR,CODESTCIT AS ESTADO,
		CITAEXTRA, RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO',  
		CASE CODESTCIT WHEN 0 THEN 'Cita asignada a:' WHEN 1 THEN 'Cita cumplida por:' WHEN 2 THEN 'Cita incumplida por:' WHEN 3 THEN 'Cita preasignada a:' END AS 'ESTADO CITA',
		rtrim(ltrim(SER.CODSERIPS)) as CODSERIPS, CODESTCIT , A.IDHCRADESQUEMAS,
		rtrim(SER.CODSERIPS) + ' - ' + rtrim(SER.DESSERIPS) as Servicio,
		rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO) as Profesional,
		rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) as Diagnostico,
		rtrim(DI.CODDIAGNO) as CodigoDiagnostico,
		rtrim(ESP.CODESPECI) + ' - ' + rtrim(ESP.DESESPECI) as Especialidad,
		rtrim(U.UFUDESCRI) as UnidadActual,CONVERT(BIT,0) AS Riesgo 
		,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END ,
		(select top 1 NUMINGRES FROM dbo.ADINGRESO  with(nolock) WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 AND IESTADOIN IN ('','P','B')) as NUMINGRES,
		(select top 1 CODTIPPAC FROM dbo.ADINGRESO  with(nolock) WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 AND IESTADOIN IN ('','P','B')) as TipoPoblacion,
		Concat('Sesión: ', (select count(*) from dbo.AGASICITA T with(nolock) where T.IDHCRADESQUEMAS is not null AND t.IPCODPACI = a.IPCODPACI and T.IDHCRADESQUEMAS = A.IDHCRADESQUEMAS AND T.FECHORAIN <=A.FECHORAIN AND T.CODESTCIT =0), '/',  ESQ.NUMSESION ) as 'Sesion'
		,[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 CODTIPPAC FROM dbo.ADINGRESO  with(nolock) WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 AND IESTADOIN IN ('','P','B')),2) AS IconoEscalas
FROM dbo.AGASICITA A  with(nolock)
		INNER JOIN dbo.ADCENATEN C with(nolock) ON A.CODCENATE=C.CODCENATE 
		INNER JOIN dbo.INPACIENT D with(nolock) ON A.IPCODPACI=D.IPCODPACI 
		INNER JOIN dbo.AGACTIMED F with(nolock) ON A.CODACTMED=F.CODACTMED 
		INNER JOIN dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA
		INNER JOIN dbo.INUNIFUNC  U with(nolock) ON U.UFUCODIGO  = S.UFUCODIGO  
		INNER JOIN dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA  
		INNER JOIN dbo.SEGusuaru G with(nolock) ON A.CODUSUASI=G.CODUSUARI 
		INNER JOIN dbo.INCUPSIPS SER with(nolock) ON A.CODSERIPS=SER.CODSERIPS 
		INNER JOIN dbo.HCRADESQUEMAS ESQ with(nolock) ON ESQ.ID =A.IDHCRADESQUEMAS
		INNER JOIN dbo.HCRADORDEN ORD with(nolock) ON ORD.ID  =ESQ.IDHCRADORDEN 
		INNER JOIN dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = ORD.CODDIAGNO  
		INNER JOIN dbo.INPROFSAL P with(nolock) on P.CODPROSAL = ORD.CODPROSAL 
		INNER JOIN dbo.INESPECIA  ESP with(nolock) on ESP.CODESPECI  = ORD.CODESPECI  
WHERE A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) AND format(A.FECHORAIN,'dd/MM/yyyy')=format(@Fecha,'dd/MM/yyyy') AND CODESTCIT =0 AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =2 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los pacientes con citas de radioterapia agendadas (estado ''asignada'') para una fecha, centro de atención y unidad funcional específicos. Combina información de agendamiento de citas, datos demográficos del paciente (nombre, dirección, teléfono, celular, edad), equipo de radioterapia asignado, sala de atención, actividad médica, servicio CUPS, diagnóstico CIE-10, especialidad, profesional de salud y esquema de radioterapia (dosis, sesiones, técnica). También calcula el número de sesión actual del paciente dentro de su esquema de radioterapia, recupera el número de ingreso activo en hospitalización oncológica, el tipo de población, la modalidad de atención (presencial o teleconsulta), e incorpora íconos de factores de riesgo y escalas de valoración. Se usa en el módulo de radioterapia para la planilla diaria de pacientes programados en sesiones de tratamiento radioterápico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de radioterapia agendadas (no cumplidas) en una fecha y centros de atención dados, con datos del paciente, profesional, esquema, sesión actual y alertas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben existir en AGASICITA con CODESTCIT=0 (asignada), TIPSOLICITU=3 y TIPTRATAMIENTO=2 (radioterapia); La cita debe estar asociada a un esquema de radioterapia (HCRADESQUEMAS) y a una orden (HCRADORDEN); Deben existir las relaciones maestras: centro de atención, paciente, actividad médica, sala, unidad funcional, equipo de tratamiento, usuario asignador, servicio CUPS, diagnóstico, profesional y especialidad; El parámetro de centros de atención se entrega como cadena separable mediante dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran citas de radioterapia (TIPTRATAMIENTO=2) con tipo de solicitud 3 y estado asignada (CODESTCIT=0); El conteo de sesión solo incluye citas asignadas (CODESTCIT=0) del mismo esquema con fecha menor o igual a la cita actual; El ingreso considerado para tipo de población es exclusivamente el de tratamiento especial 4 (radioterapia) en estados '''', ''P'' o ''B''; El campo Riesgo siempre se entrega como BIT 0 (placeholder); El filtro por fecha compara cadenas dd/MM/yyyy, ignorando la hora de FECHORAIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Radioterapia; Esquema de radioterapia; Sesión de tratamiento; Orden médica; Paciente; Centro de atención; Unidad funcional; Sala de atención; Equipo de tratamiento; Profesional de salud; Especialidad; Diagnóstico; Servicio CUPS/IPS; Modalidad presencial/teleconsulta; Ingreso/admisión; Tipo de población; Factor de riesgo / escalas; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando CODESTCIT=0 AND TIPSOLICITU=3 AND TIPTRATAMIENTO=2 y la fecha de inicio coincide con @Fecha y el centro está en la lista, retorna la cita con datos consolidados (paciente, equipo, sala, esquema, sesión y alertas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = 0/1/2/3 → Etiqueta la cita como ''Cita asignada a:'', ''Cita cumplida por:'', ''Cita incumplida por:'' o ''Cita preasignada a:'' respectivamente; si MODALIDAD = 0 / 1 / otro → Clasifica la cita como ''Presencial'', ''Teleconsulta'' o cadena vacía; si Existe ADINGRESO del paciente con TRATAESPECIA=4 e IESTADOIN en ('''',''P'',''B'') → Toma el primer NUMINGRES y CODTIPPAC asociados como ingreso vigente para radioterapia y se usa CODTIPPAC para calcular IconoEscalas; si Existen citas previas del mismo paciente y mismo esquema (IDHCRADESQUEMAS) con FECHORAIN <= la actual y CODESTCIT=0 → Calcula el número de sesión actual sobre el total NUMSESION del esquema', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.EDAD; Common.GETDATE; dbo.RiskFactorAlert; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.INUNIFUNC; dbo.AGEQUIPTRA; dbo.SEGusuaru; dbo.INCUPSIPS; dbo.HCRADESQUEMAS; dbo.HCRADORDEN; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRadioterapia';
-- GO
