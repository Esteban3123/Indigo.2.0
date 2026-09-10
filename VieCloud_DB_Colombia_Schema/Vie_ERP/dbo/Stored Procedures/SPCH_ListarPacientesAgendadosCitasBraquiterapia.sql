-- Stored Procedure

CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasBraquiterapia]
(
@centroAtencion char(300),
@UnidadFuncional Char(10),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT  
ORD.ID,A.CODCENATE,A.CODAUTONU AS AUTO, A.TIPTRATAMIENTO,FECHORAIN, FECHORAFI,A.IDEQUIPOTRA AS IDEQUIPO,E.CODEQUIPO as CODIGOEQUIPO,rtrim(E.DESCREQUI) as EQUIPO,S.CODCONCEC AS IDSALA, S.CODIGSALA as CODIGOSALA,S.DESCRIPSAL,S.CODIGSALA +' - ' + S.DESCRIPSAL  AS SALA ,
A.CODACTMED, F.DESACTMED, A.CODESPECI, null AS ID, RTRIM(A.IPCODPACI) AS IPCODPACI,RTRIM(D.IPNOMCOMP) AS IPNOMCOMP,RTRIM(D.IPDIRECCI) AS IPDIRECCI ,RTRIM(D.IPTELEFON) AS TELEFONO ,RTRIM(D.IPTELMOVI) AS CELULAR,CODESTCIT AS ESTADO,
CITAEXTRA, RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO',  Rtrim(C.NOMCENATE) as CentroAtencion,
CASE CODESTCIT WHEN 0 THEN 'Cita asignada a:' WHEN 1 THEN 'Cita cumplida por:' WHEN 2 THEN 'Cita incumplida por:' WHEN 3 THEN 'Cita preasignada a:' END AS 'ESTADO CITA',
rtrim(ltrim(SER.CODSERIPS)) as CODSERIPS, CODESTCIT , A.IDHCRADESQUEMAS,
rtrim(SER.CODSERIPS) + ' - ' + rtrim(SER.DESSERIPS) as Servicio,
rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO) as Profesional,
rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) as Diagnostico,
rtrim(DI.CODDIAGNO) as CodigoDiagnostico,
rtrim(ESP.CODESPECI) + ' - ' + rtrim(ESP.DESESPECI) as Especialidad,
(select top 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 AND IESTADOIN IN ('','P','B')) as NUMINGRES,
(select top 1 CODTIPPAC FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 AND IESTADOIN IN ('','P','B')) as TipoPoblacion,
rtrim(U.UFUDESCRI) as UnidadActual,CONVERT(BIT,0) AS Riesgo ,ORD.FECHAORDEN,[dbo].[Edad](D.IPFECNACI,[Common].[GETDATE]()) as Edad, S.DESCRIPSAL
,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END , ORD.IDHCORDPRON,
[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 AND IESTADOIN IN ('','P','B')),2) AS IconoEscalas
FROM dbo.AGASICITA A 
INNER JOIN dbo.ADCENATEN C with(nolock) ON A.CODCENATE=C.CODCENATE 
INNER JOIN dbo.INPACIENT D with(nolock) ON A.IPCODPACI=D.IPCODPACI 
INNER JOIN dbo.AGACTIMED F with(nolock) ON A.CODACTMED=F.CODACTMED 
INNER JOIN dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA
INNER JOIN dbo.INUNIFUNC  U with(nolock) ON U.UFUCODIGO  = S.UFUCODIGO  
INNER JOIN dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA  
INNER JOIN dbo.SEGusuaru G with(nolock) ON A.CODUSUASI=G.CODUSUARI 
INNER JOIN dbo.INCUPSIPS SER with(nolock) ON A.CODSERIPS=SER.CODSERIPS 
--INNER JOIN dbo.HCRADESQUEMAS ESQ with(nolock) ON ESQ.ID =A.IDHCRADESQUEMAS
INNER JOIN dbo.HCRADORDEN ORD with(nolock) ON ORD.ID  =A.IDHCRADORDEN 
INNER JOIN dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = ORD.CODDIAGNO  
INNER JOIN dbo.INPROFSAL P with(nolock) on P.CODPROSAL = ORD.CODPROSAL 
INNER JOIN dbo.INESPECIA  ESP with(nolock) on ESP.CODESPECI  = ORD.CODESPECI  
WHERE  A.CODCENATE IN (SELECT Value FROM dbo.SplitString(@CentroAtencion)) AND format(A.FECHORAIN,'dd/MM/yyyy')=format(@Fecha,'dd/MM/yyyy') AND CODESTCIT IN(0) AND TIPSOLICITU = 3 AND TIPTRATAMIENTO =4 

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con citas agendadas para sesiones de braquiterapia (tipo de tratamiento de radioterapia) en una fecha específica, filtrando únicamente citas asignadas y pendientes de atención. Integra información de agendamiento, datos personales del paciente (nombre, dirección, teléfono, celular, edad), sala y equipo de tratamiento asignado, actividad médica, servicio CUPS, diagnóstico CIE-10, especialidad, profesional de salud, orden de radiología vinculada, unidad funcional, número de ingreso activo en radioterapia, tipo de población, modalidad de atención (presencial o teleconsulta) y alertas de riesgo e iconos de escalas clínicas del paciente. Se usa para que el equipo de braquiterapia consulte la agenda del día por centro de atención y unidad funcional, conociendo el detalle completo de cada cita y su contexto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con citas de braquiterapia agendadas (asignadas) en una fecha y centros de atención específicos, incluyendo datos clínicos, administrativos e indicadores de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención recibido puede ser una lista delimitada que se separa con dbo.SplitString.; Existe correspondencia referencial entre la cita y catálogos de centro, paciente, actividad, sala, unidad funcional, equipo, usuario, servicio CUPS, orden HC, diagnóstico, profesional y especialidad.; Solo aplican citas con tipo de solicitud = 3 (braquiterapia) y tipo de tratamiento = 4.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan citas en estado asignada (CODESTCIT=0).; El filtro TIPSOLICITU=3 y TIPTRATAMIENTO=4 garantiza que el resultado corresponde exclusivamente a braquiterapia.; El campo Riesgo se devuelve siempre como BIT 0 (constante).; La comparación de fecha se hace formateando ambos lados a ''dd/MM/yyyy'', ignorando la hora.; El ingreso vinculado para población e iconos siempre corresponde a tratamiento especial = 5 y estados de ingreso vacío, ''P'' o ''B''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Braquiterapia; Centro de atención; Sala; Equipo de tratamiento; Unidad funcional; Profesional de salud; Diagnóstico; Especialidad; Servicio CUPS; Ingreso/admisión; Tipo de población; Modalidad (presencial/teleconsulta); Factores de riesgo; Escalas clínicas; Orden de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente citas con CODESTCIT=0 (asignadas), TIPSOLICITU=3 y TIPTRATAMIENTO=4, cuya fecha de inicio coincide con la fecha solicitada y cuyo centro de atención está dentro de la lista recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = 0 → Etiqueta el estado como ''Cita asignada a:''; si CODESTCIT = 1 → Etiqueta el estado como ''Cita cumplida por:''; si CODESTCIT = 2 → Etiqueta el estado como ''Cita incumplida por:''; si CODESTCIT = 3 → Etiqueta el estado como ''Cita preasignada a:''; si MODALIDAD = 0 → Modalidad ''Presencial'' else Si MODALIDAD = 1 → ''Teleconsulta''; en otro caso, cadena vacía; si Existe ADINGRESO del paciente con TRATAESPECIA=5 e IESTADOIN en ('''',''P'',''B'') → Toma el primer NUMINGRES y CODTIPPAC asociado como ingreso vigente de braquiterapia para calcular escalas de riesgo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.Edad; Common.GETDATE; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.INUNIFUNC; dbo.AGEQUIPTRA; dbo.SEGusuaru; dbo.INCUPSIPS; dbo.HCRADORDEN; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasBraquiterapia';
-- GO
