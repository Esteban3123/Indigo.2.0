-- =============================================
-- Author:      Emanuel Olaya Penagos
-- Create Date: 10/18/2024
-- Description: sp para obtener citas
-- =============================================
CREATE PROCEDURE [WebAppointment].[ObtenerCitas]
(
    @PatientCode VARCHAR(50) = NULL,
	@AppointmentCode VARCHAR(50) = NULL,
	@Status VARCHAR(10) = NULL
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	SELECT 
		AGASICITA.CODAUTONU AppointmentCode,
		AGASICITA.FECREGSIS AppointmentCreation,
		AGASICITA.FECHORAIN AppointmentStart,
		AGASICITA.FECHORAFI AppointmentEnd,
		AGASICITA.OBSCAUCAN AppointmentCancellationObservations,
		AGASICITA.FECHCANCELA AppointmentCancellationDate,
		AGASICITA.CODESTCIT AppointmentStatus,
		AGACTIMED.CODACTMED ActivityCode,
		AGACTIMED.DESACTMED ActivityName,
		AGACTIMED.INDICAMED ActivityIndications,
		AGACTIMED.DURAACTIV ActivityDuration,
		RTRIM(ADCENATEN.CODCENATE) AttentionCenterCode,
		RTRIM(ADCENATEN.NOMCENATE) AttentionCenterName,
		RTRIM(ADCENATEN.DIRCENATE) AttentionCenterAddress,
		RTRIM(INPROFSAL.CODPROSAL) DoctorCode,
		RTRIM(INPROFSAL.NOMMEDICO) DoctorName,
		I1.CODESPECI Specialty1Code,
		RTRIM(I1.DESESPECI) Specialty1Name,
		I2.CODESPECI Specialty2Code,
		RTRIM(I2.DESESPECI) Specialty2Name,
		I3.CODESPECI Specialty3Code,
		RTRIM(I3.DESESPECI) Specialty3Name,
		AGCAUCANC.CODCAUCAN CancellationReasonCode,
		AGCAUCANC.DESCAUCAN CancellationReasonName
		FROM AGASICITA
		INNER JOIN INPROFSAL on INPROFSAL.CODPROSAL = AGASICITA.CODPROSAL
		INNER JOIN AGACTIMED on AGACTIMED.CODACTMED = AGASICITA.CODACTMED
		INNER JOIN ADCENATEN on ADCENATEN.CODCENATE = AGASICITA.CODCENATE
		LEFT JOIN AGCAUCANC ON AGCAUCANC.CODCAUCAN = AGASICITA.CODCAUCAN
		LEFT JOIN INESPECIA I1 ON I1.CODESPECI = INPROFSAL.CODESPEC1
		LEFT JOIN INESPECIA I2 ON I2.CODESPECI = INPROFSAL.CODESPEC2
		LEFT JOIN INESPECIA I3 ON I3.CODESPECI = INPROFSAL.CODESPEC3
		where (@AppointmentCode IS NULL OR AGASICITA.CODAUTONU = @AppointmentCode) 
		AND (@PatientCode IS NULL OR AGASICITA.IPCODPACI = @PatientCode)
		AND (@Status IS NULL OR AGASICITA.CODESTCIT IN (SELECT value FROM STRING_SPLIT(@Status, ',')))
		order by AGASICITA.FECHORAIN desc
END

-- exec [WebAppointment].[ObtenerCitas] '145145', NULL, '4'