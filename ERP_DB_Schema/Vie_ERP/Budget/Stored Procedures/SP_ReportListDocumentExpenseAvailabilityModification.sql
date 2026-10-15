-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de modificación de disponibilidades del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseAvailabilityModification]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@AvailabilityModificationCode VARCHAR(20)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@AvailabilityModificationCode = t.x.value('AvailabilityModificationCode[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				am.Code,
				am.DocumentDate,
				am.Document,
				am.Observations,
				a.Code AvailabilityCode,
				CASE am.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,
				CASE amd.Nature
					WHEN 1 THEN '(-) Débito'
					WHEN 2 THEN '(+) Crédito'
					ELSE 'N/A'
				END NatureName,
				amd.Value
		FROM Budget.AvailabilityModification am WITH (NOLOCK)
		JOIN Budget.AvailabilityModificationDetail amd WITH (NOLOCK) ON am.Id = amd.AvailabilityModificationId
		/************************************  RECONOCIMIENTO ************************************/
		JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON amd.AvailabilityDetailId = ad.Id
		JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON b.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
		JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id/**************************************** FILTROS ****************************************/
		WHERE CAST(am.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@AvailabilityModificationCode, '') = '' OR am.Code = @AvailabilityModificationCode)
		ORDER BY am.Id
		OPTION (RECOMPILE)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END