

CREATE PROCEDURE [Scheduling].[ListRenalReplacementTherapyPatients]
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
  'Paciente: ' +  RTRIM(D.IPNOMCOMP) + char(10) + 'Identificación: ' + RTRIM(A.IPCODPACI) + char(10) + 'Servicio: ' + Rtrim(O.CODSERIPS) + ' - ' + rtrim(I.DESSERIPS) + char(10)+ 'Sesión: ' + RTRIM(CAST(1 + ( SELECT COUNT(*) FROM dbo.AGASICITA Z WITH(NOLOCK) WHERE Z.IdHCORDPRON = A.IdHCORDPRON AND (Z.FECHORAIN < A.FECHORAIN OR (Z.FECHORAIN = A.FECHORAIN AND Z.CODAUTONU < A.CODAUTONU))) AS VARCHAR(3))) + CHAR(10) + '________________________________________' + CHAR(10) + 'Asignada por: ' + RTRIM(G.CODUSUARI) + ' - ' + RTRIM(G.NOMUSUARI) AS CONTENIDO, 
  RTRIM(A.CODUSUASI) + ' - ' + RTRIM(G.NOMUSUARI) AS 'USUARIO ASIGNO', 
  CASE WHEN A.CODESTCIT = '0' THEN 'Cita asignada a:' WHEN A.CODESTCIT = '1' THEN 'Cita cumplida por:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NULL THEN 'Cita incumplida por:' WHEN A.CODESTCIT = '3' THEN 'Cita preasignada a:' WHEN A.CODESTCIT = '2' 
  AND A.CODMOTIVOREPRO IS NOT NULL THEN 'Cita reprogramada de:' END AS 'ESTADO CITA', 
  CODESTCIT,  
  rtrim(DI.CODDIAGNO) + ' - ' + rtrim(DI.NOMDIAGNO) as Diagnostico, 
  CASE WHEN A.CODMOTIVOREPRO IS NULL THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT <> '2' THEN A.CODESTCIT WHEN A.CODMOTIVOREPRO IS NOT NULL 
  AND A.CODESTCIT = '2' THEN '5' END As EstadoIcono,
  (SELECT SUM(DATEDIFF(MINUTE,SUB.FECHORAIN, SUB.FECHORAFI )) From (SELECT AG.FECHORAIN, MAX(AG.FECHORAFI) AS FECHORAFI	from AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI)	Where A.CODAUTONU = AG.CODAUTONU Group by AG.FECHORAIN) AS SUB) as MinutosXDiaCitasAsignados,
  (SELECT DISTINCT AGD.ID FROM AGASICITA AG INNER JOIN AGDISPONEQUIPO AGD ON AG.IDEQUIPOTRA = AGD.IDEQUIPO AND (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI) Where A.CODAUTONU = AG.CODAUTONU ) AS IDDISPO,
  RTRIM(CAST(1 + ( SELECT COUNT(*) FROM dbo.AGASICITA Z WITH(NOLOCK) WHERE Z.IdHCORDPRON = A.IdHCORDPRON AND (Z.FECHORAIN < A.FECHORAIN OR (Z.FECHORAIN = A.FECHORAIN AND Z.CODAUTONU < A.CODAUTONU)) ) AS VARCHAR(3))) AS Sesion,
  Rtrim(O.AUTO) AS CodigoOrden,
  Rtrim(O.CODSERIPS) + ' - ' + rtrim(I.DESSERIPS) AS Servicio
  FROM 
  dbo.AGASICITA A with(nolock) 
  INNER JOIN dbo.ADCENATEN C with(nolock) ON A.CODCENATE = C.CODCENATE 
  INNER JOIN dbo.INPACIENT D with(nolock) ON A.IPCODPACI = D.IPCODPACI 
  INNER JOIN dbo.AGACTIMED F with(nolock) ON A.CODACTMED = F.CODACTMED 
  INNER JOIN dbo.AGENSALAC S with(nolock) ON S.CODCONCEC = A.IDSALA 
  INNER JOIN dbo.AGEQUIPTRA E with(nolock) ON E.ID = A.IDEQUIPOTRA 
  INNER JOIN dbo.SEGusuaru G with(nolock) ON A.CODUSUASI = G.CODUSUARI 
  INNER JOIN dbo.HCORDPRON O with(nolock) ON A.idHCORDPRON = O.AUTO
  INNER JOIN INCUPSIPS I ON O.CODSERIPS = I.CODSERIPS
  LEFT JOIN dbo.INDIAGNOS DI with(nolock) on DI.CODDIAGNO = A.CODDIAGNO 
WHERE 
  A.CODCENATE = @CentroAtencion
  AND FECHORAIN >= @FechaInicio
  AND FECHORAFI <= @FechaFinal 
  AND CODESTCIT IN(0, 1, 2, 3) 
  AND A.TIPSOLICITU = 3 
  AND A.TIPTRATAMIENTO = 3

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes con citas de Terapia de Reemplazo Renal (diálisis y procedimientos renales afines) programadas en un centro de atención y rango de fechas determinados. Combina información de citas (AGASICITA), datos demográficos del paciente (INPACIENT), la sala y el equipo de tratamiento asignados (AGENSALAC, AGEQUIPTRA), la actividad médica (AGACTIMED), la orden médica de origen (HCORDPRON), el servicio CUPS/IPS correspondiente (INCUPSIPS) y el diagnóstico CIE-10 (INDIAGNOS). Para cada cita devuelve el nombre del paciente, cédula, contacto, número de sesión acumulada dentro de la orden, estado de la cita (asignada, cumplida, incumplida, preasignada, reprogramada), equipo y sala utilizados, minutos ocupados en el día y el usuario que realizó la asignación. Se usa en la agenda de terapia renal para visualizar y gestionar el listado diario de sesiones de reemplazo renal por sede.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListRenalReplacementTherapyPatients';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListRenalReplacementTherapyPatients';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de pacientes en terapia de reemplazo renal (diálisis) programadas en un centro de atención dentro de un rango de fechas, con datos clínicos, equipo, sala, sesión y estado.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADCENATEN.; Las citas deben tener orden médica asociada (HCORDPRON) y servicio en INCUPSIPS.; Las citas deben tener equipo de traslado, sala, actividad médica, paciente y usuario asignador válidos.; Se filtran solo citas con TIPSOLICITU = 3 y TIPTRATAMIENTO = 3 (terapia de reemplazo renal).', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas en estados 0 (asignada), 1 (cumplida), 2 (incumplida/reprogramada) o 3 (preasignada).; Solo se incluyen tratamientos tipo 3 (terapia de reemplazo renal) y solicitudes tipo 3.; El número de sesión se calcula como 1 + cantidad de citas previas de la misma orden (IdHCORDPRON), ordenadas por FECHORAIN y CODAUTONU.; Los minutos asignados por día solo cuentan citas cuya disponibilidad de equipo cubra completamente la franja (AGDISPONEQUIPO.FECHORAIN<=cita y FECHORAFI>=cita).', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapia de reemplazo renal; Cita médica; Sesión de tratamiento; Centro de atención; Sala de atención; Equipo de traslado; Orden médica; Servicio CUPS/IPS; Diagnóstico CIE; Reprogramación de cita; Disponibilidad de equipo; Paciente', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve citas donde CODCENATE=@CentroAtencion, FECHORAIN>=@FechaInicio, FECHORAFI<=@FechaFinal, CODESTCIT IN (0,1,2,3), TIPSOLICITU=3 y TIPTRATAMIENTO=3.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = ''0'' → Etiqueta estado como ''Cita asignada a:''; si CODESTCIT = ''1'' → Etiqueta estado como ''Cita cumplida por:''; si CODESTCIT = ''2'' AND CODMOTIVOREPRO IS NULL → Etiqueta estado como ''Cita incumplida por:''; si CODESTCIT = ''2'' AND CODMOTIVOREPRO IS NOT NULL → Etiqueta estado como ''Cita reprogramada de:'' y EstadoIcono=''5''; si CODESTCIT = ''3'' → Etiqueta estado como ''Cita preasignada a:''; si CODMOTIVOREPRO IS NULL → EstadoIcono toma el valor de CODESTCIT else Si hay motivo de reprogramación y CODESTCIT<>''2'', EstadoIcono = CODESTCIT', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.SEGusuaru; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INDIAGNOS; dbo.AGDISPONEQUIPO', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalReplacementTherapyPatients';
-- GO
