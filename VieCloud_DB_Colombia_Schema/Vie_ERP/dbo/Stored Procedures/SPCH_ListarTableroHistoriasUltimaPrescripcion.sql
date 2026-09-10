-- =============================================
-- Author:      María Rozo
-- Create Date: 18/04/2022
-- Description: SP para traer fecha de última prescripción médica.
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarTableroHistoriasUltimaPrescripcion]
(
@Patient Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	-- Variables generales
	DECLARE @ProfessionType INT
	DECLARE @LastMedicalTestDate DATETIME
	DECLARE @AdmissionNumber CHAR(10)
	DECLARE @PageNumber CHAR(10)
	-- Variables para evaluar SPCH_ListarTableroHistoriasTodo
	DECLARE @ProfessionType_ListAllBoardRecords INT
	DECLARE @LastMedicalTestDate_ListAllBoardRecords DATETIME
	DECLARE @AdmissionNumber_ListAllBoardRecords CHAR(10)
	-- Variables para evaluar SPCH_ListarTableroHistoriasEvoluciones
	DECLARE @ProfessionType_ListBoardRecordsEvolutions INT
	DECLARE @LastMedicalTestDate_ListBoardRecordsEvolutions DATETIME
	DECLARE @AdmissionNumber_ListBoardRecordsEvolutions CHAR(10)
	-- Variables para evaluar SPCH_ListarTableroHistoriasUnidades
	DECLARE @ProfessionType_ListBoardRecordsUnits INT
	DECLARE @LastMedicalTestDate_ListBoardRecordsUnits DATETIME
	DECLARE @AdmissionNumber_ListBoardRecordsUnits CHAR(10)

	-- Se replica selección de SPCH_ListarTableroHistoriasTodo 
	SELECT TOP 1
		@AdmissionNumber_ListAllBoardRecords = A.NUMINGRES,
		@ProfessionType_ListAllBoardRecords = C.TIPPROFES,
		@LastMedicalTestDate_ListAllBoardRecords = A.FECHISPAC,
		@PageNumber = A.NUMEFOLIO
	FROM
		.dbo.HCHISPACA A WITH(NOLOCK)
		INNER JOIN dbo.INPROFSAL C WITH(NOLOCK) ON A.CODPROSAL = C.CODPROSAL
	WHERE
		A.IPCODPACI = @Patient
		AND A.TIPHISPAC='I' OR ((A.TIPHISPAC='N' OR TIPHISPAC='JM' )
		AND (A.JUNTAMEDICA = 1 OR A.HCTELEFONICA = 1 OR A.HCOTROSPROC IN (1,2) )
		AND A.IPCODPACI = @Patient
		AND (A.CONSFOLIO IS NULL OR A.CONSFOLIO = ''))
	ORDER BY
		A.FECHISPAC DESC

	-- Se replica selección de SPCH_ListarTableroHistoriasEvoluciones 
	IF (SELECT @AdmissionNumber_ListAllBoardRecords) IS NOT NULL
	BEGIN
		SET @AdmissionNumber = @AdmissionNumber_ListAllBoardRecords
		SET @ProfessionType = @ProfessionType_ListAllBoardRecords
		SET @LastMedicalTestDate = @LastMedicalTestDate_ListAllBoardRecords

		SELECT TOP 1
			@AdmissionNumber_ListBoardRecordsEvolutions = A.NUMINGRES,
			@ProfessionType_ListBoardRecordsEvolutions = C.TIPPROFES,
			@LastMedicalTestDate_ListBoardRecordsEvolutions = A.FECHISPAC
		FROM 
			dbo.HCHISPACA A WITH(NOLOCK)
			INNER JOIN dbo.INPROFSAL C WITH(NOLOCK) ON A.CODPROSAL = C.CODPROSAL
		WHERE
			A.IPCODPACI = @Patient AND A.TIPHISPAC IN ('E','S','N','T','P','B','V', 'JM' ) 
			AND A.CONSFOLIO IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA WHERE TIPHISPAC = 'I' AND IPCODPACI = @Patient)
			AND A.NUMINGRES = @AdmissionNumber_ListAllBoardRecords
			AND A.CONSFOLIO = @PageNumber
		ORDER BY
			A.FECHISPAC DESC
	END
	ELSE
	BEGIN
	SET @ProfessionType = '0'
	END

	-- Se replica selección de SPCH_ListarTableroHistoriasUnidades 
	IF (SELECT @AdmissionNumber_ListBoardRecordsEvolutions) IS NOT NULL
	BEGIN
		SET @AdmissionNumber = @AdmissionNumber_ListBoardRecordsEvolutions
		SET @ProfessionType = @ProfessionType_ListBoardRecordsEvolutions
		SET @LastMedicalTestDate = @LastMedicalTestDate_ListBoardRecordsEvolutions

		SELECT TOP 1
			@AdmissionNumber_ListBoardRecordsUnits = A.NUMINGRES,
			@ProfessionType_ListBoardRecordsUnits = C.TIPPROFES,
			@LastMedicalTestDate_ListBoardRecordsUnits = A.FECHISPAC
		FROM 
			dbo.HCHISPACA A WITH(NOLOCK) 
			INNER JOIN dbo.INDIAGNOS B WITH(NOLOCK) ON A.CODDIAGNO = B.CODDIAGNO 
			INNER JOIN dbo.INPROFSAL C WITH(NOLOCK) ON A.CODPROSAL = C.CODPROSAL
		WHERE 
			A.IPCODPACI = @Patient AND A.TIPHISPAC IN ('E','S','N','P', 'JM' ) 
			AND A.CONSFOLIO NOT IN (SELECT NUMEFOLIO FROM dbo.HCHISPACA WHERE TIPHISPAC = 'I' AND IPCODPACI=@Patient)
			AND A.NUMINGRES = @AdmissionNumber_ListBoardRecordsEvolutions
		ORDER BY
			A.FECHISPAC DESC
	END

	-- Se evalúa condición de SPCH_ListarTableroHistoriasUnidades
	IF (SELECT @AdmissionNumber_ListBoardRecordsUnits) IS NOT NULL
	BEGIN
		SET @AdmissionNumber = @AdmissionNumber_ListBoardRecordsUnits
		SET @ProfessionType = @ProfessionType_ListBoardRecordsUnits
		SET @LastMedicalTestDate = @LastMedicalTestDate_ListBoardRecordsUnits
	END

	-- Se evalúa la especialidad del profesional que realizó última prescripción
	IF @ProfessionType IN ('1', '2')
		BEGIN
		SELECT DATEDIFF(HOUR, @LastMedicalTestDate, [Common].[GETDATE]())
	END
	ELSE
	BEGIN
		SELECT 0
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que calcula cuántas horas han transcurrido desde la última prescripción médica registrada en la historia clínica de un paciente específico (identificado por su cédula o código). Recorre las notas clínicas del paciente en orden cronológico descendente, evaluando primero ingresos e historias consolidadas, luego evoluciones asociadas a un folio de ingreso, y finalmente notas de otras unidades funcionales; en cada paso refina el ingreso y la fecha más reciente encontrada. Al final, si el profesional que generó la última nota es médico o tiene tipo de profesión 1 o 2, retorna la diferencia en horas entre esa fecha y el momento actual; de lo contrario retorna 0. Se usa en tableros clínicos y alertas de seguimiento para detectar pacientes sin prescripción médica reciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula las horas transcurridas desde la última prescripción/nota médica de un paciente, aplicada solo cuando el profesional responsable es médico (tipos 1 o 2); en caso contrario retorna 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener al menos un registro en HCHISPACA para que se obtenga ingreso y folio.; Los registros de historia deben estar enlazados a un profesional válido en INPROFSAL vía CODPROSAL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera relevante la prescripción si fue realizada por un profesional con TIPPROFES 1 o 2 (médicos).; Siempre se retorna una sola columna escalar (horas transcurridas o 0).; La fecha tomada es la más reciente (ORDER BY FECHISPAC DESC, TOP 1) en cada nivel.; Las notas de ''unidades'' se distinguen porque su CONSFOLIO no apunta a un folio tipo ''I'' del paciente.; Las notas de ''evoluciones'' se distinguen porque su CONSFOLIO sí apunta a un folio tipo ''I'' del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Prescripción médica; Folio clínico; Ingreso (admisión); Tipo de profesión / especialidad; Junta médica; Historia clínica telefónica; Notas de evolución; Tablero de seguimiento clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando el tipo de profesión de la última nota es 1 o 2, devuelve DATEDIFF(HOUR, fecha_última_nota, GETDATE actual).; [RETURN_RESULT] (resultset): Cuando el tipo de profesión no es 1 ni 2 (o no se encontró registro), devuelve 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe historia tipo ''I'' o (''N''/''JM'' con junta médica, HC telefónica u otros procedimientos en (1,2) y CONSFOLIO vacío) para el paciente → Refina la búsqueda con notas de evolución (tipos E,S,N,T,P,B,V,JM) cuyo CONSFOLIO referencie un folio tipo ''I'' del paciente y mismo NUMINGRES/folio. else Asigna ProfessionType = 0 y omite los refinamientos posteriores.; si Se encontró registro de evolución asociado → Refina nuevamente buscando ''unidades'' (tipos E,S,N,P,JM) cuyo CONSFOLIO NO esté entre folios tipo ''I'' del paciente y mismo NUMINGRES.; si Se encontró registro de unidades → Sustituye ingreso, tipo de profesión y fecha por los de unidades (último refinamiento gana).; si Tipo de profesión final IN (1,2) → Retorna diferencia en horas entre la última fecha y la fecha actual. else Retorna 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarTableroHistoriasUltimaPrescripcion';
-- GO
