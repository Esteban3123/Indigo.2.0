
-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 22/03/2022
-- Description: Sp para listar los pacientes en Tratamientos Especiales de tipo Transfusión de hemocomponentes
-- =============================================
CREATE PROCEDURE [Scheduling].[SP_AGE_ListHemocomponentesPatients]
(
 @CentroAtencion Char(10),
 @FechaInicio datetime,
 @FechaFinal datetime
)
AS
BEGIN
    SET NOCOUNT ON

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
FROM dbo.AGASICITA A with(nolock) 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes agendados para tratamientos especiales de tipo transfusión de hemocomponentes (sangre y sus derivados) dentro de un rango de fechas y centro de atención específico. Combina información de citas (AGASICITA), datos demográficos del paciente como nombre, dirección, teléfono y celular (INPACIENT), la actividad médica programada (AGACTIMED), la sala o consultorio asignado (AGENSALAC), el equipo o recurso físico utilizado (AGEQUIPTRA), el usuario que asignó la cita (SEGusuaru) y el diagnóstico CIE-10 asociado (INDIAGNOS). Adicionalmente, integra los componentes sanguíneos solicitados (AppointmentHemocomponents y HCCOMSAN) junto con los códigos CUPS de cada componente, construyendo un texto descriptivo consolidado del esquema de transfusión. Se utiliza en el módulo de agendamiento para visualizar y gestionar la agenda de transfusiones, mostrando el estado de cada cita (asignada, cumplida, incumplida, preasignada o reprogramada) y los minutos ocupados por cita en el equipo disponible.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las citas de pacientes en tratamiento especial de transfusión de hemocomponentes para un centro de atención y rango de fechas, enriquecidas con datos del paciente, sala, equipo, diagnóstico, hemocomponente solicitado y CUPS asociados.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un centro de atención válido existente en ADCENATEN; Debe recibirse un rango de fechas (inicio y fin) para acotar las citas; Las citas a listar deben tener tipo de solicitud = 3 y tipo de tratamiento = 5 (hemocomponentes); Las citas deben tener estado en {0,1,2,3}; Debe existir integridad referencial entre la cita y paciente, actividad médica, sala, equipo y usuario asignador', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas cuyo tipo de solicitud sea 3 y tipo de tratamiento sea 5 (Transfusión de hemocomponentes); Solo se incluyen citas con estado en {0,1,2,3} (asignada, cumplida, incumplida, preasignada); El rango de fechas se filtra exigiendo que la cita inicie y finalice dentro de la ventana proporcionada; Se devuelve un único hemocomponente por cita (TOP 1) aun cuando puedan existir varios asociados; Los CUPS asociados al hemocomponente se concatenan en una sola cadena separada por saltos de línea; El cálculo de minutos por día solo considera disponibilidad del equipo que cubra completamente el horario de la cita (AGD.FECHORAIN <= AG.FECHORAIN AND AGD.FECHORAFI >= AG.FECHORAFI); Cuando el motivo de reprogramación existe y la cita está incumplida (estado 2), el icono se reclasifica como ''5'' (reprogramada)', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Tratamiento especial; Transfusión de hemocomponentes; Componente sanguíneo; Reserva de sangre; Centro de atención; Sala de atención; Equipo de traslado; Diagnóstico CIE; CUPS (servicios IPS); Estado de cita (asignada, cumplida, incumplida, preasignada, reprogramada); Disponibilidad de equipo', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGASICITA: Devuelve listado de citas filtradas por CODCENATE=@CentroAtencion, FECHORAIN>=@FechaInicio, FECHORAFI<=@FechaFinal, CODESTCIT IN (0,1,2,3), TIPSOLICITU=3 y TIPTRATAMIENTO=5', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODESTCIT = ''0'' → Etiqueta de estado: ''Cita asignada a:''; si CODESTCIT = ''1'' → Etiqueta de estado: ''Cita cumplida por:''; si CODESTCIT = ''2'' AND CODMOTIVOREPRO IS NULL → Etiqueta de estado: ''Cita incumplida por:''; si CODESTCIT = ''3'' → Etiqueta de estado: ''Cita preasignada a:''; si CODESTCIT = ''2'' AND CODMOTIVOREPRO IS NOT NULL → Etiqueta de estado: ''Cita reprogramada de:'' y EstadoIcono se fuerza a ''5''; si RequestType = 1 en hemocomponente → Tipo de solicitud = ''Solicitud de transfusión''; si RequestType = 2 en hemocomponente → Tipo de solicitud = ''Reserva y transfusión''; si Diagnóstico (CODDIAGNO) sin coincidencia en INDIAGNOS → Diagnostico = ''No especificado'' else Se concatena código y nombre del diagnóstico', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPACIENT; dbo.AGACTIMED; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.SEGusuaru; dbo.INDIAGNOS; Scheduling.AppointmentHemocomponents; dbo.HCCOMSAN; Scheduling.AppointmentHemocomponentsCUPS; dbo.INCUPSIPS; dbo.AGDISPONEQUIPO', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'SP_AGE_ListHemocomponentesPatients';
-- GO
