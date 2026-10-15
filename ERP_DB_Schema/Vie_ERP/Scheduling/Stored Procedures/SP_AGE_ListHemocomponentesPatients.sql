
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