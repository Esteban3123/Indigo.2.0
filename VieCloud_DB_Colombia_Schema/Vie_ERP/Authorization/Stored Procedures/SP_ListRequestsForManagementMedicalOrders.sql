-- =============================================
-- Create date: 2026-08-27
-- =============================================
CREATE PROCEDURE [Authorization].[SP_ListRequestsForManagementMedicalOrders]
	@CareCenterCodes	VARCHAR(400),				-- Códigos de centro separados por coma, ej: '001,002'
	@TopRows			INT				= 1000,
	@AdmissionNumber	CHAR(10)		= NULL,
	@PatientCode		VARCHAR(25)		= NULL,
	@Folio				NCHAR(10)		= NULL,
	@RequestDateFrom	DATETIME		= NULL,
	@RequestDateTo		DATETIME		= NULL,
	@ItemSearch			NVARCHAR(200)	= NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		DECLARE @Where NVARCHAR(MAX) = N'';
		DECLARE @Top NVARCHAR(30) = N'';
		DECLARE @Sql NVARCHAR(MAX);
		DECLARE @Params NVARCHAR(MAX);

		-- @TopRows <= 0 = sin tope (usado cuando hay filtro activo en el grid: en ese caso
		-- ya no aplica "mostrar los N más recientes", se listan todos los que cumplan filtro).
		IF @TopRows > 0
			SET @Top = N'TOP (@TopRows) ';

		IF CHARINDEX(',', @CareCenterCodes) = 0 AND LEN(LTRIM(RTRIM(@CareCenterCodes))) > 0
			SET @Where = @Where + N' AND CareCenterCode = @CareCenterCodes';
		ELSE
			SET @Where = @Where + N' AND CareCenterCode IN (SELECT CAST(LTRIM(RTRIM(value)) AS VARCHAR(10)) FROM STRING_SPLIT(@CareCenterCodes, '','') WHERE LTRIM(RTRIM(value)) <> '''')';

		IF @AdmissionNumber IS NOT NULL
			SET @Where = @Where + N' AND AdmissionNumber = @AdmissionNumber';
		IF @PatientCode IS NOT NULL
			SET @Where = @Where + N' AND PatientCode = @PatientCode';
		IF @Folio IS NOT NULL
			SET @Where = @Where + N' AND Folio = @Folio';
		IF @RequestDateFrom IS NOT NULL
			SET @Where = @Where + N' AND RequestDate >= @RequestDateFrom';
		IF @RequestDateTo IS NOT NULL
			SET @Where = @Where + N' AND RequestDate < @RequestDateTo';
		IF @ItemSearch IS NOT NULL
			SET @Where = @Where + N' AND (ItemCode LIKE ''%'' + @ItemSearch + ''%'' OR ItemName LIKE ''%'' + @ItemSearch + ''%'')';

		SET @Sql = N'
			SELECT ' + @Top + N'
				Id, EntityName, CAST(EntityId AS INT) EntityId, CareCenterCode, CareCenterName,
				FunctionalUnitCode, FunctionalUnitName, AdmissionNumber, Folio,
				TypeClinicalHistory, CareGroupId, CareGroupCode, CareGroupName,
				HealthAdministratorId, HealthAdministratorCode, HealthAdministratorName,
				PatientCode, PatientName, PatientAddress, PatientPhone, PatientAge,
				RequestDate, ProfessionalCode, Quantity, Type, ItemId, ItemCode,
				ItemCodeOriginal, ItemName, DescriptionCodeName,
				CAST(Covered AS BIT) Covered, CAST(Contracted AS BIT) Contracted,
				CAST(Quoted AS BIT) Quoted, CAST(Authorized AS BIT) Authorized,
				ManagementMedicalOrderId, CAST(Status AS INT) Status, Observations,
				AuthorizationGroupId, AuthorizationGroupCodeName, ProfessionalCodeName
			FROM [Authorization].[ViewListRequestsForManagementMedicalOrders]
			WHERE 1 = 1' + @Where + N'
			ORDER BY RequestDate DESC
			OPTION (RECOMPILE);';

		SET @Params = N'@TopRows INT, @CareCenterCodes VARCHAR(400), @AdmissionNumber CHAR(10), @PatientCode VARCHAR(25), @Folio NCHAR(10), @RequestDateFrom DATETIME, @RequestDateTo DATETIME, @ItemSearch NVARCHAR(200)';

		EXEC sp_executesql @Sql, @Params,
			@TopRows = @TopRows,
			@CareCenterCodes = @CareCenterCodes,
			@AdmissionNumber = @AdmissionNumber,
			@PatientCode = @PatientCode,
			@Folio = @Folio,
			@RequestDateFrom = @RequestDateFrom,
			@RequestDateTo = @RequestDateTo,
			@ItemSearch = @ItemSearch;
	END TRY
	BEGIN CATCH
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
		DECLARE @ErrorState INT = ERROR_STATE();
		RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
	END CATCH
END
