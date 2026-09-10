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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las citas médicas agendadas de un paciente, permitiendo filtrar por código de cita, cédula del paciente y estado de la cita. Integra información del agendamiento con el profesional de salud (médico, especialista), la actividad médica programada (tipo de consulta o procedimiento), el centro de atención (sede) y los motivos de cancelación cuando aplica. Devuelve el detalle completo de cada cita: fechas de inicio y fin, estado, indicaciones, observaciones y causa de cancelación, junto con hasta tres especialidades del profesional. Se utiliza en el portal web de agendamiento para que el paciente o el personal administrativo consulte el historial y estado de sus citas.', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'ObtenerCitas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'WebAppointment', @level1type = N'PROCEDURE', @level1name = N'ObtenerCitas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el listado de citas médicas con datos del profesional, centro de atención, especialidades, actividad y motivo de cancelación, permitiendo filtrar por paciente, cita y estado.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas AGASICITA, INPROFSAL, AGACTIMED y ADCENATEN deben tener registros relacionables por sus códigos (joins internos obligatorios).; Si se filtra por estado, debe enviarse como lista separada por comas compatible con STRING_SPLIT.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna citas que tengan profesional, actividad médica y centro de atención válidos (joins internos).; El motivo de cancelación y las especialidades del profesional son opcionales (LEFT JOIN), no excluyen la cita.; Un profesional puede tener hasta tres especialidades asociadas en el resultado.; Los resultados siempre se ordenan de la cita más reciente a la más antigua por fecha/hora de inicio.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Paciente; Profesional de salud / Médico; Centro de atención; Especialidad médica; Actividad médica; Estado de cita; Cancelación de cita; Causa de cancelación', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Devuelve citas ordenadas por fecha/hora de inicio descendente, aplicando filtros opcionales por código de cita, código de paciente y lista de estados.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AppointmentCode IS NULL → No filtra por código de cita; devuelve todas las coincidentes con los demás filtros. else Restringe a la cita cuyo CODAUTONU coincide.; si @PatientCode IS NULL → No filtra por paciente. else Restringe a citas del paciente indicado (IPCODPACI).; si @Status IS NULL → No filtra por estado. else Restringe a citas cuyo CODESTCIT esté dentro de la lista separada por comas.', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AGASICITA; INPROFSAL; AGACTIMED; ADCENATEN; AGCAUCANC; INESPECIA', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'WebAppointment', @level1type=N'PROCEDURE', @level1name=N'ObtenerCitas';
-- GO
