-- Stored Procedure

CREATE PROCEDURE [dbo].[SPCH_ListarPacientesAgendadosCitasRenal]
(
@centroAtencion char(300),
@UnidadFuncional Char(10),
@Fecha as date,
@Usuario as varchar(20)
)
AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT B.DESCRIPSAL,  A.IPCODPACI,  RTRIM(C.IPNOMCOMP) AS IPNOMCOMP, isnull(D.NUMINGRES,'') as NUMINGRES,   
TIPOINGRE = CASE WHEN D.TIPOINGRE = 1 THEN 'AMBULATORIO' WHEN D.TIPOINGRE = 2 THEN 'HOSPITALARIO' END,  
E.DESCREQUI,  E.ID AS IDEQUIP,  CODTIPCIT = CASE WHEN A.CODTIPCIT = 0 THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = 1 THEN 'CONTROL'  WHEN A.CODTIPCIT = 2 THEN 'POS OPERATORIO' END, F.CODACTMED,  F.DESACTMED,  HORINIC = FORMAT(A.FECHORAIN, 'HH:mm:ss'),  A.FECHORAIN, null AS 'Codigo Cama', 'false' AS TrasladoMedicamentos, null as  Consecutivo,  null as UFUCODIGO, A.RELCITAINGRE as 'CitaConIngreso'   
, D.CODTIPPAC as TipoPoblacion, rtrim(ISNULL(U.UFUDESCRI,'Ambulatorio'))  as UnidadActual , rtrim(ISNULL(O.DESCCAMAS, 'Ambulatorio')) as Cama, dbo.TipoAislamiento(O.CODAISLAM) AS Aislamiento
 , D.ESCADOWNT ,  D.ESCARASS, D.ESCNORPAC, D.ESCVASPAC, D.ESCAPAPAC, dbo.PuntajeEscalaDownTon(D.NUMINGRES, D.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(D.NUMINGRES, D.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(D.NUMINGRES, D.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(D.NUMINGRES, D.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(D.NUMINGRES, D.IPCODPACI) as PUNTAJEAPACHE
 , CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
, C.ZONAPARTADA,
L.RIESGOAGRE,
(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
D.VIVESOLO,
(SELECT count(*) FROM ADACOMPAN with(nolock) WHERE NUMINGRES = D.NUMINGRES) AS ACOMPANANTES,
CONVERT(BIT,0) AS Riesgo,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END 
 FROM AGASICITA A with(nolock) 
 INNER JOIN AGENSALAC B with(nolock) ON A.IDSALA = B.CODCONCEC AND B.TIPOTRATA = 3  
 INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI  
 INNER JOIN AGEQUIPTRA E with(nolock) ON A.IDEQUIPOTRA = E.ID  
 INNER JOIN AGACTIMED F with(nolock) ON A.CODACTMED = F.CODACTMED  
INNER JOIN ADACTIVID L with(nolock) ON C.CODACTIVI = L.codactivi  
LEFT  JOIN  ADRELFICHARE P with(nolock) ON  A.IPCODPACI = P.IPCODPACI AND P.ESTADO = 1 
LEFT  JOIN ADINGRESO D with(nolock) ON A.IPCODPACI = D.IPCODPACI  AND P.NUMINGRES = D.NUMINGRES 
LEFT  JOIN CHREGESTA CH with(nolock) on CH.NUMINGRES = A.NUMINGRES 
LEFT  JOIN CHCAMASHO O with(nolock) ON  O.CODICAMAS =CH.CODICAMAS 
LEFT  JOIN INUNIFUNC U with(nolock) ON  U.UFUCODIGO =D.UFUACTPAC 
WHERE A.CODESTCIT = 0 
AND A.TIPSOLICITU = 3 
AND (A.FECHCANCELA IS NUll OR A.FECHCANCELA = '') 
AND A.IDSALA IN (SELECT  B.CODCONCEC FROM INAUTORIU A with(nolock) INNER JOIN AGENSALAC B with(nolock) ON A.PKVALORCO = B.CODIGSALA WHERE A.CODUSUARI = @Usuario AND A.PKTIPOCON = 8 AND B.ESTADO = 1
UNION  select  B.CODCONCEC from INAUTORIG  A with(nolock) INNER JOIN AGENSALAC B with(nolock) ON A.PKVALORCO = B.CODIGSALA WHERE A.CODGRUPOU  in (select CODGRUPOU  from SEGusuaru with(nolock) where CODUSUARI = @Usuario) AND A.PKTIPOCON = 8 AND B.ESTADO = 1 ) 
AND A.CODCENATE = @centroAtencion AND Convert(varchar(20),A.FECHORAIN,103) = @Fecha
AND A.RELCITAINGRE = 0 --no hospitalizados. no relaciona ingreso en cita. el ingreso sale de la ficha renal.
UNION ALL
SELECT DISTINCT B.DESCRIPSAL,  A.IPCODPACI,  RTRIM(C.IPNOMCOMP) AS IPNOMCOMP,  isnull(D.NUMINGRES,'') as NUMINGRES,   
TIPOINGRE = CASE WHEN D.TIPOINGRE = 1 THEN 'AMBULATORIO' WHEN D.TIPOINGRE = 2 THEN 'HOSPITALARIO' END,  
E.DESCREQUI,  E.ID AS IDEQUIP,  CODTIPCIT = CASE WHEN A.CODTIPCIT = 0 THEN 'PRIMERA VEZ' WHEN A.CODTIPCIT = 1 THEN 'CONTROL'  WHEN A.CODTIPCIT = 2 THEN 'POS OPERATORIO' END, F.CODACTMED,  F.DESACTMED,  HORINIC = FORMAT(A.FECHORAIN, 'HH:mm:ss'),  A.FECHORAIN,  O.CODICAMAS AS 'Codigo Cama',isnull(O.CAMTRAMED,'false') AS TrasladoMedicamentos,O.CODCONCEC AS Consecutivo,  O.UFUCODIGO, A.RELCITAINGRE as 'CitaConIngreso'   
, D.CODTIPPAC as TipoPoblacion,  ISNULL(U.UFUDESCRI,'Ambulatorio')  as UnidadActual , ISNULL(O.DESCCAMAS, 'Ambulatorio') as Cama, dbo.TipoAislamiento(O.CODAISLAM) AS Aislamiento
, D.ESCADOWNT ,  D.ESCARASS, D.ESCNORPAC, D.ESCVASPAC, D.ESCAPAPAC, dbo.PuntajeEscalaDownTon(D.NUMINGRES, D.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(D.NUMINGRES, D.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(D.NUMINGRES, D.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(D.NUMINGRES, D.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(D.NUMINGRES, D.IPCODPACI) as PUNTAJEAPACHE
 , CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
, C.ZONAPARTADA,
L.RIESGOAGRE,
(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
D.VIVESOLO,
(SELECT count(*) FROM ADACOMPAN with(nolock) WHERE NUMINGRES = D.NUMINGRES) AS ACOMPANANTES,
CONVERT(BIT,0) AS Riesgo,Modalidad = CASE WHEN A.MODALIDAD = 0 THEN 'Presencial' WHEN A.MODALIDAD = 1 THEN 'Teleconsulta' ELSE '' END 
 FROM AGASICITA A  with(nolock)
INNER JOIN AGENSALAC B with(nolock) ON A.IDSALA = B.CODCONCEC AND B.TIPOTRATA = 3  
 INNER JOIN INPACIENT C with(nolock) ON A.IPCODPACI = C.IPCODPACI  
INNER JOIN AGEQUIPTRA E with(nolock) ON A.IDEQUIPOTRA = E.ID  
INNER JOIN AGACTIMED F with(nolock) ON A.CODACTMED = F.CODACTMED  
INNER JOIN ADACTIVID L with(nolock) ON C.CODACTIVI = L.codactivi  
LEFT JOIN  ADINGRESO D with(nolock) ON A.IPCODPACI = D.IPCODPACI  AND A.NUMINGRES = D.NUMINGRES 
INNER JOIN CHREGESTA CH with(nolock) on CH.NUMINGRES = A.NUMINGRES AND CH.REGESTADO = 1 
INNER JOIN CHCAMASHO O with(nolock) ON  O.CODICAMAS =CH.CODICAMAS  AND O.ESTADCAMA = '2'
LEFT  JOIN INUNIFUNC U with(nolock) ON  U.UFUCODIGO =D.UFUACTPAC 
WHERE A.CODESTCIT = 0 
AND A.TIPSOLICITU = 3 
AND (A.FECHCANCELA IS NUll OR A.FECHCANCELA = '') 
AND A.IDSALA IN (SELECT  B.CODCONCEC FROM INAUTORIU A with(nolock) INNER JOIN AGENSALAC B with(nolock) ON A.PKVALORCO = B.CODIGSALA WHERE A.CODUSUARI = @Usuario AND A.PKTIPOCON = 8 AND B.ESTADO = 1
UNION  select  B.CODCONCEC from INAUTORIG  A with(nolock) INNER JOIN AGENSALAC B with(nolock) ON A.PKVALORCO = B.CODIGSALA WHERE A.CODGRUPOU  in (select CODGRUPOU  from SEGusuaru with(nolock) where CODUSUARI = @Usuario) AND A.PKTIPOCON = 8 AND B.ESTADO = 1 ) 
AND A.CODCENATE = @centroAtencion AND Convert(varchar(20),A.FECHORAIN,103) = @Fecha 
AND A.RELCITAINGRE = 1  --hopitalizado y relaciona el ingreso en la cita
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los pacientes agendados para sesiones de diálisis o atención renal en un centro de atención y fecha específicos, combinando datos de citas (AGASICITA), salas de tratamiento tipo renal (AGENSALAC con tipo de tratamiento 3), información del paciente (INPACIENT), equipos de traslado (AGEQUIPTRA) y actividad médica programada (AGACTIMED). Diferencia dos grupos mediante UNION ALL: pacientes ambulatorios cuyo ingreso se obtiene desde la ficha renal (sin relación directa de cita a ingreso), y pacientes hospitalizados con cama asignada activa obtenida desde el registro de estancias (CHREGESTA) y el maestro de camas (CHCAMASHO). Para cada paciente devuelve sala, cédula e identificación, nombre completo, número de ingreso, tipo de ingreso (ambulatorio u hospitalario), equipo de traslado, tipo de cita (primera vez, control o posoperatorio), actividad médica, hora de inicio, cama actual, unidad funcional, escalas clínicas de riesgo (Downton, RASS, Norton, VAS, Apache), indicador de población especial, acompañantes, modalidad de atención (presencial o teleconsulta) y riesgo agregado; filtrando únicamente las citas activas no canceladas autorizadas para el usuario que realiza la consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes con citas renales agendadas y vigentes en un centro y fecha dados, separando casos ambulatorios (ingreso desde ficha renal) y hospitalarios (con cama activa), restringido a salas autorizadas para el usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir y, si aplica, pertenecer a un grupo con autorización tipo PKTIPOCON=8 sobre la sala; Las salas implicadas deben estar activas (ESTADO=1) y ser de tipo tratamiento renal (TIPOTRATA=3); La fecha se compara en formato dd/mm/yyyy (estilo 103); el parámetro debe ser interpretable así; Deben existir catálogos de equipo de trabajo, actividad médica y actividad del paciente para hacer match en los INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas con CODESTCIT=0 (activas/no atendidas) y TIPSOLICITU=3 (solicitud renal); Se excluyen citas canceladas: FECHCANCELA debe ser NULL o vacío; El usuario debe estar autorizado sobre la sala (IDSALA) ya sea por permiso individual en INAUTORIU o por pertenencia a grupo en INAUTORIG, con PKTIPOCON=8 y sala con ESTADO=1; Solo se incluyen citas del centro de atención y fecha solicitados (comparación de fecha en formato 103); Solo se consideran salas con TIPOTRATA=3 (renal); Para citas hospitalarias (RELCITAINGRE=1) la cama asociada debe estar en estado ''2'' y el registro de estancia activo (REGESTADO=1); Para citas no hospitalarias el ingreso proviene de la ficha renal activa (ADRELFICHARE.ESTADO=1); El procedimiento solo lee datos; no modifica tablas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Agenda renal / diálisis; Sala de tratamiento (TIPOTRATA=3); Ingreso hospitalario vs ambulatorio; Ficha renal; Cama hospitalaria y traslado de medicamentos; Aislamiento; Escalas clínicas (Downton, Rass, Norton, Vas, Apache); Población especial / riesgo; Zona apartada; Riesgo de agresión; Acompañantes / paciente vive solo; Autorización de usuario por sala (individual y por grupo); Tipo de cita: primera vez, control, pos operatorio; Modalidad presencial / teleconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el conjunto unido (UNION ALL) de citas renales activas no canceladas del centro y fecha indicados, autorizadas para el usuario, con datos clínicos, escalas y cama/aislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.RELCITAINGRE = 0 (cita no relaciona ingreso) → Trae al paciente como ambulatorio renal; el ingreso se obtiene de la ficha renal (ADRELFICHARE) y la cama/unidad puede quedar como ''Ambulatorio'' else Cuando RELCITAINGRE=1, exige ingreso y registro de cama hospitalaria activos para incluirlo; si A.RELCITAINGRE = 1 (cita hospitalaria con ingreso) → Requiere CHREGESTA.REGESTADO=1 y CHCAMASHO.ESTADCAMA=''2'' (cama ocupada) para incluir al paciente con su cama y unidad funcional reales; si B.TIPOTRATA = 3 en AGENSALAC → Solo se consideran salas de tratamiento tipo renal/diálisis; si CODTIPCIT IN (0,1,2) → Se traduce a ''PRIMERA VEZ'', ''CONTROL'' o ''POS OPERATORIO'' respectivamente; si TIPOINGRE = 1 ó 2 → Se etiqueta como ''AMBULATORIO'' u ''HOSPITALARIO''; si MODALIDAD = 0 ó 1 → Se marca la cita como ''Presencial'' o ''Teleconsulta'' else Modalidad queda en cadena vacía; si C.IPTIPODOC IN (6,7) → Marca al paciente como ASMS=1 (menor sin documento de adulto / tipo doc especial) else ASMS=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.AGENSALAC; dbo.INPACIENT; dbo.AGEQUIPTRA; dbo.AGACTIMED; dbo.ADACTIVID; dbo.ADRELFICHARE; dbo.ADINGRESO; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.INAUTORIU; dbo.INAUTORIG; dbo.SEGusuaru; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesAgendadosCitasRenal';
-- GO
