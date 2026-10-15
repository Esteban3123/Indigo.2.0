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