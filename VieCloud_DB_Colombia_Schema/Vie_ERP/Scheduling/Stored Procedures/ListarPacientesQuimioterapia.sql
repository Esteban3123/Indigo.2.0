-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 22/03/2022
-- Description: Sp para listar los pacientes en Tratamientos Especiales
-- =============================================
CREATE PROCEDURE [Scheduling].[ListarPacientesQuimioterapia]
(
 @CentroAtencion Char(10),
 @FechaInicio datetime,
 @FechaFinal datetime
)
AS
BEGIN
    SET NOCOUNT ON
SELECT   
  A.CODAUTONU AS AUTO, 
  A.TIPTRATAMIENTO, 
  A.FECHORAIN, 
  A.FECHORAFI, 
  A.IDEQUIPOTRA AS IDEQUIPO, 
  E.CODEQUIPO as CODIGOEQUIPO, 
  E.DESCREQUI as EQUIPO, 
  S.CODCONCEC AS IDSALA, 
  S.CODIGSALA as CODIGOSALA, 
  S.CODIGSALA + ' - ' + S.DESCRIPSAL AS SALA, 
  A.CODACTMED, 
  F.DESACTMED, 
  A.CODESPECI, 
  null AS ID, 
  RTRIM(A.IPCODPACI) AS IPCODPACI, 
  RTRIM(D.IPNOMCOMP) AS IPNOMCOMP, 
  RTRIM(D.IPDIRECCI) AS IPDIRECCI, 
  RTRIM(D.IPTELEFON) AS TELEFONO, 
  RTRIM(D.IPTELMOVI) AS CELULAR, 
  CODESTCIT AS ESTADO, 
  CITAEXTRA, 
  CAST(
    CONVERT(DATE, FECHORAIN, 108) AS varchar
  ) AS FECHAINICIAL, 
  'Paciente:' + RTRIM(A.IPCODPACI) + ' - ' + RTRIM(D.IPNOMCOMP) + char(10) + 'Telefono: ' + RTRIM(D.IPTELEFON) + char(10)+ 'Celular: ' + RTRIM(D.IPTELMOVI) + char(10)+ 'Profesional: ' + rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO) + char(10)+ 'Diagnóstico: ' + rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) + char(10)+ 'Esquema: ' + rtrim(OS.Code) + ' - ' + rtrim(OS.Description) + char(10)+ 'Ciclo: ' + rtrim(CICLODIA.CICLO) + char(10)+ 'Día: ' + rtrim(CICLODIA.DIA) AS CONTENIDO, 
  RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO', 
  CASE WHEN A.CODESTCIT = '0' THEN 'Cita asignada a:' WHEN A.CODESTCIT = '1' THEN 'Cita cumplida por:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NULL THEN 'Cita incumplida por:' WHEN A.CODESTCIT = '3' THEN 'Cita preasignada a:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NOT NULL THEN 'Cita reprogramada de:' END AS 'ESTADO CITA', 
  rtrim(OS.Code) + ' - ' + rtrim(OS.Description) as 'Esquema', 
  CODESTCIT, 
  A.IDHCRADESQUEMAS, 
  A.IDHCORDCICLOSD, 
  CICLODIA.CICLO as 'CICLO', 
  CICLODIA.DIA as 'DIA', 
  CICLO.ID as IdCiclo, 
  rtrim(P.CODPROSAL) + ' - ' + rtrim(P.NOMMEDICO) as Profesional, 
  rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) as Diagnostico, 
  CASE WHEN A.CODMOTIVOREPRO IS NULL THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT <> '2' THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT = '2' THEN '5' END As EstadoIcono,
  (SELECT SUM(DATEDIFF(MINUTE,SUB.FECHORAIN, SUB.FECHORAFI )) From (SELECT AG.FECHORAIN, MAX(AG.FECHORAFI) AS FECHORAFI	from AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI)	Where A.CODAUTONU = AG.CODAUTONU Group by AG.FECHORAIN) AS SUB) as MinutosXDiaCitasAsignados,
  (SELECT DISTINCT AGD.ID FROM AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI) Where A.CODAUTONU = AG.CODAUTONU ) AS IDDISPO
  FROM 
  dbo.AGASICITA A with(nolock) 
  INNER JOIN dbo.ADCENATEN C with(nolock) ON A.CODCENATE = C.CODCENATE 
  INNER JOIN dbo.INPACIENT D with(nolock) ON A.IPCODPACI = D.IPCODPACI 
  INNER JOIN dbo.AGACTIMED F with(nolock) ON A.CODACTMED = F.CODACTMED 
  INNER JOIN dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA 
  INNER JOIN dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA 
  INNER JOIN dbo.SEGusuaru G with(nolock) ON A.CODUSUASI = G.CODUSUARI 
  INNER JOIN ehr.HCORDCICLOSD CICLODIA with(nolock) on A.IDHCORDCICLOSD = CICLODIA.ID 
  INNER JOIN ehr.HCORDCICLOS CICLO with(nolock) on CICLO.ID = CICLODIA.IDHCORDCICLOS 
  INNER JOIN ehr.HCORDQUIMIO O with(nolock) on O.ID = CICLO.IDHCORDQUIMIO 
  INNER JOIN ehr.Schemes OS with(nolock) on O.SchemesId = OS.Id 
  INNER JOIN dbo.INPROFSAL P with(nolock) on P.CODPROSAL = O.CODPROSAL 
  INNER JOIN dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = O.CODDIAGNO 
WHERE 
  A.CODCENATE = @CentroAtencion
  AND FECHORAIN >= @FechaInicio
  AND FECHORAFI <= @FechaFinal 
  AND CODESTCIT IN(0, 1, 2, 3) 
  AND TIPSOLICITU = 3 
  AND TIPTRATAMIENTO = 1

UNION ALL

SELECT 
A.CODAUTONU as AUTO,
A.TIPTRATAMIENTO,
A.FECHORAIN,
A.FECHORAFI,
A.IDEQUIPOTRA as IDEQUIPO,
E.CODEQUIPO as CODIGOEQUIPO, 
E.DESCREQUI as EQUIPO, 
S.CODCONCEC AS IDSALA, 
S.CODIGSALA as CODIGOSALA,
S.CODIGSALA + ' - ' + S.DESCRIPSAL AS SALA, 
A.CODACTMED, 
F.DESACTMED, 
A.CODESPECI,
null AS ID,
RTRIM(A.IPCODPACI) AS IPCODPACI, 
RTRIM(D.IPNOMCOMP) AS IPNOMCOMP, 
RTRIM(D.IPDIRECCI) AS IPDIRECCI, 
RTRIM(D.IPTELEFON) AS TELEFONO, 
RTRIM(D.IPTELMOVI) AS CELULAR, 
CODESTCIT AS ESTADO, 
CITAEXTRA,
CAST(CONVERT(DATE, FECHORAIN, 108) AS varchar) AS FECHAINICIAL,
CONCAT('Paciente:', RTRIM(A.IPCODPACI), ' - ', RTRIM(D.IPNOMCOMP), char(10), 'Telefono: ', RTRIM(D.IPTELEFON), ' ', 'Celular: ', RTRIM(D.IPTELMOVI), char(10), 'Solicitud de Componente Sanguíneo: ',char(10),BLOOD.BloodComponent, ' - ', BLOOD.TipoSolicitud, CHAR(10),BLODCUPS.BloodComponentCUPS) AS CONTENIDO, 
RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO',	
  CASE WHEN A.CODESTCIT = '0' THEN 'Cita asignada a:' WHEN A.CODESTCIT = '1' THEN 'Cita cumplida por:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NULL THEN 'Cita incumplida por:' WHEN A.CODESTCIT = '3' THEN 'Cita preasignada a:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NOT NULL THEN 'Cita reprogramada de:' END AS 'ESTADO CITA',
  BLOOD.BloodComponent + ' - ' + BLOOD.TipoSolicitud as 'Esquema',
  CODESTCIT, 
  A.IDHCRADESQUEMAS, 
  A.IDHCORDCICLOSD,
  0 CICLO, 
  0 DIA, 
  '' IdCiclo, 
  BLODCUPS.BloodComponentCUPS as Profesional, --- Se realiza la logica solo para aprovechar las columnas con información necesaria para las citas de hemocomponentes
  isnull(rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO), 'No especificado') as Diagnostico,
  CASE WHEN A.CODMOTIVOREPRO IS NULL THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT <> '2' THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT = '2' THEN '5' END As EstadoIcono,
  (SELECT SUM(DATEDIFF(MINUTE,SUB.FECHORAIN, SUB.FECHORAFI )) From (SELECT AG.FECHORAIN, MAX(AG.FECHORAFI) AS FECHORAFI	from AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI)	Where A.CODAUTONU = AG.CODAUTONU Group by AG.FECHORAIN) AS SUB) as MinutosXDiaCitasAsignados,
  (SELECT DISTINCT AGD.ID FROM AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI) Where A.CODAUTONU = AG.CODAUTONU ) AS IDDISPO
FROM
dbo.AGASICITA A with(nolock) 
INNER JOIN dbo.ADCENATEN C with(nolock) ON A.CODCENATE = C.CODCENATE 
INNER JOIN dbo.INPACIENT D with(nolock) ON A.IPCODPACI = D.IPCODPACI 
INNER JOIN dbo.AGACTIMED F with(nolock) ON A.CODACTMED = F.CODACTMED 
INNER JOIN dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA 
INNER JOIN dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA 
INNER JOIN dbo.SEGusuaru G with(nolock) ON A.CODUSUASI = G.CODUSUARI 
LEFT JOIN dbo.INDIAGNOS DI WITH(NOLOCK) ON A.CODDIAGNO = DI.CODDIAGNO
OUTER APPLY(
select TOP (1) x.CODCOMSAM + ' - ' + x.DESCOMSAM as 'BloodComponent', HEMO.id as 'Identificador',CASE Hemo.RequestType WHEN 1 THEN 'Solicitud de transfusión' WHEN 2 THEN 'Reserva y transfusión' END AS 'TipoSolicitud' from [Scheduling].AppointmentHemocomponents AS HEMO INNER JOIN dbo.HCCOMSAN X ON HEMO.IdBloodComponent = X.ID WHERE HEMO.idAGASICITA = A.CODAUTONU
)BLOOD
OUTER APPLY (
select STUFF((SELECT TRIM(HEMO.CODSERIPS) + ' - ' + CUP.DESSERIPS + CHAR(10)from [Scheduling].AppointmentHemocomponentsCUPS AS HEMO INNER JOIN INCUPSIPS CUP ON HEMO.CODSERIPS = CUP.CODSERIPS WHERE HEMO.IdAppointmentHemocomponents = BLOOD.Identificador FOR XML PATH('')),1,0,'') as 'BloodComponentCUPS'
)BLODCUPS
WHERE
  A.CODCENATE = @CentroAtencion
  AND FECHORAIN >= @FechaInicio
  AND FECHORAFI <= @FechaFinal 
  AND CODESTCIT IN(0, 1, 2, 3) 
  AND TIPSOLICITU = 3 
  AND TIPTRATAMIENTO = 5
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes agendados para tratamientos especiales en un centro de atención y rango de fechas dado, cubriendo dos tipos principales: sesiones de quimioterapia y solicitudes de hemocomponentes (componentes sanguíneos). Para las citas de quimioterapia integra el agendamiento (AGASICITA) con la historia clínica oncológica (órdenes de quimioterapia, ciclos y días de ciclo), el esquema terapéutico prescrito, el diagnóstico CIE-10 y el profesional tratante. Para las citas de hemocomponentes recupera la solicitud de componente sanguíneo y sus servicios CUPS asociados. En ambos casos enriquece la información con datos del paciente (nombre, dirección, teléfono, celular), sala o consultorio, equipo o recurso físico asignado, actividad médica, usuario que agendó y estado de la cita (asignada, cumplida, incumplida, preasignada, reprogramada), permitiendo visualizar en el módulo de agendamiento el censo diario de pacientes en tratamientos oncológicos y transfusionales.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListarPacientesQuimioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListarPacientesQuimioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de tratamientos especiales (quimioterapia y transfusión de hemocomponentes) de un centro de atención dentro de un rango de fechas, con datos del paciente, esquema/ciclo y ocupación de equipos.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las citas deben pertenecer al centro de atención indicado; Las citas deben estar dentro del rango de fechas (FECHORAIN >= inicio y FECHORAFI <= fin); Solo se consideran citas con CODESTCIT en (0,1,2,3): asignada, cumplida, incumplida/reprogramada o preasignada; Solo citas con TIPSOLICITU = 3 (tratamientos especiales); Para quimioterapia debe existir relación con esquema, ciclo y día (HCORDCICLOSD, HCORDCICLOS, HCORDQUIMIO, Schemes); Para hemocomponentes debe existir registro en Scheduling.AppointmentHemocomponents asociado a la cita', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas de tratamientos especiales (TIPSOLICITU=3); Solo se incluyen tipos de tratamiento 1 (quimioterapia) y 5 (hemocomponentes); Las citas canceladas u otros estados fuera de (0,1,2,3) nunca se incluyen; Una cita reprogramada (CODESTCIT=2 con motivo de reprogramación) se diferencia con EstadoIcono=5; El cálculo de MinutosXDiaCitasAsignados se realiza solo sobre disponibilidades de equipo que cubren el horario de la cita (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI); Para quimioterapia el diagnóstico es obligatorio (INNER JOIN); para hemocomponentes es opcional (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Quimioterapia; Esquema de quimioterapia; Ciclo y día de tratamiento; Hemocomponentes; Transfusión sanguínea; Reserva de transfusión; Centro de atención; Sala de atención; Equipo de tratamiento; Disponibilidad de equipo; Profesional de salud; Diagnóstico; Reprogramación de cita; CUPS', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando TIPTRATAMIENTO=1 y TIPSOLICITU=3 → retorna fila por cita de quimioterapia con esquema, ciclo y día; [RETURN_RESULT] resultset: Cuando TIPTRATAMIENTO=5 y TIPSOLICITU=3 → retorna fila por cita de transfusión de hemocomponentes con BloodComponent y CUPS asociados', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPTRATAMIENTO = 1 → Trae datos del esquema de quimioterapia (Schemes), ciclo, día, profesional y diagnóstico desde HCORDQUIMIO; si TIPTRATAMIENTO = 5 → Trae datos de hemocomponente vía OUTER APPLY a AppointmentHemocomponents y CUPS asociados; ciclo/día = 0; si CODESTCIT=''0'' → Etiqueta ''Cita asignada a:''; si CODESTCIT=''1'' → Etiqueta ''Cita cumplida por:''; si CODESTCIT=''2'' y CODMOTIVOREPRO IS NULL → Etiqueta ''Cita incumplida por:''; si CODESTCIT=''2'' y CODMOTIVOREPRO IS NOT NULL → Etiqueta ''Cita reprogramada de:'' y EstadoIcono=''5''; si CODESTCIT=''3'' → Etiqueta ''Cita preasignada a:''; si RequestType=1 (hemocomponente) → TipoSolicitud=''Solicitud de transfusión''; si RequestType=2 (hemocomponente) → TipoSolicitud=''Reserva y transfusión''; si En hemocomponentes el diagnóstico es NULL → Se muestra ''No especificado''', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.SEGusuaru; ehr.HCORDCICLOSD; ehr.HCORDCICLOS; ehr.HCORDQUIMIO; ehr.Schemes; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.AGDISPONEQUIPO; Scheduling.AppointmentHemocomponents; dbo.HCCOMSAN; Scheduling.AppointmentHemocomponentsCUPS; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListarPacientesQuimioterapia';
-- GO
